/*MIT License

Copyright (c) 2020 - 2026 Sergiy Tolkachov

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.*/

using SymOntoClay.Common.Cancellation;
using SymOntoClay.Common.SerializationToImage;
using SymOntoClay.Common.SerializationToImage.Attributes;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;

namespace SymOntoClay.Threading
{
    public class CustomThreadPool : ICustomThreadPool, IPostDeserializationHandler
    {
#if DEBUG
        //private static readonly NLog.ILogger _logger = NLog.LogManager.GetCurrentClassLogger();
#endif

        //public CustomThreadPool(int minThreadsCount, int maxThreadsCount)

        /// <summary>
        /// Constructor for deserialization.
        /// </summary>
        private CustomThreadPool()
        {
        }

        public CustomThreadPool(CustomThreadPoolSettings settings)
            : this(settings, new CancellationTokenContext(CancellationToken.None))
        {
        }

        //public CustomThreadPool(int minThreadsCount, int maxThreadsCount, ICancellationContext cancellationContext)
        public CustomThreadPool(CustomThreadPoolSettings settings, ICancellationContext cancellationContext)
        {
            _settings = settings;
            _cancellationContext = cancellationContext;

            Init();
        }

        /// <inheritdoc/>
        void IPostDeserializationHandler.Handle()
        {
            Init();
        }

        private void Init()
        {
            _maxThreadsCount = _settings?.MaxThreadsCount ?? DefaultCustomThreadPoolSettings.MaxThreadsCount;
            _minThreadsCount = _settings?.MinThreadsCount ?? DefaultCustomThreadPoolSettings.MinThreadsCount;

            if (_minThreadsCount > 0)
            {
                var cancellationToken = _cancellationContext.Token;

                foreach (var n in Enumerable.Range(1, _minThreadsCount))
                {
#if DEBUG
                    //_logger.Info($"n = {n}");
#endif

                    cancellationToken.ThrowIfCancellationRequested();

                    CreateThread();
                }
            }
        }

        private CustomThreadPoolSettings _settings;
        private int _maxThreadsCount;
        private int _minThreadsCount;
        private readonly ICancellationContext _cancellationContext;

        [SystemNoSerializedMember]
        private readonly ConcurrentBag<Thread> _threads = new ConcurrentBag<Thread>();

        [SystemNoSerializedMember]
        private readonly ConcurrentQueue<Thread> _readyThreads = new ConcurrentQueue<Thread>();

        [SystemNoSerializedMember]
        private readonly ConcurrentQueue<Action> _queue = new ConcurrentQueue<Action>();
        private volatile bool _needToRun = true;

        private void CreateThread()
        {
            var thread = new Thread(ThreadMethod)
            {
                IsBackground = true
            };

            _threads.Add(thread);
            thread.Start();
        }

        /// <inheritdoc/>
        public void Run(Action action)
        {
            if (!_needToRun)
            {
                return;
            }

#if DEBUG
            //_logger.Info($"_threads.Count = {_threads.Count}");
            //_logger.Info($"_readyThreads.Count = {_readyThreads.Count}");
#endif

            var cancellationToken = _cancellationContext.Token;

            cancellationToken.ThrowIfCancellationRequested();

            _queue.Enqueue(action);

            if (_readyThreads.TryDequeue(out var thread))
            {
                thread.Interrupt();
            }
            else
            {
#if DEBUG
                //_logger.Info($"Does not have enought threads.");
#endif

                if (_threads.Count < _maxThreadsCount)
                {
#if DEBUG
                    //_logger.Info($"Thread created");
#endif

                    CreateThread();
                }
            }
        }

        private void ThreadMethod()
        {
#if DEBUG
            //_logger.Info($"Begin");
#endif

            while (_needToRun)
            {
#if DEBUG
                //_logger.Info($"Begin Iteration");
#endif

                if (_cancellationContext.IsCancellationRequested)
                {
#if DEBUG
                    //_logger.Info($"Cancel");
#endif

                    return;
                }

                while (_queue.TryDequeue(out var action))
                {
#if DEBUG
                    //_logger.Info($"Dequeue");
#endif

                    action();

                    if (_cancellationContext.IsCancellationRequested)
                    {
#if DEBUG
                        //_logger.Info($"Cancel");
#endif

                        return;
                    }
                }

                try
                {
#if DEBUG
                    //_logger.Info($"Thread.Sleep(Timeout.Infinite)");
#endif

                    _readyThreads.Enqueue(Thread.CurrentThread);

                    Thread.Sleep(Timeout.Infinite);
                }
                catch (ThreadInterruptedException)
                {
#if DEBUG
                    //_logger.Info($"Iteration awoken");
#endif
                }

#if DEBUG
                //_logger.Info($"End Itration");
#endif
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            if (!_needToRun)
            {
                return;
            }

            _needToRun = false;

#if DEBUG
            //_logger.Info($"_readyThreads.Count = {_readyThreads.Count}");
#endif

            while (_queue.TryDequeue(out var action))
            {
            }

            while (_readyThreads.TryDequeue(out var thread))
            {
                thread.Interrupt();
            }

            while (_threads.TryTake(out var thread))
            {
            }
        }
    }
}
