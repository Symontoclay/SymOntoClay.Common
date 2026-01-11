/*MIT License

Copyright (c) 2020 - 2026 Sergiy Tolkachov

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.*/

using SymOntoClay.CLI.Helpers.CommandLineParsing;
using SymOntoClay.CLI.Helpers.CommandLineParsing.Options;
using SymOntoClay.CLI.Helpers.CommandLineParsing.Options.TypeCheckers;
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
        public void OneListValueOption_TwoValuesInTheEndOfCommandLine_Success()
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
        public void OneListValueOption_OneValueInTheEndOfCommandLine_Success()
        {
            var args = new List<string>()
                {
                    "--input",
                    "value1",
                };

            var inputOptionIdentifier = "--input";

            var parser = new CommandLineParser(OneListValueOption());
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(0));

            Assert.That(result.Params.Count, Is.EqualTo(1));
            Assert.That(result.Params.ContainsKey(inputOptionIdentifier), Is.EqualTo(true));

            var inputValue = (List<string>)result.Params[inputOptionIdentifier];
            Assert.That(inputValue.Count, Is.EqualTo(1));
            Assert.That(inputValue.Contains("value1"), Is.EqualTo(true));
        }

        [Test]
        public void OneListValueOption_TwoValuesInTheMiddleOfCommandLine_Success()
        {
            var args = new List<string>()
                {
                    "--input",
                    "value1",
                    "value2",
                    "--output",
                    "someValue"
                };

            var inputOptionIdentifier = "--input";
            var outputOptionIdentifier = "--output";

            var parser = new CommandLineParser(OneListValueOptionAndNextSingleValueOption());
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(0));

            Assert.That(result.Params.Count, Is.EqualTo(2));
            Assert.That(result.Params.ContainsKey(inputOptionIdentifier), Is.EqualTo(true));

            var inputValue = (List<string>)result.Params[inputOptionIdentifier];
            Assert.That(inputValue.Count, Is.EqualTo(2));
            Assert.That(inputValue.Contains("value1"), Is.EqualTo(true));
            Assert.That(inputValue.Contains("value2"), Is.EqualTo(true));

            Assert.That(result.Params.ContainsKey(outputOptionIdentifier), Is.EqualTo(true));
            Assert.That(result.Params[outputOptionIdentifier], Is.EqualTo("someValue"));
        }

        [Test]
        public void OneListValueOption_OneValueInTheMiddleOfCommandLine_Success()
        {
            var args = new List<string>()
                {
                    "--input",
                    "value1",
                    "--output",
                    "someValue"
                };

            var inputOptionIdentifier = "--input";
            var outputOptionIdentifier = "--output";

            var parser = new CommandLineParser(OneListValueOptionAndNextSingleValueOption());
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(0));

            Assert.That(result.Params.Count, Is.EqualTo(2));
            Assert.That(result.Params.ContainsKey(inputOptionIdentifier), Is.EqualTo(true));

            var inputValue = (List<string>)result.Params[inputOptionIdentifier];
            Assert.That(inputValue.Count, Is.EqualTo(1));
            Assert.That(inputValue.Contains("value1"), Is.EqualTo(true));

            Assert.That(result.Params.ContainsKey(outputOptionIdentifier), Is.EqualTo(true));
            Assert.That(result.Params[outputOptionIdentifier], Is.EqualTo("someValue"));
        }

        [Test]
        public void OneSingleValueOrListOption_TwoValuesInTheEndOfCommandLine_Success()
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
        public void OneSingleValueOrListOption_OneValueInTheEndOfCommandLine_Success()
        {
            var args = new List<string>()
                {
                    "--input",
                    "value1",
                };

            var inputOptionIdentifier = "--input";

            var parser = new CommandLineParser(OneSingleValueOrListOption());
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(0));

            Assert.That(result.Params.Count, Is.EqualTo(1));
            Assert.That(result.Params.ContainsKey(inputOptionIdentifier), Is.EqualTo(true));

            var inputValue = (List<string>)result.Params[inputOptionIdentifier];
            Assert.That(inputValue.Count, Is.EqualTo(1));
            Assert.That(inputValue.Contains("value1"), Is.EqualTo(true));
        }

        [Test]
        public void OneSingleValueOrListOption_TwoValuesInTheMiddleOfCommandLine_Success()
        {
            var args = new List<string>()
                {
                    "--input",
                    "value1",
                    "--output",
                    "someValue"
                };

            var inputOptionIdentifier = "--input";
            var outputOptionIdentifier = "--output";

            var parser = new CommandLineParser(OneSingleValueOrListOptionAndNextSingleValueOption());
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(0));

            Assert.That(result.Params.Count, Is.EqualTo(2));
            Assert.That(result.Params.ContainsKey(inputOptionIdentifier), Is.EqualTo(true));

            var inputValue = (List<string>)result.Params[inputOptionIdentifier];
            Assert.That(inputValue.Count, Is.EqualTo(1));
            Assert.That(inputValue.Contains("value1"), Is.EqualTo(true));

            Assert.That(result.Params.ContainsKey(outputOptionIdentifier), Is.EqualTo(true));
            Assert.That(result.Params[outputOptionIdentifier], Is.EqualTo("someValue"));
        }

        [Test]
        public void OneSingleValueOrListOption_OneValueInTheMiddleOfCommandLine_Success()
        {
            var args = new List<string>()
                {
                    "--input",
                    "value1",
                    "value2",
                    "--output",
                    "someValue"
                };

            var inputOptionIdentifier = "--input";
            var outputOptionIdentifier = "--output";

            var parser = new CommandLineParser(OneSingleValueOrListOptionAndNextSingleValueOption());
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(0));

            Assert.That(result.Params.Count, Is.EqualTo(2));
            Assert.That(result.Params.ContainsKey(inputOptionIdentifier), Is.EqualTo(true));

            var inputValue = (List<string>)result.Params[inputOptionIdentifier];
            Assert.That(inputValue.Count, Is.EqualTo(2));
            Assert.That(inputValue.Contains("value1"), Is.EqualTo(true));
            Assert.That(inputValue.Contains("value2"), Is.EqualTo(true));

            Assert.That(result.Params.ContainsKey(outputOptionIdentifier), Is.EqualTo(true));
            Assert.That(result.Params[outputOptionIdentifier], Is.EqualTo("someValue"));
        }

        [Test]
        public void OneFlagOrSingleValueOption_SingleValueInTheEndOfCommandLine_Success()
        {
            var args = new List<string>()
                {
                    "--input",
                    "value1",
                };

            var inputOptionIdentifier = "--input";

            var parser = new CommandLineParser(OneFlagOrSingleValueOption());
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(0));

            Assert.That(result.Params.Count, Is.EqualTo(1));
            Assert.That(result.Params.ContainsKey(inputOptionIdentifier), Is.EqualTo(true));
            Assert.That(result.Params[inputOptionIdentifier], Is.EqualTo("value1"));
        }

        [Test]
        public void OneFlagOrSingleValueOption_FlagInTheEndOfCommandLine_Success()
        {
            var args = new List<string>()
                {
                    "--input"
                };

            var inputOptionIdentifier = "--input";

            var parser = new CommandLineParser(OneFlagOrSingleValueOption());
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(0));

            Assert.That(result.Params.Count, Is.EqualTo(1));
            Assert.That(result.Params.ContainsKey(inputOptionIdentifier), Is.EqualTo(true));
            Assert.That(result.Params[inputOptionIdentifier], Is.EqualTo(true));
        }

        [Test]
        public void OneFlagOrSingleValueOption_SingleValueInTheMiddleOfCommandLine_Success()
        {
            var args = new List<string>()
                {
                    "--input",
                    "value1",
                    "--output",
                    "someValue"
                };

            var inputOptionIdentifier = "--input";
            var outputOptionIdentifier = "--output";

            var parser = new CommandLineParser(OneFlagOrSingleValueOptionAndNextSingleValueOption());
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(0));

            Assert.That(result.Params.Count, Is.EqualTo(2));
            Assert.That(result.Params.ContainsKey(inputOptionIdentifier), Is.EqualTo(true));
            Assert.That(result.Params[inputOptionIdentifier], Is.EqualTo("value1"));

            Assert.That(result.Params.ContainsKey(outputOptionIdentifier), Is.EqualTo(true));
            Assert.That(result.Params[outputOptionIdentifier], Is.EqualTo("someValue"));
        }

        [Test]
        public void OneFlagOrSingleValueOption_FlagInTheMiddleOfCommandLine_Success()
        {
            var args = new List<string>()
                {
                    "--input",
                    "--output",
                    "someValue"
                };

            var inputOptionIdentifier = "--input";
            var outputOptionIdentifier = "--output";

            var parser = new CommandLineParser(OneFlagOrSingleValueOptionAndNextSingleValueOption());
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(0));

            Assert.That(result.Params.Count, Is.EqualTo(2));
            Assert.That(result.Params.ContainsKey(inputOptionIdentifier), Is.EqualTo(true));
            Assert.That(result.Params[inputOptionIdentifier], Is.EqualTo(true));

            Assert.That(result.Params.ContainsKey(outputOptionIdentifier), Is.EqualTo(true));
            Assert.That(result.Params[outputOptionIdentifier], Is.EqualTo("someValue"));
        }

        [Test]
        public void OneSingleValuePositionedOptionWithEnumChecker_ValidCommandLine_Success()
        {
            var args = new List<string>()
            {
                "NetStandard"
            };

            var targetFrameworkIdentifier = "TargetFramework";

            var parser = new CommandLineParser(OneSingleValuePositionedOptionWithEnumChecker());
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(0));

            Assert.That(result.Params.Count, Is.EqualTo(1));
            Assert.That(result.Params.ContainsKey(targetFrameworkIdentifier), Is.EqualTo(true));
            Assert.That((TestEnum)result.Params[targetFrameworkIdentifier], Is.EqualTo(TestEnum.NetStandard));
        }

        [Test]
        public void OneSingleValuePositionedOptionWithIntChecker_ValidCommandLine_Success()
        {
            var args = new List<string>()
            {
                "16"
            };

            var portIdentifier = "Port";

            var parser = new CommandLineParser(OneSingleValuePositionedOptionWithIntChecker());
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(0));

            Assert.That(result.Params.Count, Is.EqualTo(1));
            Assert.That(result.Params.ContainsKey(portIdentifier), Is.EqualTo(true));
            Assert.That((int)result.Params[portIdentifier], Is.EqualTo(16));
        }

        [Test]
        public void OneSingleValuePositionedOptionWithVersionChecker_ValidCommandLine_Success()
        {
            var args = new List<string>()
            {
                "0.3.1"
            };

            var versionIdentifier = "Version";

            var parser = new CommandLineParser(OneSingleValuePositionedOptionWithVersionChecker());
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(0));

            Assert.That(result.Params.Count, Is.EqualTo(1));
            Assert.That(result.Params.ContainsKey(versionIdentifier), Is.EqualTo(true));
            Assert.That((Version)result.Params[versionIdentifier], Is.EqualTo(new Version(0, 3, 1)));
        }

        [Test]
        public void Requires_ValidCommandLine_Success()
        {
            var args = new List<string>()
            {
                "--html",
                "--abs-url"
            };

            var htmlOptionIdentifier = "--html";
            var absUrlOptionIdentifier = "--abs-url";

            var parser = new CommandLineParser(Requires());
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(0));

            Assert.That(result.Params.Count, Is.EqualTo(2));
            Assert.That(result.Params.ContainsKey(htmlOptionIdentifier), Is.EqualTo(true));
            Assert.That(result.Params[htmlOptionIdentifier], Is.EqualTo(true));

            Assert.That(result.Params.ContainsKey(absUrlOptionIdentifier), Is.EqualTo(true));
            Assert.That(result.Params[absUrlOptionIdentifier], Is.EqualTo(true));
        }

        [Test]
        public void Requires_EmptyCommandLine_Success()
        {
            var args = new List<string>();

            var parser = new CommandLineParser(Requires());
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(0));

            Assert.That(result.Params.Count, Is.EqualTo(0));
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

        private List<BaseCommandLineArgument> OneFlagOrSingleValueOption()
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
                    Kind = KindOfCommandLineArgument.FlagOrSingleValue
                }
            };
        }

        private List<BaseCommandLineArgument> OneFlagOrSingleValueOptionAndNextSingleValueOption()
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
                    Kind = KindOfCommandLineArgument.FlagOrSingleValue
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

        private List<BaseCommandLineArgument> OneSingleValuePositionedOptionWithEnumChecker()
        {
            return new List<BaseCommandLineArgument>()
            {
                new CommandLineArgument()
                {
                    Target = "TargetFramework",
                    Kind = KindOfCommandLineArgument.SingleValue,
                    Index = 0,
                    TypeChecker = new EnumChecker<TestEnum>(),
                    TypeCheckErrorMessage = "Unknown target framework"
                }
            };
        }

        private List<BaseCommandLineArgument> OneSingleValuePositionedOptionWithIntChecker()
        {
            return new List<BaseCommandLineArgument>()
            {
                new CommandLineArgument()
                {
                    Target = "Port",
                    Kind = KindOfCommandLineArgument.SingleValue,
                    Index = 0,
                    TypeChecker = new IntChecker(),
                    TypeCheckErrorMessage = "Unknown port"
                }
            };
        }

        private List<BaseCommandLineArgument> OneSingleValuePositionedOptionWithVersionChecker()
        {
            return new List<BaseCommandLineArgument>()
            {
                new CommandLineArgument()
                {
                    Target = "Version",
                    Kind = KindOfCommandLineArgument.SingleValue,
                    Index = 0,
                    TypeChecker = new VersionChecker(),
                    TypeCheckErrorMessage = "Unknown version"
                }
            };
        }

        private List<BaseCommandLineArgument> Requires()
        {
            return new List<BaseCommandLineArgument>()
            {
                new CommandLineArgument
                {
                    Name = "--html",
                    Kind = KindOfCommandLineArgument.Flag
                },
                new CommandLineArgument
                {
                    Name = "--abs-url",
                    Kind = KindOfCommandLineArgument.Flag,
                    Requires = new List<string>
                    {
                        "--html"
                    }
                }
            };
        }
    }
}
