using System.Text;
using SymOntoClay.Common.DebugHelpers;

namespace SymOntoClay.Common.Tests
{
    public class DisplayHelper_Tests
    {
        [Test]
        public void PrintExisting()
        {
            var n = 0u;

            var sb = new StringBuilder();

            object obj1 = null;

            sb.PrintExisting(n, nameof(obj1), obj1);

            Assert.That(sb.ToString().Contains("obj1 = No"), Is.EqualTo(true));

            sb = new StringBuilder();

            var obj2 = new object();

            sb.PrintExisting(n, nameof(obj2), obj2);

            Assert.That(sb.ToString().Contains("obj2 = Yes"), Is.EqualTo(true));

            sb = new StringBuilder();

            var list1 = new List<string>();

            sb.PrintExisting(n, nameof(list1), list1);

            Assert.That(sb.ToString().Contains("list1 = No"), Is.EqualTo(true));

            sb = new StringBuilder();

            List<string> list2 = null;

            sb.PrintExisting(n, nameof(list2), list2);

            Assert.That(sb.ToString().Contains("list2 = No"), Is.EqualTo(true));

            sb = new StringBuilder();

            var list3 = new List<string>()
            {
                "Hi!"
            };

            sb.PrintExisting(n, nameof(list3), list3);

            Assert.That(sb.ToString().Contains("list3 = Yes"), Is.EqualTo(true));

            sb = new StringBuilder();

            var str1 = "";

            sb.PrintExisting(n, nameof(str1), str1);

            Assert.That(sb.ToString().Contains("str1 = No"), Is.EqualTo(true));

            sb = new StringBuilder();

            string str2 = null;

            sb.PrintExisting(n, nameof(str2), str2);

            Assert.That(sb.ToString().Contains("str2 = No"), Is.EqualTo(true));

            sb = new StringBuilder();

            var str3 = "Hi!";

            sb.PrintExisting(n, nameof(str3), str3);

            Assert.That(sb.ToString().Contains("str3 = Yes"), Is.EqualTo(true));
        }
    }
}