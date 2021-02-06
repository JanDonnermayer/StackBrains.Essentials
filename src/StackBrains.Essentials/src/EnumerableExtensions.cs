using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.Linq
{
    public static class EnumerableExtensions
    {
        public static Task<TElement> AggregateAsync<TElement>(
            this IEnumerable<TElement> source,
            Func<TElement, TElement, Task<TElement>> reduceAsync
        )
        {
            return AggregateAsync(
                source: source.Skip(1),
                seed: source.First(),
                reduceAsync: reduceAsync
            );
        }

        public static async Task<TState> AggregateAsync<TState, TElement>(
            this IEnumerable<TElement> source,
            TState seed,
            Func<TState, TElement, Task<TState>> reduceAsync
        )
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            if (reduceAsync is null)
                throw new ArgumentNullException(nameof(reduceAsync));

            var state = seed;

            foreach (var element in source)
            {
                state = await reduceAsync(state, element);
            }
            
            return state;
        }

        public static IEnumerable<T> Choose<T>(this IEnumerable<T?> source) =>
            source.Where(e => e != null).Select(e => e!);
    }
}
