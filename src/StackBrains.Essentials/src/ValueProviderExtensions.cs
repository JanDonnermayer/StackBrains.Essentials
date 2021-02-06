using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace StackBrains.Essentials
{
    public static class ValueProviderExtensions
    {
        public static IValueProvider<TKey, TValue?> OrElse<TKey, TValue>(
            this IValueProvider<TKey, TValue?> first,
            params IValueProvider<TKey, TValue?>[] next
        )
        {
            return Seq.Of(first).Concat(next).FirstOrDefault();
        }

        public static IValueProvider<TKey, TValue?> FirstOrDefault<TKey, TValue>(
            this IEnumerable<IValueProvider<TKey, TValue?>> source
        )
        {
            TValue? GetValue(TKey key) => source
                .Select(v => v.Get(key))
                .FirstOrDefault(o => o != null);

            return ValueProvider.Create<TKey, TValue?>(GetValue);
        }

        public static IValueProvider<TKey, Task<TValue?>> OrElse<TKey, TValue>(
            this IValueProvider<TKey, Task<TValue?>> first,
            params IValueProvider<TKey, Task<TValue?>>[] next
        )
        {
            return Seq.Of(first).Concat(next).FirstOrDefault();
        }

        public static IValueProvider<TKey, Task<TValue?>> FirstOrDefault<TKey, TValue>(
            this IEnumerable<IValueProvider<TKey, Task<TValue?>>> source
        )
        {
            async Task<TValue?> GetValue(TKey key) => await source
                .Skip(1)
                .AggregateAsync(
                    seed: await source.First().Get(key),
                    reduceAsync: async (res, src) => res ?? await src.Get(key)
                );

            return ValueProvider.Create<TKey, Task<TValue?>>(GetValue);
        }

        public static IValueProvider<TKey, Task<TValue?>> OrElseTry<TKey, TValue>(
            this IValueProvider<TKey, Task<TValue?>> first,
            params IValueProvider<TKey,  Task<TValue?>>[] next
        )
        {
            async Task<TValue?> GetValue(TKey key) {
                return await next.AggregateAsync(
                    await first.Get(key),
                    async (val, next) => val ?? await next.Get(key)
                );
            }

            return ValueProvider.Create<TKey, Task<TValue?>>(GetValue);
        }

        public static IValueProvider<TKey, TValue> WithMemoryCache<TKey, TValue>(
            this IValueProvider<TKey, TValue> source
        ) where TKey : notnull
        {
            var dict = ImmutableDictionary<TKey, TValue>.Empty;

            TValue GetValue(TKey key) =>
                ImmutableInterlocked.GetOrAdd(
                    location: ref dict,
                    key: key,
                    valueFactory: source.Get
                );

            return ValueProvider.Create<TKey, TValue>(GetValue);
        }

        public static IValueProvider<TKey, Task<TValue>> ToAsync<TKey, TValue>(
            this IValueProvider<TKey, TValue> source
        )
        {
            Task<TValue> GetValue(TKey key) =>
                Task.FromResult(source.Get(key));

            return ValueProvider.Create<TKey, Task<TValue>>(GetValue);
        }
    }
}
