using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ReportErrorParser.Logic
{
    public static class Parser
    {
        public static string Parse(string input)
        {
            var startIndex = input.IndexOf("SQL query string:");
            var queryStart = input.Substring(startIndex + "SQL query string:".Length);
            var queryBuilder = new StringBuilder();
            var parameters = new List<QueryParameter>();
            var queryLines = queryStart.Replace("\r\n", "\n").Split('\n');

            foreach (var line in queryLines)
            {
                if (line.Contains("DEBUG JRJdbcQueryExecuter"))
                {
                    if (line.Contains("Parameter #"))
                    {
                        parameters.Add(new QueryParameter(line));
                    }
                }
                else
                {
                    queryBuilder.AppendLine(line);
                }
            }

            var queryWithoutParameters = queryBuilder.ToString();
            foreach (var parameter in parameters)
            {
                queryWithoutParameters = ReplaceParameterInTheQuery(queryWithoutParameters, parameter);
            }

            return queryWithoutParameters;
        }

        private static string ReplaceParameterInTheQuery(string queryWithoutParameters, QueryParameter parameter)
        {
            if (!queryWithoutParameters.Contains("?"))
            {
                throw new ApplicationException($"No parameter '?' found to replace {parameter}");
            }

            var parameterRegex = new Regex(Regex.Escape("?"));
            queryWithoutParameters = parameterRegex.Replace(queryWithoutParameters, parameter.Value, 1);
            return queryWithoutParameters;
        }
    }
}
