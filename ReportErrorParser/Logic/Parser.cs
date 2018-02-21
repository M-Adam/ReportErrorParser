using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ReportErrorParser.Logic
{
    public static partial class Parser
    {
        private const string EqualsFirst = "= ?";
        private const string ExclFirst = "? =";
        private const string IsFirst = "? IS";
        private const string BracesFirst = "(?)";
        private static readonly Regex EqualsFirstRegex = new Regex(Regex.Escape(EqualsFirst));
        private static readonly Regex ExclFirstRegex = new Regex(Regex.Escape(ExclFirst));
        private static readonly Regex IsFirstRegex = new Regex(Regex.Escape(IsFirst));
        private static readonly Regex BracesFirstRegex = new Regex(Regex.Escape(BracesFirst));

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
            var parsedParameter = "?";
            var reg = new Regex("");

            var replacementActions = new Dictionary<Action, int>()
            {
                [() =>
                {
                    parsedParameter = $" = {parameter.Value} ";
                    reg = EqualsFirstRegex;
                }
                ] = queryWithoutParameters.IndexOf(EqualsFirst),
                [() =>
                {
                    parsedParameter = $" {parameter.Value} = ";
                    reg = ExclFirstRegex;
                }
                ] = queryWithoutParameters.IndexOf(ExclFirst),
                [() =>
                {
                    parsedParameter = $" {parameter.Value} IS ";
                    reg = IsFirstRegex;
                }
                ] = queryWithoutParameters.IndexOf(IsFirst),
                [() =>
                {
                    parsedParameter = $"({parameter.Value})";
                    reg = BracesFirstRegex;
                }
                ] = queryWithoutParameters.IndexOf(BracesFirst),
            };

            if (replacementActions.All(x => x.Value == -1))
            {
                throw new ApplicationException("No parameter to replace found for: " + parameter);
            }

            replacementActions.Where(x => x.Value != -1).OrderBy(x => x.Value).First().Key();
            queryWithoutParameters = reg.Replace(queryWithoutParameters, parsedParameter, 1);
            return queryWithoutParameters;
        }
    }
}
