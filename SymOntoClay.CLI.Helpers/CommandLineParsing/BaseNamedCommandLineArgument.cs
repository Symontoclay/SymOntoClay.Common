using SymOntoClay.Common.DebugHelpers;
using System.Text;

namespace SymOntoClay.CLI.Helpers.CommandLineParsing
{
    public abstract class BaseNamedCommandLineArgument : BaseCommandLineArgument
    {
        public string Name { get; set; }
        public List<string> Aliases { get; set; }
        public List<string> Names => new List<string> { Name }.Concat(Aliases ?? new List<string>()).ToList();
        public bool UseIfCommandLineIsEmpty { get; set; }
        public bool IsUnique { get; set; }
        public bool IsDefault { get; set; }

        /// <inheritdoc/>
        protected override IReadOnlyList<IInternalBaseCommandLineArgument> NGetSubItems() => null;

        /// <inheritdoc/>
        protected override string NGetName() => Name;

        /// <inheritdoc/>
        protected override IReadOnlyList<string> NGetAliases() => Aliases;

        /// <inheritdoc/>
        protected override IReadOnlyList<string> NGetNames() => Names;

        /// <inheritdoc/>
        protected override bool NGetUseIfCommandLineIsEmpty() => UseIfCommandLineIsEmpty;

        /// <inheritdoc/>
        protected override bool NGetIsUnique() => IsUnique;

        /// <inheritdoc/>
        protected override bool NGetIsDefault() => IsDefault;

        /// <inheritdoc/>
        protected override string PropertiesToString(uint n)
        {
            var spaces = DisplayHelper.Spaces(n);
            var sb = new StringBuilder();

            sb.AppendLine($"{spaces}{nameof(Name)} = {Name}");
            sb.PrintPODList(n, nameof(Aliases), Aliases);
            sb.PrintPODList(n, nameof(Names), Names);
            sb.AppendLine($"{spaces}{nameof(UseIfCommandLineIsEmpty)} = {UseIfCommandLineIsEmpty}");
            sb.AppendLine($"{spaces}{nameof(IsUnique)} = {IsUnique}");
            sb.AppendLine($"{spaces}{nameof(IsDefault)} = {IsDefault}");

            sb.Append(base.PropertiesToString(n));

            return sb.ToString();
        }
    }
}
