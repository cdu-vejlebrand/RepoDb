﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepoDb.UnitTests.Setup;

namespace RepoDb.UnitTests
{
    public partial class QueryGroupTest
    {
        // String

        [TestMethod]
        public void TestQueryGroupParseExpressionStringConstant()
        {
            // Act
            var actual = QueryGroup.Parse<QueryGroupTestExpressionClass>(e => e.PropertyString == "A").GetString(Helper.DbSetting);
            var expected = "([PropertyString] = @PropertyString)";

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestQueryGroupParseExpressionStringVariable()
        {
            // Setup
            var value = "A";

            // Act
            var actual = QueryGroup.Parse<QueryGroupTestExpressionClass>(e => e.PropertyString == value).GetString(Helper.DbSetting);
            var expected = "([PropertyString] = @PropertyString)";

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestQueryGroupParseExpressionStringClassProperty()
        {
            // Setup
            var value = new QueryGroupTestExpressionClass
            {
                PropertyString = "A"
            };

            // Act
            var actual = QueryGroup.Parse<QueryGroupTestExpressionClass>(e => e.PropertyString == value.PropertyString).GetString(Helper.DbSetting);
            var expected = "([PropertyString] = @PropertyString)";

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestQueryGroupParseExpressionStringMethodCall()
        {
            // Act
            var actual = QueryGroup.Parse<QueryGroupTestExpressionClass>(e => e.PropertyString == GetStringValueForParseExpression()).GetString(Helper.DbSetting);
            var expected = "([PropertyString] = @PropertyString)";

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestQueryGroupParseExpressionStringVariableMethodCall()
        {
            // Setup
            var value = GetStringValueForParseExpression();

            // Act
            var actual = QueryGroup.Parse<QueryGroupTestExpressionClass>(e => e.PropertyString == value).GetString(Helper.DbSetting);
            var expected = "([PropertyString] = @PropertyString)";

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
