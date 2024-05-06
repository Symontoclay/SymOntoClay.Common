namespace SymOntoClay.CLI.Helpers.CommandLineParsing
{
    public abstract class BaseNamedCommandLineArgument : BaseCommandLineArgument
    {
        public string Name { get; set; }
        public List<string> Aliases { get; set; }
    }
}
