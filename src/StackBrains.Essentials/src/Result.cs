namespace StackBrains.Essentials
{
    public abstract class Result : IResult
    {
        protected Result()
        {
            Success = true;
            Message = "";
        }

        protected Result(string errorMessage)
        {
            Success = false;
            Message = errorMessage;
        }

        public bool Success { get; }

        public string? Message { get; }
    }

    public abstract class Result<T> : IResult<T> where T : class
    {
        protected Result(T data)
        {
            Success = true;
            Message = "";
            Data = data;
        }

        protected Result(string errorMessage)
        {
            Success = false;
            Message = errorMessage;
            Data = default;
        }

        public bool Success { get; }

        public string? Message { get; }

        public T? Data { get; }
    }
}