using SymOntoClay.Common;
using SymOntoClay.Common.DebugHelpers;
using System.Text;

namespace SymOntoClay.CLI.Helpers.CommandLineParsing.Options.TypeCheckers
{
    public abstract class BaseChecker : IObjectToString
    {
        /// <summary>
        /// Returns name of type which is checked by the checker.
        /// </summary>
        /// <returns>Name of type which is checked by the checker.</returns>
        public abstract string GetTypeName();

        /// <summary>
        /// Checks if the string value belongs to the type.
        /// </summary>
        /// <param name="value">Checked value.</param>
        /// <returns></returns>
        public abstract bool Check(string value);

        /// <summary>
        /// Converts a string value to instance of the type.
        /// </summary>
        /// <param name="value">Converted string value.</param>
        /// <returns>Instance of the type.</returns>
        public abstract object ConvertFromString(string value);

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
            return sb.ToString();
        }
    }
}
