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
    }
}
