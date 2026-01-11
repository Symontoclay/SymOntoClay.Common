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

using System.Collections.Concurrent;

namespace SymOntoClay.Threading.Tests
{
    public class CustomThreadPoolTests
    {
        [Test]
        public void RunMultipleTimes_ExecuteEverithing()
        {
            var timeoutBetweenSets = 10000;
            var itemTimeout = 100;

            using var threadPool = new CustomThreadPool(0, 20);

            var case1BeginList = new ConcurrentBag<int>();
            var case1EndList = new ConcurrentBag<int>();

            var case2BeginList = new ConcurrentBag<int>();
            var case2EndList = new ConcurrentBag<int>();

            var count = 200;

            foreach (var n in Enumerable.Range(1, count))
            {
                case1BeginList.Add(n);

                threadPool.Run(() => {
                    Thread.Sleep(itemTimeout);
                    case1EndList.Add(n);
                });
            }

            Thread.Sleep(timeoutBetweenSets);

            Assert.That(case1BeginList.Count, Is.EqualTo(count));
            Assert.That(case1EndList.Count, Is.EqualTo(count));

            foreach (var n in Enumerable.Range(1, count))
            {
                case2BeginList.Add(n);

                threadPool.Run(() => {
                    Thread.Sleep(itemTimeout);
                    case2EndList.Add(n);
                });
            }

            Thread.Sleep(timeoutBetweenSets);

            Assert.That(case2BeginList.Count, Is.EqualTo(count));
            Assert.That(case2EndList.Count, Is.EqualTo(count));
        }
    }
}
