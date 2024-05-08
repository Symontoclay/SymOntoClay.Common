namespace SymOntoClay.CLI.Helpers.CommandLineParsing
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
