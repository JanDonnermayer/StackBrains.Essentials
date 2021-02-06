using System;
using System.Threading.Tasks;

namespace StackBrains.Essentials
{
    public static class Opt
    {
        public static Opt<T> Some<T>(T payload) 
            => new(payload);

        public static Opt<T> None<T>() 
             => new();

        public static Opt<T> FromNullable<T>(T? source) 
            => source is null ? None<T>() : Some(source);
    }

    public class Opt<T>
    {
        public bool IsSome { get; }

        private readonly T? payload;

        public Opt()
        {
            IsSome = false;
        }

        public Opt(T payload)
        {
            IsSome = true;
            this.payload = payload ?? throw new ArgumentNullException(nameof(payload));
        }

        public T GetValue() => !IsSome
            ? throw new Exception("The result has no value!")
            : payload!;

    }
}
