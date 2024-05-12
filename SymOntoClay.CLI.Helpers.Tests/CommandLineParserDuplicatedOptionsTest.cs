using SymOntoClay.CLI.Helpers.CommandLineParsing;
using SymOntoClay.CLI.Helpers.CommandLineParsing.Exceptions;
using SymOntoClay.CLI.Helpers.CommandLineParsing.Options;

namespace SymOntoClay.CLI.Helpers.Tests
{
    public class CommandLineParserDuplicatedOptionsTest
    {
        [Test]
        public void NoDuplicatedOptions_EmptyCommandLine_Success()
        {
            var args = new List<string>();
            var parser = new CommandLineParser(GetNoDuplicatedOptions());

            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(0));

            Assert.That(result.Params.Count, Is.EqualTo(0));
        }

        [Test]
        public void DuplicatedOptions_EmptyCommandLine_Fail()
        {
            var args = new List<string>();

            var exception = Assert.Catch<DuplicatedOptionException>(() => {
                var parser = new CommandLineParser(GetDuplicatedOptions());
                parser.Parse(args.ToArray());
            });

            Assert.That(exception.Message == "Option 'run' is declared 2 times." || exception.Message == "Option 'r' is declared 2 times.", Is.EqualTo(true));
        }

        [Test]
        public void DuplicatedOptions_EmptyCommandLine_ErrorsList()
        {
            var args = new List<string>();

            var parser = new CommandLineParser(GetDuplicatedOptions(), true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(2));

            Assert.That(result.Errors.Contains("Option 'run' is declared 2 times."), Is.EqualTo(true));
            Assert.That(result.Errors.Contains("Option 'r' is declared 2 times."), Is.EqualTo(true));
        }

        private List<BaseCommandLineArgument> GetNoDuplicatedOptions()
        {
            return new List<BaseCommandLineArgument>()
                {
                    new CommandLineArgument()
                    {
                        Name = "help",
                        Aliases = new List<string>
                        {
                            "h"
                        },
                        Kind = KindOfCommandLineArgument.Flag
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
                };
        }

        private List<BaseCommandLineArgument> GetDuplicatedOptions()
        {
            return new List<BaseCommandLineArgument>()
                {
                    new CommandLineArgument()
                    {
                        Name = "run",
                        Aliases = new List<string>
                        {
                            "r"
                        },
                        Kind = KindOfCommandLineArgument.Flag
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
                };
        }
    }
}
