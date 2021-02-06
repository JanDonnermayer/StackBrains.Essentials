using System;

namespace StackBrains.Essentials
{

    internal class FuncValueProvider<TKey, TValue> : IValueProvider<TKey, TValue>
    {
        private readonly Func<TKey, TValue> source;

        public FuncValueProvider(Func<TKey, TValue> source) => this.source = source;

        public TValue GetValue(TKey key) => source(key);
    }
}
