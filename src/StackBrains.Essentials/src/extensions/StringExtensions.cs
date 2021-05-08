using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace System
{
    public static class StringExtensions
    {
        public static string Remove(this string source, string expression) =>
            source.Replace(expression, string.Empty);

        public static string Remove(this string source, params string[] expressions) =>
            expressions.Aggregate(source, Remove);

        public static string Remove(this string source, IEnumerable<string> expressions) =>
            expressions.Aggregate(source, Remove);

        public static string RegexReplace(this string source, string pattern, string replacement) =>
            Regex.Replace(source, pattern, replacement);

        public static string Aggregate(
            this string seed,
            Func<string, string, string> aggregator,
            IEnumerable<string> elements
        ) => elements.Aggregate(seed, aggregator);
    }
}
