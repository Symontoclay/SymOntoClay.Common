/*MIT License

Copyright (c) 2020 - 2026 Sergiy Tolkachov

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.*/

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
