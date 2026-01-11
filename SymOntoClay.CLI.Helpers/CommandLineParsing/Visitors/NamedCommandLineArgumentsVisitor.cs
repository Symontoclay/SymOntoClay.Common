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
    public class NamedCommandLineArgumentsVisitor: BaseCommandLineParsingVisitor
    {
#if DEBUG
        //private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
#endif

        private List<BaseNamedCommandLineArgument> _result;
        private object _lockObj = new object();

        public List<BaseNamedCommandLineArgument> Run(CommandLineVirtualRootGroup rootElement)
        {
            lock(_lockObj)
            {
                if (_result != null)
                {
                    throw new Exception($"{nameof(NamedCommandLineArgumentsVisitor)} can not be used in multiple threads.");
                }

                _result = new List<BaseNamedCommandLineArgument>();
            }

            rootElement.Accept(this);

            var result = _result;
            _result = null;

            return result;
        }

        /// <inheritdoc/>
        protected override void OnVisitCommandLineArgument(CommandLineArgument element)
        {
#if DEBUG
            //_logger.Info($"element = {element}");
#endif

            _result.Add(element);
        }

        /// <inheritdoc/>
        protected override void OnVisitCommandLineNamedGroup(CommandLineNamedGroup element)
        {
#if DEBUG
            //_logger.Info($"element = {element}");
#endif

            _result.Add(element);
        }
    }
}
