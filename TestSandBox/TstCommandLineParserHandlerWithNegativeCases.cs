﻿using NLog;
using SymOntoClay.CLI.Helpers.CommandLineParsing;
using SymOntoClay.CLI.Helpers.CommandLineParsing.Options;

namespace TestSandBox
{
    public class TstCommandLineParserHandlerWithNegativeCases
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public void Run()
        {
            _logger.Info("Begin");

            FlagInsteadOfSingleValue_Case2();//TODO: cover by tests
            FlagInsteadOfSingleValue();//TODO: cover by tests
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

        private void FlagInsteadOfSingleValue_Case2()//TODO: cover by tests
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
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_13_58_31\"
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

        //TODO: check also "--input" as flag with --output as second valid parameter
        private void FlagInsteadOfSingleValue()//TODO: cover by tests
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
