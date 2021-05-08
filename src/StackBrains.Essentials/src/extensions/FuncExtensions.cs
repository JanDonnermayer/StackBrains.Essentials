using System.Threading.Tasks;

namespace System
{
    public static class FuncExtensions
    {
        public static Func<TOut> Map<TIn, TOut>(this Func<TIn> source, Func<TIn, TOut> map) =>
            Func.New(() => map(source()));

        public static Func<T> Tap<T>(this Func<T> source, Action<T> tap) =>
            Func.New(() => { var res = source(); tap(res); return res; });

        public static Lazy<T> ToLazy<T>(this Func<T> source) => new(source);

        public static Task<T> ToTask<T>(this Func<T> source) => new(source);
    }
}
