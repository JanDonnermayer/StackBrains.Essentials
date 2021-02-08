namespace StackBrains.Essentials
{
    public interface IResult<TOk, out TError>
    {
        bool Success { get; }

        TError GetError();

        TOk GetOk();
    }
}