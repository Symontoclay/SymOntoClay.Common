﻿using NLog;
using SymOntoClay.CLI.Helpers.CommandLineParsing;

namespace TestSandBox
{
    public class TstCommandLineParserHandler
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public void Run()
        {
            _logger.Info("Begin");

            CaseSymOntoClayCLI();
            CaseUpdateInstalledNuGetPackagesInAllCSharpProjects();
            CaseLogFileBuilderApp();

            _logger.Info("End");
        }

        private void CaseSymOntoClayCLI()
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

            _logger.Info("End");
        }

        private void CaseUpdateInstalledNuGetPackagesInAllCSharpProjects()
        {
            _logger.Info("Begin");

            var parser = new CommandLineParser(new List<BaseCommandLineArgument>()
            {
                new CommandLineArgument()
                {
                    Target = "PackageId",
                    Kind = KindOfCommandLineArgument.SingleValue,
                    Index = 0
                },
                new CommandLineArgument()
                {
                    Target = "PackageVersion",
                    Kind = KindOfCommandLineArgument.SingleValue,
                    Index = 1
                }
            });

            _logger.Info("End");
        }

        private void CaseLogFileBuilderApp()
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
                        new CommandLineGroup()
                        {
                            SubItems = new List<BaseCommandLineArgument>
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
new CommandLineArgumentOptions
            {
                Name = "--output",
                Aliases = new List<string>
                {
                    "--o"
                },
                Kind = KindOfCommandLineArgument.SingleValue
            },
new CommandLineArgumentOptions
            {
                Name = "--nologo",
                Kind = KindOfCommandLineArgument.Flag
            },
new CommandLineArgumentOptions
            {
                Name = "--target-nodeid",
                Kind = KindOfCommandLineArgument.SingleValue
            },
new CommandLineArgumentOptions
            {
                Name = "--target-threadid",
                Kind = KindOfCommandLineArgument.SingleValue
            },
new CommandLineArgumentOptions
            {
                Name = "--split-by-nodes",
                Kind = KindOfCommandLineArgument.Flag
            },
new CommandLineArgumentOptions
            {
                Name = "--split-by-threads",
                Kind = KindOfCommandLineArgument.Flag
            },
new CommandLineArgumentOptions
            {
                Name = "--configuration",
                Aliases = new List<string>
                {
                    "--c",
                    "--cfg",
                    "--config"
                },
                Kind = KindOfCommandLineArgument.SingleValue
            },
new CommandLineArgumentOptions
            {
                Name = "--html",
                Kind = KindOfCommandLineArgument.Flag
            },
new CommandLineArgumentOptions
            {
                Name = "--abs-url",
                Kind = KindOfCommandLineArgument.Flag
            }
                            }
                        }
                    }
                }
            });

            _logger.Info("End");
        }
    }
}
