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
