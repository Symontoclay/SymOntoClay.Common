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

using NLog;
using SymOntoClay.CLI.Helpers.CommandLineParsing.Options;
using SymOntoClay.CLI.Helpers.CommandLineParsing;

namespace TestSandBox
{
    public class TstCommandLineParserRealAppHandler
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

            var args = new List<string>();

            var result = parser.Parse(args.ToArray());

            _logger.Info($"result = {result}");

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

            var args = new List<string>();

            var result = parser.Parse(args.ToArray());

            _logger.Info($"result = {result}");

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
                                new CommandLineArgument
                                {
                                    Name = "--output",
                                    Aliases = new List<string>
                                    {
                                        "--o"
                                    },
                                    Kind = KindOfCommandLineArgument.SingleValue,
                                    Index = 1
                                },
                                new CommandLineArgument
                                {
                                    Name = "--nologo",
                                    Kind = KindOfCommandLineArgument.Flag
                                },
                                new CommandLineArgument
                                {
                                    Name = "--target-nodeid",
                                    Kind = KindOfCommandLineArgument.SingleValue
                                },
                                new CommandLineArgument
                                {
                                    Name = "--target-threadid",
                                    Kind = KindOfCommandLineArgument.SingleValue
                                },
                                new CommandLineArgument
                                {
                                    Name = "--split-by-nodes",
                                    Kind = KindOfCommandLineArgument.Flag
                                },
                                new CommandLineArgument
                                {
                                    Name = "--split-by-threads",
                                    Kind = KindOfCommandLineArgument.Flag
                                },
                                new CommandLineArgument
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
                            }
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
                var commandLineStr = @"--i c:\Users\Acer\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_14_58_31\ --o c:\Users\Acer\source\repos\SymOntoClay\TestSandbox\bin\Debug\net7.0\MessagesLogsOutputDir\ --target-nodeid #DummyNPC --html --abs-url";

                _logger.Info($"commandLineStr = {commandLineStr}");

                var args = commandLineStr.Split(' ').ToList();

                var result = parser.Parse(args.ToArray());

                _logger.Info($"result = {result}");
            }

            _logger.Info("End");
        }
    }
}
