using static System.Func;

namespace System
{
    public static class LazyExtensions
    {
        public static Lazy<TOut> Map<TIn, TOut>(
            this Lazy<TIn> source,
            Func<TIn, TOut> map
        )
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            if (map is null)
                throw new ArgumentNullException(nameof(map));

            return New(() => map(source.Value)).ToLazy();
        }

        public static Lazy<T> Tap<T>(
            this Lazy<T> source,
            Action<T> tap
        )
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            if (tap is null)
                throw new ArgumentNullException(nameof(tap));

            return New(() => source.Value).Tap(tap).ToLazy();
        }
    }
}
