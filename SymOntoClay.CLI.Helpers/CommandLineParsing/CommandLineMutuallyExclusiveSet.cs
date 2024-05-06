namespace SymOntoClay.CLI.Helpers.CommandLineParsing
{
    public class CommandLineMutuallyExclusiveSet: BaseCommandLineArgument
    {
        public List<BaseCommandLineArgument> SubItems { get; set; }
    }
}
