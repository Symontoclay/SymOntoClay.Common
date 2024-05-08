using SymOntoClay.Common.DebugHelpers;
using System.Text;

namespace SymOntoClay.CLI.Helpers.CommandLineParsing
{
    public class CommandLineNamedGroup: BaseNamedCommandLineArgument
    {
        public List<BaseCommandLineArgument> SubItems { get; set; }

        /// <inheritdoc/>
        protected override KindOfCommandLineArgument NGetKind() => KindOfCommandLineArgument.NamedGroup;

        /// <inheritdoc/>
        protected override IReadOnlyList<IInternalBaseCommandLineArgument> NGetSubItems() => SubItems;

        /// <inheritdoc/>
        protected override uint? NGetIndex() => null;

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
