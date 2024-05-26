namespace SymOntoClay.CLI.Helpers.CommandLineParsing.Exceptions
{
    public class UniqueOptionException : CommandLineParsingException
    {
        public UniqueOptionException(string message)
            : base(message)
        {
        }
    }
}
