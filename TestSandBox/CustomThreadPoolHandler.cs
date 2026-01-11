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
using System.Collections.Concurrent;
using System.Threading;

namespace TestSandBox
{
    public class CustomThreadPoolHandler
    {
#if DEBUG
        private static readonly NLog.ILogger _logger = NLog.LogManager.GetCurrentClassLogger();
#endif

        public void Run()
        {
            _logger.Info("Begin");

            RunMultipleTimes_ExecuteEverithing();
            //Case5();
            //Case4();
            //Case3();
            //Case2();
            //Case1();

            _logger.Info("End");
        }

        private void RunMultipleTimes_ExecuteEverithing()
        {
            _logger.Info("Begin");

            var timeoutBetweenSets = 10000;
            var itemTimeout = 100;

            using var threadPool = new CustomThreadPool(0, 20);

            var case1BeginList = new ConcurrentBag<int>();
            var case1EndList = new ConcurrentBag<int>();

            var case2BeginList = new ConcurrentBag<int>();
            var case2EndList = new ConcurrentBag<int>();

            var count = 200;

            foreach (var n in Enumerable.Range(1, count))
            {
                case1BeginList.Add(n);

                threadPool.Run(() => {
                    Thread.Sleep(itemTimeout);
                    case1EndList.Add(n);
                });
            }

            Thread.Sleep(timeoutBetweenSets);

            _logger.Info($"case1BeginList.Count = {case1BeginList.Count}");
            _logger.Info($"case1EndList.Count = {case1EndList.Count}");

            foreach (var n in Enumerable.Range(1, count))
            {
                case2BeginList.Add(n);

                threadPool.Run(() => {
                    Thread.Sleep(itemTimeout);
                    case2EndList.Add(n);
                });
            }

            Thread.Sleep(timeoutBetweenSets);

            _logger.Info($"case2BeginList.Count = {case2BeginList.Count}");
            _logger.Info($"case2EndList.Count = {case2EndList.Count}");

            _logger.Info("End");
        }

        private void Case5()
        {
            _logger.Info("Begin");

            var threadPool = new CustomThreadPool(0, 20);

            foreach (var n in Enumerable.Range(1, 200))
            {
                threadPool.Run(() => {
                    _logger.Info($"Begin 1 {n}");
                    Thread.Sleep(100);
                    _logger.Info($"End 1 {n}");
                });
            }

            Thread.Sleep(10000);

            _logger.Info("###################");

            threadPool.Dispose();

            _logger.Info("End");
        }

        private void Case4()
        {
            _logger.Info("Begin");

            using var source = new CancellationTokenSource();

            using var threadPool1 = new CustomThreadPool(0, 20, source.Token);
            using var threadPool2 = new CustomThreadPool(0, 20, source.Token);
            using var threadPool3 = new CustomThreadPool(0, 20, source.Token);

            foreach (var n in Enumerable.Range(1, 200))
            {
                _logger.Info($"1 {n}");

                threadPool1.Run(() => {
                    _logger.Info($"Begin 1 {n}");
                    Thread.Sleep(100);
                    _logger.Info($"End 1 {n}");
                });
            }

            foreach (var n in Enumerable.Range(1, 200))
            {
                _logger.Info($"2 {n}");

                threadPool2.Run(() => {
                    _logger.Info($"Begin 2 {n}");
                    Thread.Sleep(100);
                    _logger.Info($"End 2 {n}");
                });
            }

            foreach (var n in Enumerable.Range(1, 200))
            {
                _logger.Info($"3 {n}");

                threadPool3.Run(() => {
                    _logger.Info($"Begin 3 {n}");
                    Thread.Sleep(100);
                    _logger.Info($"End 3 {n}");
                });
            }

            Thread.Sleep(100);

            _logger.Info("###################");

            source.Cancel();

            Thread.Sleep(100000);

            _logger.Info("End");
        }

        private void Case3()
        {
            _logger.Info("Begin");

            using var threadPool1 = new CustomThreadPool(0, 20);
            using var threadPool2 = new CustomThreadPool(0, 20);
            using var threadPool3 = new CustomThreadPool(0, 20);

            foreach (var n in Enumerable.Range(1, 200))
            {
                threadPool1.Run(() => {
                    _logger.Info($"Begin 1 {n}");
                    Thread.Sleep(100);
                    _logger.Info($"End 1 {n}");
                });
            }

            foreach (var n in Enumerable.Range(1, 200))
            {
                threadPool2.Run(() => {
                    _logger.Info($"Begin 2 {n}");
                    Thread.Sleep(100);
                    _logger.Info($"End 2 {n}");
                });
            }

            foreach (var n in Enumerable.Range(1, 200))
            {
                threadPool3.Run(() => {
                    _logger.Info($"Begin 3 {n}");
                    Thread.Sleep(100);
                    _logger.Info($"End 3 {n}");
                });
            }

            Thread.Sleep(10000);

            _logger.Info("End");
        }

        private void Case2()
        {
            _logger.Info("Begin");

            using var threadPool1 = new CustomThreadPool(20, 20);
            using var threadPool2 = new CustomThreadPool(20, 20);
            using var threadPool3 = new CustomThreadPool(20, 20);

            foreach (var n in Enumerable.Range(1, 200))
            {
                threadPool1.Run(() => {
                    _logger.Info($"Begin 1 {n}");
                    Thread.Sleep(100);
                    _logger.Info($"End 1 {n}");
                });
            }

            foreach (var n in Enumerable.Range(1, 200))
            {
                threadPool2.Run(() => {
                    _logger.Info($"Begin 2 {n}");
                    Thread.Sleep(100);
                    _logger.Info($"End 2 {n}");
                });
            }

            foreach (var n in Enumerable.Range(1, 200))
            {
                threadPool3.Run(() => {
                    _logger.Info($"Begin 3 {n}");
                    Thread.Sleep(100);
                    _logger.Info($"End 3 {n}");
                });
            }

            Thread.Sleep(10000);

            _logger.Info("End");
        }

        private void Case1()
        {
            _logger.Info("Begin");

            var threadPool = new CustomThreadPool(200, 200);

            Thread.Sleep(100);

            threadPool.Run(() => {
                _logger.Info($"Begin");
                _logger.Info($"End");
            });

            foreach (var n in Enumerable.Range(1, 200))
            {
                threadPool.Run(() => {
                    _logger.Info($"Begin {n}");
                    _logger.Info($"End {n}");
                });
            }

            Thread.Sleep(10000);

            _logger.Info("End");
        }
    }
}
