using SymOntoClay.Threading;

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

            Case4();
            //Case3();
            //Case2();
            //Case1();

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
