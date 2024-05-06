namespace SymOntoClay.CLI.Helpers.CommandLineParsing
{
    public class CommandLineGroup : BaseCommandLineArgument
    {
        public List<BaseCommandLineArgument> SubItems { get; set; }
    }
}
