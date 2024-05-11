using Newtonsoft.Json;
using NLog;
using SymOntoClay.CLI.Helpers.CommandLineParsing.Exceptions;
using SymOntoClay.CLI.Helpers.CommandLineParsing.Helpers;
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
            : this(commandLineArguments, false)
        {
        }

        public CommandLineParser(List<BaseCommandLineArgument> commandLineArguments, bool initWithoutExceptions)
        {
#if DEBUG
            //_logger.Info($"commandLineArguments = {JsonConvert.SerializeObject(commandLineArguments, Formatting.Indented)}");
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
                var errorMessage = $"Too many options set as default: {string.Join(", ", defaultElementsList.Select(p => $"'{p.Name}'"))}. There must be only one default option.";
                
                if(initWithoutExceptions)
                {
                    _initialErrors.Add(errorMessage);
                }
                else
                {
                    throw new DefaultOptionException(errorMessage);
                }                
            }
            else
            {
                _defaultCommandLineArgumentOptions = defaultElementsList.SingleOrDefault() as BaseNamedCommandLineArgument;

                if (_defaultCommandLineArgumentOptions != null)
                {
                    var kind = _defaultCommandLineArgumentOptions.GetKind();

#if DEBUG
                    _logger.Info($"kind = {kind}");
#endif

                    if (!KindOfCommandLineArgumentHelper.CanBeUsedIfCommandLineIsEmpty(kind))
                    {
                        var errorMessage = $"{kind} must not be used as default option.";

                        if (initWithoutExceptions)
                        {
                            _initialErrors.Add(errorMessage);
                        }
                        else
                        {
                            throw new DefaultOptionException(errorMessage);
                        }
                    }
                }
            }
        }

        private readonly CommandLineVirtualRootGroup _сommandLineVirtualRootGroup;
        private readonly BaseNamedCommandLineArgument _defaultCommandLineArgumentOptions;
        private readonly List<string> _initialErrors = new List<string>();

        public CommandLineParsingResult Parse(string[] args)
        {
#if DEBUG
            _logger.Info($"args = {args.WritePODListToString()}");
            _logger.Info($"_initialErrors = {_initialErrors.WritePODListToString()}");
#endif

            if(_initialErrors.Any())
            {
                return new CommandLineParsingResult
                {
                    Params = new Dictionary<string, object>(),
                    Errors = _initialErrors.ToList()
                };
            }

            if (args.IsNullOrEmpty())
            {
                return ProcessEmptyArgs();
            }

            throw new NotImplementedException();
        }

        private CommandLineParsingResult ProcessEmptyArgs()
        {
            if(_defaultCommandLineArgumentOptions == null)
            {
                return new CommandLineParsingResult
                {
                    Params = new Dictionary<string, object>(),
                    Errors = new List<string>()
                };
            }

            return new CommandLineParsingResult
            {
                Params = new Dictionary<string, object>
                {
                    { _defaultCommandLineArgumentOptions.Name , true }
                },
                Errors = new List<string>()
            };
        }
    }
}
