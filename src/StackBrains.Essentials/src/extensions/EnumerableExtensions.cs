using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.Linq
{
    public static class EnumerableExtensions
    {
        public static async Task<TState> AggregateAsync<TState, TElement>(
            this IEnumerable<TElement> source,
            TState seed,
            Func<TState, TElement, Task<TState>> reducer,
            Func<TState, bool> stop
        )
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            if (reducer is null)
                throw new ArgumentNullException(nameof(reducer));

            if (stop is null)
                throw new ArgumentNullException(nameof(stop));

            var state = seed;

            foreach (var element in source)
            {
                state = await reducer(state, element)
                    .ConfigureAwait(false);
                if (stop(state))
                    return state;
            }

            return state;
        }

        public static Task<TState> AggregateAsync<TState, TElement>(
            this IEnumerable<TElement> source,
            TState seed,
            Func<TState, TElement, Task<TState>> reducer
        )
        {
            return AggregateAsync(
                source: source,
                seed: seed,
                reducer: reducer,
                stop: _ => false
            );
        }

        public static Task<TElement> AggregateAsync<TElement>(
            this IEnumerable<TElement> source,
            Func<TElement, TElement, Task<TElement>> reducer
        )
        {
            return AggregateAsync(
                source: source.Skip(1),
                seed: source.First(),
                reducer: reducer
            );
        }

        public static Task<bool> AnyAsync<T>(
            this IEnumerable<T> source,
            Func<T, Task<bool>> asyncPredicate
        )
        {
            return AggregateAsync(
                source: source,
                seed: false,
                reducer: async (s, e) => s && await asyncPredicate(e),
                stop: s => s
            );
        }

        public static async Task<T?> FirstOrDefaultAsync<T>(
            this IEnumerable<T> source,
            Func<T, Task<bool>> asyncPredicate,
            T? @default = default
        )
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            if (asyncPredicate is null)
                throw new ArgumentNullException(nameof(asyncPredicate));

            foreach (var item in source)
                if (await asyncPredicate(item)) return item;

            return @default;
        }

        public static IDictionary<TKey, TValue> ToDictionary<TSource, TKey, TValue>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TValue> valueSelector,
            Func<TValue, TValue, TValue> reducer
        )
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            if (keySelector is null)
                throw new ArgumentNullException(nameof(keySelector));

            if (reducer is null)
                throw new ArgumentNullException(nameof(reducer));

            TValue Reduce(IEnumerable<TSource> source) =>
                source.Select(valueSelector).Aggregate(reducer);

            return source
                .GroupBy(keySelector)
                .ToDictionary(g => g.Key, Reduce);
        }

        public static IDictionary<TKey, TValue> ToDictionary<TKey, TValue>(
            this IEnumerable<TValue> source,
            Func<TValue, TKey> keySelector,
            Func<TValue, TValue, TValue> reducer
        )
        {
            return source.ToDictionary(
                keySelector: keySelector,
                valueSelector: val => val,
                reducer: reducer
            );
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector
        )
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            if (keySelector is null)
                throw new ArgumentNullException(nameof(keySelector));

            return source.GroupBy(keySelector).Select(g => g.First());
        }

        /// <summary>
        /// Returns elements that are not null
        /// </summary>
        public static IEnumerable<T> Choose<T>(this IEnumerable<T?> source) =>
            source.Where(e => e != null).Select(e => e!);
    }
}
