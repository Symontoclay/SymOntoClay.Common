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

using NLog;
using SymOntoClay.CLI.Helpers.CommandLineParsing.Options;
using SymOntoClay.Common;
using SymOntoClay.Common.DebugHelpers;
using System.Text;

namespace SymOntoClay.CLI.Helpers.CommandLineParsing.Internal
{
    public class CommandLineParserContext : IObjectToString
    {
#if DEBUG
        //private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
#endif

        public CommandLineParserContext()
            : this(null)
        { 
        }

        public CommandLineParserContext(CommandLineParserContext parentContext)
            : this(parentContext, null)
        {
        }

        public CommandLineParserContext(CommandLineParserContext parentContext, int? absIndex)
        {
            ParentContext = parentContext;
            AbsIndex = absIndex;
        }

        public CommandLineParserContext ParentContext { get; }

        public int? AbsIndex { get; }

        public int? GetAbsIndex(int index)
        {
#if DEBUG
            //_logger.Info($"index = {index}");
            //_logger.Info($"AbsIndex = {AbsIndex}");
#endif

            if (AbsIndex.HasValue)
            {
                return AbsIndex + index;
            }

            if(ParentContext != null)
            {
                return ParentContext.GetAbsIndex(index);
            }

            return index;
        }

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
            sb.PrintExisting(n, nameof(ParentContext), ParentContext);
            sb.AppendLine($"{spaces}{nameof(AbsIndex)} = {AbsIndex}");
            return sb.ToString();
        }
    }
}
