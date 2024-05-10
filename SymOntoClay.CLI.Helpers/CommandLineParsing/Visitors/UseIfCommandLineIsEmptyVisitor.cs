using NLog;

namespace SymOntoClay.CLI.Helpers.CommandLineParsing.Visitors
{
    public class UseIfCommandLineIsEmptyVisitor: BaseCommandLineParsingVisitor
    {
#if DEBUG
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
#endif

        private List<BaseNamedCommandLineArgument> _result;
        private object _lockObj = new object();

        public List<BaseNamedCommandLineArgument> Run(CommandLineVirtualRootGroup rootElement)
        {
            lock(_lockObj)
            {
                if (_result != null)
                {
                    throw new Exception($"{nameof(UseIfCommandLineIsEmptyVisitor)} can not be used in multiple threads.");
                }

                _result = new List<BaseNamedCommandLineArgument>();
            }

            throw new NotImplementedException();
        }
    }
}
