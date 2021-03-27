using System;
using System.Collections.Generic;
using System.Linq;

namespace StackBrains.Essentials
{
    public static class Seq
    {
        public static IEnumerable<T> Of<T>(params T[] elements) => elements;

        public static IDictionary<TKey, TValue> Map<TKey, TValue>(params (TKey Key, TValue Value)[] elements)
            where TKey : notnull
        {
            return elements.ToDictionary(
                keySelector: e => e.Key,
                elementSelector: e => e.Value
            );
        }

        public static IEnumerable<TElement> Unfold<TState, TElement>(
            TState seed,
            Func<TState, (TState state, TElement element)> unfolder,
            Func<TState, bool> stop
        )
        {
            var state = seed;

            while (!stop(state))
            {
                TElement element;
                (state, element) = unfolder(state);
                yield return element;
            }
        }
    }
}
