namespace SymOntoClay.CLI.Helpers.CommandLineParsing.Exceptions
{
    public class DuplicatedOptionException : CommandLineParsingException
    {
        public DuplicatedOptionException(string message) 
            : base(message)
        {
        }
    }
}
