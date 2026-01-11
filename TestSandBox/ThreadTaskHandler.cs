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

using SymOntoClay.Threading;

namespace TestSandBox
{
    public class ThreadTaskHandler
    {
#if DEBUG
        private static readonly NLog.ILogger _logger = NLog.LogManager.GetCurrentClassLogger();
#endif

        public void Run()
        {
            _logger.Info("Begin");

            //RunWithResult();
            //RunManyWithCustomThreadPool();
            //RunWithCustomThreadPool();
            RunWithOwnThread();

            _logger.Info("End");
        }

        private void RunWithResult()
        {
            _logger.Info("Begin");

            using var threadPool = new CustomThreadPool(0, 20);

            var task = ThreadTask<int>.Run(() => { return 16; }, threadPool);

            var result = task.Result;

            _logger.Info($"result = {result}");

            Thread.Sleep(10000);

            _logger.Info("End");
        }

        private void RunManyWithCustomThreadPool()
        {
            _logger.Info("Begin");

            using var threadPool = new CustomThreadPool(0, 20);

            foreach (var n in Enumerable.Range(1, 2000))
            {
                Task.Run(() => {
                    Thread.Sleep(1000);
                });
            }

            foreach (var n in Enumerable.Range(1, 200))
            {
                _logger.Info($"1 {n}");

                ThreadTask.Run(() => {
                    _logger.Info($"Begin 1 {n}");
                    Thread.Sleep(100);
                    _logger.Info($"End 1 {n}");
                }, threadPool);
            }

            Thread.Sleep(10000);

            _logger.Info("End");
        }

        private void RunWithCustomThreadPool()
        {
            _logger.Info("Begin");

            using var threadPool = new CustomThreadPool(0, 20);

            var task = new ThreadTask(() => {
                _logger.Info("Run");
            }, threadPool);

            task.OnStarted += () => { _logger.Info("task.OnStarted"); };
            task.OnCanceled += () => { _logger.Info("task.OnCanceled"); };
            task.OnCompleted += () => { _logger.Info("task.OnCompleted"); };
            task.OnCompletedSuccessfully += () => { _logger.Info("task.OnCompletedSuccessfully"); };
            task.OnFaulted += () => { _logger.Info("task.OnFaulted"); };

            task.Start();

            Thread.Sleep(1000);

            _logger.Info("End");
        }

        private void RunWithOwnThread()
        {
            _logger.Info("Begin");

            var task = new ThreadTask(() => {
                _logger.Info("Run");
            });

            task.OnStarted += () => { _logger.Info("task.OnStarted"); };
            task.OnCanceled += () => { _logger.Info("task.OnCanceled"); };
            task.OnCompleted += () => { _logger.Info("task.OnCompleted"); };
            task.OnCompletedSuccessfully += () => { _logger.Info("task.OnCompletedSuccessfully"); };
            task.OnFaulted += () => { _logger.Info("task.OnFaulted"); };

            task.Start();

            Thread.Sleep(1000);

            _logger.Info("End");
        }
    }
}
