namespace SymOntoClay.CLI.Helpers.CommandLineParsing.Exceptions
{
    public class DuplicatedMutuallyExclusiveOptionsSetException : Exception
    {
        public DuplicatedMutuallyExclusiveOptionsSetException(string message)
             : base(message)
        { 
        }
    }
}
