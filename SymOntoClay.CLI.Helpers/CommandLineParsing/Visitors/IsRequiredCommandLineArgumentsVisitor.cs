using SymOntoClay.CLI.Helpers.CommandLineParsing.Options;

namespace SymOntoClay.CLI.Helpers.CommandLineParsing.Visitors
{
    public class IsRequiredCommandLineArgumentsVisitor : BaseCommandLineParsingVisitor
    {
        private List<BaseCommandLineArgument> _result;
        private object _lockObj = new object();

        public List<BaseCommandLineArgument> Run(CommandLineVirtualRootGroup rootElement)
        {
            lock (_lockObj)
            {
                if (_result != null)
                {
                    throw new Exception($"{nameof(IsRequiredCommandLineArgumentsVisitor)} can not be used in multiple threads.");
                }

                _result = new List<BaseCommandLineArgument>();
            }

            rootElement.Accept(this);

            var result = _result;
            _result = null;

            return result;
        }

        /// <inheritdoc/>
        protected override void OnVisitCommandLineArgument(CommandLineArgument element)
        {
            if(element.IsRequired)
            {
                _result.Add(element);
            }
        }

        /// <inheritdoc/>
        protected override void OnVisitCommandLineGroup(CommandLineGroup element)
        {
            if (element.IsRequired)
            {
                _result.Add(element);
            }
        }

        /// <inheritdoc/>
        protected override void OnVisitCommandLineMutuallyExclusiveSet(CommandLineMutuallyExclusiveSet element)
        {
            if (element.IsRequired)
            {
                _result.Add(element);
            }
        }

        /// <inheritdoc/>
        protected override void OnVisitCommandLineNamedGroup(CommandLineNamedGroup element)
        {
            if (element.IsRequired)
            {
                _result.Add(element);
            }
        }

        /// <inheritdoc/>
        protected override void OnVisitCommandLineVirtualRootGroup(CommandLineVirtualRootGroup element)
        {
            if (element.IsRequired)
            {
                _result.Add(element);
            }
        }
    }
}
