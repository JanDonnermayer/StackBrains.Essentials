using System;

namespace StackBrains.Essentials
{
    public static class Result
    {
        public static Result<TOk, TError> Ok<TOk, TError>(TOk ok)
            => new(ok: ok);

        public static Result<TOk, TError> Error<TOk, TError>(TError error)
            => new(error: error);
    }

    public class Result<TOk, TError> : IResult<TOk, TError>
    {
        public bool Success { get; }

        private readonly TError? error;

        private readonly TOk? ok;

        public Result(TOk ok)
        {
            Success = true;
            this.ok = ok ?? throw new ArgumentNullException(nameof(ok));
        }

        public Result(TError error)
        {
            Success = false;
            this.error = error ?? throw new ArgumentNullException(nameof(error));
        }

        public TOk GetOk() => Success
            ? ok!
            : throw new Exception("The result has no Ok-payload!");

        public TError GetError() => Success
            ? throw new Exception("The result has no Error-payload!")
            : error!;
    }
}