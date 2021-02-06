using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.Linq
{
    public static class EnumerableExtensions
    {
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

        public static async Task<TState> AggregateAsync<TState, TElement>(
            this IEnumerable<TElement> source,
            TState seed,
            Func<TState, TElement, Task<TState>> reducer
        )
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            if (reducer is null)
                throw new ArgumentNullException(nameof(reducer));

            var state = seed;

            foreach (var element in source)
            {
                state = await reducer(state, element);
            }
            
            return state;
        }

        public static IEnumerable<T> Choose<T>(this IEnumerable<T?> source) =>
            source.Where(e => e != null).Select(e => e!);
    }
}
