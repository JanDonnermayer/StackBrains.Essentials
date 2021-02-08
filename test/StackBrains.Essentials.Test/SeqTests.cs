using NUnit.Framework;

namespace StackBrains.Essentials.Test
{
    public class SeqTests
    {
        [Test]
        public void When_MapWithValidValues_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => Seq.Map(("key", "val")));
        }
    }
}