using SymOntoClay.CLI.Helpers.CommandLineParsing.Options;

namespace SymOntoClay.CLI.Helpers.CommandLineParsing.Helpers
{
    public static class KindOfCommandLineArgumentHelper
    {
        public static bool CanBeUsedIfCommandLineIsEmpty(KindOfCommandLineArgument kind)
        {
            return kind == KindOfCommandLineArgument.Flag || kind == KindOfCommandLineArgument.NamedGroup;
        }
    }
}
