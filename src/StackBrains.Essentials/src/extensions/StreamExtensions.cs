using System.IO;
using System.Threading.Tasks;

namespace System
{
    public static class StreamExtensions
    {
        public static async Task<string> ReadAllTextAsync(this Stream stream)
        {
            if (stream is null)
                throw new ArgumentNullException(nameof(stream));

            using var sr = new StreamReader(stream);
            return await sr.ReadToEndAsync().ConfigureAwait(false);
        }
    }
}
