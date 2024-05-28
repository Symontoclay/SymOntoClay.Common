using NLog;
using SymOntoClay.CLI.Helpers.CommandLineParsing.Options;

namespace SymOntoClay.CLI.Helpers.CommandLineParsing.Visitors
{
    public class OptionsValidationsVisitor : BaseCommandLineParsingVisitor
    {
#if DEBUG
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
#endif

        private List<string> _result;
        private object _lockObj = new object();

        public List<string> Run(CommandLineVirtualRootGroup rootElement)
        {
            lock (_lockObj)
            {
                if (_result != null)
                {
                    throw new Exception($"{nameof(NamedCommandLineArgumentsVisitor)} can not be used in multiple threads.");
                }

                _result = new List<string>();
            }

            rootElement.Accept(this);

            var result = _result;
            _result = null;

            return result;
        }

        /// <inheritdoc/>
        protected override void OnVisitCommandLineArgument(CommandLineArgument element)
        {
#if DEBUG
            _logger.Info($"element = {element}");
#endif

            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        protected override void OnVisitCommandLineGroup(CommandLineGroup element)
        {
#if DEBUG
            _logger.Info($"element = {element}");
#endif

            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        protected override void OnVisitCommandLineMutuallyExclusiveSet(CommandLineMutuallyExclusiveSet element)
        {
#if DEBUG
            _logger.Info($"element = {element}");
#endif

            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        protected override void OnVisitCommandLineNamedGroup(CommandLineNamedGroup element)
        {
#if DEBUG
            _logger.Info($"element = {element}");
#endif

            throw new NotImplementedException();
        }
    }
}
