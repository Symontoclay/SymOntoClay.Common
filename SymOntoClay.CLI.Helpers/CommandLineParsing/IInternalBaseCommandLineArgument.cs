using SymOntoClay.Common;

namespace SymOntoClay.CLI.Helpers.CommandLineParsing
{
    public interface IInternalBaseCommandLineArgument: IObjectToString
    {
        KindOfCommandLineArgument Kind { get; }
        string Target { get; }      
        string Name { get; }
        IReadOnlyList<string> Aliases { get; }
        IReadOnlyList<string> Names { get; }
        uint? Index { get; }
        IReadOnlyList<string> Requires { get; }
        bool IsRequired { get; }
        bool UseIfCommandLineIsEmpty { get; }
        bool IsUnique { get; }
        bool IsDefault { get; }
        IReadOnlyList<IInternalBaseCommandLineArgument> SubItems { get; }
    }
}
