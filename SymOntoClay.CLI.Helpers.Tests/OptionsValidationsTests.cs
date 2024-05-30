using SymOntoClay.CLI.Helpers.CommandLineParsing.Exceptions;
using SymOntoClay.CLI.Helpers.CommandLineParsing;
using SymOntoClay.CLI.Helpers.CommandLineParsing.Options;

namespace SymOntoClay.CLI.Helpers.Tests
{
    public class OptionsValidationsTests
    {
        [Test]
        public void NullInsteadOfOptionsList_Fail()
        {
            var exception = Assert.Catch<ArgumentNullException>(() => {
                var parser = new CommandLineParser(null);
            });

            Assert.That(exception.Message, Is.EqualTo("Value cannot be null. (Parameter 'commandLineArguments')"));
        }

        [Test]
        public void NullInsteadOfOptionsList_ErrorsList()
        {
            var args = new List<string>();

            var parser = new CommandLineParser(null, true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors[0], Is.EqualTo("Value cannot be null. (Parameter 'commandLineArguments')"));
        }

        [Test]
        public void EmptyOptionsList_Fail()
        {
            var exception = Assert.Catch<OptionsValidationException>(() => {
                var parser = new CommandLineParser(new List<BaseCommandLineArgument>());
            });

            Assert.That(exception.Message, Is.EqualTo("There must be at least one option."));
        }

        [Test]
        public void EmptyOptionsList_ErrorsList()
        {
            var args = new List<string>();

            var parser = new CommandLineParser(new List<BaseCommandLineArgument>(), true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors[0], Is.EqualTo("There must be at least one option."));
        }

        [Test]
        public void EmptyCommandLineGroup_Fail()
        {
            var exception = Assert.Catch<OptionsValidationException>(() => {
                var parser = new CommandLineParser(EmptyCommandLineGroup());
            });

            Assert.That(exception.Message, Is.EqualTo("CommandLineGroup must have subitems."));
        }

        [Test]
        public void EmptyCommandLineGroup_ErrorsList()
        {
            var args = new List<string>();

            var parser = new CommandLineParser(EmptyCommandLineGroup(), true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors[0], Is.EqualTo("CommandLineGroup must have subitems."));
        }

        [Test]
        public void RequiredCommandLineGroup_Fail()
        {
            var exception = Assert.Catch<OptionsValidationException>(() => {
                var parser = new CommandLineParser(RequiredCommandLineGroup());
            });

            Assert.That(exception.Message, Is.EqualTo("CommandLineGroup must not be required."));
        }

        [Test]
        public void RequiredCommandLineGroup_ErrorsList()
        {
            var args = new List<string>();

            var parser = new CommandLineParser(RequiredCommandLineGroup(), true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors[0], Is.EqualTo("CommandLineGroup must not be required."));
        }

        [Test]
        public void EmptyMutuallyExclusiveSet_Fail()
        {
            var exception = Assert.Catch<OptionsValidationException>(() => {
                var parser = new CommandLineParser(EmptyMutuallyExclusiveSet());
            });

            Assert.That(exception.Message, Is.EqualTo("CommandLineMutuallyExclusiveSet must have subitems."));
        }

        [Test]
        public void EmptyMutuallyExclusiveSet_ErrorsList()
        {
            var args = new List<string>();

            var parser = new CommandLineParser(EmptyMutuallyExclusiveSet(), true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors[0], Is.EqualTo("CommandLineMutuallyExclusiveSet must have subitems."));
        }

        [Test]
        public void EmptyCommandLineNamedGroup_Fail()
        {
            var exception = Assert.Catch<OptionsValidationException>(() => {
                var parser = new CommandLineParser(EmptyCommandLineNamedGroup());
            });

            Assert.That(exception.Message, Is.EqualTo("CommandLineNamedGroup must have subitems."));
        }

        [Test]
        public void EmptyCommandLineNamedGroup_ErrorsList()
        {
            var args = new List<string>();

            var parser = new CommandLineParser(EmptyCommandLineNamedGroup(), true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors[0], Is.EqualTo("CommandLineNamedGroup must have subitems."));
        }

        [Test]
        public void CommandLineNamedGroupWithoutNameAndAliases_Fail()
        {
            var exception = Assert.Catch<OptionsValidationException>(() => {
                var parser = new CommandLineParser(CommandLineNamedGroupWithoutNameAndAliases());
            });

            Assert.That(exception.Message, Is.EqualTo("CommandLineNamedGroup must have Name."));
        }

        [Test]
        public void CommandLineNamedGroupWithoutNameAndAliases_ErrorsList()
        {
            var args = new List<string>();

            var parser = new CommandLineParser(CommandLineNamedGroupWithoutNameAndAliases(), true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors[0], Is.EqualTo("CommandLineNamedGroup must have Name."));
        }

        [Test]
        public void CommandLineNamedGroupWithoutNameButWithAliases_Fail()
        {
            var exception = Assert.Catch<OptionsValidationException>(() => {
                var parser = new CommandLineParser(CommandLineNamedGroupWithoutNameButWithAliases());
            });

            Assert.That(exception.Message, Is.EqualTo("CommandLineNamedGroup must have Name."));
        }

        [Test]
        public void CommandLineNamedGroupWithoutNameButWithAliases_ErrorsList()
        {
            var args = new List<string>();

            var parser = new CommandLineParser(CommandLineNamedGroupWithoutNameButWithAliases(), true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors[0], Is.EqualTo("CommandLineNamedGroup must have Name."));
        }

        [Test]
        public void CommandLineArgumentWithoutNameAndAliasesAndIndexes_Fail()
        {
            var exception = Assert.Catch<OptionsValidationException>(() => {
                var parser = new CommandLineParser(CommandLineArgumentWithoutNameAndAliasesAndIndexes());
            });

            Assert.That(exception.Message, Is.EqualTo("CommandLineArgument must have either Name or Index."));
        }

        [Test]
        public void CommandLineArgumentWithoutNameAndAliasesAndIndexes_ErrorsList()
        {
            var args = new List<string>();

            var parser = new CommandLineParser(CommandLineArgumentWithoutNameAndAliasesAndIndexes(), true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors[0], Is.EqualTo("CommandLineArgument must have either Name or Index."));
        }

        [Test]
        public void CommandLineArgumentWithoutNameAndIndexesButWithAliases_Fail()
        {
            var exception = Assert.Catch<OptionsValidationException>(() => {
                var parser = new CommandLineParser(CommandLineArgumentWithoutNameAndIndexesButWithAliases());
            });

            Assert.That(exception.Message, Is.EqualTo("CommandLineArgument must have either Name or Index."));
        }

        [Test]
        public void CommandLineArgumentWithoutNameAndIndexesButWithAliases_ErrorsList()
        {
            var args = new List<string>();

            var parser = new CommandLineParser(CommandLineArgumentWithoutNameAndIndexesButWithAliases(), true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors[0], Is.EqualTo("CommandLineArgument must have either Name or Index."));
        }

        private List<BaseCommandLineArgument> EmptyCommandLineGroup()
        {
            return new List<BaseCommandLineArgument>()
                {
                    new CommandLineGroup()
                    {
                    }
                };
        }

        private List<BaseCommandLineArgument> RequiredCommandLineGroup()
        {
            return new List<BaseCommandLineArgument>()
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
            };
        }

        private List<BaseCommandLineArgument> EmptyMutuallyExclusiveSet()
        {
            return new List<BaseCommandLineArgument>()
                {
                    new CommandLineMutuallyExclusiveSet()
                    {
                    }
                };
        }

        private List<BaseCommandLineArgument> EmptyCommandLineNamedGroup()
        {
            return new List<BaseCommandLineArgument>()
                {
                    new CommandLineNamedGroup()
                    {
                        Name = "new",
                        Aliases = new List<string>
                        {
                            "n"
                        }
                    }
                };
        }

        private List<BaseCommandLineArgument> CommandLineNamedGroupWithoutNameAndAliases()
        {
            return new List<BaseCommandLineArgument>()
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
                };
        }

        private List<BaseCommandLineArgument> CommandLineNamedGroupWithoutNameButWithAliases()
        {
            return new List<BaseCommandLineArgument>()
                {
                    new CommandLineNamedGroup()
                    {
                        Aliases = new List<string>
                        {
                            "n"
                        },
                        SubItems = new List<BaseCommandLineArgument>
                        {
                            new CommandLineArgument()
                            {
                                Name = "-thing",
                                Kind = KindOfCommandLineArgument.SingleValue
                            }
                        }
                    }
                };
        }

        private List<BaseCommandLineArgument> CommandLineArgumentWithoutNameAndAliasesAndIndexes()
        {
            return new List<BaseCommandLineArgument>()
            {
                new CommandLineArgument()
                {
                }
            };
        }

        private List<BaseCommandLineArgument> CommandLineArgumentWithoutNameAndIndexesButWithAliases()
        {
            return new List<BaseCommandLineArgument>()
                {
                    new CommandLineArgument()
                    {
                        Aliases = new List<string>
                        {
                            "n"
                        }
                    }
                };
        }
    }
}
