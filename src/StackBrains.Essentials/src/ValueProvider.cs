using System;
using System.Collections.Generic;

namespace StackBrains.Essentials
{
    public static class ValueProvider
    {
        public static IValueProvider<TKey, TValue?> FromDictionary<TKey, TValue>(IDictionary<TKey, TValue> source)
        {
            TValue? GetValue(TKey key) =>
                source.TryGetValue(key, out var value)
                    ? value
                    : default;

            return Create<TKey, TValue?>(GetValue);
        }

        public static IValueProvider<TKey, TValue> Create<TKey, TValue>(Func<TKey, TValue> source) =>
            new FuncValueProvider<TKey, TValue>(source);
    }
}
