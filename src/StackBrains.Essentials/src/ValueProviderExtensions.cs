using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace StackBrains.Essentials
{
    public static class ValueProviderExtensions
    {
        public static IValueProvider<TKey, TValue?> OrElseTry<TKey, TValue>(
            this IValueProvider<TKey, TValue?> first,
            params IValueProvider<TKey, TValue?>[] next
        )
        {
            TValue? GetValue(TKey key) => Seq
                .Of(first)
                .Concat(next)
                .Select(v => v.GetValue(key))
                .FirstOrDefault(o => o != null);

            return ValueProvider.Create<TKey, TValue?>(GetValue);
        }

        public static IValueProvider<TKey, Task<TValue?>> OrElseTry<TKey, TValue>(
            this IValueProvider<TKey, Task<TValue?>> first,
            params IValueProvider<TKey,  Task<TValue?>>[] next
        )
        {
            async Task<TValue?> GetValue(TKey key) {
                return await next.AggregateAsync(
                    await first.GetValue(key),
                    async (val, next) => val ?? await next.GetValue(key)
                );
            }

            return ValueProvider.Create<TKey, Task<TValue?>>(GetValue);
        }

        public static IValueProvider<TKey, TValue> WithCache<TKey, TValue>(
            this IValueProvider<TKey, TValue> source
        ) where TKey : notnull
        {
            var dict = ImmutableDictionary<TKey, TValue>.Empty;

            TValue GetValue(TKey key) =>
                ImmutableInterlocked.GetOrAdd(
                    location: ref dict,
                    key: key,
                    valueFactory: source.GetValue
                );

            return ValueProvider.Create<TKey, TValue>(GetValue);
        }

        public static IValueProvider<TKey, Task<TValue>> ToAsync<TKey, TValue>(
            this IValueProvider<TKey, TValue> source
        )
        {
            Task<TValue> GetValue(TKey key) =>
                Task.FromResult(source.GetValue(key));

            return ValueProvider.Create<TKey, Task<TValue>>(GetValue);
        }
    }
}
