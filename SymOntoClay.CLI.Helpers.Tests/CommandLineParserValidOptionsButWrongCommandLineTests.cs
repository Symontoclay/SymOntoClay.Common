using SymOntoClay.CLI.Helpers.CommandLineParsing.Exceptions;
using SymOntoClay.CLI.Helpers.CommandLineParsing;
using SymOntoClay.CLI.Helpers.CommandLineParsing.Options;

namespace SymOntoClay.CLI.Helpers.Tests
{
    public class CommandLineParserValidOptionsButWrongCommandLineTests
    {
        [Test]
        public void DuplicatedMutuallyExclusiveSet_WrongCommandLine_Fail()
        {
            var args = new List<string>()
            { 
                "help",
                "run"
            };

            var exception = Assert.Catch<DuplicatedMutuallyExclusiveOptionsSetException>(() => {
                var parser = new CommandLineParser(GetMinimalRequiredMutuallyExclusiveSet());
                parser.Parse(args.ToArray());
            });

            Assert.That(exception.Message, Is.EqualTo("Options 'help', 'run' cannot be used at the same time."));
        }

        [Test]
        public void DuplicatedMutuallyExclusiveSet_WrongCommandLine_ErrorsList()
        {
            var args = new List<string>()
            {
                "help",
                "run"
            };

            var parser = new CommandLineParser(GetMinimalRequiredMutuallyExclusiveSet(), true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors[0], Is.EqualTo("Options 'help', 'run' cannot be used at the same time."));
        }

        [Test]
        public void RequredNoDefaultOptions_EmptyCommandLine_Fail()
        {
            var args = new List<string>();

            var exception = Assert.Catch<RequiredOptionException>(() => {
                var parser = new CommandLineParser(GetMinimalRequiredMutuallyExclusiveSet());
                parser.Parse(args.ToArray());
            });

            Assert.That(exception.Message, Is.EqualTo("Required command line arguments must be entered."));
        }

        [Test]
        public void RequredNoDefaultOptions_EmptyCommandLine_ErrorsList()
        {
            var args = new List<string>();

            var parser = new CommandLineParser(GetMinimalRequiredMutuallyExclusiveSet(), true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors[0], Is.EqualTo("Required command line arguments must be entered."));
        }

        [Test]
        public void RequredNoDefaultOptions_WrongCommandLine_Fail()
        {
            var args = new List<string>()
                {
                    "-nologo"
                };

            var exception = Assert.Catch<RequiredOptionException>(() => {
                var parser = new CommandLineParser(GetMinimalRequiredMutuallyExclusiveSetAndOptionalParams());
                parser.Parse(args.ToArray());
            });

            Assert.That(exception.Message, Is.EqualTo("Required command line arguments must be entered."));
        }

        [Test]
        public void RequredNoDefaultOptions_WrongCommandLine_ErrorsList()
        {
            var args = new List<string>()
                {
                    "-nologo"
                };

            var parser = new CommandLineParser(GetMinimalRequiredMutuallyExclusiveSetAndOptionalParams(), true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors[0], Is.EqualTo("Required command line arguments must be entered."));
        }

        private List<BaseCommandLineArgument> GetMinimalRequiredMutuallyExclusiveSet()
        {
            return new List<BaseCommandLineArgument>()
                {
                    new CommandLineMutuallyExclusiveSet()
                    {
                        IsRequired = true,
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
                };
        }

        private List<BaseCommandLineArgument> GetMinimalRequiredMutuallyExclusiveSetAndOptionalParams()
        {
            return new List<BaseCommandLineArgument>()
                {
                    new CommandLineMutuallyExclusiveSet()
                    {
                        IsRequired = true,
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
                };
        }
    }
}
