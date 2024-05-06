namespace SymOntoClay.CLI.Helpers.CommandLineParsing
{
    public class CommandLineArgument: BaseNamedCommandLineArgument
    {
        public KindOfCommandLineArgument Kind { get; set; }
        public uint? Index { get; set; }
    }
}
