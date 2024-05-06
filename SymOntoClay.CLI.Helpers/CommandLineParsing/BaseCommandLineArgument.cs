namespace SymOntoClay.CLI.Helpers.CommandLineParsing
{
    public abstract class BaseCommandLineArgument
    {
        public bool IsRequired { get; set; }
        public string Target { get; set; }
    }
}
