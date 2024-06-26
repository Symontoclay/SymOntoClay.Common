﻿using Newtonsoft.Json;
using NLog;
using SymOntoClay.CLI.Helpers.CommandLineParsing;
using SymOntoClay.CLI.Helpers.CommandLineParsing.Options;
using SymOntoClay.CLI.Helpers.CommandLineParsing.Options.TypeCheckers;
using SymOntoClay.CLI.Helpers.Tests;

namespace TestSandBox
{
    public class TstCommandLineParserHandler
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public void Run()
        {
            _logger.Info("Begin");

            //Requires_2();
            Requires();
            //VersionChecker();
            //IntChecker();
            //EnumChecker();
            //FlagOrSingleValue_Flag_2();
            //FlagOrSingleValue_Flag();
            //FlagOrSingleValue_SingleValue_2();
            //FlagOrSingleValue_SingleValue();
            //TstValueList_Case2();
            //TstValueList_Case1_1();
            //TstValueList_Case1();
            //TstPositionedSingleValueCase1();
            //OnePositionedSingleValueCase();
            //TwoNamedSingleValueCase();
            //OneNamedSingleValueCase();
            //NonRequiredMutuallyExclusiveSet_EmptyCommandLine_Success();

            _logger.Info("End");
        }

        private void Requires_2()
        {
            _logger.Info("Begin");

            var parser = new CommandLineParser(new List<BaseCommandLineArgument>()
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
            });

            var args = new List<string>();

            var result = parser.Parse(args.ToArray());

            _logger.Info($"result = {result}");

            _logger.Info("End");
        }

        private void Requires()
        {
            _logger.Info("Begin");

            var parser = new CommandLineParser(new List<BaseCommandLineArgument>()
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
            });

            var args = new List<string>()
            {
                "--html",
                "--abs-url"
            };

            var result = parser.Parse(args.ToArray());

            _logger.Info($"result = {result}");

            _logger.Info("End");
        }

        private void VersionChecker()
        {
            _logger.Info("Begin");

            var parser = new CommandLineParser(new List<BaseCommandLineArgument>()
            {
                new CommandLineArgument()
                {
                    Target = "Version",
                    Kind = KindOfCommandLineArgument.SingleValue,
                    Index = 0,
                    TypeChecker = new VersionChecker(),
                    TypeCheckErrorMessage = "Unknown version"
                }
            });

            var args = new List<string>()
            {
                "0.3.1"
            };

            var result = parser.Parse(args.ToArray());

            _logger.Info($"result = {result}");

            var version = (Version)result.Params["Version"];

            _logger.Info($"version = {version}");

            _logger.Info("End");
        }

        private void IntChecker()
        {
            _logger.Info("Begin");

            var parser = new CommandLineParser(new List<BaseCommandLineArgument>()
            {
                new CommandLineArgument()
                {
                    Target = "Port",
                    Kind = KindOfCommandLineArgument.SingleValue,
                    Index = 0,
                    TypeChecker = new IntChecker(),
                    TypeCheckErrorMessage = "Unknown port"
                }
            });

            var args = new List<string>()
            {
                "16"
            };

            var result = parser.Parse(args.ToArray());

            _logger.Info($"result = {result}");

            var port = (int)result.Params["Port"];

            _logger.Info($"port = {port}");

            _logger.Info("End");
        }

        private void EnumChecker()
        {
            _logger.Info("Begin");

            var parser = new CommandLineParser(new List<BaseCommandLineArgument>()
            {
                new CommandLineArgument()
                {
                    Target = "TargetFramework",
                    Kind = KindOfCommandLineArgument.SingleValue,
                    Index = 0,
                    TypeChecker = new EnumChecker<TestEnum>(),
                    TypeCheckErrorMessage = "Unknown target framework"
                }
            });

            var args = new List<string>()
            {
                "NetStandard"
            };

            var result = parser.Parse(args.ToArray());

            _logger.Info($"result = {result}");

            var targetFramework = (TestEnum)result.Params["TargetFramework"];

            _logger.Info($"targetFramework = {targetFramework}");

            _logger.Info("End");
        }

        private void FlagOrSingleValue_Flag_2()
        {
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
            });

            {
                var args = new List<string>()
                {
                    "--input",
                    "--output",
                    "someValue"
                };

                var result = parser.Parse(args.ToArray());

                _logger.Info($"result = {result}");
            }

            _logger.Info("End");
        }

        private void FlagOrSingleValue_Flag()
        {
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
                    Kind = KindOfCommandLineArgument.FlagOrSingleValue
                }
            });

            {
                var args = new List<string>()
                {
                    "--input"
                };

                var result = parser.Parse(args.ToArray());

                _logger.Info($"result = {result}");
            }

            _logger.Info("End");
        }

        private void FlagOrSingleValue_SingleValue_2()
        {
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
            });

            {
                var args = new List<string>()
                {
                    "--input",
                    "value1",
                    "--output",
                    "someValue"
                };

                var result = parser.Parse(args.ToArray());

                _logger.Info($"result = {result}");
            }

            _logger.Info("End");
        }

        private void FlagOrSingleValue_SingleValue()
        {
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
                    Kind = KindOfCommandLineArgument.FlagOrSingleValue
                }
            });

            {
                var args = new List<string>()
                {
                    "--input",
                    "value1",
                };

                var result = parser.Parse(args.ToArray());

                _logger.Info($"result = {result}");
            }

            _logger.Info("End");
        }

        private void TstValueList_Case2()
        {
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
            });

            {
                var args = new List<string>()
                {
                    "--input",
                    "value1",
                    "value2",
                    "--output",
                    "someValue"
                };

                var result = parser.Parse(args.ToArray());

                _logger.Info($"result = {result}");

                var inputValue = (List<string>)result.Params["--input"];

#if DEBUG
                _logger.Info($"inputValue = {JsonConvert.SerializeObject(inputValue, Formatting.Indented)}");
#endif
            }

            _logger.Info("End");
        }

        private void TstValueList_Case1_1()
        {
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
                    Kind = KindOfCommandLineArgument.List,
                    Index = 0
                }
            });

            {
                var args = new List<string>()
                {
                    "--input",
                    "value1",
                };

                var result = parser.Parse(args.ToArray());

                _logger.Info($"result = {result}");

                var inputValue = (List<string>)result.Params["--input"];

#if DEBUG
                _logger.Info($"inputValue = {JsonConvert.SerializeObject(inputValue, Formatting.Indented)}");
#endif
            }

            _logger.Info("End");
        }

        private void TstValueList_Case1()
        {
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
                    Kind = KindOfCommandLineArgument.List,
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
            });

            {
                var args = new List<string>()
                {
                    "--input",
                    "value1",
                    "value2",
                    "--output",
                    "s"
                };

                var result = parser.Parse(args.ToArray());

                _logger.Info($"result = {result}");

                var inputValue = (List<string>)result.Params["--input"];

#if DEBUG
                _logger.Info($"inputValue = {JsonConvert.SerializeObject(inputValue, Formatting.Indented)}");
#endif
            }

            _logger.Info("End");
        }

        private void TstPositionedSingleValueCase1()
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
            });

            //{
            //    var args = new List<string>();

            //    var result = parser.Parse(args.ToArray());

            //    _logger.Info($"result = {result}");
            //}

            {
                var commandLineStr = @"new Elf";

                _logger.Info($"commandLineStr = {commandLineStr}");

                var args = commandLineStr.Split(' ').ToList();

                var result = parser.Parse(args.ToArray());

                _logger.Info($"result = {result}");
            }

            _logger.Info("End");
        }

        private void OnePositionedSingleValueCase()
        {
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
                var commandLineStr = @"c:\Users\Acer\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_14_58_31\";

                _logger.Info($"commandLineStr = {commandLineStr}");

                var args = commandLineStr.Split(' ').ToList();

                var result = parser.Parse(args.ToArray());

                _logger.Info($"result = {result}");
            }

            _logger.Info("End");
        }

        private void TwoNamedSingleValueCase()
        {
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
            });

            //{
            //    var args = new List<string>();

            //    var result = parser.Parse(args.ToArray());

            //    _logger.Info($"result = {result}");
            //}

            {
                var commandLineStr = @"--i c:\Users\Acer\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_14_58_31\ --o c:\Users\Acer\source\repos\SymOntoClay\TestSandbox\bin\Debug\net7.0\MessagesLogsOutputDir\";

                _logger.Info($"commandLineStr = {commandLineStr}");

                var args = commandLineStr.Split(' ').ToList();

                var result = parser.Parse(args.ToArray());

                _logger.Info($"result = {result}");
            }

            _logger.Info("End");
        }

        private void OneNamedSingleValueCase()
        {
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
                var commandLineStr = @"--i c:\Users\Acer\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_14_58_31\";

                _logger.Info($"commandLineStr = {commandLineStr}");

                var args = commandLineStr.Split(' ').ToList();

                var result = parser.Parse(args.ToArray());

                _logger.Info($"result = {result}");
            }

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
