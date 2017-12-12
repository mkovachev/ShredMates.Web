using System.Text.RegularExpressions;

namespace ShredMates.Web.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string ToFriendlyUrl(this string text)
            => Regex.Replace(text, @"[^A-Za-z0-9\.~]+", "-").ToLower();
    }
}
