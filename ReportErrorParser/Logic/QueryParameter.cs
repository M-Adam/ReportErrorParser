using System;
using System.Collections.Generic;
using System.Globalization;

namespace ReportErrorParser.Logic
{
    public class QueryParameter
    {
        private const string StringDelimiter = "java.lang.String):";
        private const string DateDelimiter = "java.util.Date):";
        private const string BooleanDelimiter = "java.lang.Boolean):";
        private const string BigdecimalDelimiter = "java.math.BigDecimal):";

        public string Name { get; private set; }
        public string Value { get; private set; }
        public QueryParameterType Type { get; private set; }

        public QueryParameter(string parameterLine)
        {
            SetParameterType(parameterLine);
            SetValue(parameterLine);
            SetName(parameterLine);
        }

        private void SetParameterType(string parameterLine)
        {
            if (parameterLine.Contains(StringDelimiter))
            {
                Type = QueryParameterType.String;
            }
            else if (parameterLine.Contains(DateDelimiter))
            {
                Type = QueryParameterType.Date;
            }
            else if (parameterLine.Contains(BooleanDelimiter))
            {
                Type = QueryParameterType.Boolean;
            }
            else if (parameterLine.Contains(BigdecimalDelimiter))
            {
                Type = QueryParameterType.BigDecimal;
            }
            else
            {
                throw new ApplicationException($"Unknown parameter type: {parameterLine}");
            }
        }

        private void SetName(string parameterLine)
        {
            int end;
            var start = parameterLine.IndexOf(" (");
            var startOfName = parameterLine.Substring(start + " (".Length);

            switch (Type)
            {
                case QueryParameterType.String:
                    end = startOfName.IndexOf(" of type java.lang.String");
                    break;
                case QueryParameterType.Date:
                    end = startOfName.IndexOf(" of type java.util.Date");
                    break;
                case QueryParameterType.Boolean:
                    end = startOfName.IndexOf(" of type java.lang.Boolean");
                    break;
                case QueryParameterType.BigDecimal:
                    end = startOfName.IndexOf(" of type java.math.BigDecimal");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(Type), $@"Source: {parameterLine}");
            }

            var actName = startOfName.Substring(0, end);
            Name = $" {actName} ";
        }

        private void SetValue(string parameterLine)
        {
            string actValue;
            int end;
            switch (Type)
            {
                case QueryParameterType.String:
                    end = parameterLine.LastIndexOf(StringDelimiter);
                    actValue = parameterLine.Substring(end + StringDelimiter.Length).Trim();
                    Value = $"'{actValue}'";
                    break;
                case QueryParameterType.Date:
                    end = parameterLine.LastIndexOf(DateDelimiter);
                    actValue = parameterLine.Substring(end + DateDelimiter.Length).Trim().ConvertTimeZone();
                    var dateValue = DateTime.ParseExact(actValue, "ddd MMM dd HH:mm:ss zzz yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);
                    var dateString = dateValue.ToString().Replace(".", "-");
                    Value = $"'{dateString}'";
                    break;
                case QueryParameterType.Boolean:
                    end = parameterLine.LastIndexOf(BooleanDelimiter);
                    actValue = parameterLine.Substring(end + BooleanDelimiter.Length).Trim();
                    if (actValue.Equals("true", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Value = " 1 ";
                    }
                    Value = " 0 ";
                    break;
                case QueryParameterType.BigDecimal:
                    end = parameterLine.LastIndexOf(BigdecimalDelimiter);
                    actValue = parameterLine.Substring(end + BigdecimalDelimiter.Length).Trim();
                    Value = $" {actValue} ";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(Type), $@"Source: {parameterLine}");
            }
        }

        public override string ToString()
        {
            return $"{Name} - {Value} - {Type}";
        }
    }
}