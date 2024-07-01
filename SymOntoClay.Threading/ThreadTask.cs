﻿/*MIT License

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
    public class ThreadTask: BaseThreadTask
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
            : base(new Task(action, cancellationToken), threadPool, cancellationToken)
        {
        }

        public ThreadTask(Action action, ICustomThreadPool threadPool)
            : base(new Task(action), threadPool, CancellationToken.None)
        {
        }

        public ThreadTask(Action action, CancellationToken cancellationToken)
            : base(new Task(action, cancellationToken), null, cancellationToken)
        {
        }

        public ThreadTask(Action action)
            : base(new Task(action), null, CancellationToken.None)
        {
        }
    }

    public class ThreadTask<TResult> : BaseThreadTask
    {
        public static ThreadTask<TResult> Run(Func<TResult> function, ICustomThreadPool threadPool)
        {
            var task = new ThreadTask<TResult>(function, threadPool);
            task.Start();
            return task;
        }

        public static ThreadTask<TResult> Run(Func<TResult> function, ICustomThreadPool threadPool, CancellationToken cancellationToken)
        {
            var task = new ThreadTask<TResult>(function, threadPool, cancellationToken);
            task.Start();
            return task;
        }

        public static ThreadTask<TResult> Run(Func<TResult> function)
        {
            var task = new ThreadTask<TResult>(function);
            task.Start();
            return task;
        }

        public static ThreadTask<TResult> Run(Func<TResult> function, CancellationToken cancellationToken)
        {
            var task = new ThreadTask<TResult>(function, cancellationToken);
            task.Start();
            return task;
        }

        public ThreadTask(Func<TResult> function, ICustomThreadPool threadPool, CancellationToken cancellationToken)
            : this(new Task<TResult>(function, cancellationToken), threadPool, cancellationToken)
        {
        }

        public ThreadTask(Func<TResult> function, ICustomThreadPool threadPool)
            : this(new Task<TResult>(function), threadPool, CancellationToken.None)
        {
        }

        public ThreadTask(Func<TResult> function, CancellationToken cancellationToken)
            : this(new Task<TResult>(function, cancellationToken), null, cancellationToken)
        {
        }

        public ThreadTask(Func<TResult> function)
            : this(new Task<TResult>(function), null, CancellationToken.None)
        {
        }

        private ThreadTask(Task<TResult> task, ICustomThreadPool threadPool, CancellationToken cancellationToken)
            : base(task, threadPool, cancellationToken)
        {
            _taskWithResult = task;
        }

        private readonly Task<TResult> _taskWithResult;

        public Task<TResult> StandardTaskWithResult => _taskWithResult;

        /// <summary>
        /// Gets the result value of this <see cref="ThreadTask{TResult}"/>.
        /// </summary>
        public TResult Result => _taskWithResult.Result;
    }
}
