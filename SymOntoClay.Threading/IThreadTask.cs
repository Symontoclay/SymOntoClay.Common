using SymOntoClay.Common.Disposing;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SymOntoClay.Threading
{
    public interface IThreadTask : ISymOntoClayDisposable
    {
        /// <summary>
        /// Gets the <see cref="ThreadTaskStatus"/> of this task.
        /// </summary>
        ThreadTaskStatus Status { get; }

        /// <summary>
        /// Gets whether this task has completed execution due to being canceled.
        /// </summary>
        bool IsCanceled { get; }

        /// <summary>
        /// Gets a value that indicates whether the task has completed.
        /// </summary>
        bool IsCompleted { get; }

        /// <summary>
        /// Gets whether the task ran to completion.
        /// </summary>
        bool IsCompletedSuccessfully { get; }

        /// <summary>
        /// Gets whether the task completed due to an unhandled exception.
        /// </summary>
        bool IsFaulted { get; }

        /// <summary>
        /// Starts the <see cref="ThreadTask"/>.
        /// </summary>
        void Start();

        /// <summary>
        /// Waits for the <see cref="ThreadTask"/> to complete execution.
        /// </summary>
        void Wait();

        event Action OnStarted;
        event Action OnCanceled;
        event Action OnCompleted;
        event Action OnCompletedSuccessfully;
        event Action OnFaulted;

        Task StandardTask { get; }

        CancellationTokenSource CancellationTokenSource { get; }

        CancellationToken Token { get; }

        void Cancel();
    }

    public interface IThreadTask<TResult> : IThreadTask
    {
        Task<TResult> StandardTaskWithResult { get; }

        /// <summary>
        /// Gets the result value of this <see cref="ThreadTask{TResult}"/>.
        /// </summary>
        TResult Result { get; }
    }
}
