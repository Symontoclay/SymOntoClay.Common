using NLog;
using SymOntoClay.CLI.Helpers.CommandLineParsing;
using SymOntoClay.CLI.Helpers.CommandLineParsing.Options;

namespace TestSandBox
{
    public class TstCommandLineParserHandler
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public void Run()
        {
            _logger.Info("Begin");

            SingleValueCase();
            //NonRequiredMutuallyExclusiveSet_EmptyCommandLine_Success();

            _logger.Info("End");
        }

        private void SingleValueCase()
        {
            _logger.Info("Begin");

            _logger.Info("Begin");

            var parser = new CommandLineParser(new List<BaseCommandLineArgument>()
            {
                new CommandLineArgument()
                {
                    Name = "--input",
                    Aliases = new List<string>()
                    {
                        "--i"
                    },
                    Kind = KindOfCommandLineArgument.SingleValue,
                    Index = 0
                }
            });

            //{
            //    var args = new List<string>();

            //    var result = parser.Parse(args.ToArray());

            //    _logger.Info($"result = {result}");
            //}

            {
                var commandLineStr = @"--i c:\Users\Acer\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_13_58_31\";

                _logger.Info($"commandLineStr = {commandLineStr}");

                var args = commandLineStr.Split(' ').ToList();

                var result = parser.Parse(args.ToArray());

                _logger.Info($"result = {result}");
            }

            _logger.Info("End");

            _logger.Info("End");
        }

        private void NonRequiredMutuallyExclusiveSet_EmptyCommandLine_Success()
        {
            _logger.Info("Begin");

            var parser = new CommandLineParser(new List<BaseCommandLineArgument>()
                {
                    new CommandLineMutuallyExclusiveSet()
                    {
                        SubItems = new List<BaseCommandLineArgument>
                        {
                            new CommandLineArgument()
                            {
                                Name = "help",
                                Aliases = new List<string>
                                {
                                    "h"
                                },
                                Kind = KindOfCommandLineArgument.Flag,
                            },
                            new CommandLineArgument()
                            {
                                Name = "run",
                                Aliases = new List<string>
                                {
                                    "r"
                                },
                                Kind = KindOfCommandLineArgument.Flag,
                            }
                        }
                    }
                }, true);

            var args = new List<string>();

            var result = parser.Parse(args.ToArray());

            _logger.Info($"result = {result}");

            _logger.Info("End");
        }
    }
}
