using SymOntoClay.CLI.Helpers.CommandLineParsing.Visitors;
using SymOntoClay.Common.DebugHelpers;
using System.Text;

namespace SymOntoClay.CLI.Helpers.CommandLineParsing.Options
{
    public class CommandLineVirtualRootGroup : BaseCommandLineArgument
    {
        public List<BaseCommandLineArgument> SubItems { get; set; }

        /// <inheritdoc/>
        public override string GetIdentifier()
        {
            return "NoName CommandLineVirtualRootGroup";
        }

        /// <inheritdoc/>
        public override KindOfCommandLineArgument GetKind()
        {
            return KindOfCommandLineArgument.VirtualRootGroup;
        }

        /// <inheritdoc/>
        public override void Accept(ICommandLineParsingVisitor visitor)
        {
            visitor.VisitCommandLineVirtualRootGroup(this);
        }

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
