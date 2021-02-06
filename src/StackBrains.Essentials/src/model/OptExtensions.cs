using System;

namespace StackBrains.Essentials
{
    public static class OptExtensions
    {
        
        public static Opt<TOut> Then<TIn, TOut>(this Opt<TIn> opt, Func<TIn, TOut> valueMapper) 
        {
            if (opt is null)
                throw new ArgumentNullException(nameof(opt));

            if (valueMapper is null)
                throw new ArgumentNullException(nameof(valueMapper));

            return opt.Then<TIn, TOut>(val => new(valueMapper(val)));
        }

        public static Opt<TOut> Then<TIn, TOut>(this Opt<TIn> opt, Func<TIn, Opt<TOut>> valueMapper) 
        {
            if (opt is null)
                throw new ArgumentNullException(nameof(opt));

            if (valueMapper is null)
                throw new ArgumentNullException(nameof(valueMapper));

            return opt.IsSome ? valueMapper(opt.GetValue()!) : new();
        }

        public static Opt<TIn> OrElse<TIn>(this Opt<TIn> opt, Func<Opt<TIn>> second) 
        {
            if (opt is null)
                throw new ArgumentNullException(nameof(opt));

            if (second is null)
                throw new ArgumentNullException(nameof(second));

            return opt.IsSome ? opt : second();
        }
    }
}
