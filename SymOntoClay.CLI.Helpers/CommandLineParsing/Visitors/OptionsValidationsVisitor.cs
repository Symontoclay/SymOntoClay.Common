using SymOntoClay.CLI.Helpers.CommandLineParsing.Options;

namespace SymOntoClay.CLI.Helpers.CommandLineParsing.Visitors
{
    public class OptionsValidationsVisitor : BaseCommandLineParsingVisitor
    {
#if DEBUG
        //private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
#endif

        private List<string> _result;
        private object _lockObj = new object();

        public List<string> Run(CommandLineVirtualRootGroup rootElement)
        {
            lock (_lockObj)
            {
                if (_result != null)
                {
                    throw new Exception($"{nameof(OptionsValidationsVisitor)} can not be used in multiple threads.");
                }

                _result = new List<string>();
            }

            rootElement.Accept(this);

            var result = _result.Distinct().ToList();
            _result = null;

            return result;
        }

        /// <inheritdoc/>
        protected override void OnVisitCommandLineArgument(CommandLineArgument element)
        {
#if DEBUG
            //_logger.Info($"element = {element}");
#endif

            if(string.IsNullOrWhiteSpace(element.Name) && !element.Index.HasValue && string.IsNullOrWhiteSpace(element.Target))
            {
                _result.Add($"{nameof(CommandLineArgument)} must have either Name or Index or Target.");
            }

            CheckEmptyRequiresElements(element);

            //OnVisitBaseNamedCommandLineArgument<CommandLineArgument>(element, true);
        }

        /// <inheritdoc/>
        protected override void OnVisitCommandLineGroup(CommandLineGroup element)
        {
#if DEBUG
            //_logger.Info($"element = {element}");
#endif

            if((element.SubItems?.Count ?? 0) == 0)
            {
                _result.Add($"{nameof(CommandLineGroup)} must have subitems.");
            }

            if(element.IsRequired)
            {
                _result.Add($"{nameof(CommandLineGroup)} must not be required.");
            }

            CheckEmptyRequiresElements(element);
        }

        /// <inheritdoc/>
        protected override void OnVisitCommandLineMutuallyExclusiveSet(CommandLineMutuallyExclusiveSet element)
        {
#if DEBUG
            //_logger.Info($"element = {element}");
#endif

            if ((element.SubItems?.Count ?? 0) == 0)
            {
                _result.Add($"{nameof(CommandLineMutuallyExclusiveSet)} must have subitems.");
            }

            CheckEmptyRequiresElements(element);
        }

        /// <inheritdoc/>
        protected override void OnVisitCommandLineNamedGroup(CommandLineNamedGroup element)
        {
#if DEBUG
            //_logger.Info($"element = {element}");
#endif

            if ((element.SubItems?.Count ?? 0) == 0)
            {
                _result.Add($"{nameof(CommandLineNamedGroup)} must have subitems.");
            }

            CheckEmptyRequiresElements(element);

            OnVisitBaseNamedCommandLineArgument<CommandLineNamedGroup>(element, false);
        }

        private void OnVisitBaseNamedCommandLineArgument<T>(BaseNamedCommandLineArgument element, bool skipNameCheck)
            where T : BaseNamedCommandLineArgument
        {
#if DEBUG
            //_logger.Info($"element = {element}");
            //_logger.Info($"typeof(T).Name = {typeof(T).Name}");
#endif

            if(!skipNameCheck && (string.IsNullOrWhiteSpace(element.Name) && string.IsNullOrWhiteSpace(element.Target)))
            {
                _result.Add($"{typeof(T).Name} must have Name or Target.");
            }

            //if(string.IsNullOrWhiteSpace(element.Name) && (element.Aliases?.Count ?? 0) > 0)
            //{
            //    _result.Add($"{typeof(T).Name} must have Name.");
            //}            
        }

        private void CheckEmptyRequiresElements(BaseCommandLineArgument element)
        {
            if(element.Requires?.Any(string.IsNullOrWhiteSpace) ?? false)
            {
                _result.Add("There is empty item in requires of an option.");
            }
        }
    }
}
