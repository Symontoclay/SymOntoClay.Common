namespace SymOntoClay.CLI.Helpers.CommandLineParsing
{
    public enum KindOfCommandLineArgument
    {
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
