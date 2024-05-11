namespace SymOntoClay.CLI.Helpers.CommandLineParsing.Options
{
    public enum KindOfCommandLineArgument
    {
        VirtualRootGroup,
        NamedGroup,
        Group,
        MutuallyExclusiveSet,
        Flag,
        SingleValue,
        FlagOrSingleValue,
        List,
        SingleValueOrList
    }
}
