using System.Collections.Generic;
using System.Linq;

namespace StackBrains.Essentials
{
    public static class Seq
    {
        public static IEnumerable<T> Of<T>(params T[] elements) => elements;

        public static IDictionary<TKey, TValue> Map<TKey, TValue>(params (TKey, TValue)[] elements)
            where TKey : notnull
        {
            return elements.ToDictionary(
                keySelector: e => e.Item1,
                elementSelector: e => e.Item2
            );
        }
    }
}
