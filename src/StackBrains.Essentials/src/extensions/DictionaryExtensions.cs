using System.Collections.Generic;

namespace System
{
    public static class DictionaryExtensions
    {
        public static TValue AddOrUpdate<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            TKey key,
            Func<TValue> addValue,
            Func<TValue, TValue> updateValue
        )
        {
            if (dictionary is null)
                throw new ArgumentNullException(nameof(dictionary));

            if (addValue is null)
                throw new ArgumentNullException(nameof(addValue));

            if (updateValue is null)
                throw new ArgumentNullException(nameof(updateValue));

            return dictionary.TryGetValue(key, out var current)
                ? updateValue(current)
                : addValue();
        }

        public static TValue GetOrAdd<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            TKey key,
            Func<TValue> addValue
        ) {
            return dictionary.AddOrUpdate(
                key: key,
                addValue: addValue,
                updateValue: value => value
            );
        }

        public static IDictionary<TKey, TValue> Modify<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            Action<IDictionary<TKey, TValue>> modify
        ) {
            modify(dictionary);
            return dictionary;
        }
    }
}
