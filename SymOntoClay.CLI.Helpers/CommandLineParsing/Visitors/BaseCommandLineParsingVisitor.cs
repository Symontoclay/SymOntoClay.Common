using SymOntoClay.CLI.Helpers.CommandLineParsing.Options;

namespace SymOntoClay.CLI.Helpers.CommandLineParsing.Visitors
{
    public abstract class BaseCommandLineParsingVisitor: ICommandLineParsingVisitor
    {
        /// <inheritdoc/>
        public void VisitCommandLineArgument(CommandLineArgument element)
        {
            OnVisitCommandLineArgument(element);
        }

        /// <inheritdoc/>
        public void VisitCommandLineGroup(CommandLineGroup element)
        {
            OnVisitCommandLineGroup(element);

            if(element.SubItems != null)
            {
                foreach(var item in element.SubItems)
                {
                    item.Accept(this);
                }
            }
        }

        /// <inheritdoc/>
        public void VisitCommandLineMutuallyExclusiveSet(CommandLineMutuallyExclusiveSet element)
        {
            OnVisitCommandLineMutuallyExclusiveSet(element);

            if (element.SubItems != null)
            {
                foreach (var item in element.SubItems)
                {
                    item.Accept(this);
                }
            }
        }

        /// <inheritdoc/>
        public void VisitCommandLineNamedGroup(CommandLineNamedGroup element)
        {
            OnVisitCommandLineNamedGroup(element);

            if (element.SubItems != null)
            {
                foreach (var item in element.SubItems)
                {
                    item.Accept(this);
                }
            }
        }

        /// <inheritdoc/>
        public void VisitCommandLineVirtualRootGroup(CommandLineVirtualRootGroup element)
        {
            OnVisitCommandLineVirtualRootGroup(element);

            if (element.SubItems != null)
            {
                foreach (var item in element.SubItems)
                {
                    item.Accept(this);
                }
            }
        }

        protected virtual void OnVisitCommandLineArgument(CommandLineArgument element)
        {
        }

        protected virtual void OnVisitCommandLineGroup(CommandLineGroup element)
        {
        }

        protected virtual void OnVisitCommandLineMutuallyExclusiveSet(CommandLineMutuallyExclusiveSet element)
        {
        }

        protected virtual void OnVisitCommandLineNamedGroup(CommandLineNamedGroup element)
        {
        }

        protected virtual void OnVisitCommandLineVirtualRootGroup(CommandLineVirtualRootGroup element)
        {
        }
    }
}
