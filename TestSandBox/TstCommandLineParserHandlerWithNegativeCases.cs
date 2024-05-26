using NLog;
using SymOntoClay.CLI.Helpers.CommandLineParsing;
using SymOntoClay.CLI.Helpers.CommandLineParsing.Options;
using SymOntoClay.CLI.Helpers.CommandLineParsing.Options.TypeCheckers;
using SymOntoClay.CLI.Helpers.Tests;

namespace TestSandBox
{
    public class TstCommandLineParserHandlerWithNegativeCases
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public void Run()
        {
            _logger.Info("Begin");

            EnumChecker_NonexistentEnum();//It should be covered
            //DuplicatedUniqueOption();
            //HasOptionalValueButDoesNotHaveRequired_CommandLineNamedGroup();
            //HasOptionalValueButDoesNotHaveRequired_CommandLineArgument();
            //FlagInsteadOfListInTheMiddle_a();
            //FlagInsteadOfListInTheMiddle();
            //FlagInsteadOfListInTheEnd();
            //ExtraValue_Case3_a_1();
            //ExtraValue_Case3_1();
            //ExtraValue_Case3_a();
            //ExtraValue_Case3();
            //ExtraValue_Case2_a_1();
            //ExtraValue_Case2_1();
            //ExtraValue_Case2_a();
            //ExtraValue_Case2();
            //ExtraValue_Case1_a_1();
            //ExtraValue_Case1_1();
            //ExtraValue_Case1_a();
            //ExtraValue_Case1();
            //FlagInsteadOfSingleValue_Case3();
            //FlagInsteadOfSingleValue_Case2_a();
            //FlagInsteadOfSingleValue_Case2();
            //FlagInsteadOfSingleValue_a();
            //FlagInsteadOfSingleValue();
            //Case6();
            //Case5_a();
            //Case5();
            //Case4_a();
            //Case4();
            //Case3_a();
            //Case3();
            //Case2_a();
            //Case2();
            //Case1();

            _logger.Info("End");
        }

        private void EnumChecker_NonexistentEnum()//It should be covered
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
                "Cat"
            };

            var result = parser.Parse(args.ToArray());

            _logger.Info($"result = {result}");

            var targetFramework = (TestEnum)result.Params["TargetFramework"];

            _logger.Info($"targetFramework = {targetFramework}");

            _logger.Info("End");
        }

        private void DuplicatedUniqueOption()
        {
            _logger.Info("Begin");

            try
            {
                var parser = new CommandLineParser(new List<BaseCommandLineArgument>()
                {
                    new CommandLineArgument()
                    {
                        Name = "-nologo",
                        Kind = KindOfCommandLineArgument.Flag,
                        IsUnique = true
                    }
                });

                var args = new List<string>()
                {
                    "-nologo",
                    "-nologo"
                };

                var result = parser.Parse(args.ToArray());

                _logger.Info($"result = {result}");
            }
            catch (Exception ex)
            {
                _logger.Info($"ex.Message = '{ex.Message}'");
                _logger.Info($"ex = {ex}");
            }

            _logger.Info("End");
        }

        private void HasOptionalValueButDoesNotHaveRequired_CommandLineNamedGroup()
        {
            _logger.Info("Begin");

            try
            {
                var parser = new CommandLineParser(new List<BaseCommandLineArgument>()
                {
                    new CommandLineNamedGroup()
                    {
                        Name = "new",
                        Aliases = new List<string>
                        {
                            "n"
                        },
                        IsRequired = true,
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
                                        Kind = KindOfCommandLineArgument.SingleValue
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
                        Name = "-nologo",
                        Kind = KindOfCommandLineArgument.Flag
                    }
                });

                var args = new List<string>()
                {
                    "-nologo"
                };

                var result = parser.Parse(args.ToArray());

                _logger.Info($"result = {result}");
            }
            catch (Exception ex)
            {
                _logger.Info($"ex.Message = '{ex.Message}'");
                _logger.Info($"ex = {ex}");
            }

            _logger.Info("End");
        }

        private void HasOptionalValueButDoesNotHaveRequired_CommandLineArgument()
        {
            _logger.Info("Begin");

            try
            {
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
                        IsRequired = true
                    },
                    new CommandLineArgument()
                    {
                        Name = "-nologo",
                        Kind = KindOfCommandLineArgument.Flag
                    }
                });

                var args = new List<string>()
                {
                    "-nologo"
                };

                var result = parser.Parse(args.ToArray());

                _logger.Info($"result = {result}");
            }
            catch (Exception ex)
            {
                _logger.Info($"ex.Message = '{ex.Message}'");
                _logger.Info($"ex = {ex}");
            }

            _logger.Info("End");
        }

        private void FlagInsteadOfListInTheMiddle_a()
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
                }, true);

            var args = new List<string>()
                {
                    "--input",
                    "--output",
                    "someValue"
                };

            var result = parser.Parse(args.ToArray());

            _logger.Info($"result = {result}");

            _logger.Info("End");
        }

        private void FlagInsteadOfListInTheMiddle()
        {
            _logger.Info("Begin");

            try
            {
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

                var args = new List<string>()
                {
                    "--input",
                    "--output",
                    "someValue"
                };

                var result = parser.Parse(args.ToArray());

                _logger.Info($"result = {result}");
            }
            catch (Exception ex)
            {
                _logger.Info($"ex.Message = '{ex.Message}'");
                _logger.Info($"ex = {ex}");
            }

            _logger.Info("End");
        }

        private void FlagInsteadOfListInTheEnd()
        {
            _logger.Info("Begin");

            try
            {
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
                    }
                });

                var args = new List<string>()
                {
                    "--input"
                };

                var result = parser.Parse(args.ToArray());

                _logger.Info($"result = {result}");
            }
            catch (Exception ex)
            {
                _logger.Info($"ex.Message = '{ex.Message}'");
                _logger.Info($"ex = {ex}");
            }

            _logger.Info("End");
        }

        private void ExtraValue_Case3_a_1()
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
                }, true);

            var args = new List<string>()
                {
                    "--input",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_14_58_31\",
                    "--o",
                    @"c:\Users\SomeUser\source\repos\SymOntoClay\TestSandbox\bin\Debug\net7.0\MessagesLogsOutputDir\",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_11_14_58_31\"
                };

            var result = parser.Parse(args.ToArray());

            _logger.Info($"result = {result}");

            _logger.Info("End");
        }

        private void ExtraValue_Case3_1()
        {
            _logger.Info("Begin");

            try
            {
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

                var args = new List<string>()
                {
                    "--input",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_14_58_31\",
                    "--o",
                    @"c:\Users\SomeUser\source\repos\SymOntoClay\TestSandbox\bin\Debug\net7.0\MessagesLogsOutputDir\",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_11_14_58_31\",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\58_31\"
                };

                var result = parser.Parse(args.ToArray());

                _logger.Info($"result = {result}");
            }
            catch (Exception ex)
            {
                _logger.Info($"ex.Message = '{ex.Message}'");
                _logger.Info($"ex = {ex}");
            }

            _logger.Info("End");
        }

        private void ExtraValue_Case3_a()
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
                }, true);

            var args = new List<string>()
                {
                    "--input",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_14_58_31\",
                    "--o",
                    @"c:\Users\SomeUser\source\repos\SymOntoClay\TestSandbox\bin\Debug\net7.0\MessagesLogsOutputDir\",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_11_14_58_31\",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\58_31\"
                };

            var result = parser.Parse(args.ToArray());

            _logger.Info($"result = {result}");

            _logger.Info("End");
        }

        private void ExtraValue_Case3()
        {
            _logger.Info("Begin");

            try
            {
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

                var args = new List<string>()
                {
                    "--input",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_14_58_31\",
                    "--o",
                    @"c:\Users\SomeUser\source\repos\SymOntoClay\TestSandbox\bin\Debug\net7.0\MessagesLogsOutputDir\",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_11_14_58_31\"
                };

                var result = parser.Parse(args.ToArray());

                _logger.Info($"result = {result}");
            }
            catch (Exception ex)
            {
                _logger.Info($"ex.Message = '{ex.Message}'");
                _logger.Info($"ex = {ex}");
            }

            _logger.Info("End");
        }

        private void ExtraValue_Case2_a_1()
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
                }, true);

            var args = new List<string>()
                {
                    "--input",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_14_58_31\",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_11_14_58_31\",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\58_31\",
                    "--o",
                    @"c:\Users\SomeUser\source\repos\SymOntoClay\TestSandbox\bin\Debug\net7.0\MessagesLogsOutputDir\"
                };

            var result = parser.Parse(args.ToArray());

            _logger.Info($"result = {result}");

            _logger.Info("End");
        }

        private void ExtraValue_Case2_1()
        {
            _logger.Info("Begin");

            try
            {
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

                var args = new List<string>()
                {
                    "--input",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_14_58_31\",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_11_14_58_31\",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\58_31\",
                    "--o",
                    @"c:\Users\SomeUser\source\repos\SymOntoClay\TestSandbox\bin\Debug\net7.0\MessagesLogsOutputDir\"
                };

                var result = parser.Parse(args.ToArray());

                _logger.Info($"result = {result}");
            }
            catch (Exception ex)
            {
                _logger.Info($"ex.Message = '{ex.Message}'");
                _logger.Info($"ex = {ex}");
            }

            _logger.Info("End");
        }

        private void ExtraValue_Case2_a()
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
                }, true);

            var args = new List<string>()
                {
                    "--input",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_14_58_31\",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_11_14_58_31\",
                    "--o",
                    @"c:\Users\SomeUser\source\repos\SymOntoClay\TestSandbox\bin\Debug\net7.0\MessagesLogsOutputDir\"
                };

            var result = parser.Parse(args.ToArray());

            _logger.Info($"result = {result}");

            _logger.Info("End");
        }

        private void ExtraValue_Case2()
        {
            _logger.Info("Begin");

            try
            {
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

                var args = new List<string>()
                {
                    "--input",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_14_58_31\",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_11_14_58_31\",
                    "--o",
                    @"c:\Users\SomeUser\source\repos\SymOntoClay\TestSandbox\bin\Debug\net7.0\MessagesLogsOutputDir\"
                };

                var result = parser.Parse(args.ToArray());

                _logger.Info($"result = {result}");
            }
            catch (Exception ex)
            {
                _logger.Info($"ex.Message = '{ex.Message}'");
                _logger.Info($"ex = {ex}");
            }

            _logger.Info("End");
        }

        private void ExtraValue_Case1_a_1()
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
                }, true);

            var args = new List<string>()
                {
                    "--input",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_14_58_31\",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_11_14_58_31\",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\58_31\"
                };

            var result = parser.Parse(args.ToArray());

            _logger.Info($"result = {result}");

            _logger.Info("End");
        }

        private void ExtraValue_Case1_1()
        {
            _logger.Info("Begin");

            try
            {
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

                var args = new List<string>()
                {
                    "--input",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_14_58_31\",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_11_14_58_31\",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\58_31\"
                };

                var result = parser.Parse(args.ToArray());

                _logger.Info($"result = {result}");
            }
            catch (Exception ex)
            {
                _logger.Info($"ex.Message = '{ex.Message}'");
                _logger.Info($"ex = {ex}");
            }

            _logger.Info("End");
        }

        private void ExtraValue_Case1_a()
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
                }, true);

            var args = new List<string>()
                {
                    "--input",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_14_58_31\",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_11_14_58_31\"
                };

            var result = parser.Parse(args.ToArray());

            _logger.Info($"result = {result}");

            _logger.Info("End");
        }

        private void ExtraValue_Case1()
        {
            _logger.Info("Begin");

            try
            {
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

                var args = new List<string>()
                {
                    "--input",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_14_58_31\",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_11_14_58_31\"
                };

                var result = parser.Parse(args.ToArray());

                _logger.Info($"result = {result}");
            }
            catch (Exception ex)
            {
                _logger.Info($"ex.Message = '{ex.Message}'");
                _logger.Info($"ex = {ex}");
            }

            _logger.Info("End");
        }

        private void FlagInsteadOfSingleValue_Case3()
        {
            _logger.Info("Begin");

            try
            {
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

                var args = new List<string>()
                {
                    "--input",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_14_58_31\",
                    "--o"                    
                };

                var result = parser.Parse(args.ToArray());

                _logger.Info($"result = {result}");
            }
            catch (Exception ex)
            {
                _logger.Info($"ex.Message = '{ex.Message}'");
                _logger.Info($"ex = {ex}");
            }

            _logger.Info("End");
        }


        private void FlagInsteadOfSingleValue_Case2_a()
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
                }, true);

            var args = new List<string>()
                {
                    "--input",
                    "--o",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_14_58_31\"
                };

            var result = parser.Parse(args.ToArray());

            _logger.Info($"result = {result}");

            _logger.Info("End");
        }

        private void FlagInsteadOfSingleValue_Case2()
        {
            _logger.Info("Begin");

            try
            {
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

                var args = new List<string>()
                {
                    "--input",
                    "--o",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_14_58_31\"
                };

                var result = parser.Parse(args.ToArray());

                _logger.Info($"result = {result}");
            }
            catch (Exception ex)
            {
                _logger.Info($"ex.Message = '{ex.Message}'");
                _logger.Info($"ex = {ex}");
            }

            _logger.Info("End");
        }

        private void FlagInsteadOfSingleValue_a()
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
                }, true);

            var args = new List<string>()
                {
                    "--input"
                };

            var result = parser.Parse(args.ToArray());

            _logger.Info($"result = {result}");

            _logger.Info("End");
        }

        private void FlagInsteadOfSingleValue()
        {
            _logger.Info("Begin");

            try
            {
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

                var args = new List<string>()
                {
                    "--input"
                };

                var result = parser.Parse(args.ToArray());

                _logger.Info($"result = {result}");
            }
            catch (Exception ex)
            {
                _logger.Info($"ex.Message = '{ex.Message}'");
                _logger.Info($"ex = {ex}");
            }

            _logger.Info("End");
        }

        private void Case6()
        {
            _logger.Info("Begin");

            try
            {
                var parser = new CommandLineParser(new List<BaseCommandLineArgument>()
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
                });

                var args = new List<string>()
                {
                    "-nologo"
                };

                var result = parser.Parse(args.ToArray());

                _logger.Info($"result = {result}");
            }
            catch (Exception ex)
            {
                _logger.Info($"ex.Message = '{ex.Message}'");
            }

            _logger.Info("End");
        }

        private void Case5_a()
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

        private void Case5()
        {
            _logger.Info("Begin");

            try
            {
                var parser = new CommandLineParser(new List<BaseCommandLineArgument>()
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
                });

                var args = new List<string>();

                var result = parser.Parse(args.ToArray());

                _logger.Info($"result = {result}");
            }
            catch (Exception ex)
            {
                _logger.Info($"ex.Message = '{ex.Message}'");
            }

            _logger.Info("End");
        }

        private void Case4_a()
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

            var args = new List<string>()
                { "help", "run" };

            var result = parser.Parse(args.ToArray());

            _logger.Info($"result = {result}");

            _logger.Info("End");
        }

        private void Case4()
        {
            _logger.Info("Begin");

            try
            {
                var parser = new CommandLineParser(new List<BaseCommandLineArgument>()
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
                });

                var args = new List<string>()
                { "help", "run" };

                var result = parser.Parse(args.ToArray());

                _logger.Info($"result = {result}");
            }
            catch (Exception ex)
            {
                _logger.Info($"ex.Message = '{ex.Message}'");
            }

            _logger.Info("End");
        }

        private void Case3_a()
        {
            _logger.Info("Begin");

            var parser = new CommandLineParser(new List<BaseCommandLineArgument>()
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
                }, true);

            var args = new List<string>();

            var result = parser.Parse(args.ToArray());

            _logger.Info($"result = {result}");

            _logger.Info("End");
        }

        private void Case3()
        {
            _logger.Info("Begin");

            try
            {
                var parser = new CommandLineParser(new List<BaseCommandLineArgument>()
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
                });

                var args = new List<string>();

                var result = parser.Parse(args.ToArray());

                _logger.Info($"result = {result}");
            }
            catch (Exception ex)
            {
                _logger.Info($"ex.Message = '{ex.Message}'");
            }

            _logger.Info("End");
        }

        private void Case2_a()
        {
            _logger.Info("Begin");

            var parser = new CommandLineParser(new List<BaseCommandLineArgument>()
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
                }, true);

            var args = new List<string>();

            var result = parser.Parse(args.ToArray());

            _logger.Info($"result = {result}");

            _logger.Info("End");
        }

        private void Case2()
        {
            _logger.Info("Begin");

            try
            {
                var parser = new CommandLineParser(new List<BaseCommandLineArgument>()
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

                var args = new List<string>();

                var result = parser.Parse(args.ToArray());

                _logger.Info($"result = {result}");
            }
            catch (Exception ex)
            {
                _logger.Info($"ex.Message = '{ex.Message}'");
            }

            _logger.Info("End");
        }

        private void Case1()
        {
            _logger.Info("Begin");

            try
            {
                var parser = new CommandLineParser(new List<BaseCommandLineArgument>()
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

                var args = new List<string>();

                var result = parser.Parse(args.ToArray());

                _logger.Info($"result = {result}");
            }
            catch (Exception ex)
            {
                _logger.Info($"ex.Message = '{ex.Message}'");
            }

            _logger.Info("End");
        }
    }
}
