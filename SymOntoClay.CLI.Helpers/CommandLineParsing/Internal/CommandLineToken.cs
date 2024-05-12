using SymOntoClay.CLI.Helpers.CommandLineParsing.Options;
using SymOntoClay.Common;
using SymOntoClay.Common.DebugHelpers;
using System.Text;

namespace SymOntoClay.CLI.Helpers.CommandLineParsing.Internal
{
    public class CommandLineToken : IObjectToString
    {
        public KindOfCommandLineToken Kind { get; set; } = KindOfCommandLineToken.Unknown;
        public string Content { get; set; }
        public BaseNamedCommandLineArgument Option { get; set; }
        public int Position { get; set; }

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
            var spaces = DisplayHelper.Spaces(n);
            var sb = new StringBuilder();
            sb.AppendLine($"{spaces}{nameof(Kind)} = {Kind}");
            sb.AppendLine($"{spaces}{nameof(Content)} = {Content}");
            sb.PrintObjProp(n, nameof(Option), Option);
            sb.AppendLine($"{spaces}{nameof(Position)} = {Position}");

            return sb.ToString();
        }
    }
}
