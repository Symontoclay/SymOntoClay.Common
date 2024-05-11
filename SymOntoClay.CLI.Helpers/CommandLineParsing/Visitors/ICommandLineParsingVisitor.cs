using SymOntoClay.CLI.Helpers.CommandLineParsing.Options;

namespace SymOntoClay.CLI.Helpers.CommandLineParsing.Visitors
{
    public interface ICommandLineParsingVisitor
    {
        void VisitCommandLineArgument(CommandLineArgument element);
        void VisitCommandLineGroup(CommandLineGroup element);
        void VisitCommandLineMutuallyExclusiveSet(CommandLineMutuallyExclusiveSet element);
        void VisitCommandLineNamedGroup(CommandLineNamedGroup element);
        void VisitCommandLineVirtualRootGroup(CommandLineVirtualRootGroup element);
    }
}
