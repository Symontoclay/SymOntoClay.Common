using SymOntoClay.Common;
using SymOntoClay.Common.DebugHelpers;
using System.Text;

namespace SymOntoClay.CLI.Helpers.CommandLineParsing
{
    public class CommandLineParsingResult : IObjectToString
    {
        public IReadOnlyDictionary<string, object> Params { get; set; }
        public IReadOnlyList<string> Errors { get; set; }

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
            sb.PrintPODDictProp(n, nameof(Params), Params);
            sb.PrintPODList(n, nameof(Errors), Errors);
            return sb.ToString();
        }
    }
}
