using System.Threading.Tasks;
using Newtonsoft.Json;

namespace System.IO
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

        public static async Task<T> ReadJsonAsync<T>(this Stream stream)
        {
            if (stream is null)
                throw new ArgumentNullException(nameof(stream));

            var json = await stream
                .ReadAllTextAsync()
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
