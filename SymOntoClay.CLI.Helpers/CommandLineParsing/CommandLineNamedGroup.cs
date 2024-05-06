namespace SymOntoClay.CLI.Helpers.CommandLineParsing
{
    public class CommandLineNamedGroup: BaseNamedCommandLineArgument
    {
        public List<BaseCommandLineArgument> SubItems { get; set; }
    }
}
