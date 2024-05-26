namespace SymOntoClay.CLI.Helpers.CommandLineParsing.Exceptions
{
    public class DuplicatedMutuallyExclusiveOptionsSetException : CommandLineParsingException
    {
        public DuplicatedMutuallyExclusiveOptionsSetException(string message)
             : base(message)
        { 
        }
    }
}
