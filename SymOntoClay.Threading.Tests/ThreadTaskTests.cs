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
