using Newtonsoft.Json;
using NLog;
using SymOntoClay.Common.DebugHelpers;

namespace SymOntoClay.CLI.Helpers.CommandLineParsing
{
    public class CommandLineParser
    {
#if DEBUG
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
#endif

        public CommandLineParser(List<BaseCommandLineArgument> commandLineArguments) 
        {
#if DEBUG
            _logger.Info($"commandLineArguments = {JsonConvert.SerializeObject(commandLineArguments, Formatting.Indented)}");
            _logger.Info($"commandLineArguments = {commandLineArguments.WriteListToString()}");
#endif
        }
    }
}
