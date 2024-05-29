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
            throw new NotImplementedException();
        }

        [Test]
        public void EmptyCommandLineNamedGroup_ErrorsList()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void CommandLineNamedGroupWithoutNameAndAliases_Fail()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void CommandLineNamedGroupWithoutNameAndAliases_ErrorsList()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void CommandLineNamedGroupWithoutNameButWithAliases_Fail()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void CommandLineNamedGroupWithoutNameButWithAliases_ErrorsList()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void CommandLineArgumentWithoutNameAndAliasesAndIndexes_Fail()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void CommandLineArgumentWithoutNameAndAliasesAndIndexes_ErrorsList()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void CommandLineArgumentWithoutNameAndIndexesButWithAliases_Fail()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void CommandLineArgumentWithoutNameAndIndexesButWithAliases_ErrorsList()
        {
            throw new NotImplementedException();
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
    }
}
