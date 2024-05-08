using SymOntoClay.Common.DebugHelpers;
using System.Text;

namespace SymOntoClay.CLI.Helpers.CommandLineParsing
{
    public class CommandLineGroup : BaseCommandLineArgument
    {
        public List<BaseCommandLineArgument> SubItems { get; set; }

        /// <inheritdoc/>
        protected override IReadOnlyList<string> NGetAliases() => null;

        /// <inheritdoc/>
        protected override uint? NGetIndex() => null;

        /// <inheritdoc/>
        protected override bool NGetIsUnique() => false;

        /// <inheritdoc/>
        protected override KindOfCommandLineArgument NGetKind() => KindOfCommandLineArgument.Group;

        /// <inheritdoc/>
        protected override string NGetName() => null;

        /// <inheritdoc/>
        protected override IReadOnlyList<string> NGetNames() => null;

        /// <inheritdoc/>
        protected override IReadOnlyList<IInternalBaseCommandLineArgument> NGetSubItems() => SubItems;

        /// <inheritdoc/>
        protected override bool NGetUseIfCommandLineIsEmpty() => false;

        /// <inheritdoc/>
        protected override string PropertiesToString(uint n)
        {
            var spaces = DisplayHelper.Spaces(n);
            var sb = new StringBuilder();

            sb.PrintObjListProp(n, nameof(SubItems), SubItems);

            sb.Append(base.PropertiesToString(n));

            return sb.ToString();
        }
    }
}
