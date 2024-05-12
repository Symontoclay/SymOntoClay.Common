namespace SymOntoClay.CLI.Helpers.CommandLineParsing.Exceptions
{
    public class DuplicatedOptionException : Exception
    {
        public DuplicatedOptionException(string message) : base(message)
        {
        }
    }
}
