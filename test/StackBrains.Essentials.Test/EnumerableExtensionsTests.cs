using System.Linq;
using NUnit.Framework;

namespace StackBrains.Essentials.Test
{
    public class EnumerableExtensionsTests
    {
        [Test]
        public void Test_DistinctBy()
        {
            var seq = Enumerable.Repeat(1, 2);
            var distinct = seq.DistinctBy(e => e);

            Assert.AreEqual(1, distinct.Count());
        }

        [Test]
        public void Test_ToDictionary()
        {
            var source = Enumerable.Range(0, 100);
            var dict = source.ToDictionary(
                x => x % 3,
                (x1, x2) => x1 + x2
            );
        }
    }
}