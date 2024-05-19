using SymOntoClay.CLI.Helpers.CommandLineParsing;
using SymOntoClay.CLI.Helpers.CommandLineParsing.Options;

namespace SymOntoClay.CLI.Helpers.Tests
{
    public class CommandLineParserTests
    {
        [Test]
        public void NonRequiredMutuallyExclusiveSet_EmptyCommandLine_Success()
        {
            var args = new List<string>();
            var parser = new CommandLineParser(GetMinimalNonRequiredMutuallyExclusiveSet());

            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(0));

            Assert.That(result.Params.Count, Is.EqualTo(0));
        }

        [Test]
        public void OneNamedOption_NonEmptyCommandLine_Success()
        {
            var args = new List<string>()
            {
                "--i",
                @"c:\Users\Acer\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_13_58_31\"
            };

            var inputOptionIdentifier = "--input";

            var parser = new CommandLineParser(GetOneSingleValueOption());
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(0));

            Assert.That(result.Params.Count, Is.EqualTo(1));
            Assert.That(result.Params.ContainsKey(inputOptionIdentifier), Is.EqualTo(true));
            Assert.That(result.Params[inputOptionIdentifier], Is.EqualTo(@"c:\Users\Acer\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_13_58_31\"));
        }

        [Test]
        public void TwoNamedOptions_NonEmptyCommandLine_Success()
        {
            var args = new List<string>()
            {
                "--i",
                @"c:\Users\Acer\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_13_58_31\",
                "--o",
                @"c:\Users\Acer\source\repos\SymOntoClay\TestSandbox\bin\Debug\net7.0\MessagesLogsOutputDir\"
            };

            var inputOptionIdentifier = "--input";
            var outputOptionIdentifier = "--output";

            var parser = new CommandLineParser(GetTwoSingleValueOptions());
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(0));

            Assert.That(result.Params.Count, Is.EqualTo(2));
            Assert.That(result.Params.ContainsKey(inputOptionIdentifier), Is.EqualTo(true));
            Assert.That(result.Params[inputOptionIdentifier], Is.EqualTo(@"c:\Users\Acer\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_13_58_31\"));

            Assert.That(result.Params.ContainsKey(outputOptionIdentifier), Is.EqualTo(true));
            Assert.That(result.Params[outputOptionIdentifier], Is.EqualTo(@"c:\Users\Acer\source\repos\SymOntoClay\TestSandbox\bin\Debug\net7.0\MessagesLogsOutputDir\"));
        }

        private List<BaseCommandLineArgument> GetMinimalNonRequiredMutuallyExclusiveSet()
        {
            return new List<BaseCommandLineArgument>()
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
                };
        }

        private List<BaseCommandLineArgument> GetOneSingleValueOption()
        {
            return new List<BaseCommandLineArgument>()
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
            };
        }

        private List<BaseCommandLineArgument> GetTwoSingleValueOptions()
        {
            return new List<BaseCommandLineArgument>()
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
                },
                new CommandLineArgument
                {
                    Name = "--output",
                    Aliases = new List<string>
                    {
                        "--o"
                    },
                    Kind = KindOfCommandLineArgument.SingleValue
                }
            };
        }
    }
}
