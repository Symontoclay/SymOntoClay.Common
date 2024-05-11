using SymOntoClay.CLI.Helpers.CommandLineParsing;
using SymOntoClay.CLI.Helpers.CommandLineParsing.Exceptions;
using SymOntoClay.CLI.Helpers.CommandLineParsing.Options;

namespace SymOntoClay.CLI.Helpers.Tests
{
    public class CommandLineParserDefaultOptionsTest
    {
        [Test]
        public void OneDefaultOption_EmptyCommandLine_Success()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void TwoDefaultOptions_EmptyCommandLine_Fail()
        {
            var args = new List<string>();

            var exception = Assert.Catch<DefaultOptionException>(() => { var parser = CreateParserWithTwoDefaultOptions();
                parser.Parse(args.ToArray()); });

            Assert.That(exception.Message, Is.EqualTo("Too many options set as default: 'help', 'run'. There must be only one default option."));
        }

        [Test]
        public void OneWrongDefaultOption_EmptyCommandLine_Fail()
        {
            var args = new List<string>();

            var exception = Assert.Catch<DefaultOptionException>(() => {
                var parser = CreateParserWithOneWrongDefaultOption();
                parser.Parse(args.ToArray());
            });

            Assert.That(exception.Message, Is.EqualTo("SingleValue must not be used as default option."));
        }

        private CommandLineParser CreateParserWithTwoDefaultOptions()
        {
            return new CommandLineParser(new List<BaseCommandLineArgument>()
            {
                new CommandLineArgument()
                {
                    Name = "help",
                    Aliases = new List<string>
                    {
                        "h"
                    },
                    Kind = KindOfCommandLineArgument.Flag,
                    UseIfCommandLineIsEmpty = true
                },
                new CommandLineArgument()
                {
                    Name = "run",
                    Aliases = new List<string>
                    {
                        "r"
                    },
                    Kind = KindOfCommandLineArgument.Flag,
                    UseIfCommandLineIsEmpty = true
                }
            });
        }

        private CommandLineParser CreateParserWithOneWrongDefaultOption()
        {
            return new CommandLineParser(new List<BaseCommandLineArgument>()
            {
                new CommandLineArgument()
                {
                    Name = "help",
                    Aliases = new List<string>
                    {
                        "h"
                    },
                    Kind = KindOfCommandLineArgument.SingleValue,
                    UseIfCommandLineIsEmpty = true
                },
                new CommandLineArgument()
                {
                    Name = "run",
                    Aliases = new List<string>
                    {
                        "r"
                    },
                    Kind = KindOfCommandLineArgument.Flag
                }
            });
        }
    }
}
