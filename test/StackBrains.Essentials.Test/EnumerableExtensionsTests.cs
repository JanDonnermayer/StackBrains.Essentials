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
    }
}