﻿using SymOntoClay.CLI.Helpers.CommandLineParsing;
using SymOntoClay.CLI.Helpers.CommandLineParsing.Exceptions;
using SymOntoClay.CLI.Helpers.CommandLineParsing.Options;

namespace SymOntoClay.CLI.Helpers.Tests
{
    public class CommandLineParserDefaultOptionsTest
    {
        [Test]
        public void OneDefaultOption_EmptyCommandLine_Success()
        {
            throw new NotImplementedException();
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
