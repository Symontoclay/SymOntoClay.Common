using Newtonsoft.Json;
using NLog;
using SymOntoClay.CLI.Helpers;
using SymOntoClay.Common;
using SymOntoClay.Common.DebugHelpers;
using System.IO;
using System.Reflection;
using System.Text;

namespace TestSandBox
{
    internal class Program
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

#if DEBUG
            _logger.Info($"namedCommandLineArgumentsRawDict = {JsonConvert.SerializeObject(args, Formatting.Indented)}");
#endif

            //TstNLogInAppConfig();
            //TstConsoleWrapper();
            //TstEVPathNormalize();
            //TstCommandLineParserHandlerWithNegativeCases();
            //TstCommandLineParser();
            //TstCommandLineParserRealAppHandler();
            //TstPrintExisting();
        }

        private static void TstNLogInAppConfig()
        {
            //https://github.com/NLog/NLog/wiki/Configuration-file
            //https://github.com/NLog/NLog/issues/4722

            NLogSetupHelper.UseAppConfig();

            _logger.Info("Hi! I am configured in App.Config");
        }

        private static void TstConsoleWrapper()
        {
            _logger.Info("Begin");

            //ConsoleWrapper.SetNLogLogger(_logger);
            ConsoleWrapper.WriteOutputToTextFileAsParallel = true;

            ConsoleWrapper.WriteCopyright();
            ConsoleWrapper.WriteText("Hi!!!");

            _logger.Info("End");
        }

        private static void TstEVPathNormalize()
        {
            _logger.Info("Begin");

            var path = @"D:/";

            _logger.Info($"path = '{path}'");

            var result = EVPath.Normalize(path);

            _logger.Info($"result = '{result}'");

            _logger.Info("End");
        }

        private static void TstCommandLineParserHandlerWithNegativeCases()
        {
            _logger.Info("Begin");

            var handler = new TstCommandLineParserHandlerWithNegativeCases();
            handler.Run();

            _logger.Info("End");
        }

        private static void TstCommandLineParser()
        {
            _logger.Info("Begin");

            var handler = new TstCommandLineParserHandler();
            handler.Run();

            _logger.Info("End");
        }

        private static void TstCommandLineParserRealAppHandler()
        {
            _logger.Info("Begin");

            var handler = new TstCommandLineParserRealAppHandler();
            handler.Run();

            _logger.Info("End");
        }

        private static void TstPrintExisting()
        {
            _logger.Info("Begin");

            var n = 0u;

            var sb = new StringBuilder();

            object obj1 = null;

            sb.PrintExisting(n, nameof(obj1), obj1);
            _logger.Info(sb);

            sb = new StringBuilder();

            var obj2 = new object();

            sb.PrintExisting(n, nameof(obj2), obj2);
            _logger.Info(sb);

            sb = new StringBuilder();

            var list1 = new List<string>();

            sb.PrintExisting(n, nameof(list1), list1);
            _logger.Info(sb);

            sb = new StringBuilder();

            List<string> list2 = null;

            sb.PrintExisting(n, nameof(list2), list2);
            _logger.Info(sb);

            sb = new StringBuilder();

            var list3 = new List<string>()
            {
                "Hi!"
            };

            sb.PrintExisting(n, nameof(list3), list3);
            _logger.Info(sb);

            sb = new StringBuilder();

            var str1 = "";

            sb.PrintExisting(n, nameof(str1), str1);
            _logger.Info(sb);

            sb = new StringBuilder();

            string str2 = null;

            sb.PrintExisting(n, nameof(str2), str2);
            _logger.Info(sb);

            sb = new StringBuilder();

            var str3 = "Hi!";

            sb.PrintExisting(n, nameof(str3), str3);

            _logger.Info(sb);
            _logger.Info("End");
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            _logger.Info($"e.ExceptionObject = {e.ExceptionObject}");
        }
    }
}
