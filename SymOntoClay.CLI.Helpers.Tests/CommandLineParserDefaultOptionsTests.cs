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
using SymOntoClay.CLI.Helpers.CommandLineParsing.Exceptions;
using SymOntoClay.CLI.Helpers.CommandLineParsing.Options;

namespace SymOntoClay.CLI.Helpers.Tests
{
    public class CommandLineParserDefaultOptionsTests
    {
        [Test]
        public void OneDefaultOption_EmptyCommandLine_Success()
        {
            var args = new List<string>();
            var parser = new CommandLineParser(GetOneDefaultOption());

            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(0));

            Assert.That(result.Params.Count, Is.EqualTo(1));

            Assert.That(result.Params.ContainsKey("help"), Is.EqualTo(true));
            Assert.That(result.Params["help"], Is.EqualTo(true));
        }

        [Test]
        public void NoDefaultOptions_EmptyCommandLine_Success()
        {
            var args = new List<string>();
            var parser = new CommandLineParser(GetNoDefaultOptions());

            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(0));

            Assert.That(result.Params.Count, Is.EqualTo(0));
        }

        [Test]
        public void TwoDefaultOptions_EmptyCommandLine_Fail()
        {
            var args = new List<string>();

            var exception = Assert.Catch<DefaultOptionException>(() => { var parser = new CommandLineParser(GetTwoDefaultOptions());
                parser.Parse(args.ToArray()); });

            Assert.That(exception.Message, Is.EqualTo("Too many options set as default: 'help', 'run'. There must be only one default option."));
        }

        [Test]
        public void TwoDefaultOptions_EmptyCommandLine_ErrorsList()
        {
            var args = new List<string>();

            var parser = new CommandLineParser(GetTwoDefaultOptions(), true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors[0], Is.EqualTo("Too many options set as default: 'help', 'run'. There must be only one default option."));
        }

        [Test]
        public void OneWrongDefaultOption_EmptyCommandLine_Fail()
        {
            var args = new List<string>();

            var exception = Assert.Catch<DefaultOptionException>(() => {
                var parser = new CommandLineParser(GetOneWrongDefaultOption());
                parser.Parse(args.ToArray());
            });

            Assert.That(exception.Message, Is.EqualTo("SingleValue must not be used as default option."));
        }

        [Test]
        public void OneWrongDefaultOption_EmptyCommandLine_ErrorsList()
        {
            var args = new List<string>();

            var parser = new CommandLineParser(GetOneWrongDefaultOption(), true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors[0], Is.EqualTo("SingleValue must not be used as default option."));
        }

        private List<BaseCommandLineArgument> GetOneDefaultOption()
        {
            return new List<BaseCommandLineArgument>()
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
                    Kind = KindOfCommandLineArgument.Flag
                }
            };
        }

        private List<BaseCommandLineArgument> GetNoDefaultOptions()
        {
            return new List<BaseCommandLineArgument>()
            {
                new CommandLineArgument()
                {
                    Name = "help",
                    Aliases = new List<string>
                    {
                        "h"
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
            };
        }

        private List<BaseCommandLineArgument> GetTwoDefaultOptions()
        {
            return new List<BaseCommandLineArgument>()
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
            };
        }

        private List<BaseCommandLineArgument> GetOneWrongDefaultOption()
        {
            return new List<BaseCommandLineArgument>()
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
            };
        }
    }
}
