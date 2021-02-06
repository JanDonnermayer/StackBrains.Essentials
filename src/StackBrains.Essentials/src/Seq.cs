using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace StackBrains.Essentials
{
    public static class Seq
    {
        public static IEnumerable<T> Of<T>(params T[] elements) => elements;

        public static IImmutableDictionary<TKey, TValue> Map<TKey, TValue>(params (TKey, TValue)[] elements)
            where TKey : notnull
        {
            return elements.ToImmutableDictionary(
                keySelector: e => e.Item1,
                elementSelector: e => e.Item2
            );
        }

    }
}
