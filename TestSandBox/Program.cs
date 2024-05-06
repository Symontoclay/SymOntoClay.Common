using NLog;
using System.Text;
using SymOntoClay.Common.DebugHelpers;
using SymOntoClay.CLI.Helpers.CommandLineParsing;

namespace TestSandBox
{
    internal class Program
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            TstCommandLineParser();
            //TstPrintExisting();
        }

        private static void TstCommandLineParser()
        {
            _logger.Info("Begin");

            var parser = new CommandLineParser(new List<BaseCommandLineArgument>()
            {
                new CommandLineMutuallyExclusiveSet()
                {
                    IsRequired = true,
                    SubItems = new List<BaseCommandLineArgument>
                    {
                        new CommandLineArgument()
                        {
                            Name = "h",
                            Aliases = new List<string>
                            {
                                "help"
                            },
                            Kind = KindOfCommandLineArgument.Flag
                        },
                        new CommandLineArgument()
                        {
                            Name = "version",
                            Aliases = new List<string>
                            {
                                "v"
                            },
                            Kind = KindOfCommandLineArgument.Flag
                        },
                        new CommandLineNamedGroup()
                        {
                            Name = "run",
                            SubItems = new List<BaseCommandLineArgument>
                            {

                            }
                        },
                        new CommandLineNamedGroup()
                        {
                            Name = "new",
                            Aliases = new List<string>
                            {
                                "n"
                            },
                            SubItems = new List<BaseCommandLineArgument>
                            {

                            }
                        },
                        new CommandLineNamedGroup()
                        {
                            Name = "install",
                            SubItems = new List<BaseCommandLineArgument>
                            {

                            }
                        }
                    }
                },
                new CommandLineGroup()
                {
                    SubItems = new List<BaseCommandLineArgument>
                    {
                        new CommandLineArgument()
                        {
                            Name = "-nologo",
                            Kind = KindOfCommandLineArgument.Flag
                        },
                        new CommandLineArgument()
                        {
                            Name = "-timeout",
                            Kind = KindOfCommandLineArgument.SingleValue
                        },
                        new CommandLineArgument()
                        {
                            Name = "-nlp",
                            Kind = KindOfCommandLineArgument.Flag
                        }
                    }
                }
            });

            _logger.Info("End");
        }

        private static void TstPrintExisting()
        {
            _logger.Info("Begin");

            var n = 0u;

            var sb = new StringBuilder();

            object obj1 = null;

            sb.PrintExisting(n, nameof(obj1), obj1);
            //_logger.Info(sb);

            var obj2 = new object();

            sb.PrintExisting(n, nameof(obj2), obj2);
            //_logger.Info(sb);

            var list1 = new List<string>();

            sb.PrintExisting(n, nameof(list1), list1);
            //_logger.Info(sb);

            List<string> list2 = null;

            sb.PrintExisting(n, nameof(list2), list2);
            //_logger.Info(sb);

            var list3 = new List<string>()
            {
                "Hi!"
            };

            sb.PrintExisting(n, nameof(list3), list3);
            //_logger.Info(sb);

            var str1 = "";

            sb.PrintExisting(n, nameof(str1), str1);
            //_logger.Info(sb);

            string str2 = null;

            sb.PrintExisting(n, nameof(str2), str2);
            //_logger.Info(sb);

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
