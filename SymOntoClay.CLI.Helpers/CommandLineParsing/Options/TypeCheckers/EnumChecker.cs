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
