using System.Collections.Generic;

namespace System
{
    public static class ActionSequenceExtensions
    {
        public static Action<T> Combine<T>(this IEnumerable<Action<T>> actions)
        {
            if (actions is null)
                throw new ArgumentNullException(nameof(actions));

            return (T state) => { foreach (var action in actions) action.Invoke(state); };
        }

        public static Action Combine(this IEnumerable<Action> actions)
        {
            if (actions is null)
                throw new ArgumentNullException(nameof(actions));

            return () => { foreach (var action in actions) action.Invoke(); };
        }
    }
}
