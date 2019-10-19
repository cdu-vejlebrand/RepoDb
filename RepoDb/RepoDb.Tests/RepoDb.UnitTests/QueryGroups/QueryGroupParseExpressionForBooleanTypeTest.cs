﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepoDb.UnitTests.Setup;

namespace RepoDb.UnitTests
{
    public partial class QueryGroupTest
    {
        // Boolean

        [TestMethod]
        public void TestQueryGroupParseExpressionBooleanConstant()
        {
            // Act
            var actual = QueryGroup.Parse<QueryGroupTestExpressionClass>(e => e.PropertyBoolean == true).GetString(Helper.DbSetting);
            var expected = "([PropertyBoolean] = @PropertyBoolean)";

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestQueryGroupParseExpressionBooleanVariable()
        {
            // Setup
            var value = true;

            // Act
            var actual = QueryGroup.Parse<QueryGroupTestExpressionClass>(e => e.PropertyBoolean == value).GetString(Helper.DbSetting);
            var expected = "([PropertyBoolean] = @PropertyBoolean)";

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestQueryGroupParseExpressionBooleanClassProperty()
        {
            // Setup
            var value = new QueryGroupTestExpressionClass
            {
                PropertyBoolean = true
            };

            // Act
            var actual = QueryGroup.Parse<QueryGroupTestExpressionClass>(e => e.PropertyBoolean == value.PropertyBoolean).GetString(Helper.DbSetting);
            var expected = "([PropertyBoolean] = @PropertyBoolean)";

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestQueryGroupParseExpressionBooleanMethodCall()
        {
            // Act
            var actual = QueryGroup.Parse<QueryGroupTestExpressionClass>(e => e.PropertyBoolean == GetBooleanValueForParseExpression()).GetString(Helper.DbSetting);
            var expected = "([PropertyBoolean] = @PropertyBoolean)";

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestQueryGroupParseExpressionBooleanVariableMethodCall()
        {
            // Setup
            var value = GetBooleanValueForParseExpression();

            // Act
            var actual = QueryGroup.Parse<QueryGroupTestExpressionClass>(e => e.PropertyBoolean == value).GetString(Helper.DbSetting);
            var expected = "([PropertyBoolean] = @PropertyBoolean)";

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
