namespace SymOntoClay.CLI.Helpers.CommandLineParsing
{
    public enum KindOfCommandLineArgument
    {
        NamedGroup,
        MutuallyExclusiveSet,
        Flag,
        SingleValue,
        FlagOrSingleValue,
        List,
        SingleValueOrList
    }
}
