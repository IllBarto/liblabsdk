using System.Diagnostics.CodeAnalysis;

namespace LibLab.Interview.Lotr.SDK;

[ExcludeFromCodeCoverage]
internal static class Constants
{
    public static class ApiAccess
    {
        public const string ApiURL = "https://the-one-api.dev/v2/";
        public static string ApiToken = "zRwAI00CcUGGSD5CJi6A";
    }

    public static class ApiEndpoints
    {
        public const string Movie = "movie";
        public const string MovieById = "movie/{0}";
        public const string MovieQuotes = "movie/{0}/quote";
    }
}