/*MIT License

Copyright (c) 2020 - 2026 Sergiy Tolkachov

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.*/

using SymOntoClay.CLI.Helpers.CommandLineParsing.Options.TypeCheckers;
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

        public string Identifier => string.IsNullOrWhiteSpace(Target) ? Name : Target;

        /// <inheritdoc/>
        public override string GetIdentifier()
        {
            return Identifier;
        }

        public BaseChecker TypeChecker { get; set; }
        public string TypeCheckErrorMessage { get; set; }

        /// <inheritdoc/>
        protected override string PropertiesToString(uint n)
        {
            var spaces = DisplayHelper.Spaces(n);
            var sb = new StringBuilder();

            sb.AppendLine($"{spaces}{nameof(Identifier)} = {Identifier}");
            sb.AppendLine($"{spaces}{nameof(Name)} = {Name}");
            sb.PrintPODList(n, nameof(Aliases), Aliases);
            sb.PrintPODList(n, nameof(Names), Names);
            sb.AppendLine($"{spaces}{nameof(UseIfCommandLineIsEmpty)} = {UseIfCommandLineIsEmpty}");
            sb.AppendLine($"{spaces}{nameof(IsUnique)} = {IsUnique}");
            sb.PrintObjProp(n, nameof(TypeChecker), TypeChecker);
            sb.AppendLine($"{spaces}{nameof(TypeCheckErrorMessage)} = {TypeCheckErrorMessage}");

            sb.Append(base.PropertiesToString(n));

            return sb.ToString();
        }
    }
}
