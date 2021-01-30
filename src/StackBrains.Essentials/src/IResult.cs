using System;
using System.Security.Claims;

namespace StackBrains.Essentials
{
    public interface IResult
    {
        bool Success { get; }

        string? Message { get; }
    }

    public interface IResult<out T> : IResult where T : class
    {
        public T? Data { get; }
    }
}