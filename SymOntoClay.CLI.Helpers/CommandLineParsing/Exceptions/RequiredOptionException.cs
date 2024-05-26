namespace SymOntoClay.CLI.Helpers.CommandLineParsing.Exceptions
{
    public class RequiredOptionException : CommandLineParsingException
    {
        public RequiredOptionException(string message)
            : base(message)
        {
        }
    }
}
