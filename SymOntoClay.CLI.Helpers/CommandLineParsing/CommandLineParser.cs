using Newtonsoft.Json;
using NLog;
using SymOntoClay.CLI.Helpers.CommandLineParsing.Options;
using SymOntoClay.CLI.Helpers.CommandLineParsing.Visitors;
using SymOntoClay.Common.CollectionsHelpers;
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

            _сommandLineVirtualRootGroup = new CommandLineVirtualRootGroup
            {
                SubItems = commandLineArguments
            };

#if DEBUG
            _logger.Info($"_сommandLineVirtualRootGroup = {_сommandLineVirtualRootGroup}");
#endif

            var useIfCommandLineIsEmptyVisitor = new UseIfCommandLineIsEmptyVisitor();

            var defaultElementsList = useIfCommandLineIsEmptyVisitor.Run(_сommandLineVirtualRootGroup);

#if DEBUG
            _logger.Info($"defaultElementsList = {defaultElementsList.WriteListToString()}");
#endif

            if(defaultElementsList.Count > 1)
            {
                throw new Exception($"Too many options set as default: {string.Join(", ", defaultElementsList.Select(p => $"'{p.Name}'"))}. There can be only one default option.");
            }
        }

        private readonly CommandLineVirtualRootGroup _сommandLineVirtualRootGroup;
        private readonly BaseCommandLineArgument _defaultCommandLineArgumentOptions;

        public CommandLineParsingResult Parse(string[] args)
        {
#if DEBUG
            _logger.Info($"args = {args.WritePODListToString()}");
#endif

            if(args.IsNullOrEmpty())
            {
                return ProcessEmptyArgs();
            }

            throw new NotImplementedException();
        }

        private CommandLineParsingResult ProcessEmptyArgs()
        {
            throw new NotImplementedException();
        }
    }
}
