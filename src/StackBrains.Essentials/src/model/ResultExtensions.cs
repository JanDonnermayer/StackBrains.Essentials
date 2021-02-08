using System;
using System.Threading.Tasks;

namespace StackBrains.Essentials
{
    public static class ResultExtensions
    {
        public static TOut Map<TOk, TError, TOut>(
            this IResult<TOk, TError> result,
            Func<TOk, TOut> okMapper,
            Func<TError, TOut> errorMapper
        )
        {
            if (result is null)
                throw new ArgumentNullException(nameof(result));

            if (okMapper is null)
                throw new ArgumentNullException(nameof(okMapper));

            if (errorMapper is null)
                throw new ArgumentNullException(nameof(errorMapper));

            return result.Success
                ? okMapper(result.GetOk())
                : errorMapper(result.GetError());
        }

        public static async Task<TOut> MapAsync<TOk, TError, TOut>(
            this Task<IResult<TOk, TError>> resultTask,
            Func<TOk, TOut> okMapper,
            Func<TError, TOut> errorMapper
        )
        {
            if (resultTask is null)
                throw new ArgumentNullException(nameof(resultTask));

            var res = await resultTask.ConfigureAwait(false);
            return res.Map(okMapper, errorMapper);
        }
    }
}
