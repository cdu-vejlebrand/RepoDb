﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using RepoDb.Attributes;
using RepoDb.Exceptions;
using System;
using System.Linq;
using System.Reflection;

namespace RepoDb.UnitTests.Others
{
    [TestClass]
    public partial class IdentityMapperTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Cleanup();
        }

        [TestCleanup]
        public void Cleanup()
        {
            IdentityCache.Flush();
            IdentityMapper.Clear();
        }

        #region SubClasses

        private class IdentityMapperTestClass
        {
            public string IdentityColumn { get; set; }
            public string ColumnName { get; set; }
        }

        private class IdentityMapperTestWithAttributeClass
        {
            [Identity]
            public string IdentityColumn { get; set; }
            public string ColumnName { get; set; }
        }

        #endregion

        #region Methods

        /*
         * No IdentityAttribute
         */

        [TestMethod]
        public void TestIdentityMapperMappingViaPropertyName()
        {
            // Setup
            IdentityMapper.Add<IdentityMapperTestClass>("ColumnName");

            // Act
            var actual = IdentityMapper.Get<IdentityMapperTestClass>();
            var expected = "ColumnName";

            // Assert
            Assert.IsTrue(actual?.IsIdentity() == true);
            Assert.AreEqual(expected, actual?.GetMappedName());
        }

        [TestMethod]
        public void TestIdentityMapperMappingViaField()
        {
            // Setup
            IdentityMapper.Add<IdentityMapperTestClass>(new Field("ColumnName"));

            // Act
            var actual = IdentityMapper.Get<IdentityMapperTestClass>();
            var expected = "ColumnName";

            // Assert
            Assert.IsTrue(actual?.IsIdentity() == true);
            Assert.AreEqual(expected, actual?.GetMappedName());
        }

        [TestMethod]
        public void TestIdentityMapperMappingViaExpression()
        {
            // Setup
            IdentityMapper.Add<IdentityMapperTestClass>(e => e.ColumnName);

            // Act
            var actual = IdentityMapper.Get<IdentityMapperTestClass>();
            var expected = "ColumnName";

            // Assert
            Assert.IsTrue(actual?.IsIdentity() == true);
            Assert.AreEqual(expected, actual?.GetMappedName());
        }

        [TestMethod]
        public void TestIdentityMapperMappingViaPropertyInfo()
        {
            // Setup
            var propertyInfo = typeof(IdentityMapperTestClass)
                .GetProperties()
                .First(p => p.Name == "ColumnName");
            IdentityMapper.Add(propertyInfo);

            // Act
            var actual = IdentityMapper.Get<IdentityMapperTestClass>();
            var expected = "ColumnName";

            // Assert
            Assert.IsTrue(actual?.IsIdentity() == true);
            Assert.AreEqual(expected, actual?.GetMappedName());
        }

        [TestMethod]
        public void TestIdentityMapperMappingViaClassProperty()
        {
            // Setup
            var classProperty = PropertyCache.Get<IdentityMapperTestClass>()
                .First(p => p.PropertyInfo.Name == "ColumnName");
            IdentityMapper.Add(classProperty);

            // Act
            var actual = IdentityMapper.Get<IdentityMapperTestClass>();
            var expected = "ColumnName";

            // Assert
            Assert.IsTrue(actual?.IsIdentity() == true);
            Assert.AreEqual(expected, actual?.GetMappedName());
        }

        /*
         * With IdentityAttribute
         */

        [TestMethod]
        public void TestIdentityMapperMappingViaPropertyNameWithMapAttribute()
        {
            // Setup
            IdentityMapper.Add<IdentityMapperTestWithAttributeClass>("ColumnName");

            // Act
            var actual = IdentityMapper.Get<IdentityMapperTestWithAttributeClass>();
            var expected = "ColumnName";

            // Assert
            Assert.IsTrue(actual?.IsIdentity() == true);
            Assert.AreEqual(expected, actual?.GetMappedName());

            // Act
            actual = IdentityCache.Get<IdentityMapperTestWithAttributeClass>();
            expected = "IdentityColumn";

            // Assert
            Assert.IsTrue(actual?.IsIdentity() == true);
            Assert.AreEqual(expected, actual?.GetMappedName());
        }

        [TestMethod]
        public void TestIdentityMapperMappingViaFieldWithMapAttribute()
        {
            // Setup
            IdentityMapper.Add<IdentityMapperTestWithAttributeClass>(new Field("ColumnName"));

            // Act
            var actual = IdentityMapper.Get<IdentityMapperTestWithAttributeClass>();
            var expected = "ColumnName";

            // Assert
            Assert.IsTrue(actual?.IsIdentity() == true);
            Assert.AreEqual(expected, actual?.GetMappedName());

            // Act
            actual = IdentityCache.Get<IdentityMapperTestWithAttributeClass>();
            expected = "IdentityColumn";

            // Assert
            Assert.IsTrue(actual?.IsIdentity() == true);
            Assert.AreEqual(expected, actual?.GetMappedName());
        }

        [TestMethod]
        public void TestIdentityMapperMappingViaExpressionWithMapAttribute()
        {
            // Setup
            IdentityMapper.Add<IdentityMapperTestWithAttributeClass>(e => e.ColumnName);

            // Act
            var actual = IdentityMapper.Get<IdentityMapperTestWithAttributeClass>();
            var expected = "ColumnName";

            // Assert
            Assert.IsTrue(actual?.IsIdentity() == true);
            Assert.AreEqual(expected, actual?.GetMappedName());

            // Act
            actual = IdentityCache.Get<IdentityMapperTestWithAttributeClass>();
            expected = "IdentityColumn";

            // Assert
            Assert.IsTrue(actual?.IsIdentity() == true);
            Assert.AreEqual(expected, actual?.GetMappedName());
        }

        [TestMethod]
        public void TestIdentityMapperMappingViaPropertyInfoWithMapAttribute()
        {
            // Setup
            var propertyInfo = typeof(IdentityMapperTestWithAttributeClass)
                .GetProperties()
                .First(p => p.Name == "ColumnName");
            IdentityMapper.Add(propertyInfo);

            // Act
            var actual = IdentityMapper.Get<IdentityMapperTestWithAttributeClass>();
            var expected = "ColumnName";

            // Assert
            Assert.IsTrue(actual?.IsIdentity() == true);
            Assert.AreEqual(expected, actual?.GetMappedName());

            // Act
            actual = IdentityCache.Get<IdentityMapperTestWithAttributeClass>();
            expected = "IdentityColumn";

            // Assert
            Assert.IsTrue(actual?.IsIdentity() == true);
            Assert.AreEqual(expected, actual?.GetMappedName());
        }

        [TestMethod]
        public void TestIdentityMapperMappingViaClassPropertyWithMapAttribute()
        {
            // Setup
            var classProperty = PropertyCache.Get<IdentityMapperTestWithAttributeClass>()
                .First(p => p.PropertyInfo.Name == "ColumnName");
            IdentityMapper.Add(classProperty);

            // Act
            var actual = IdentityMapper.Get<IdentityMapperTestWithAttributeClass>();
            var expected = "ColumnName";

            // Assert
            Assert.IsTrue(actual?.IsIdentity() == true);
            Assert.AreEqual(expected, actual?.GetMappedName());

            // Act
            actual = IdentityCache.Get<IdentityMapperTestWithAttributeClass>();
            expected = "IdentityColumn";

            // Assert
            Assert.IsTrue(actual?.IsIdentity() == true);
            Assert.AreEqual(expected, actual?.GetMappedName());
        }

        /*
         * Override
         */

        [TestMethod]
        public void TestIdentityMapperMappingViaPropertyNameOverride()
        {
            // Setup
            IdentityMapper.Add<IdentityMapperTestClass>("ColumnName");
            IdentityMapper.Add<IdentityMapperTestClass>("IdentityColumn", true);

            // Act
            var actual = IdentityMapper.Get<IdentityMapperTestClass>();
            var expected = "IdentityColumn";

            // Assert
            Assert.IsTrue(actual?.IsIdentity() == true);
            Assert.AreEqual(expected, actual?.GetMappedName());
        }

        [TestMethod]
        public void TestIdentityMapperMappingViaFieldOverride()
        {
            // Setup
            IdentityMapper.Add<IdentityMapperTestClass>(new Field("ColumnName"));
            IdentityMapper.Add<IdentityMapperTestClass>(new Field("IdentityColumn"), true);

            // Act
            var actual = IdentityMapper.Get<IdentityMapperTestClass>();
            var expected = "IdentityColumn";

            // Assert
            Assert.IsTrue(actual?.IsIdentity() == true);
            Assert.AreEqual(expected, actual?.GetMappedName());
        }

        [TestMethod]
        public void TestIdentityMapperMappingViaExpressionOverride()
        {
            // Setup
            IdentityMapper.Add<IdentityMapperTestClass>(e => e.ColumnName);
            IdentityMapper.Add<IdentityMapperTestClass>(e => e.IdentityColumn, true);

            // Act
            var actual = IdentityMapper.Get<IdentityMapperTestClass>();
            var expected = "IdentityColumn";

            // Assert
            Assert.IsTrue(actual?.IsIdentity() == true);
            Assert.AreEqual(expected, actual?.GetMappedName());
        }

        [TestMethod]
        public void TestIdentityMapperMappingViaPropertyInfoOverride()
        {
            // Setup
            var columnNamePropertyInfo = typeof(IdentityMapperTestClass)
                .GetProperties()
                .First(p => p.Name == "ColumnName");
            var primaryColumnPropertyInfo = typeof(IdentityMapperTestClass)
                .GetProperties()
                .First(p => p.Name == "IdentityColumn");
            IdentityMapper.Add(columnNamePropertyInfo);
            IdentityMapper.Add(primaryColumnPropertyInfo, true);

            // Act
            var actual = IdentityMapper.Get<IdentityMapperTestClass>();
            var expected = "IdentityColumn";

            // Assert
            Assert.IsTrue(actual?.IsIdentity() == true);
            Assert.AreEqual(expected, actual?.GetMappedName());
        }

        [TestMethod]
        public void TestIdentityMapperMappingViaClassPropertyOverride()
        {
            // Setup
            var columnNameClassProperty = PropertyCache.Get<IdentityMapperTestClass>()
                .First(p => p.PropertyInfo.Name == "ColumnName");
            var primaryColumnClassProperty = PropertyCache.Get<IdentityMapperTestClass>()
                .First(p => p.PropertyInfo.Name == "IdentityColumn");
            IdentityMapper.Add(columnNameClassProperty);
            IdentityMapper.Add(primaryColumnClassProperty, true);

            // Act
            var actual = IdentityMapper.Get<IdentityMapperTestClass>();
            var expected = "IdentityColumn";

            // Assert
            Assert.IsTrue(actual?.IsIdentity() == true);
            Assert.AreEqual(expected, actual?.GetMappedName());
        }

        /*
         * Override False
         */

        [TestMethod, ExpectedException(typeof(MappingExistsException))]
        public void ThrowExceptionOnIdentityMapperViaPropertyNameThatIsAlreadyExisting()
        {
            // Setup
            IdentityMapper.Add<IdentityMapperTestClass>("ColumnName");
            IdentityMapper.Add<IdentityMapperTestClass>("IdentityColumn");
        }

        [TestMethod, ExpectedException(typeof(MappingExistsException))]
        public void ThrowExceptionOnIdentityMapperViaFieldThatIsAlreadyExisting()
        {
            // Setup
            IdentityMapper.Add<IdentityMapperTestClass>(new Field("ColumnName"));
            IdentityMapper.Add<IdentityMapperTestClass>(new Field("IdentityColumn"));
        }

        [TestMethod, ExpectedException(typeof(MappingExistsException))]
        public void ThrowExceptionOnIdentityMapperViaExpressionThatIsAlreadyExisting()
        {
            // Setup
            IdentityMapper.Add<IdentityMapperTestClass>(e => e.ColumnName);
            IdentityMapper.Add<IdentityMapperTestClass>(e => e.IdentityColumn);
        }

        [TestMethod, ExpectedException(typeof(MappingExistsException))]
        public void ThrowExceptionOnIdentityMapperViaPropertyInfoThatIsAlreadyExisting()
        {
            // Setup
            var columnNamePropertyInfo = typeof(IdentityMapperTestClass)
                .GetProperties()
                .First(p => p.Name == "ColumnName");
            var primaryColumnPropertyInfo = typeof(IdentityMapperTestClass)
                .GetProperties()
                .First(p => p.Name == "IdentityColumn");
            IdentityMapper.Add(columnNamePropertyInfo);
            IdentityMapper.Add(primaryColumnPropertyInfo);
        }

        [TestMethod, ExpectedException(typeof(MappingExistsException))]
        public void ThrowExceptionOnIdentityMapperViaClassPropertyThatIsAlreadyExisting()
        {
            // Setup
            var columnNameClassProperty = PropertyCache.Get<IdentityMapperTestClass>()
                .First(p => p.PropertyInfo.Name == "ColumnName");
            var primaryColumnClassProperty = PropertyCache.Get<IdentityMapperTestClass>()
                .First(p => p.PropertyInfo.Name == "IdentityColumn");
            IdentityMapper.Add(columnNameClassProperty);
            IdentityMapper.Add(primaryColumnClassProperty);
        }

        /*
         * Null Properties
         */

        [TestMethod, ExpectedException(typeof(NullReferenceException))]
        public void ThrowExceptionOnIdentityMapperViaPropertyNameThatIsNull()
        {
            // Setup
            IdentityMapper.Add<IdentityMapperTestClass>(propertyName: null);
        }

        [TestMethod, ExpectedException(typeof(NullReferenceException))]
        public void ThrowExceptionOnIdentityMapperViaFieldThatIsNull()
        {
            // Setup
            IdentityMapper.Add<IdentityMapperTestClass>(field: null);
        }

        [TestMethod, ExpectedException(typeof(NullReferenceException))]
        public void ThrowExceptionOnIdentityMapperViaExpressionThatIsNull()
        {
            // Setup
            IdentityMapper.Add<IdentityMapperTestClass>(expression: null);
        }

        [TestMethod, ExpectedException(typeof(NullReferenceException))]
        public void ThrowExceptionOnIdentityMapperViaPropertyInfoThatIsNull()
        {
            // Setup
            IdentityMapper.Add((PropertyInfo)null);
        }

        [TestMethod, ExpectedException(typeof(NullReferenceException))]
        public void ThrowExceptionOnIdentityMapperViaClassPropertyThatIsNull()
        {
            // Setup
            IdentityMapper.Add((ClassProperty)null);
        }

        /*
         * Missing Properties
         */

        [TestMethod, ExpectedException(typeof(PropertyNotFoundException))]
        public void ThrowExceptionOnIdentityMapperViaPropertyNameThatIsMissing()
        {
            // Setup
            IdentityMapper.Add<IdentityMapperTestClass>("Whatever");
        }

        [TestMethod, ExpectedException(typeof(PropertyNotFoundException))]
        public void ThrowExceptionOnIdentityMapperViaFieldThatIsIsMissing()
        {
            // Setup
            IdentityMapper.Add<IdentityMapperTestClass>(new Field("Whatever"));
        }

        #endregion
    }
}