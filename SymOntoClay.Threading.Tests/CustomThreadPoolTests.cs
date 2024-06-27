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
