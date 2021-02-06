using System.Collections.Generic;
using System.Linq;

namespace StackBrains.Essentials
{
    public static class Seq
    {
        public static IEnumerable<T> Of<T>(params T[] elements) => elements;

        public static Dictionary<TKey, TValue> Map<TKey, TValue>(params (TKey, TValue)[] elements)
            where TKey : notnull
        {
            return new Dictionary<TKey, TValue>(
                elements.ToDictionary(
                    e => e.Item1,
                    e => e.Item2
                )
            );
        }

    }
}
