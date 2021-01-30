using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StackBrains.Essentials
{
    public static class ResultExtensions
    {
        public static TOut Map<TIn, TOut>(
            this IResult<TIn> result,
            Func<TIn, TOut> okMapper,
            Func<string, TOut> errorMapper
        ) where TIn : class
        {
            if (result is null)
                throw new ArgumentNullException(nameof(result));

            if (okMapper is null)
                throw new ArgumentNullException(nameof(okMapper));

            if (errorMapper is null)
                throw new ArgumentNullException(nameof(errorMapper));

            return result.Success
                ? okMapper(result.Data!)
                : errorMapper(result.Message!);
        }

        public static async Task<TOut> MapAsync<TIn, TOut>(
            this Task<IResult<TIn>> resultTask,
            Func<TIn, TOut> okMapper,
            Func<string, TOut> errorMapper
        ) where TIn : class
        {
            if (resultTask is null)
                throw new ArgumentNullException(nameof(resultTask));

            var res = await resultTask.ConfigureAwait(false);
            return res.Map(okMapper, errorMapper);
        }
    }
}
