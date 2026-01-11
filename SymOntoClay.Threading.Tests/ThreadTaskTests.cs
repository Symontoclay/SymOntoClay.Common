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

namespace SymOntoClay.Threading.Tests
{
    public class ThreadTaskTests
    {
        [Test]
        public void RunThreadTaskWithoutResultWithThreadPool_Success()
        {
            using var threadPool = new CustomThreadPool(0, 20);

            var task = new ThreadTask(() => {
            }, threadPool);

            task.Start();

            Thread.Sleep(1000);
        }

        [Test]
        public void RunThreadTaskWithResultWithThreadPool_Success()
        {
            using var threadPool = new CustomThreadPool(0, 20);

            var task = ThreadTask<int>.Run(() => { return 16; }, threadPool);

            var result = task.Result;

            Assert.That(result, Is.EqualTo(16));
        }

        [Test]
        public void RunThreadTaskWithoutResultWithoutThreadPool_Success()
        {
            var task = new ThreadTask(() => {
            });

            task.Start();

            Thread.Sleep(1000);
        }

        [Test]
        public void RunThreadTaskWithResultWithoutThreadPool_Success()
        {
            var task = ThreadTask<int>.Run(() => { return 16; });

            var result = task.Result;

            Assert.That(result, Is.EqualTo(16));
        }
    }
}
