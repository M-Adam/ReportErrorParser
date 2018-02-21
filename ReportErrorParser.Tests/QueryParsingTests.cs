using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportErrorParser.Logic;

namespace ReportErrorParser.Tests
{
    [TestClass]
    public class QueryParsingTests
    {
        [TestMethod]
        public void CapVsDemandByRoleQueryParsingSuccedes()
        {
            const string query = Queries.CapVsDemandByRoleQuery;
            var parsingOutput = Parser.Parse(query);

            Assert.IsFalse(string.IsNullOrWhiteSpace(parsingOutput));
            var sqlQueryParseResult = new SqlParser().Parse(parsingOutput);
            Assert.IsNull(sqlQueryParseResult);
        }
    }
}
