using System.Collections.Generic;
using System.Linq;

namespace System
{
    public static class StringSequenceExtensions
    {
        public static string JoinString(this IEnumerable<string> sequence, string separator = "") =>
            string.Join(separator, sequence);

        public static IEnumerable<string> WhereNonEmpty(this IEnumerable<string> sequence) =>
            sequence.Where(s => !string.IsNullOrEmpty(s));
    }
}
