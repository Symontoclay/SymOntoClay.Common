using SymOntoClay.Common.DebugHelpers;
using System.Text;

namespace SymOntoClay.CLI.Helpers.CommandLineParsing
{
    public class CommandLineArgument: BaseNamedCommandLineArgument
    {
        public KindOfCommandLineArgument Kind { get; set; }
        public uint? Index { get; set; }

        /// <inheritdoc/>
        protected override KindOfCommandLineArgument NGetKind() => Kind;

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
