
namespace System.Collections.Generic
{
    public class DelegationEqualityComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T?, T?, bool> equals;

        private readonly Func<T, int> getHashCode;

        public DelegationEqualityComparer(Func<T?, T?, bool> equals, Func<T, int> getHashCode)
        {
            this.equals = equals ?? throw new ArgumentNullException(nameof(equals));
            this.getHashCode = getHashCode ?? throw new ArgumentNullException(nameof(getHashCode));
        }

        public DelegationEqualityComparer(Func<T?, T?, bool> equals)
            : this(equals, o => o?.GetHashCode() ?? 0) { }

        public bool Equals(T? x, T? y) => equals(x, y);

        public int GetHashCode(T obj) => getHashCode(obj);

        public static DelegationEqualityComparer<T> Create(Func<T?, T?, bool> equals) =>
            new(equals);

        public static DelegationEqualityComparer<T> Create(Func<T?, T?, bool> equals, Func<T, int> getHashCode) =>
            new(equals, getHashCode);
    }
}
