using System.Text.RegularExpressions;

namespace Fb2Library.Domain.Shared
{
    public static partial class RegexPatterns
    {
        [GeneratedRegex(@"\s+")]
        public static partial Regex WhitespaceRegex();
    }
}
