using System.Threading.Tasks;

namespace System
{
    public static class TaskExtensions
    {
        public static async Task<TOut> MapAsync<TIn, TOut>(
            this Task<TIn> task,
            Func<TIn, Task<TOut>> mapAsync
        )
        {
            if (task is null)
                throw new ArgumentNullException(nameof(task));

            if (mapAsync is null)
                throw new ArgumentNullException(nameof(mapAsync));

            return await mapAsync(await task);
        }

        public static async Task<TOut> MapAsync<TIn, TOut>(
            this Task<TIn> task,
            Func<TIn, TOut> map
        )
        {
            if (task is null)
                throw new ArgumentNullException(nameof(task));

            if (map is null)
                throw new ArgumentNullException(nameof(map));

            return map(await task);
        }

        public static async Task<T> TapAsync<T>(
            this Task<T> task,
            Func<T, Task> tapAsync
        )
        {
            if (task is null)
                throw new ArgumentNullException(nameof(task));

            if (tapAsync is null)
                throw new ArgumentNullException(nameof(tapAsync));

            var res = await task;
            await tapAsync(res);
            return res;
        }

        public static async Task<T> TapAsync<T>(
            this Task<T> task,
            Action<T> tap
        )
        {
            if (task is null)
                throw new ArgumentNullException(nameof(task));

            if (tap is null)
                throw new ArgumentNullException(nameof(tap));

            var res = await task;
            tap(res);
            return res;
        }
    }
}
