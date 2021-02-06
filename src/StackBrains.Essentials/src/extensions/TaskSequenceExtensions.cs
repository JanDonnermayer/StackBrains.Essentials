using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.Threading.Tasks
{

    public static class TaskSequenceExtensions
    {
        /// <summary>
        /// Lazily evaluates the results of the specified sequence of <see cref="Task{T}"/>.
        /// </summary>
        public static async Task<IEnumerable<T>> WhenAllSequentialAsync<T>(this IEnumerable<Task<T>> tasks)
        {
            if (tasks is null)
                throw new ArgumentNullException(nameof(tasks));

            var results = new List<T>();
            foreach (var task in tasks)
                results.Add(await task.ConfigureAwait(false));
            return results;
        }

        public static async Task WhenAllSequentialAsync(this IEnumerable<Task> tasks)
        {
            if (tasks is null)
                throw new ArgumentNullException(nameof(tasks));

            foreach (var task in tasks)
                await task.ConfigureAwait(false);
        }

        public static async Task<IEnumerable<T>> WhenAllAsync<T>(this IEnumerable<Task<T>> tasks)
        {
            if (tasks is null)
                throw new ArgumentNullException(nameof(tasks));

            return await Task
                .WhenAll(tasks)
                .ConfigureAwait(false);
        }

        public static async Task WhenAllAsync(this IEnumerable<Task> tasks)
        {
            if (tasks is null)
                throw new ArgumentNullException(nameof(tasks));

            await Task
                .WhenAll(tasks)
                .ConfigureAwait(false);
        }
    }
}
