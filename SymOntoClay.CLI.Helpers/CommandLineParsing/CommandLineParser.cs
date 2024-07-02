using SymOntoClay.CLI.Helpers.CommandLineParsing.Exceptions;
using SymOntoClay.CLI.Helpers.CommandLineParsing.Helpers;
using SymOntoClay.CLI.Helpers.CommandLineParsing.Internal;
using SymOntoClay.CLI.Helpers.CommandLineParsing.Options;
using SymOntoClay.CLI.Helpers.CommandLineParsing.Visitors;
using SymOntoClay.Common.CollectionsHelpers;

namespace SymOntoClay.CLI.Helpers.CommandLineParsing
{
    public class CommandLineParser
    {
#if DEBUG
        //private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
#endif

        public CommandLineParser(List<BaseCommandLineArgument> commandLineArguments)
            : this(commandLineArguments, false)
        {
        }

        public CommandLineParser(List<BaseCommandLineArgument> commandLineArguments, bool initWithoutExceptions)
        {
            _initWithoutExceptions = initWithoutExceptions;

            if (commandLineArguments == null)
            {
                if(initWithoutExceptions)
                {
                    _initialErrors.Add(new ArgumentNullException(nameof(commandLineArguments)).Message);

                    return;
                }
                else
                {
                    throw new ArgumentNullException(nameof(commandLineArguments));
                }
            }

            if(!commandLineArguments.Any())
            {
                var errorMessage = "There must be at least one option.";

#if DEBUG
                //_logger.Info($"errorMessage = {errorMessage}");
#endif

                if (initWithoutExceptions)
                {
                    _initialErrors.Add(errorMessage);

                    return;
                }
                else
                {
                    throw new OptionsValidationException(errorMessage);
                }
            }

#if DEBUG
            //_logger.Info($"commandLineArguments = {JsonConvert.SerializeObject(commandLineArguments, Formatting.Indented)}");
            //_logger.Info($"commandLineArguments = {commandLineArguments.WriteListToString()}");
#endif

            _сommandLineVirtualRootGroup = new CommandLineVirtualRootGroup
            {
                SubItems = commandLineArguments
            };

#if DEBUG
            //_logger.Info($"_сommandLineVirtualRootGroup = {_сommandLineVirtualRootGroup}");
#endif

            var optionsValidationsVisitor = new OptionsValidationsVisitor();

            var errorList = optionsValidationsVisitor.Run(_сommandLineVirtualRootGroup);

            if(errorList.Any())
            {
                if (initWithoutExceptions)
                {
                    _initialErrors.AddRange(errorList);

                    return;
                }
                else
                {
                    throw new OptionsValidationException(errorList.First());
                }
            }

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
                //_logger.Info($"namedCommandLineArgumentsKvpItem.Key = {namedCommandLineArgumentsKvpItem.Key}");
                //_logger.Info($"namedCommandLineArgumentsKvpItem.Value.Count = {namedCommandLineArgumentsKvpItem.Value.Count}");
#endif

                if(namedCommandLineArgumentsKvpItem.Value.Count == 1)
                {
                    _namedCommandLineArgumentsDict[namedCommandLineArgumentsKvpItem.Key] = namedCommandLineArgumentsKvpItem.Value.Single();
                    continue;
                }

                var errorMessage = $"Option '{namedCommandLineArgumentsKvpItem.Key}' is declared {namedCommandLineArgumentsKvpItem.Value.Count} times.";

#if DEBUG
                //_logger.Info($"errorMessage = {errorMessage}");
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
            //_logger.Info($"defaultElementsList = {defaultElementsList.WriteListToString()}");
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
                    //_logger.Info($"kind = {kind}");
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

            var elementsWithRequiresVisitor = new ElementsWithRequiresVisitor();

            var elementsWithRequiresList = elementsWithRequiresVisitor.Run(_сommandLineVirtualRootGroup);

#if DEBUG
            //_logger.Info($"elementsWithRequiresList = {elementsWithRequiresList.WriteListToString()}");
#endif

            foreach(var element in elementsWithRequiresList)
            {
#if DEBUG
                //_logger.Info($"element = {element}");
#endif

                var requiresIsAvailable = false;

                foreach(var item in element.Requires)
                {
#if DEBUG
                    //_logger.Info($"item = {item}");
                    //_logger.Info($"_namedCommandLineArgumentsDict.ContainsKey(item) = {_namedCommandLineArgumentsDict.ContainsKey(item)}");
#endif

                    if(_namedCommandLineArgumentsDict.ContainsKey(item))
                    {
                        requiresIsAvailable = true;
                    }
                }

#if DEBUG
                //_logger.Info($"requiresIsAvailable = {requiresIsAvailable}");
#endif

                if(!requiresIsAvailable)
                {
                    var errorMessage = string.Empty;

                    if (element.Requires.Count == 1)
                    {
                        errorMessage = $"Option '{element.Requires.First()}' is required for an option.";
                    }
                    else
                    {
                        errorMessage = $"Options {string.Join(", ", element.Requires.Select(p => $"'{p}'"))} are required for an option.";
                    }

#if DEBUG
                    //_logger.Info($"errorMessage = {errorMessage}");
#endif

                    if (initWithoutExceptions)
                    {
                        _initialErrors.Add(errorMessage);
                    }
                    else
                    {
                        throw new RequiredOptionException(errorMessage);
                    }
                }
            }
        }

        private readonly bool _initWithoutExceptions;
        private readonly CommandLineVirtualRootGroup _сommandLineVirtualRootGroup;
        private readonly Dictionary<string, BaseNamedCommandLineArgument> _namedCommandLineArgumentsDict;
        private readonly BaseNamedCommandLineArgument _defaultCommandLineArgumentOptions;
        private readonly List<BaseNamedCommandLineArgument> _uniqueElementsList;
        private readonly List<string> _initialErrors = new List<string>();

        /// <summary>
        /// Converts list of command line arguments to structured result.
        /// </summary>
        /// <param name="args">List of command line arguments.</param>
        /// <returns>Structured result which represents list of command line arguments.</returns>
        /// <exception cref="UnknownValueException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="UniqueOptionException"></exception>
        /// <exception cref="RequiredOptionException"></exception>
        public CommandLineParsingResult Parse(string[] args)
        {
#if DEBUG
            //_logger.Info($"args = {args.WritePODListToString()}");
            //_logger.Info($"_initialErrors = {_initialErrors.WritePODListToString()}");
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
            //_logger.Info($"tokensList = {tokensList.WriteListToString()}");
#endif

            var errorsList = new List<string>();

            var processingResult = ProcessCommandLineVirtualRootGroup(_сommandLineVirtualRootGroup, tokensList, null, errorsList);

#if DEBUG
            //_logger.Info($"processingResult = {processingResult}");
            //_logger.Info($"errorsList = {errorsList.WritePODListToString()}");
#endif

            if(errorsList.Any())
            {
                return new CommandLineParsingResult
                {
                    Params = new Dictionary<string, object>(),
                    Errors = errorsList
                };
            }

#if DEBUG
            //_logger.Info($"tokensList (after) = {tokensList.WriteListToString()}");
#endif

            var unknownTokens = tokensList.Where(p => p.Option == null).ToList();

            if(unknownTokens.Count > 0)
            {
#if DEBUG
                //_logger.Info($"unknownTokens = {unknownTokens.WriteListToString()}");
#endif

                if (_initWithoutExceptions)
                {
                    foreach(var unknownToken in unknownTokens)
                    {
                        errorsList.Add(UnknownValueException.GetMessage(unknownToken.Content, unknownToken.Position));
                    }

                    return new CommandLineParsingResult
                    {
                        Params = new Dictionary<string, object>(),
                        Errors = errorsList
                    };
                }
                else
                {
                    var firstUnknownToken = unknownTokens.First();

#if DEBUG
                    //_logger.Info($"firstUnknownToken = {firstUnknownToken}");
#endif

                    throw new UnknownValueException(firstUnknownToken.Content, firstUnknownToken.Position);
                }
            }

            var rawResultsList = new List<(BaseNamedCommandLineArgument Option, List<string> ParamValues)>();

            BaseNamedCommandLineArgument currentOption = null;
            List<string> currentValuesList = null;
            CommandLineToken prevToken = null;

            foreach (var token in tokensList)
            {
#if DEBUG
                //_logger.Info($"token = {token}");
#endif

                if(currentOption == null || currentOption != token.Option || (prevToken.Kind == KindOfCommandLineToken.Option && token.Kind == KindOfCommandLineToken.Option))
                {
                    if(currentOption != null && (currentOption != token.Option || prevToken.Kind == KindOfCommandLineToken.Option && token.Kind == KindOfCommandLineToken.Option))
                    {
#if DEBUG
                        //_logger.Info($"currentOption = {currentOption}");
                        //_logger.Info($"currentValuesList = {currentValuesList.WritePODListToString()}");
#endif

                        rawResultsList.Add((currentOption, currentValuesList));
                    }

                    currentOption = token.Option;
                    prevToken = token;
                    currentValuesList = new List<string>();
                }

                var tokenKind  = token.Kind;

#if DEBUG
                //_logger.Info($"tokenKind = {tokenKind}");
#endif

                switch(tokenKind)
                {
                    case KindOfCommandLineToken.Option:
                        break;

                    case KindOfCommandLineToken.Value:
                        currentValuesList.Add(token.Content);
                        break;

                    default: 
                        throw new ArgumentOutOfRangeException(nameof(tokenKind), tokenKind, null);
                }
            }

#if DEBUG
            //_logger.Info($"currentOption = {currentOption}");
            //_logger.Info($"currentValuesList = {currentValuesList.WritePODListToString()}");
#endif

            rawResultsList.Add((currentOption, currentValuesList));

#if DEBUG
            //_logger.Info($"rawResultsList.Count = {rawResultsList.Count}");
            //_logger.Info($"rawResultsList = {JsonConvert.SerializeObject(rawResultsList, Formatting.Indented)}");
            //_logger.Info($"_uniqueElementsList.Count = {_uniqueElementsList.Count}");
#endif

            var rawResultsDict = rawResultsList.GroupBy(p => p.Option).ToDictionary(p => p.Key, p => p.Select(x => x.ParamValues).ToList());

            var resultOptionsDict = new Dictionary<string, object>();

            foreach (var rawResultsKvpItem in rawResultsDict)
            {
                var option = rawResultsKvpItem.Key;

#if DEBUG
                //_logger.Info($"option = {option}");
                //_logger.Info($"rawResultsKvpItem.Value = {JsonConvert.SerializeObject(rawResultsKvpItem.Value, Formatting.Indented)}");
#endif

                var identifier = option.Identifier;

#if DEBUG
                //_logger.Info($"identifier = {identifier}");
#endif

                if (rawResultsKvpItem.Value.Count > 1 && _uniqueElementsList.Contains(option))
                {
                    var errorMessage = $"Option '{identifier}' must be unique.";

#if DEBUG
                    //_logger.Info($"errorMessage = {errorMessage}");
#endif

                    if (_initWithoutExceptions)
                    {
                        errorsList.Add(errorMessage);

                        break;
                    }
                    else
                    {
                        throw new UniqueOptionException(errorMessage);
                    }
                }

                if((option.Requires?.Count ?? 0) > 0)
                {
                    var requiresIsAvailable = false;

                    foreach (var item in option.Requires)
                    {
#if DEBUG
                        //_logger.Info($"item = {item}");
                        //_logger.Info($"_namedCommandLineArgumentsDict.ContainsKey(item) = {_namedCommandLineArgumentsDict.ContainsKey(item)}");
#endif

                        var requiredOption = _namedCommandLineArgumentsDict[item];

#if DEBUG
                        //_logger.Info($"requiredOption = {requiredOption}");
                        //_logger.Info($"rawResultsDict.ContainsKey(requiredOption) = {rawResultsDict.ContainsKey(requiredOption)}");
#endif

                        if(rawResultsDict.ContainsKey(requiredOption))
                        {
                            requiresIsAvailable = true;
                        }
                    }

#if DEBUG
                    //_logger.Info($"requiresIsAvailable = {requiresIsAvailable}");
#endif
                    if(!requiresIsAvailable)
                    {
                        var errorMessage = string.Empty;

                        if(option.Requires.Count == 1)
                        {
                            errorMessage = $"Option '{option.Requires.First()}' is required for '{identifier}'.";
                        }
                        else
                        {
                            errorMessage = $"Options {string.Join(", ", option.Requires.Select(p => $"'{p}'"))} are required for '{identifier}'.";
                        }

#if DEBUG
                        //_logger.Info($"errorMessage = {errorMessage}");
#endif

                        if (_initWithoutExceptions)
                        {
                            errorsList.Add(errorMessage);
                        }
                        else
                        {
                            throw new RequiredOptionException(errorMessage);
                        }
                    }
                }

                var targetValue = rawResultsKvpItem.Value.LastOrDefault();

#if DEBUG
                //_logger.Info($"targetValue = {targetValue.WritePODListToString()}");
#endif

                var optionKind = option.GetKind();

#if DEBUG
                //_logger.Info($"optionKind = {optionKind}");
#endif

                var typeChecker = option.TypeChecker;

                if (targetValue.Any())
                {
                    switch(optionKind)
                    {
                        case KindOfCommandLineArgument.SingleValue:
                        case KindOfCommandLineArgument.FlagOrSingleValue:
                            {
                                var targetValueItem = targetValue.FirstOrDefault();

                                if (typeChecker == null)
                                {
                                    resultOptionsDict[identifier] = targetValueItem;
                                }
                                else
                                {
                                    resultOptionsDict[identifier] = typeChecker.ConvertFromString(targetValueItem);
                                }                                
                            }                            
                            break;

                        default:
                            if(typeChecker == null)
                            {
                                resultOptionsDict[identifier] = targetValue;
                            }
                            else
                            {
                                resultOptionsDict[identifier] = targetValue.Select(typeChecker.ConvertFromString).ToList();
                            }
                            break;
                    }
                }
                else
                {
                    resultOptionsDict[identifier] = true;
                }
            }

            if (errorsList.Any())
            {
                return new CommandLineParsingResult
                {
                    Params = new Dictionary<string, object>(),
                    Errors = errorsList
                };
            }

            return new CommandLineParsingResult
            {
                Params = resultOptionsDict,
                Errors = new List<string>(),
            };
        }

        private (bool Result, string Name, BaseNamedCommandLineArgument NamedElement, List<(string Name, BaseNamedCommandLineArgument NamedElement)> FoundBaseNamedCommandLineArguments) ProcessBaseCommandLineArgument(BaseCommandLineArgument element, List<CommandLineToken> commandLineTokens, CommandLineParserContext parserContext, List<string> errorsList)
        {
#if DEBUG
            //_logger.Info($"element = {element}");
            //_logger.Info($"parserContext = {parserContext}");
#endif

            var kind = element.GetKind();

#if DEBUG
            //_logger.Info($"kind = {kind}");
#endif

            switch(kind)
            {
                case KindOfCommandLineArgument.VirtualRootGroup:
                    return ProcessCommandLineVirtualRootGroup(element as CommandLineVirtualRootGroup, commandLineTokens, parserContext, errorsList);

                case KindOfCommandLineArgument.NamedGroup:
                    return ProcessCommandLineNamedGroup(element as CommandLineNamedGroup, commandLineTokens, parserContext, errorsList);

                case KindOfCommandLineArgument.Group:
                    return ProcessCommandLineGroup(element as CommandLineGroup, commandLineTokens, parserContext, errorsList);

                case KindOfCommandLineArgument.MutuallyExclusiveSet:
                    return ProcessCommandLineMutuallyExclusiveSet(element as CommandLineMutuallyExclusiveSet, commandLineTokens, parserContext, errorsList);

                case KindOfCommandLineArgument.Flag:
                case KindOfCommandLineArgument.SingleValue:
                case KindOfCommandLineArgument.FlagOrSingleValue:
                case KindOfCommandLineArgument.List:
                case KindOfCommandLineArgument.SingleValueOrList:
                    return ProcessCommandLineArgument(element as CommandLineArgument, commandLineTokens, parserContext, errorsList);

                default:
                    throw new ArgumentOutOfRangeException(nameof(kind), kind, null);
            }
        }

        private (bool Result, string Name, BaseNamedCommandLineArgument NamedElement, List<(string Name, BaseNamedCommandLineArgument NamedElement)> FoundBaseNamedCommandLineArguments) ProcessCommandLineGroup(CommandLineGroup element, List<CommandLineToken> commandLineTokens, CommandLineParserContext parserContext, List<string> errorsList)
        {
#if DEBUG
            //_logger.Info($"element = {element}");
            //_logger.Info($"parserContext = {parserContext}");
#endif

            var ownParserContext = new CommandLineParserContext(parserContext);

            var processingResult = false;

            var foundBaseNamedCommandLineArguments = new List<(string Name, BaseNamedCommandLineArgument NamedElement)>();

            foreach (var subItem in element.SubItems)
            {
                var processingItemResult = ProcessBaseCommandLineArgument(subItem, commandLineTokens, ownParserContext, errorsList);

#if DEBUG
                //_logger.Info($"processingItemResult = {processingItemResult}");
#endif

                if(processingItemResult.Result)
                {
                    processingResult = true;

                    if(processingItemResult.FoundBaseNamedCommandLineArguments != null)
                    {
                        foundBaseNamedCommandLineArguments.AddRange(processingItemResult.FoundBaseNamedCommandLineArguments);
                    }                    
                }
            }

            var firstNamedElement = GetFirstNamedElement(foundBaseNamedCommandLineArguments);

#if DEBUG
            //_logger.Info($"firstNamedElement = {firstNamedElement}");
#endif

            return (processingResult, firstNamedElement.Name, firstNamedElement.NamedElement, foundBaseNamedCommandLineArguments);
        }

        private (bool Result, string Name, BaseNamedCommandLineArgument NamedElement, List<(string Name, BaseNamedCommandLineArgument NamedElement)> FoundBaseNamedCommandLineArguments) ProcessCommandLineMutuallyExclusiveSet(CommandLineMutuallyExclusiveSet element, List<CommandLineToken> commandLineTokens, CommandLineParserContext parserContext, List<string> errorsList)
        {
#if DEBUG
            //_logger.Info($"element = {element}");
            //_logger.Info($"parserContext = {parserContext}");
#endif

            var ownParserContext = new CommandLineParserContext(parserContext);

            var processingItemResultsList = new List<(bool Result, string Name, BaseNamedCommandLineArgument NamedElement, List<(string Name, BaseNamedCommandLineArgument NamedElement)> FoundBaseNamedCommandLineArguments)>();

            foreach (var subItem in element.SubItems)
            {
                var processingItemResult = ProcessBaseCommandLineArgument(subItem, commandLineTokens, ownParserContext, errorsList);

#if DEBUG
                //_logger.Info($"processingItemResult = {processingItemResult}");
#endif

                processingItemResultsList.Add(processingItemResult);
            }

#if DEBUG
            //_logger.Info($"processingItemResultsList = {processingItemResultsList.WritePODListToString()}");
#endif

            var foundBaseNamedCommandLineArguments = processingItemResultsList.Where(p => p.FoundBaseNamedCommandLineArguments != null).SelectMany(p => p.FoundBaseNamedCommandLineArguments).ToList();

#if DEBUG
            //_logger.Info($"processingItemResultsList = {processingItemResultsList.WritePODListToString()}");
#endif

            var trueProcessingItemResultsList = processingItemResultsList.Where(p => p.Result == true).ToList();

            var trueCount = trueProcessingItemResultsList.Count;

#if DEBUG
            //_logger.Info($"trueCount = {trueCount}");
#endif

            switch(trueCount)
            {
                case 0:
                    if(element.IsRequired)
                    {
                        var errorMessage = $"Required command line argument '{element.GetIdentifier()}' must be entered.";

#if DEBUG
                        //_logger.Info($"errorMessage = {errorMessage}");
#endif

                        if (_initWithoutExceptions)
                        {
                            errorsList.Add(errorMessage);

                            return (false, null, null, null);
                        }
                        else
                        {
                            throw new RequiredOptionException(errorMessage);
                        }
                    }
                    return (false, null, null, null);

                case 1:
                    {
                        var firstNamedElement = GetFirstNamedElement(foundBaseNamedCommandLineArguments);

#if DEBUG
                        //_logger.Info($"firstNamedElement = {firstNamedElement}");
#endif

                        return (true, firstNamedElement.Name, firstNamedElement.NamedElement, foundBaseNamedCommandLineArguments);
                    }

                default:
                    {
                        var errorMessage = $"Options {string.Join(", ", trueProcessingItemResultsList.Select(p => $"'{p.Name}'"))} cannot be used at the same time.";

#if DEBUG
                        //_logger.Info($"errorMessage = {errorMessage}");
#endif

                        if (_initWithoutExceptions)
                        {
                            errorsList.Add(errorMessage);

                            return (false, null, null, null);
                        }
                        else
                        {
                            throw new DuplicatedMutuallyExclusiveOptionsSetException(errorMessage);
                        }
                    }
            }
        }

        private (bool Result, string Name, BaseNamedCommandLineArgument NamedElement, List<(string Name, BaseNamedCommandLineArgument NamedElement)> FoundBaseNamedCommandLineArguments) ProcessCommandLineNamedGroup(CommandLineNamedGroup element, List<CommandLineToken> commandLineTokens, CommandLineParserContext parserContext, List<string> errorsList)
        {
#if DEBUG
            //_logger.Info($"element = {element}");
            //_logger.Info($"parserContext = {parserContext}");
#endif

            if (element.Names?.Any() ?? false)
            {
                var foundTokens = commandLineTokens.Where(p => p.Kind == KindOfCommandLineToken.Option && element.Names.Contains(p.Content)).ToList();

#if DEBUG
                //_logger.Info($"foundTokens = {foundTokens.WriteListToString()}");
#endif

                if(foundTokens.Any())
                {
                    var firstFoundToken = foundTokens.First();

#if DEBUG
                    //_logger.Info($"firstFoundToken = {firstFoundToken}");
#endif

                    var firstFoundTokenOption = firstFoundToken.Option;
                    var firstFoundTokenContent = firstFoundToken.Content;

                    var foundBaseNamedCommandLineArguments = new List<(string Name, BaseNamedCommandLineArgument NamedElement)>()
                    {
                        (firstFoundTokenContent, firstFoundTokenOption)
                    };

                    foreach (var foundToken in foundTokens)
                    {
#if DEBUG
                        //_logger.Info($"foundToken = {foundToken}");
#endif

                        var ownParserContext = new CommandLineParserContext(parserContext, foundToken.Position + 1);

                        foreach (var subItem in element.SubItems)
                        {
                            var processingItemResult = ProcessBaseCommandLineArgument(subItem, commandLineTokens, ownParserContext, errorsList);

#if DEBUG
                            //_logger.Info($"processingItemResult = {processingItemResult}");
#endif

                            if (processingItemResult.Result)
                            {
                                if(processingItemResult.FoundBaseNamedCommandLineArguments != null)
                                {
                                    foundBaseNamedCommandLineArguments.AddRange(processingItemResult.FoundBaseNamedCommandLineArguments);
                                }
                            }
                        }
                    }

                    return (true, firstFoundTokenContent, firstFoundTokenOption, foundBaseNamedCommandLineArguments);
                }
                else
                {
                    if (element.IsRequired)
                    {
                        ProcessRequiredOptionError(element, errorsList);
                    }

                    return (false, null, null, null);
                }
            }
            else
            {
                throw new NotImplementedException("75A80DB8-CCB9-4111-9A32-418399869664");
            }
        }

        private (bool Result, string Name, BaseNamedCommandLineArgument NamedElement, List<(string Name, BaseNamedCommandLineArgument NamedElement)> FoundBaseNamedCommandLineArguments) ProcessCommandLineArgument(CommandLineArgument element, List<CommandLineToken> commandLineTokens, CommandLineParserContext parserContext, List<string> errorsList)
        {
#if DEBUG
            //_logger.Info($"element = {element}");
            //_logger.Info($"parserContext = {parserContext}");
            //_logger.Info($"tokensList = {commandLineTokens.WriteListToString()}");
#endif

            var kind = element.Kind;

#if DEBUG
            //_logger.Info($"kind = {kind}");
#endif

            if (element.Names?.Any() ?? false)
            {
                var foundTokens = commandLineTokens.Where(p => p.Kind == KindOfCommandLineToken.Option && element.Names.Contains(p.Content)).ToList();

#if DEBUG
                //_logger.Info($"foundTokens = {foundTokens.WriteListToString()}");
#endif

                if(foundTokens.Any())
                {
                    var firstFoundToken = foundTokens.First();

#if DEBUG
                    //_logger.Info($"firstFoundToken = {firstFoundToken}");
#endif

                    var firstFoundTokenOption = firstFoundToken.Option;
                    var firstFoundTokenContent = firstFoundToken.Content;

                    switch (kind)
                    {
                        case KindOfCommandLineArgument.Flag:
                            return (true, firstFoundToken.Content, firstFoundTokenOption, new List<(string Name, BaseNamedCommandLineArgument NamedElement)> { (firstFoundTokenContent, firstFoundTokenOption) });

                        case KindOfCommandLineArgument.SingleValue:
                            {
                                foreach (var foundToken in foundTokens)
                                {
#if DEBUG
                                    //_logger.Info($"foundToken = {foundToken}");
#endif

                                    ProcessSingleValue(element, foundToken.Position + 1, true, commandLineTokens, errorsList);
                                }

                                return (true, firstFoundTokenContent, firstFoundTokenOption, new List<(string Name, BaseNamedCommandLineArgument NamedElement)> { (firstFoundTokenContent, firstFoundTokenOption) });
                            }

                        case KindOfCommandLineArgument.List:
                        case KindOfCommandLineArgument.SingleValueOrList:
                            {
                                foreach (var foundToken in foundTokens)
                                {
#if DEBUG
                                    //_logger.Info($"foundToken = {foundToken}");
#endif

                                    ProcessValueList(element, foundToken.Position + 1, true, commandLineTokens, errorsList);
                                }

                                return (true, firstFoundTokenContent, firstFoundTokenOption, new List<(string Name, BaseNamedCommandLineArgument NamedElement)> { (firstFoundTokenContent, firstFoundTokenOption) });
                            }

                        case KindOfCommandLineArgument.FlagOrSingleValue:
                            {
                                foreach (var foundToken in foundTokens)
                                {
#if DEBUG
                                    //_logger.Info($"foundToken = {foundToken}");
#endif

                                    ProcessFlagOrSingleValue(element, foundToken.Position + 1, commandLineTokens, errorsList);
                                }

                                return (true, firstFoundTokenContent, firstFoundTokenOption, new List<(string Name, BaseNamedCommandLineArgument NamedElement)> { (firstFoundTokenContent, firstFoundTokenOption) });
                            }

                        default:
                            throw new ArgumentOutOfRangeException(nameof(kind), kind, null);
                    }
                }
                else
                {
                    return BindSingleValueByPosition(element, commandLineTokens, parserContext, errorsList);
                }
            }
            else
            {
                switch (kind)
                {
                    case KindOfCommandLineArgument.SingleValue:
                        return BindSingleValueByPosition(element, commandLineTokens, parserContext, errorsList);

                    default:
                        throw new ArgumentOutOfRangeException(nameof(kind), kind, null);
                }
            }

            //return (false, null, null);
        }

        private void CheckValueType(BaseNamedCommandLineArgument element, string content, List<string> errorsList)
        {
#if DEBUG
            //_logger.Info($"element = {element}");
            //_logger.Info($"content = {content}");
#endif

            var typeChecker = element.TypeChecker;

            if(typeChecker == null)
            {
                return;
            }

            if (typeChecker.Check(content))
            {
                return;
            }

            var errorMessage = element.TypeCheckErrorMessage;

#if DEBUG
            //_logger.Info($"errorMessage = {errorMessage}");
#endif

            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                errorMessage = $"Can not convert value '{content}' to type '{typeChecker.GetTypeName()}'.";
            }
            else
            {
                if(!errorMessage.EndsWith(" "))
                {
                    errorMessage = $"{errorMessage} ";
                }

                errorMessage = $"{errorMessage}'{content}'.";
            }

#if DEBUG
            //_logger.Info($"errorMessage (after) = {errorMessage}");
#endif

            if (_initWithoutExceptions)
            {
                errorsList.Add(errorMessage);
            }
            else
            {
                throw new TypeCheckingException(errorMessage);
            }
        }

        private (bool Result, string Name, BaseNamedCommandLineArgument NamedElement, List<(string Name, BaseNamedCommandLineArgument NamedElement)> FoundBaseNamedCommandLineArguments) BindSingleValueByPosition(CommandLineArgument element, List<CommandLineToken> commandLineTokens, CommandLineParserContext parserContext, List<string> errorsList)
        {
#if DEBUG
            //_logger.Info($"element = {element}");
            //_logger.Info($"parserContext = {parserContext}");
            //_logger.Info($"tokensList = {commandLineTokens.WriteListToString()}");
#endif

            if (element.Index.HasValue)
            {
                var absIndex = parserContext.GetAbsIndex(element.Index.Value);

#if DEBUG
                //_logger.Info($"absIndex = {absIndex}");
#endif

                if (absIndex < commandLineTokens.Count)
                {
                    var targetToken = commandLineTokens[absIndex.Value];

#if DEBUG
                    //_logger.Info($"targetToken = {targetToken}");
#endif

                    var targetTokenContent = targetToken.Content;

                    if (targetToken.Kind == KindOfCommandLineToken.Value && targetToken.Option == null)
                    {
                        targetToken.Option = element;

                        CheckValueType(element, targetTokenContent, errorsList);

                        return (true, targetTokenContent, element, new List<(string Name, BaseNamedCommandLineArgument NamedElement)> { (targetTokenContent, element) });
                    }
                }
            }

            if(element.IsRequired)
            {
                ProcessRequiredOptionError(element, errorsList);
            }

            return (false, null, null, null);
        }

        private void ProcessFlagOrSingleValue(BaseNamedCommandLineArgument element, int targetIndex, List<CommandLineToken> commandLineTokens, List<string> errorsList)
        {
#if DEBUG
            //_logger.Info($"targetIndex = {targetIndex}");
            //_logger.Info($"commandLineTokens.Count = {commandLineTokens.Count}");
            //_logger.Info($"tokensList = {commandLineTokens.WriteListToString()}");
#endif

            if (commandLineTokens.Count >= targetIndex + 1)
            {
                var targetToken = commandLineTokens[targetIndex];

#if DEBUG
                //_logger.Info($"targetToken = {targetToken}");
#endif

                if (targetToken.Kind == KindOfCommandLineToken.Value)
                {
                    if (targetToken.Option == null)
                    {
                        targetToken.Option = element;

                        CheckValueType(element, targetToken.Content, errorsList);
                    }
                    else
                    {
                        throw new NotImplementedException("AA01B329-947B-4F68-AED7-EB0126A59795");
                    }
                }
                else
                {
#if DEBUG
                    //_logger.Info($"tokensList (after) = {commandLineTokens.WriteListToString()}");
#endif

                    return;
                }

#if DEBUG
                //_logger.Info($"tokensList (after) = {commandLineTokens.WriteListToString()}");
#endif
            }
            else
            {
#if DEBUG
                //_logger.Info($"tokensList (after) = {commandLineTokens.WriteListToString()}");
#endif

                return;
            }
        }

        private void ProcessSingleValue(BaseNamedCommandLineArgument element, int targetIndex, bool isObligate, List<CommandLineToken> commandLineTokens, List<string> errorsList)
        {
#if DEBUG
            //_logger.Info($"targetIndex = {targetIndex}");
            //_logger.Info($"commandLineTokens.Count = {commandLineTokens.Count}");
            //_logger.Info($"isObligate = {isObligate}");
            //_logger.Info($"tokensList = {commandLineTokens.WriteListToString()}");
#endif

            var flagInsteadOfSingleValueErrorMessage = $"'{element.Identifier}' must be a single value, but used as flag.";

            if (commandLineTokens.Count >= targetIndex + 1)
            {
                var targetToken = commandLineTokens[targetIndex];

#if DEBUG
                //_logger.Info($"targetToken = {targetToken}");
#endif

                if(targetToken.Kind == KindOfCommandLineToken.Value)
                {
                    if (targetToken.Option == null)
                    {
                        targetToken.Option = element;

                        CheckValueType(element, targetToken.Content, errorsList);
                    }
                    else
                    {
                        throw new NotImplementedException("F3C72040-2E83-43E8-8E19-09426F14AE7E");
                    }
                }
                else
                {
                    if (_initWithoutExceptions)
                    {
                        errorsList.Add(flagInsteadOfSingleValueErrorMessage);
                    }
                    else
                    {
                        throw new ValueException(flagInsteadOfSingleValueErrorMessage);
                    }
                }
            }
            else
            {
                if (_initWithoutExceptions)
                {
                    errorsList.Add(flagInsteadOfSingleValueErrorMessage);
                }
                else
                {
                    throw new ValueException(flagInsteadOfSingleValueErrorMessage);
                }
            }

#if DEBUG
            //_logger.Info($"tokensList (after) = {commandLineTokens.WriteListToString()}");
#endif
        }

        private void ProcessValueList(BaseNamedCommandLineArgument element, int targetIndex, bool isObligate, List<CommandLineToken> commandLineTokens, List<string> errorsList)
        {
#if DEBUG
            //_logger.Info($"targetIndex = {targetIndex}");
            //_logger.Info($"commandLineTokens.Count = {commandLineTokens.Count}");
            //_logger.Info($"isObligate = {isObligate}");
            //_logger.Info($"tokensList = {commandLineTokens.WriteListToString()}");
#endif

            var flagInsteadOfValueListErrorMessage = $"'{element.Identifier}' must be a single value or list of values, but used as flag.";

            if (commandLineTokens.Count >= targetIndex + 1)
            {
                var isFirst = true;

                for(var i = targetIndex; i < commandLineTokens.Count; i++)
                {
                    var targetToken = commandLineTokens[i];

#if DEBUG
                    //_logger.Info($"targetToken = {targetToken}");
                    //_logger.Info($"isFirst = {isFirst}");
#endif

                    if (targetToken.Kind == KindOfCommandLineToken.Value)
                    {
                        if (targetToken.Option == null)
                        {
                            targetToken.Option = element;

                            CheckValueType(element, targetToken.Content, errorsList);
                        }
                        else
                        {
                            throw new NotImplementedException("DF736ABA-9286-42C0-B84C-10C20FA24C4E");
                        }
                    }
                    else
                    {
                        if(isFirst)
                        {
                            if (_initWithoutExceptions)
                            {
                                errorsList.Add(flagInsteadOfValueListErrorMessage);

                                break;
                            }
                            else
                            {
                                throw new ValueException(flagInsteadOfValueListErrorMessage);
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (isFirst)
                    {
                        isFirst = false;
                    }
                }
            }
            else
            {
                if (_initWithoutExceptions)
                {
                    errorsList.Add(flagInsteadOfValueListErrorMessage);
                }
                else
                {
                    throw new ValueException(flagInsteadOfValueListErrorMessage);
                }
            }

#if DEBUG
            //_logger.Info($"tokensList (after) = {commandLineTokens.WriteListToString()}");
#endif
        }

        private (bool Result, string Name, BaseNamedCommandLineArgument NamedElement, List<(string Name, BaseNamedCommandLineArgument NamedElement)> FoundBaseNamedCommandLineArguments) ProcessCommandLineVirtualRootGroup(CommandLineVirtualRootGroup element, List<CommandLineToken> commandLineTokens, CommandLineParserContext parserContext, List<string> errorsList)
        {
#if DEBUG
            //_logger.Info($"element = {element}");
            //_logger.Info($"parserContext = {parserContext}");
#endif

            var ownParserContext = new CommandLineParserContext(parserContext);

            var processingResult = false;

            var foundBaseNamedCommandLineArguments = new List<(string Name, BaseNamedCommandLineArgument NamedElement)>();

            foreach (var subItem in element.SubItems)
            {
                var processingItemResult = ProcessBaseCommandLineArgument(subItem, commandLineTokens, ownParserContext, errorsList);

#if DEBUG
                //_logger.Info($"processingItemResult = {processingItemResult}");
#endif

                if (processingItemResult.Result)
                {
                    processingResult = true;

                    if(processingItemResult.FoundBaseNamedCommandLineArguments != null)
                    {
                        foundBaseNamedCommandLineArguments.AddRange(processingItemResult.FoundBaseNamedCommandLineArguments);
                    }
                }
            }

            var firstNamedElement = GetFirstNamedElement(foundBaseNamedCommandLineArguments);

            return (processingResult, firstNamedElement.Name, firstNamedElement.NamedElement, foundBaseNamedCommandLineArguments);
        }

        private (string Name, BaseNamedCommandLineArgument NamedElement) GetFirstNamedElement(List<(string Name, BaseNamedCommandLineArgument NamedElement)> foundBaseNamedCommandLineArguments)
        {
            return foundBaseNamedCommandLineArguments?.OrderByDescending(p => p.NamedElement.IsRequired)?.FirstOrDefault() ?? (null, null);
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
                //_logger.Info($"arg = {arg}");
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
                //_logger.Info($"token = {token}");
#endif

                n++;

                tokensList.Add(token);
            }

            return tokensList;
        }

        private void ProcessRequiredOptionError(BaseCommandLineArgument element, List<string> errorsList)
        {
#if DEBUG
            //_logger.Info($"element = {element}");
#endif

            var errorMessage = $"Required command line argument '{element.GetIdentifier()}' must be entered.";

            if (_initWithoutExceptions)
            {
                errorsList.Add(errorMessage);
            }
            else
            {
                throw new RequiredOptionException(errorMessage);
            }
        }

        private CommandLineParsingResult ProcessEmptyArgs()
        {
            if(_defaultCommandLineArgumentOptions == null)
            {
                var isRequiredCommandLineArgumentsVisitor = new IsRequiredCommandLineArgumentsVisitor();

                var requiredCommandLineArgumentsList = isRequiredCommandLineArgumentsVisitor.Run(_сommandLineVirtualRootGroup);

#if DEBUG
                //_logger.Info($"requiredCommandLineArgumentsList = {requiredCommandLineArgumentsList.WriteListToString()}");
#endif

                if(requiredCommandLineArgumentsList.Count > 0)
                {
                    var errorList = new List<string>();

                    foreach(var requiredCommandLineArgument in requiredCommandLineArgumentsList)
                    {
                        var errorMessage = $"Required command line argument '{requiredCommandLineArgument.GetIdentifier()}' must be entered.";

#if DEBUG
                        //_logger.Info($"errorMessage = {errorMessage}");
#endif

                        if (_initWithoutExceptions)
                        {
                            errorList.Add(errorMessage);
                        }
                        else
                        {
                            throw new RequiredOptionException(errorMessage);
                        }
                    }

                    return new CommandLineParsingResult
                    {
                        Params = new Dictionary<string, object>(),
                        Errors = errorList
                    };
                }

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
