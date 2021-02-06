namespace StackBrains.Essentials
{
    public interface IValueProvider<TKey, TValue>
    {
        public TValue Get(TKey key);
    }
}
