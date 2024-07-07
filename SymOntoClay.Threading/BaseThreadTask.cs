using SymOntoClay.Common.Disposing;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SymOntoClay.Threading
{
    public abstract class BaseThreadTask : Disposable
    {
        protected BaseThreadTask(Task task, ICustomThreadPool threadPool, CancellationTokenSource cancellationTokenSource, CancellationTokenSource linkedCancellationTokenSource)
        {
            _task = task;
            _threadPool = threadPool;
            _cancellationTokenSource = cancellationTokenSource;
            _cancellationToken = linkedCancellationTokenSource.Token;
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
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly CancellationToken _cancellationToken;

        public CancellationTokenSource CancellationTokenSource => _cancellationTokenSource;
        public CancellationToken Token => _cancellationToken;

        public void Cancel()
        {
            _cancellationTokenSource.Cancel();
        }

        private Thread _thread;
        private object _lockObj = new object();
        private volatile ThreadTaskStatus _status = ThreadTaskStatus.Created;

        private void RunDelegate()
        {
            try
            {
                if (_cancellationToken.IsCancellationRequested)
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
