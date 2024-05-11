using SymOntoClay.Common.DebugHelpers;
using System.Text;

namespace SymOntoClay.CLI.Helpers.CommandLineParsing.Options
{
    public abstract class BaseNamedCommandLineArgument : BaseCommandLineArgument
    {
        public string Name { get; set; }
        public List<string> Aliases { get; set; }
        public List<string> Names
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Name) && Aliases == null)
                {
                    return null;
                }

                var result = new List<string>();

                if (!string.IsNullOrWhiteSpace(Name))
                {
                    result.Add(Name);
                }

                if (Aliases != null)
                {
                    result.AddRange(Aliases);
                }

                return result;
            }
        }

        public bool UseIfCommandLineIsEmpty { get; set; }
        public bool IsUnique { get; set; }

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

            sb.Append(base.PropertiesToString(n));

            return sb.ToString();
        }
    }
}
