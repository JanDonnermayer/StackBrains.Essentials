namespace System
{
    public static class Func
    {
        public static Func<T1, TOut> New<T1, TOut>(Func<T1, TOut> source) => source;

        public static Func<T1, T2, TOut> New<T1, T2, TOut>(Func<T1, T2, TOut> source) => source;

        public static Func<T1, T2, T3, TOut> New<T1, T2, T3, TOut>(Func<T1, T2, T3, TOut> source) => source;
    }
}
