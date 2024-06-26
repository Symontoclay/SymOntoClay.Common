﻿using SymOntoClay.CLI.Helpers.CommandLineParsing.Options;

namespace SymOntoClay.CLI.Helpers.CommandLineParsing.Visitors
{
    public class NamedCommandLineArgumentsVisitor: BaseCommandLineParsingVisitor
    {
#if DEBUG
        //private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
#endif

        private List<BaseNamedCommandLineArgument> _result;
        private object _lockObj = new object();

        public List<BaseNamedCommandLineArgument> Run(CommandLineVirtualRootGroup rootElement)
        {
            lock(_lockObj)
            {
                if (_result != null)
                {
                    throw new Exception($"{nameof(NamedCommandLineArgumentsVisitor)} can not be used in multiple threads.");
                }

                _result = new List<BaseNamedCommandLineArgument>();
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
            //_logger.Info($"element = {element}");
#endif

            _result.Add(element);
        }

        /// <inheritdoc/>
        protected override void OnVisitCommandLineNamedGroup(CommandLineNamedGroup element)
        {
#if DEBUG
            //_logger.Info($"element = {element}");
#endif

            _result.Add(element);
        }
    }
}
