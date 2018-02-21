using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Data.Schema.ScriptDom;
using Microsoft.Data.Schema.ScriptDom.Sql;

namespace ReportErrorParser.Tests
{
    internal class SqlParser
    {
        public List<string> Parse(string sql)
        {
            var parser = new TSql100Parser(false);
            parser.Parse(new StringReader(sql), out var errors);
            if (errors == null || errors.Count <= 0) return null;
            return errors.Select(error => error.Message).ToList();
        }
    }
}