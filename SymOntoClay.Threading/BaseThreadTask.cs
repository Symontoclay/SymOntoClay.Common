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
using SymOntoClay.Common.Disposing;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SymOntoClay.Threading
{
    public abstract class BaseThreadTask : Disposable, IThreadTask
    {
        protected BaseThreadTask(Task task, ICustomThreadPool threadPool, CancellationTokenSourceContext cancellationTokenSourceContext, CancellationTokenSourceContext linkedCancellationTokenSourceContext)
        {
            _task = task;
            _threadPool = threadPool;
            _cancellationTokenSourceContext = cancellationTokenSourceContext;
            _linkedCancellationTokenSourceContext = linkedCancellationTokenSourceContext;
        }

        /// <inheritdoc/>
        public ThreadTaskStatus Status => _status;

        /// <inheritdoc/>
        public bool IsCanceled => _status == ThreadTaskStatus.Canceled;

        /// <inheritdoc/>
        public bool IsCompleted => _status == ThreadTaskStatus.RanToCompletion || _status == ThreadTaskStatus.Faulted || _status == ThreadTaskStatus.Canceled;

        /// <inheritdoc/>
        public bool IsCompletedSuccessfully => _status == ThreadTaskStatus.RanToCompletion;

        /// <inheritdoc/>
        public bool IsFaulted => _status == ThreadTaskStatus.Faulted;

        /// <inheritdoc/>
        public void Start()
        {
            lock (_lockObj)
            {
                if (_status == ThreadTaskStatus.Running)
                {
                    return;
                }

                _status = ThreadTaskStatus.Running;
            }

            if (_threadPool == null)
            {
                if (_thread == null)
                {
                    var threadDelegate = new ThreadStart(RunDelegate);
                    _thread = new Thread(threadDelegate);
                    _thread.IsBackground = true;
                }

                _thread.Start();
            }
            else
            {
                _threadPool.Run(RunDelegate);
            }
        }

        /// <inheritdoc/>
        public void Wait()
        {
            _task.Wait();
        }

        /// <inheritdoc/>
        public event Action OnStarted;

        /// <inheritdoc/>
        public event Action OnCanceled;

        /// <inheritdoc/>
        public event Action OnCompleted;

        /// <inheritdoc/>
        public event Action OnCompletedSuccessfully;

        /// <inheritdoc/>
        public event Action OnFaulted;

        private readonly Task _task;

        /// <inheritdoc/>
        public Task StandardTask => _task;

        private readonly ICustomThreadPool _threadPool;
        private readonly CancellationTokenSourceContext _cancellationTokenSourceContext;
        private readonly CancellationTokenSourceContext _linkedCancellationTokenSourceContext;

        /// <inheritdoc/>
        public CancellationTokenSourceContext CancellationTokenSourceContext => _cancellationTokenSourceContext;

        /// <inheritdoc/>
        public ICancellationContext CancellationContext => _linkedCancellationTokenSourceContext;

        /// <inheritdoc/>
        public void Cancel()
        {
            _cancellationTokenSourceContext.Cancel();
        }

        private Thread _thread;
        private object _lockObj = new object();
        private volatile ThreadTaskStatus _status = ThreadTaskStatus.Created;

        private void RunDelegate()
        {
            try
            {
                if (_linkedCancellationTokenSourceContext.IsCancellationRequested)
                {
                    PerformCanceled();
                    OnCompleted?.Invoke();
                    return;
                }

                OnStarted?.Invoke();

                if (_linkedCancellationTokenSourceContext.IsCancellationRequested)
                {
                    PerformCanceled();
                    OnCompleted?.Invoke();
                    return;
                }

                _task.RunSynchronously();

                _status = ThreadTaskStatus.RanToCompletion;

                OnCompletedSuccessfully?.Invoke();
            }
            catch (OperationCanceledException)
            {
                PerformCanceled();
            }
            catch (Exception)
            {
                _status = ThreadTaskStatus.Faulted;
                OnFaulted?.Invoke();
            }

            OnCompleted?.Invoke();
        }

        private void PerformCanceled()
        {
            _status = ThreadTaskStatus.Canceled;
            OnCanceled?.Invoke();
        }

        /// <inheritdoc/>
        protected override void OnDisposing()
        {
            _task.Dispose();

            base.OnDisposing();
        }
    }
}
