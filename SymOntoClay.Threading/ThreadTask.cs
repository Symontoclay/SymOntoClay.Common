/*MIT License

Copyright (c) 2020 - 2024 Sergiy Tolkachov

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

using System.Threading;
using System;
using System.Threading.Tasks;
using SymOntoClay.Common.Disposing;

namespace SymOntoClay.Threading
{
    /// <summary>
    /// Represents an asynchronous operation.
    /// This is a wrapper over Thread.
    /// </summary>
    public class ThreadTask: Disposable
    {
        public static ThreadTask Run(Action action, ICustomThreadPool threadPool)
        {
            var task = new ThreadTask(action, threadPool);
            task.Start();
            return task;
        }

        public static ThreadTask Run(Action action, ICustomThreadPool threadPool, CancellationToken cancellationToken)
        {
            var task = new ThreadTask(action, threadPool, cancellationToken);
            task.Start();
            return task;
        }

        public static ThreadTask Run(Action action)
        {
            var task = new ThreadTask(action);
            task.Start();
            return task;
        }

        public static ThreadTask Run(Action action, CancellationToken cancellationToken)
        {
            var task = new ThreadTask(action, cancellationToken);
            task.Start();
            return task;
        }

        public ThreadTask(Action action, ICustomThreadPool threadPool, CancellationToken cancellationToken)
        {
            _task = new Task(action, cancellationToken);
            _threadPool = threadPool;
            _cancellationToken = cancellationToken;
        }

        public ThreadTask(Action action, ICustomThreadPool threadPool)
        {
            _task = new Task(action);
            _threadPool = threadPool;
        }

        public ThreadTask(Action action, CancellationToken cancellationToken)
        {
            _task = new Task(action, cancellationToken);
            _cancellationToken = cancellationToken;
        }

        public ThreadTask(Action action)
        {
            _task = new Task(action);
        }

        /// <summary>
        /// Gets the <see cref="ThreadTaskStatus"/> of this task.
        /// </summary>
        public ThreadTaskStatus Status => _status;

        /// <summary>
        /// Gets whether this task has completed execution due to being canceled.
        /// </summary>
        public bool IsCanceled => _status == ThreadTaskStatus.Canceled;

        /// <summary>
        /// Gets a value that indicates whether the task has completed.
        /// </summary>
        public bool IsCompleted => _status == ThreadTaskStatus.RanToCompletion || _status == ThreadTaskStatus.Faulted || _status == ThreadTaskStatus.Canceled;

        /// <summary>
        /// Gets whether the task ran to completion.
        /// </summary>
        public bool IsCompletedSuccessfully => _status == ThreadTaskStatus.RanToCompletion;

        /// <summary>
        /// Gets whether the task completed due to an unhandled exception.
        /// </summary>
        public bool IsFaulted => _status == ThreadTaskStatus.Faulted;

        /// <summary>
        /// Starts the <see cref="ThreadTask"/>.
        /// </summary>
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

            if(_threadPool == null)
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

        /// <summary>
        /// Waits for the <see cref="ThreadTask"/> to complete execution.
        /// </summary>
        public void Wait()
        {
            _task.Wait();
        }

        public event Action OnStarted;
        public event Action OnCanceled;
        public event Action OnCompleted;
        public event Action OnCompletedSuccessfully;
        public event Action OnFaulted;

        private readonly Task _task;

        public Task StandardTask => _task;

        private readonly ICustomThreadPool _threadPool;
        private readonly CancellationToken _cancellationToken;
        private Thread _thread;
        private object _lockObj = new object();
        private volatile ThreadTaskStatus _status = ThreadTaskStatus.Created;

        private void RunDelegate()
        {
            try
            {
                if(_cancellationToken.IsCancellationRequested)
                {
                    PerformCanceled();
                    OnCompleted?.Invoke();
                    return;
                }

                OnStarted?.Invoke();

                if (_cancellationToken.IsCancellationRequested)
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
