using System.Collections.Generic;

namespace ReportErrorParser.Logic
{
    public static class Extensions
    {
        private static readonly Dictionary<string, string> TimeZones =
            new Dictionary<string, string> { { "EST", "-05:00" }, { "PST", "-08:00" } };

        public static string ConvertTimeZone(this string s)
        {
            var tz = s.Substring(20, 3);
            return s.Replace(tz, TimeZones[tz]);
        }
    }
}