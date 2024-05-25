using SymOntoClay.CLI.Helpers.CommandLineParsing;
using SymOntoClay.CLI.Helpers.CommandLineParsing.Options;
using System.Linq;

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
                @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_14_58_31\"
            };

            var inputOptionIdentifier = "--input";

            var parser = new CommandLineParser(GetOneSingleValueOption());
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(0));

            Assert.That(result.Params.Count, Is.EqualTo(1));
            Assert.That(result.Params.ContainsKey(inputOptionIdentifier), Is.EqualTo(true));
            Assert.That(result.Params[inputOptionIdentifier], Is.EqualTo(@"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_14_58_31\"));
        }

        [Test]
        public void TwoNamedOptions_NonEmptyCommandLine_Success()
        {
            var args = new List<string>()
            {
                "--i",
                @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_14_58_31\",
                "--o",
                @"c:\Users\SomeUser\source\repos\SymOntoClay\TestSandbox\bin\Debug\net7.0\MessagesLogsOutputDir\"
            };

            var inputOptionIdentifier = "--input";
            var outputOptionIdentifier = "--output";

            var parser = new CommandLineParser(GetTwoSingleValueOptions());
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(0));

            Assert.That(result.Params.Count, Is.EqualTo(2));
            Assert.That(result.Params.ContainsKey(inputOptionIdentifier), Is.EqualTo(true));
            Assert.That(result.Params[inputOptionIdentifier], Is.EqualTo(@"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_14_58_31\"));

            Assert.That(result.Params.ContainsKey(outputOptionIdentifier), Is.EqualTo(true));
            Assert.That(result.Params[outputOptionIdentifier], Is.EqualTo(@"c:\Users\SomeUser\source\repos\SymOntoClay\TestSandbox\bin\Debug\net7.0\MessagesLogsOutputDir\"));
        }

        [Test]
        public void OnePositionedOption_NonEmptyCommandLine_Success()
        {
            var args = new List<string>()
            {
                @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_14_58_31\"
            };

            var inputOptionIdentifier = "--input";

            var parser = new CommandLineParser(GetOneSingleValueOption());
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(0));

            Assert.That(result.Params.Count, Is.EqualTo(1));
            Assert.That(result.Params.ContainsKey(inputOptionIdentifier), Is.EqualTo(true));
            Assert.That(result.Params[inputOptionIdentifier], Is.EqualTo(@"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_14_58_31\"));
        }

        [Test]
        public void NamedGroupWithPositionedOption_NonEmptyCommandLine_Success()
        {
            var args = new List<string>()
            {
                "new",
                "Elf"
            };

            var newIdentifier = "new";
            var elfIdentifier = "Elf";
            var npcNameIdentifier = "NPCName";

            var parser = new CommandLineParser(GetCommandLineNamedGroupOptions());
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(0));

            Assert.That(result.Params.Count, Is.EqualTo(2));

            Assert.That(result.Params.ContainsKey(newIdentifier), Is.EqualTo(true));
            Assert.That(result.Params[newIdentifier], Is.EqualTo(true));

            Assert.That(result.Params.ContainsKey(npcNameIdentifier), Is.EqualTo(true));
            Assert.That(result.Params[npcNameIdentifier], Is.EqualTo(elfIdentifier));
        }

        [Test]
        public void OneListValueOption_TwoValuesInCommandLine_Success()
        {
            var args = new List<string>()
                {
                    "--input",
                    "value1",
                    "value2"
                };

            var inputOptionIdentifier = "--input";

            var parser = new CommandLineParser(OneListValueOption());
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(0));

            Assert.That(result.Params.Count, Is.EqualTo(1));
            Assert.That(result.Params.ContainsKey(inputOptionIdentifier), Is.EqualTo(true));

            var inputValue = (List<string>)result.Params[inputOptionIdentifier];
            Assert.That(inputValue.Count, Is.EqualTo(2));
            Assert.That(inputValue.Contains("value1"), Is.EqualTo(true));
            Assert.That(inputValue.Contains("value2"), Is.EqualTo(true));
        }

        [Test]
        public void OneListValueOption_OneValueInCommandLine_Success()
        {
            var parser = new CommandLineParser(OneListValueOption());

            throw new NotImplementedException();
        }

        [Test]
        public void OneSingleValueOrListOption_TwoValuesInCommandLine_Success()
        {
            var args = new List<string>()
                {
                    "--input",
                    "value1",
                    "value2"
                };

            var inputOptionIdentifier = "--input";

            var parser = new CommandLineParser(OneSingleValueOrListOption());
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(0));

            Assert.That(result.Params.Count, Is.EqualTo(1));
            Assert.That(result.Params.ContainsKey(inputOptionIdentifier), Is.EqualTo(true));

            var inputValue = (List<string>)result.Params[inputOptionIdentifier];
            Assert.That(inputValue.Count, Is.EqualTo(2));
            Assert.That(inputValue.Contains("value1"), Is.EqualTo(true));
            Assert.That(inputValue.Contains("value2"), Is.EqualTo(true));
        }

        [Test]
        public void OneSingleValueOrListOption_OneValueInCommandLine_Success()
        {
            var parser = new CommandLineParser(OneSingleValueOrListOption());

            throw new NotImplementedException();
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

        private List<BaseCommandLineArgument> GetCommandLineNamedGroupOptions()
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
                            Name = "version",
                            Aliases = new List<string>
                            {
                                "v"
                            },
                            Kind = KindOfCommandLineArgument.Flag
                        },
                        new CommandLineArgument()
                        {
                            Name = "run",
                            Kind = KindOfCommandLineArgument.FlagOrSingleValue
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
                                new CommandLineMutuallyExclusiveSet()
                                {
                                    IsRequired = true,
                                    SubItems = new List<BaseCommandLineArgument>
                                    {
                                        new CommandLineArgument()
                                        {
                                            Target = "NPCName",
                                            Kind = KindOfCommandLineArgument.SingleValue,
                                            Index = 0
                                        },
                                        new CommandLineArgument()
                                        {
                                            Target = "NPCName",
                                            Name = "-npc",
                                            Kind = KindOfCommandLineArgument.SingleValue
                                        },
                                        new CommandLineArgument()
                                        {
                                            Name = "-thing",
                                            Kind = KindOfCommandLineArgument.SingleValue
                                        },
                                        new CommandLineArgument()
                                        {
                                            Name = "-world",
                                            Aliases = new List<string>()
                                            {
                                                "-w"
                                            },
                                            Kind = KindOfCommandLineArgument.SingleValue
                                        },
                                        new CommandLineArgument()
                                        {
                                            Name = "-lib",
                                            Aliases = new List<string>()
                                            {
                                                "-l"
                                            },
                                            Kind = KindOfCommandLineArgument.SingleValue
                                        },
                                        new CommandLineArgument()
                                        {
                                            Name = "-nav",
                                            Kind = KindOfCommandLineArgument.SingleValue
                                        },
                                        new CommandLineArgument()
                                        {
                                            Name = "-player",
                                            Aliases = new List<string>()
                                            {
                                                "-p"
                                            },
                                            Kind = KindOfCommandLineArgument.SingleValue
                                        }
                                    }
                                }
                            }
                        },
                        new CommandLineArgument()
                        {
                            Name = "install",
                            Kind = KindOfCommandLineArgument.SingleValue
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

        private List<BaseCommandLineArgument> OneListValueOption()
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
                        Kind = KindOfCommandLineArgument.List
                    }
                };
        }

        private List<BaseCommandLineArgument> OneListValueOptionAndNextSingleValueOption()
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
                        Kind = KindOfCommandLineArgument.List
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

        private List<BaseCommandLineArgument> OneSingleValueOrListOption()
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
                        Kind = KindOfCommandLineArgument.SingleValueOrList
                    }
                };
        }

        private List<BaseCommandLineArgument> OneSingleValueOrListOptionAndNextSingleValueOption()
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
                        Kind = KindOfCommandLineArgument.SingleValueOrList
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
