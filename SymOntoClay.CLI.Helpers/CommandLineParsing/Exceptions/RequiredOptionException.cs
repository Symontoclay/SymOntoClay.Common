namespace SymOntoClay.CLI.Helpers.CommandLineParsing.Exceptions
{
    public class RequiredOptionException : Exception
    {
        public RequiredOptionException(string message)
            : base(message)
        {
        }
    }
}
