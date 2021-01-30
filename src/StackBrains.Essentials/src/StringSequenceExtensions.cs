using System.Collections.Generic;

namespace System.Security.Claims
{
    public static class StringSequenceExtensions
    {
        public static string JoinString(this IEnumerable<string> sequence, string separator = "") =>
            string.Join(separator, sequence);
    }
}
