using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;

namespace SymOntoClay.Threading
{
    public class CustomThreadPool : IDisposable
    {
#if DEBUG
        //private static readonly NLog.ILogger _logger = NLog.LogManager.GetCurrentClassLogger();
#endif

        public CustomThreadPool(int minThreadsCount, int maxThreadsCount)
            : this(minThreadsCount, maxThreadsCount, CancellationToken.None)
        {
        }

        public CustomThreadPool(int minThreadsCount, int maxThreadsCount, CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;

            _maxThreadsCount = maxThreadsCount;

            if (minThreadsCount > 0)
            {
                foreach (var n in Enumerable.Range(1, minThreadsCount))
                {
#if DEBUG
                    //_logger.Info($"n = {n}");
#endif

                    cancellationToken.ThrowIfCancellationRequested();

                    CreateThread();
                }
            }
        }

        private readonly int _maxThreadsCount;
        private readonly CancellationToken _cancellationToken;
        private readonly ConcurrentBag<Thread> _threads = new ConcurrentBag<Thread>();
        private readonly ConcurrentQueue<Thread> _readyThreads = new ConcurrentQueue<Thread>();
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

            _cancellationToken.ThrowIfCancellationRequested();

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

                if (_cancellationToken.IsCancellationRequested)
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

                    if (_cancellationToken.IsCancellationRequested)
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
