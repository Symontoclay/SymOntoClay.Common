﻿using NLog;
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

            MutuallyExclusiveSetWithGroup();
            //EmptyCommandLineWithRequiredParameters();
            //EmptyRequiredParameterInSettings();
            //AbsentRequiredParameterInSettings();
            //AbsentRequiredParameterInCommandline();
            //CommandLineArgumentWithoutNameAndIndexesButWithAliases_a();
            //CommandLineArgumentWithoutNameAndIndexesButWithAliases();
            //CommandLineArgumentWithoutNameAndAliasesAndIndexes();
            //CommandLineNamedGroupWithoutNameAndAliases();
            //EmptyCommandLineNamedGroup();
            //EmptyMutuallyExclusiveSet();
            //RequiredGroup();
            //EmptyGroup();
            //EmptyOptionsList();
            //NullInsteadOfOptionsList_a();
            //NullInsteadOfOptionsList();
            //VersionCheckerWithoutTypeCheckErrorMessage_WrongValue();
            //VersionCheckerWithTypeCheckErrorMessage_WrongValue();
            //IntCheckerWithoutTypeCheckErrorMessage_WrongValue();
            //IntCheckerWithTypeCheckErrorMessage_WrongValue();
            //EnumCheckerWithoutTypeCheckErrorMessage_NonexistentEnum();
            //EnumCheckerWithTypeCheckErrorMessage_NonexistentEnum();
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

        private void MutuallyExclusiveSetWithGroup()
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
                                Name = "--help",
                                Aliases = new List<string>
                                {
                                    "h",
                                    "-h",
                                    "--h",
                                    "-help",
                                    "help"
                                },
                                Kind = KindOfCommandLineArgument.Flag,
                                UseIfCommandLineIsEmpty = true
                            },
                            new CommandLineGroup()
                            {
                                SubItems = new List<BaseCommandLineArgument>
                                {
                                    new CommandLineArgument()
                                    {
                                        Name = "--input",
                                        Aliases = new List<string>()
                                        {
                                            "--i",
                                            "-input",
                                            "-i"
                                        },
                                        Kind = KindOfCommandLineArgument.SingleValue,
                                        Index = 0
                                    },
                                    new CommandLineArgument
                                    {
                                        Name = "--output",
                                        Aliases = new List<string>
                                        {
                                            "--o",
                                            "-output",
                                            "-o"
                                        },
                                        Kind = KindOfCommandLineArgument.SingleValue,
                                        Index = 1
                                    },
                                    new CommandLineArgument
                                    {
                                        Name = "--nologo",
                                        Aliases = new List<string>
                                        {
                                            "-nologo"
                                        },
                                        Kind = KindOfCommandLineArgument.Flag
                                    },
                                    new CommandLineArgument
                                    {
                                        Name = "--target-nodeid",
                                        Aliases = new List<string>
                                        {
                                            "-target-nodeid"
                                        },
                                        Kind = KindOfCommandLineArgument.SingleValue
                                    },
                                    new CommandLineArgument
                                    {
                                        Name = "--target-threadid",
                                        Aliases = new List<string>
                                        {
                                            "-target-threadid"
                                        },
                                        Kind = KindOfCommandLineArgument.SingleValue
                                    },
                                    new CommandLineArgument
                                    {
                                        Name = "--split-by-nodes",
                                        Aliases = new List<string>
                                        {
                                            "-split-by-nodes"
                                        },
                                        Kind = KindOfCommandLineArgument.Flag
                                    },
                                    new CommandLineArgument
                                    {
                                        Name = "--split-by-threads",
                                        Aliases = new List<string>
                                        {
                                            "-split-by-threads"
                                        },
                                        Kind = KindOfCommandLineArgument.Flag
                                    },
                                    new CommandLineArgument
                                    {
                                        Name = "--configuration",
                                        Aliases = new List<string>
                                        {
                                            "--c",
                                            "--cfg",
                                            "--config",
                                            "-configuration",
                                            "-c",
                                            "-cfg",
                                            "-config"
                                        },
                                        Kind = KindOfCommandLineArgument.SingleValue
                                    },
                                    new CommandLineArgument
                                    {
                                        Name = "--html",
                                        Aliases = new List<string>
                                        {
                                            "-html"
                                        },
                                        Kind = KindOfCommandLineArgument.Flag
                                    },
                                    new CommandLineArgument
                                    {
                                        Name = "--abs-url",
                                        Aliases = new List<string>
                                        {
                                            "-abs-url"
                                        },
                                        Kind = KindOfCommandLineArgument.Flag,
                                        Requires = new List<string>
                                        {
                                            "--html"
                                        }
                                    }
                                }
                            }
                        }
                    }
                }, true);

                var args = new List<string>()
                {
                    @"%USERPROFILE%\SomeInputDir\",
                    @"%USERPROFILE%\SomeOutputDir\",
                    "--help"
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

        private void EmptyCommandLineWithRequiredParameters()
        {
            _logger.Info("Begin");

            try
            {
                var parser = new CommandLineParser(new List<BaseCommandLineArgument>()
                {
                    new CommandLineArgument
                    {
                        Name = "TargetFramework",
                        Index = 0,
                        Kind = KindOfCommandLineArgument.SingleValue,
                        TypeChecker = new EnumChecker<TestEnum>(),
                        TypeCheckErrorMessage = "Unknown target framework",
                        IsRequired = true
                    },
                    new CommandLineArgument
                    {
                        Name = "TargetVersion",
                        Index = 1,
                        Kind = KindOfCommandLineArgument.SingleValue,
                        TypeChecker = new VersionChecker(),
                        TypeCheckErrorMessage = "Unknown version",
                        IsRequired = true
                    }
                }, true);

                var args = new List<string>();

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

        private void EmptyRequiredParameterInSettings()
        {
            _logger.Info("Begin");

            try
            {
                var parser = new CommandLineParser(new List<BaseCommandLineArgument>()
                {
                    new CommandLineArgument
                    {
                        Name = "--abs-url",
                        Kind = KindOfCommandLineArgument.Flag,
                        Requires = new List<string>
                        {
                            ""
                        }
                    }
                });

                var args = new List<string>()
                {
                    "--abs-url"
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

        private void AbsentRequiredParameterInSettings()
        {
            _logger.Info("Begin");

            try
            {
                var parser = new CommandLineParser(new List<BaseCommandLineArgument>()
                {
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
                    "--abs-url"
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

        private void AbsentRequiredParameterInCommandline()
        {
            _logger.Info("Begin");

            try
            {
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
                }, true);

                var args = new List<string>()
                {
                    "--abs-url"
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

        private void CommandLineArgumentWithoutNameAndIndexesButWithAliases_a()
        {
            _logger.Info("Begin");

            try
            {
                var parser = new CommandLineParser(new List<BaseCommandLineArgument>()
                {
                    new CommandLineArgument()
                    {
                        Aliases = new List<string>
                        {
                            "n"
                        }
                    }
                }, true);

                var args = new List<string>();

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

        private void CommandLineArgumentWithoutNameAndIndexesButWithAliases()
        {
            _logger.Info("Begin");

            try
            {
                var parser = new CommandLineParser(new List<BaseCommandLineArgument>()
                {
                    new CommandLineArgument()
                    {
                        Aliases = new List<string>
                        {
                            "n"
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
                _logger.Info($"ex = {ex}");
            }

            _logger.Info("End");
        }

        private void CommandLineArgumentWithoutNameAndAliasesAndIndexes()
        {
            _logger.Info("Begin");

            try
            {
                var parser = new CommandLineParser(new List<BaseCommandLineArgument>()
                {
                    new CommandLineArgument()
                    {
                    }
                });

                var args = new List<string>();

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

        private void CommandLineNamedGroupWithoutNameAndAliases()
        {
            _logger.Info("Begin");

            try
            {
                var parser = new CommandLineParser(new List<BaseCommandLineArgument>()
                {
                    new CommandLineNamedGroup()
                    {
                        SubItems = new List<BaseCommandLineArgument>
                        {
                            new CommandLineArgument()
                            {
                                Name = "-thing",
                                Kind = KindOfCommandLineArgument.SingleValue
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
                _logger.Info($"ex = {ex}");
            }

            _logger.Info("End");
        }

        private void EmptyCommandLineNamedGroup()
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
                _logger.Info($"ex = {ex}");
            }

            _logger.Info("End");
        }

        private void EmptyMutuallyExclusiveSet()
        {
            _logger.Info("Begin");

            try
            {
                var parser = new CommandLineParser(new List<BaseCommandLineArgument>()
                {
                    new CommandLineMutuallyExclusiveSet()
                    {
                    }
                });

                var args = new List<string>();

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

        private void RequiredGroup()
        {
            _logger.Info("Begin");

            try
            {
                var parser = new CommandLineParser(new List<BaseCommandLineArgument>()
                {
                    new CommandLineGroup()
                    {
                        IsRequired = true,
                        SubItems = new List<BaseCommandLineArgument>
                        {
                            new CommandLineArgument()
                            {
                                Name = "-nologo",
                                Kind = KindOfCommandLineArgument.Flag
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
                _logger.Info($"ex = {ex}");
            }

            _logger.Info("End");
        }

        private void EmptyGroup()
        {
            _logger.Info("Begin");

            try
            {
                var parser = new CommandLineParser(new List<BaseCommandLineArgument>()
                {
                    new CommandLineGroup()
                    {
                    }
                });

                var args = new List<string>();

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

        private void EmptyOptionsList()
        {
            _logger.Info("Begin");

            try
            {
                var parser = new CommandLineParser(new List<BaseCommandLineArgument>());

                var args = new List<string>();

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

        private void NullInsteadOfOptionsList_a()
        {
            _logger.Info("Begin");

            try
            {
                var parser = new CommandLineParser(null, true);

                var args = new List<string>();

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

        private void NullInsteadOfOptionsList()
        {
            _logger.Info("Begin");

            try
            {
                var parser = new CommandLineParser(null);

                //var args = new List<string>();

                //var result = parser.Parse(args.ToArray());

                //_logger.Info($"result = {result}");
            }
            catch (Exception ex)
            {
                _logger.Info($"ex.Message = '{ex.Message}'");
                _logger.Info($"ex = {ex}");
            }

            _logger.Info("End");
        }

        private void VersionCheckerWithoutTypeCheckErrorMessage_WrongValue()
        {
            _logger.Info("Begin");

            try
            {
                var parser = new CommandLineParser(new List<BaseCommandLineArgument>()
                {
                    new CommandLineArgument()
                    {
                        Target = "Version",
                        Kind = KindOfCommandLineArgument.SingleValue,
                        Index = 0,
                        TypeChecker = new VersionChecker()
                    }
                });

                var args = new List<string>()
                {
                    "Cat"
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

        private void VersionCheckerWithTypeCheckErrorMessage_WrongValue()
        {
            _logger.Info("Begin");

            try
            {
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
                    "Cat"
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

        private void IntCheckerWithoutTypeCheckErrorMessage_WrongValue()
        {
            _logger.Info("Begin");

            try
            {
                var parser = new CommandLineParser(new List<BaseCommandLineArgument>()
                {
                    new CommandLineArgument()
                    {
                        Target = "Port",
                        Kind = KindOfCommandLineArgument.SingleValue,
                        Index = 0,
                        TypeChecker = new IntChecker()
                    }
                });

                var args = new List<string>()
                {
                    "Cat"
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

        private void IntCheckerWithTypeCheckErrorMessage_WrongValue()
        {
            _logger.Info("Begin");

            try
            {
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
                    "Cat"
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

        private void EnumCheckerWithoutTypeCheckErrorMessage_NonexistentEnum()
        {
            _logger.Info("Begin");

            try
            {
                var parser = new CommandLineParser(new List<BaseCommandLineArgument>()
                {
                    new CommandLineArgument()
                    {
                        Target = "TargetFramework",
                        Kind = KindOfCommandLineArgument.SingleValue,
                        Index = 0,
                        TypeChecker = new EnumChecker<TestEnum>()
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
            }
            catch (Exception ex)
            {
                _logger.Info($"ex.Message = '{ex.Message}'");
                _logger.Info($"ex = {ex}");
            }

            _logger.Info("End");
        }

        private void EnumCheckerWithTypeCheckErrorMessage_NonexistentEnum()
        {
            _logger.Info("Begin");

            try
            {
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
            }
            catch (Exception ex)
            {
                _logger.Info($"ex.Message = '{ex.Message}'");
                _logger.Info($"ex = {ex}");
            }

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
