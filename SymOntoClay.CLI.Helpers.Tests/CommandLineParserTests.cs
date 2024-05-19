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

            var optionIdentifier = "--input";

            var parser = new CommandLineParser(GetOneOption());
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(0));

            Assert.That(result.Params.Count, Is.EqualTo(1));
            Assert.That(result.Params.ContainsKey(optionIdentifier), Is.EqualTo(true));
            Assert.That(result.Params[optionIdentifier], Is.EqualTo(@"c:\Users\Acer\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_13_58_31\"));
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

        private List<BaseCommandLineArgument> GetOneOption()
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
    }
}
