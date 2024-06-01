namespace SymOntoClay.CLI.Helpers.CommandLineParsing.Options.TypeCheckers
{
    public class IntChecker : BaseChecker
    {
#if DEBUG
        //private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
#endif

        /// <inheritdoc/>
        public override string GetTypeName()
        {
            return typeof(int).Name;
        }

        /// <inheritdoc/>
        public override bool Check(string value)
        {
#if DEBUG
            //_logger.Info($"value = {value}");
#endif

            return int.TryParse(value, out var result);
        }

        /// <inheritdoc/>
        public override object ConvertFromString(string value)
        {
#if DEBUG
            //_logger.Info($"value = {value}");
#endif

            int.TryParse(value, out var result);

            return result;
        }
    }
}
