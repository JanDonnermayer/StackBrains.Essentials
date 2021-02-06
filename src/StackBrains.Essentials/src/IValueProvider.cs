namespace StackBrains.Essentials
{
    public interface IValueProvider<TKey, TValue>
    {
        public TValue GetValue(TKey key);
    }
}
