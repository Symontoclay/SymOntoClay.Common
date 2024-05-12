using Newtonsoft.Json;
using NLog;
using SymOntoClay.CLI.Helpers.CommandLineParsing.Exceptions;
using SymOntoClay.CLI.Helpers.CommandLineParsing.Helpers;
using SymOntoClay.CLI.Helpers.CommandLineParsing.Internal;
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

            var namedCommandLineArgumentsVisitor = new NamedCommandLineArgumentsVisitor();

            var namedCommandLineArgumentsList = namedCommandLineArgumentsVisitor.Run(_сommandLineVirtualRootGroup);

            var namedCommandLineArgumentsRawDict = namedCommandLineArgumentsList
                .Where(p => (p.Names?.Count ?? 0) > 0)
                .Select(p => p.Names.Select(x => new KeyValuePair<string, BaseNamedCommandLineArgument>(x, p)))
                .SelectMany(p => p).GroupBy(p => p.Key)
                .ToDictionary(p => p.Key, p => p.Select(x => x.Value).ToList());

#if DEBUG
            //_logger.Info($"namedCommandLineArgumentsRawDict = {JsonConvert.SerializeObject(namedCommandLineArgumentsRawDict, Formatting.Indented)}");
#endif

            _namedCommandLineArgumentsDict = new Dictionary<string, BaseNamedCommandLineArgument>();

            foreach (var namedCommandLineArgumentsKvpItem in namedCommandLineArgumentsRawDict)
            {
#if DEBUG
                _logger.Info($"namedCommandLineArgumentsKvpItem.Key = {namedCommandLineArgumentsKvpItem.Key}");
                _logger.Info($"namedCommandLineArgumentsKvpItem.Value.Count = {namedCommandLineArgumentsKvpItem.Value.Count}");
#endif

                if(namedCommandLineArgumentsKvpItem.Value.Count == 1)
                {
                    _namedCommandLineArgumentsDict[namedCommandLineArgumentsKvpItem.Key] = namedCommandLineArgumentsKvpItem.Value.Single();
                    continue;
                }

                var errorMessage = $"Option '{namedCommandLineArgumentsKvpItem.Key}' is declared {namedCommandLineArgumentsKvpItem.Value.Count} times.";

#if DEBUG
                _logger.Info($"errorMessage = {errorMessage}");
#endif

                if (initWithoutExceptions)
                {
                    _initialErrors.Add(errorMessage);
                }
                else
                {
                    throw new DuplicatedOptionException(errorMessage);
                }
            }

            var defaultElementsList = namedCommandLineArgumentsList.Where(p => p.UseIfCommandLineIsEmpty).ToList();

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
                _defaultCommandLineArgumentOptions = defaultElementsList.SingleOrDefault();

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

            _uniqueElementsList = namedCommandLineArgumentsList.Where(p => p.IsUnique).ToList();

            _requiredElementsList = namedCommandLineArgumentsList.Where(p => p.IsRequired).ToList();
        }

        private readonly CommandLineVirtualRootGroup _сommandLineVirtualRootGroup;
        private readonly Dictionary<string, BaseNamedCommandLineArgument> _namedCommandLineArgumentsDict;
        private readonly BaseNamedCommandLineArgument _defaultCommandLineArgumentOptions;
        private readonly List<BaseNamedCommandLineArgument> _uniqueElementsList;
        private readonly List<BaseNamedCommandLineArgument> _requiredElementsList;
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

            var tokensList = ConvertToTokens(args);

#if DEBUG
            _logger.Info($"tokensList = {tokensList.WriteListToString()}");
#endif

            throw new NotImplementedException();
        }

        private List<CommandLineToken> ConvertToTokens(string[] args)
        {
            if(_namedCommandLineArgumentsDict.Count == 0)
            {
                return args.Select(p => new CommandLineToken { Kind = KindOfCommandLineToken.Value, Content = p}).ToList();
            }

            var tokensList = new List<CommandLineToken>();

            var n = 0;

            foreach (var arg in args)
            {
#if DEBUG
                _logger.Info($"arg = {arg}");
#endif

                var token = new CommandLineToken 
                { 
                    Content = arg,
                    Position = n
                };

                if(_namedCommandLineArgumentsDict.ContainsKey(arg))
                {
                    token.Option = _namedCommandLineArgumentsDict[arg];
                    token.Kind = KindOfCommandLineToken.Option;
                }
                else
                {
                    token.Kind = KindOfCommandLineToken.Value;
                }

#if DEBUG
                _logger.Info($"token = {token}");
#endif

                n++;

                tokensList.Add(token);
            }

            return tokensList;
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
