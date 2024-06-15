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
