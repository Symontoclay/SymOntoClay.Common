namespace SymOntoClay.CLI.Helpers.CommandLineParsing.Exceptions
{
    public class OptionsValidationException : CommandLineParsingException
    {
        public OptionsValidationException(string message)
            : base(message)
        {
        }
    }
}
