using System.Threading.Tasks;

using static System.Func;

namespace System
{
    public static class FuncExtensions
    {
        public static Func<TOut> Map<TIn, TOut>(this Func<TIn> source, Func<TIn, TOut> map) =>
            New(() => map(source()));

        public static Func<T> Tap<T>(this Func<T> source, Action<T> tap) =>
            New(() => { var res = source(); tap(res); return res; });

        public static Lazy<T> ToLazy<T>(this Func<T> source) => new(source);

        public static Task<T> ToTask<T>(this Func<T> source) => new(source);

        public static Func<TOut> Catch<T, TOut, TException>(
            this Func<T> source,
            Func<T, TOut> onOk,
            Func<TException, TOut> onError
        ) where TException : Exception
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            if (onOk is null)
                throw new ArgumentNullException(nameof(onOk));

            if (onError is null)
                throw new ArgumentNullException(nameof(onError));

            TOut Invoke()
            {
                try
                {
                    return onOk(source.Invoke());
                }
                catch (TException ex)
                {
                    return onError(ex);
                }
            }

            return New(Invoke);
        }

        public static Func<TOut> Catch<T, TOut>(
            this Func<T> source,
            Func<T, TOut> onOk,
            Func<Exception, TOut> onError
        ) => source.Catch<T, TOut, Exception>(
            onOk: onOk,
            onError: onError
        );

        public static Func<T> Catch<T, TException>(
            this Func<T> source,
            Action<T> onOk,
            Func<TException, T> onError
        ) where TException : Exception
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            if (onOk is null)
                throw new ArgumentNullException(nameof(onOk));

            if (onError is null)
                throw new ArgumentNullException(nameof(onError));

            return source.Catch(
                ok => ok.Tap(onOk),
                onError
            );
        }

        public static Func<T> Catch<T>(
            this Func<T> source,
            Action<T> onOk,
            Func<Exception, T> onError
        ) => source.Catch(
            ok => ok.Tap(onOk),
            onError
        );

        private static T Tap<T>(this T source, Action<T> tap)
        {
            tap(source);
            return source;
        }
    }
}
