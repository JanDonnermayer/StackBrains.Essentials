namespace System
{
    public class Disposable : IDisposable
    {
        private readonly Action dispose;

        public Disposable(Action dispose)
        {
            this.dispose = dispose ?? throw new ArgumentNullException(nameof(dispose));
        }

        public void Dispose() => dispose();

        public static Disposable Create(Action dispose) => new(dispose);

        public static Disposable Empty => new(() => {});
    }
}
