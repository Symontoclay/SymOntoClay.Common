using SymOntoClay.Common;

namespace SymOntoClay.CLI.Helpers.CommandLineParsing
{
    public interface IInternalBaseCommandLineArgument: IObjectToString
    {
        KindOfCommandLineArgument Kind { get; }
        IReadOnlyList<IInternalBaseCommandLineArgument> SubItems { get; }
    }
}
