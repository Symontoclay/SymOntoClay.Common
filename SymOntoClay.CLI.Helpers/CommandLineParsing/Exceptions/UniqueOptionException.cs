namespace SymOntoClay.CLI.Helpers.CommandLineParsing.Exceptions
{
    public class UniqueOptionException : Exception
    {
        public UniqueOptionException(string message)
            : base(message)
        {
        }
    }
}
