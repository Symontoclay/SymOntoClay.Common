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

using SymOntoClay.Common.DebugHelpers;
using System.Text;

namespace SymOntoClay.CLI.Helpers.CommandLineParsing.Options.TypeCheckers
{
    public class EnumChecker<TEnum> : BaseChecker where TEnum : struct
    {
#if DEBUG
        //private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
#endif

        public EnumChecker(bool ignoreCase = true)
        {
            _ignoreCase = ignoreCase;
        }

        private bool _ignoreCase;

        /// <inheritdoc/>
        public override string GetTypeName()
        {
            return typeof(TEnum).Name;
        }

        /// <inheritdoc/>
        public override bool Check(string value)
        {
#if DEBUG
            //_logger.Info($"value = {value}");
#endif

            return Enum.TryParse<TEnum>(value, _ignoreCase, out var result);
        }

        /// <inheritdoc/>
        public override object ConvertFromString(string value)
        {
#if DEBUG
            //_logger.Info($"value = {value}");
#endif

            Enum.TryParse<TEnum>(value, _ignoreCase, out var result);

            return result;
        }

        /// <inheritdoc/>
        protected override string PropertiesToString(uint n)
        {
            var spaces = DisplayHelper.Spaces(n);
            var sb = new StringBuilder();

            sb.AppendLine($"{spaces}{nameof(TEnum)} = {typeof(TEnum).Name}");
            sb.AppendLine($"{spaces}{_ignoreCase} = {_ignoreCase}");

            sb.Append(base.PropertiesToString(n));

            return sb.ToString();
        }
    }
}
