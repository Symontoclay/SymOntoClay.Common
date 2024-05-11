using NLog;
using SymOntoClay.CLI.Helpers.CommandLineParsing;
using SymOntoClay.CLI.Helpers.CommandLineParsing.Options;

namespace TestSandBox
{
    public class TstCommandLineParserHandlerWithNegativeCases
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public void Run()
        {
            _logger.Info("Begin");

            Case1();

            _logger.Info("End");
        }

        private void Case1()
        {
            _logger.Info("Begin");

            var parser = new CommandLineParser(new List<BaseCommandLineArgument>()
            {
                new CommandLineArgument()
                {
                    Name = "h",
                    Aliases = new List<string>
                    {
                        "help"
                    },
                    Kind = KindOfCommandLineArgument.Flag,
                    UseIfCommandLineIsEmpty = true
                },
                new CommandLineArgument()
                {
                    Name = "r",
                    Aliases = new List<string>
                    {
                        "run"
                    },
                    Kind = KindOfCommandLineArgument.Flag,
                    UseIfCommandLineIsEmpty = true
                }

            });

            var args = new List<string>();

            var result = parser.Parse(args.ToArray());

            _logger.Info($"result = {result}");

            _logger.Info("End");
        }
    }
}
