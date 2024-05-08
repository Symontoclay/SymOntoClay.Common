using SymOntoClay.Common.DebugHelpers;
using SymOntoClay.Common;
using System.Text;

namespace SymOntoClay.CLI.Helpers.CommandLineParsing
{
    public abstract class BaseCommandLineArgument: IInternalBaseCommandLineArgument
    {
        public bool IsRequired { get; set; }
        public string Target { get; set; }
        public List<string> Requires { get; set; }

        IReadOnlyList<string> IInternalBaseCommandLineArgument.Requires => Requires;

        protected abstract KindOfCommandLineArgument NGetKind();

        KindOfCommandLineArgument IInternalBaseCommandLineArgument.Kind => NGetKind();

        protected abstract IReadOnlyList<IInternalBaseCommandLineArgument> NGetSubItems();

        IReadOnlyList<IInternalBaseCommandLineArgument> IInternalBaseCommandLineArgument.SubItems => NGetSubItems();

        protected abstract string NGetName();

        string IInternalBaseCommandLineArgument.Name => NGetName();

        protected abstract IReadOnlyList<string> NGetAliases();

        IReadOnlyList<string> IInternalBaseCommandLineArgument.Aliases => NGetAliases();

        protected abstract IReadOnlyList<string> NGetNames();

        IReadOnlyList<string> IInternalBaseCommandLineArgument.Names => NGetNames();

        protected abstract uint? NGetIndex();

        uint? IInternalBaseCommandLineArgument.Index => NGetIndex();

        protected abstract bool NGetUseIfCommandLineIsEmpty();

        bool IInternalBaseCommandLineArgument.UseIfCommandLineIsEmpty => NGetUseIfCommandLineIsEmpty();

        protected abstract bool NGetIsUnique();

        bool IInternalBaseCommandLineArgument.IsUnique => NGetIsUnique();

        /// <inheritdoc/>
        public override string ToString()
        {
            return ToString(0u);
        }

        /// <inheritdoc/>
        public string ToString(uint n)
        {
            return this.GetDefaultToStringInformation(n);
        }

        /// <inheritdoc/>
        string IObjectToString.PropertiesToString(uint n)
        {
            return PropertiesToString(n);
        }

        protected virtual string PropertiesToString(uint n)
        {
            var spaces = DisplayHelper.Spaces(n);
            var sb = new StringBuilder();

            sb.AppendLine($"{spaces}{nameof(IsRequired)} = {IsRequired}");
            sb.AppendLine($"{spaces}{nameof(Target)} = {Target}");
            sb.PrintPODList(n, nameof(Requires), Requires);

            return sb.ToString();
        }
    }
}
