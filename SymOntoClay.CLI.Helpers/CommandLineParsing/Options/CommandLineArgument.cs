using SymOntoClay.CLI.Helpers.CommandLineParsing.Visitors;
using SymOntoClay.Common.DebugHelpers;
using System.Text;

namespace SymOntoClay.CLI.Helpers.CommandLineParsing.Options
{
    public class CommandLineArgument : BaseNamedCommandLineArgument
    {
        public KindOfCommandLineArgument Kind { get; set; }
        public uint? Index { get; set; }

        /// <inheritdoc/>
        public override KindOfCommandLineArgument GetKind()
        {
            return Kind;
        }

        /// <inheritdoc/>
        public override void Accept(ICommandLineParsingVisitor visitor)
        {
            visitor.VisitCommandLineArgument(this);
        }

        /// <inheritdoc/>
        protected override string PropertiesToString(uint n)
        {
            var spaces = DisplayHelper.Spaces(n);
            var sb = new StringBuilder();

            sb.AppendLine($"{spaces}{nameof(Kind)} = {Kind}");
            sb.AppendLine($"{spaces}{nameof(Index)} = {Index}");

            sb.Append(base.PropertiesToString(n));

            return sb.ToString();
        }
    }
}
