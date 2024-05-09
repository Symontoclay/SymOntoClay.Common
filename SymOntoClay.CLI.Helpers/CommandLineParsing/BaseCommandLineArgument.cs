using SymOntoClay.Common.DebugHelpers;
using SymOntoClay.Common;
using System.Text;

namespace SymOntoClay.CLI.Helpers.CommandLineParsing
{
    public abstract class BaseCommandLineArgument: IObjectToString
    {
        public bool IsRequired { get; set; }
        public string Target { get; set; }
        public List<string> Requires { get; set; }

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
