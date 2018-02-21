using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ReportErrorParser
{
    class Parser
    {
        public static string Parse(string input)
        {
            var startIndex = input.IndexOf("SQL query string:");
            var queryStart = input.Substring(startIndex + "SQL query string:".Length);

            var queryBuilder = new StringBuilder();
            var parameters = new List<string>();
            

            var a = queryStart.Replace("\r\n", "\n").Split('\n');

            foreach (var line in a)
            {
                if (line.Contains("DEBUG JRJdbcQueryExecuter"))
                {
                    if (line.Contains("Parameter #"))
                    {
                        parameters.Add(line);
                    }
                }
                else
                {
                    queryBuilder.AppendLine(line);
                }
            }

            var queryWithoutErrors = queryBuilder.ToString();
            foreach (var parameter in parameters)
            {
                string parsedParameter = "???????????????????";
                Regex reg = new Regex("");

                var v = new Dictionary<Action, int>()
                {
                    [() =>
                        {
                            parsedParameter = $" = {ParseParameterValue(parameter)} ";
                            reg = r1;
                        }
                    ] = queryWithoutErrors.IndexOf("= ?"),
                    [() =>
                        {
                            parsedParameter = $" {ParseParameterName(parameter)} = ";
                            reg = r2;
                        }
                    ] = queryWithoutErrors.IndexOf("? ="),
                    [() =>
                        {
                            parsedParameter = $"{ParseParameterName(parameter)} IS ";
                            reg = r3;
                        }
                    ] = queryWithoutErrors.IndexOf("? IS"),
                    [() =>
                        {
                            parsedParameter = $"({ParseParameterValue(parameter)} || {ParseParameterName(parameter)})";
                            reg = r4;
                        }
                    ] = queryWithoutErrors.IndexOf("(?)"),
                };

                if (v.All(x => x.Value == -1))
                {
                    throw new ApplicationException("No parameter to replace found for: " + parameter);
                }

                v.Where(x => x.Value != -1).OrderBy(x => x.Value).First().Key();
                
                queryWithoutErrors = reg.Replace(queryWithoutErrors, parsedParameter, 1);
            }

            return queryWithoutErrors;
        }

        static readonly Regex r1 = new Regex(Regex.Escape("= ?"));
        static readonly Regex r2 = new Regex(Regex.Escape("? ="));
        static readonly Regex r3 = new Regex(Regex.Escape("? IS"));
        static readonly Regex r4 = new Regex(Regex.Escape("(?)"));
        private const string stringDelimiter = "java.lang.String):";
        private const string dateDelimiter = "java.util.Date):";
        private const string booleanDelimiter = "java.lang.Boolean):";
        private const string bigdecimalDelimiter = "java.math.BigDecimal):";

        private static string ParseParameterValue(string parameterLine)
        {
            if (parameterLine.Contains(stringDelimiter))
            {
                var end = parameterLine.LastIndexOf(stringDelimiter);
                var actValue = parameterLine.Substring(end + stringDelimiter.Length).Trim();
                return $"'{actValue}'";
            }

            if (parameterLine.Contains(dateDelimiter))
            {
                var end = parameterLine.LastIndexOf(dateDelimiter);
                var actValue = parameterLine.Substring(end + dateDelimiter.Length).Trim().ConvertTimeZone();
                var dateValue = DateTime.ParseExact(actValue, "ddd MMM dd HH:mm:ss zzz yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
                return dateValue.ToString().Replace(".","-");
            }

            if (parameterLine.Contains(booleanDelimiter))
            {
                var end = parameterLine.LastIndexOf(booleanDelimiter);
                var actValue = parameterLine.Substring(end + booleanDelimiter.Length).Trim();

                if (actValue.Equals("true", StringComparison.InvariantCultureIgnoreCase))
                {
                    return " 1 ";
                }
                return $" 0 ";
            }

            if (parameterLine.Contains(bigdecimalDelimiter))
            {
                var end = parameterLine.LastIndexOf(bigdecimalDelimiter);
                var actValue = parameterLine.Substring(end + bigdecimalDelimiter.Length).Trim();
                return $" {actValue} ";
            }

            return "YYYYY";
        }

        private static string ParseParameterName(string parameterLine)
        {
            if (parameterLine.Contains(stringDelimiter))
            {
                var start = parameterLine.IndexOf(" (");
                var startOfValue = parameterLine.Substring(start + " (".Length);

                var end = startOfValue.IndexOf(" of type java.lang.String");
                var actValue = startOfValue.Substring(0, end);

                return $" {actValue} ";
            }

            if (parameterLine.Contains(dateDelimiter))
            {
                var start = parameterLine.IndexOf(" (");
                var startOfValue = parameterLine.Substring(start + " (".Length);

                var end = startOfValue.IndexOf(" of type java.util.Date");
                var actValue = startOfValue.Substring(0, end);

                return $" {actValue} ";
            }

            if (parameterLine.Contains(booleanDelimiter))
            {
                var start = parameterLine.IndexOf(" (");
                var startOfValue = parameterLine.Substring(start + " (".Length);

                var end = startOfValue.IndexOf(" of type java.lang.Boolean");
                var actValue = startOfValue.Substring(0, end);

                return $" {actValue} ";
            }

            if (parameterLine.Contains(bigdecimalDelimiter))
            {
                var start = parameterLine.IndexOf(" (");
                var startOfValue = parameterLine.Substring(start + " (".Length);

                var end = startOfValue.IndexOf(" of type java.math.BigDecimal");
                var actValue = startOfValue.Substring(0, end);

                return $" {actValue} ";
            }

            return "ZZZZZ";
        }
    }

    public static class Extensions
    {
        private static Dictionary<string, string> _timeZones =
            new Dictionary<string, string> { { "EST", "-05:00" }, { "PST", "-08:00" } };

        public static string ConvertTimeZone(this string s)
        {
            var tz = s.Substring(20, 3);
            return s.Replace(tz, _timeZones[tz]);
        }
    }
}
