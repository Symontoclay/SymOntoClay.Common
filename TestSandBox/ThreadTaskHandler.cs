using SymOntoClay.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            RunWithCustomThreadPool();
            //RunWithOwnThread();

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
