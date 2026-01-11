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
    public class CommandLineParserDuplicatedOptionsTests
    {
        [Test]
        public void NoDuplicatedOptions_EmptyCommandLine_Success()
        {
            var args = new List<string>();
            var parser = new CommandLineParser(GetNoDuplicatedOptions());

            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(0));

            Assert.That(result.Params.Count, Is.EqualTo(0));
        }

        [Test]
        public void DuplicatedOptions_EmptyCommandLine_Fail()
        {
            var args = new List<string>();

            var exception = Assert.Catch<DuplicatedOptionException>(() => {
                var parser = new CommandLineParser(GetDuplicatedOptions());
                parser.Parse(args.ToArray());
            });

            Assert.That(exception.Message == "Option 'run' is declared 2 times." || exception.Message == "Option 'r' is declared 2 times.", Is.EqualTo(true));
        }

        [Test]
        public void DuplicatedOptions_EmptyCommandLine_ErrorsList()
        {
            var args = new List<string>();

            var parser = new CommandLineParser(GetDuplicatedOptions(), true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(2));

            Assert.That(result.Errors.Contains("Option 'run' is declared 2 times."), Is.EqualTo(true));
            Assert.That(result.Errors.Contains("Option 'r' is declared 2 times."), Is.EqualTo(true));
        }

        private List<BaseCommandLineArgument> GetNoDuplicatedOptions()
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

        private List<BaseCommandLineArgument> GetDuplicatedOptions()
        {
            return new List<BaseCommandLineArgument>()
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
                };
        }
    }
}
