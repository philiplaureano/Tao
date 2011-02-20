using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tao;
using Tao.Interfaces;
using Tao.Containers;

namespace Tao.UnitTests
{
    [TestFixture]
    public class MetadataTableRowSizeTests : BaseStreamTests
    {
        [Test]
        public void ShouldReturnCorrectRowSizeForAssemblyTable()
        {
            var tableId = TableId.Assembly;
            var expectedRowSize = 22;

            TestRowSizeCalculation(tableId, expectedRowSize);
        }

        [Test]
        public void ShouldReturnCorrectRowSizeForModuleTable()
        {
            TestRowSizeCalculation(TableId.Module, 0xA);
        }

        [Test]
        public void ShouldReturnCorrectRowSizeForTypeDefTable()
        {
            TestRowSizeCalculation(TableId.TypeDef, 0xE);
        }

        [Test]
        public void ShouldReturnCorrectRowSizeForMethodDefTable()
        {
            TestRowSizeCalculation(TableId.MethodDef, 0xE);
        }

        #region Column Size Count Tests
        [Test]
        public void ShouldBeAbleToReturnCorrectColumnSizeCountsForAssemblyTable()
        {
            var tableId = TableId.Assembly;
            var expectedSingleByteColumnCount = 0;
            var expectedWordColumnCount = 4;
            var expectedDwordColumnCount = 2;
            var expectedStringsColumnCount = 2;
            var expectedBlobColumnCount = 1;
            var expectedGuidColumnCount = 0;

            TestCounts(tableId, expectedSingleByteColumnCount, expectedWordColumnCount, expectedDwordColumnCount,
                expectedStringsColumnCount, expectedBlobColumnCount, expectedGuidColumnCount);
        }

        [Test]
        public void ShouldBeAbleToReturnCorrectColumnSizeCountsForAssemblyRefTable()
        {
            var tableId = TableId.AssemblyRef;
            var expectedSingleByteColumnCount = 0;
            var expectedWordColumnCount = 4;
            var expectedDwordColumnCount = 1;
            var expectedStringsColumnCount = 2;
            var expectedBlobColumnCount = 1;
            var expectedGuidColumnCount = 0;

            TestCounts(tableId, expectedSingleByteColumnCount, expectedWordColumnCount, expectedDwordColumnCount,
                expectedStringsColumnCount, expectedBlobColumnCount, expectedGuidColumnCount);
        }

        [Test]
        public void ShouldBeAbleToReturnCorrectColumnSizeCountsForClassLayoutTable()
        {
            var tableId = TableId.ClassLayout;
            var expectedSingleByteColumnCount = 0;
            var expectedWordColumnCount = 1;
            var expectedDwordColumnCount = 1;
            var expectedStringsColumnCount = 0;
            var expectedBlobColumnCount = 0;
            var expectedGuidColumnCount = 0;

            TestCounts(tableId, expectedSingleByteColumnCount, expectedWordColumnCount, expectedDwordColumnCount,
                expectedStringsColumnCount, expectedBlobColumnCount, expectedGuidColumnCount);
        }

        [Test]
        public void ShouldBeAbleToReturnCorrectColumnSizeCountsForMethodDefTable()
        {
            var tableId = TableId.MethodDef;
            var expectedSingleByteColumnCount = 0;
            var expectedWordColumnCount = 2;
            var expectedDwordColumnCount = 1;
            var expectedStringsColumnCount = 1;
            var expectedBlobColumnCount = 1;
            var expectedGuidColumnCount = 0;

            TestCounts(tableId, expectedSingleByteColumnCount, expectedWordColumnCount, expectedDwordColumnCount,
                       expectedStringsColumnCount, expectedBlobColumnCount, expectedGuidColumnCount);
        }
        [Test]
        public void ShouldBeAbleToReturnCorrectColumnSizeCountsForConstantTable()
        {
            var tableId = TableId.Constant;
            var expectedSingleByteColumnCount = 1;
            var expectedWordColumnCount = 0;
            var expectedDwordColumnCount = 0;
            var expectedStringsColumnCount = 0;
            var expectedBlobColumnCount = 1;
            var expectedGuidColumnCount = 0;

            TestCounts(tableId, expectedSingleByteColumnCount, expectedWordColumnCount, expectedDwordColumnCount,
                       expectedStringsColumnCount, expectedBlobColumnCount, expectedGuidColumnCount);
        }

        [Test]
        public void ShouldBeAbleToReturnCorrectColumnSizeCountsForModuleTable()
        {
            var tableId = TableId.Module;
            var expectedSingleByteColumnCount = 0;
            var expectedWordColumnCount = 1;
            var expectedDwordColumnCount = 0;
            var expectedStringsColumnCount = 1;
            var expectedBlobColumnCount = 0;
            var expectedGuidColumnCount = 3;

            TestCounts(tableId, expectedSingleByteColumnCount, expectedWordColumnCount, expectedDwordColumnCount,
                       expectedStringsColumnCount, expectedBlobColumnCount, expectedGuidColumnCount);
        }
        [Test]
        public void ShouldBeAbleToReturnCorrectColumnSizeCountsForCustomAttributeTable()
        {
            var tableId = TableId.CustomAttribute;
            var expectedSingleByteColumnCount = 0;
            var expectedWordColumnCount = 0;
            var expectedDwordColumnCount = 1;
            var expectedStringsColumnCount = 0;
            var expectedBlobColumnCount = 1;
            var expectedGuidColumnCount = 0;

            TestCounts(tableId, expectedSingleByteColumnCount, expectedWordColumnCount, expectedDwordColumnCount,
                       expectedStringsColumnCount, expectedBlobColumnCount, expectedGuidColumnCount);
        }

        [Test]
        public void ShouldBeAbleToReturnCorrectColumnSizeCountsForModuleRefTable()
        {
            var tableId = TableId.ModuleRef;
            var expectedSingleByteColumnCount = 0;
            var expectedWordColumnCount = 0;
            var expectedDwordColumnCount = 0;
            var expectedStringsColumnCount = 1;
            var expectedBlobColumnCount = 0;
            var expectedGuidColumnCount = 0;

            TestCounts(tableId, expectedSingleByteColumnCount, expectedWordColumnCount, expectedDwordColumnCount,
                       expectedStringsColumnCount, expectedBlobColumnCount, expectedGuidColumnCount);
        }

        [Test]
        public void ShouldBeAbleToReturnCorrectColumnSizeCountsForTypeDefTable()
        {
            var tableId = TableId.TypeDef;
            var expectedSingleByteColumnCount = 0;
            var expectedWordColumnCount = 0;
            var expectedDwordColumnCount = 1;
            var expectedStringsColumnCount = 2;
            var expectedBlobColumnCount = 0;
            var expectedGuidColumnCount = 0;

            TestCounts(tableId, expectedSingleByteColumnCount, expectedWordColumnCount, expectedDwordColumnCount,
                       expectedStringsColumnCount, expectedBlobColumnCount, expectedGuidColumnCount);
        }

        [Test]
        public void ShouldBeAbleToReturnCorrectColumnSizeCountsForFieldTable()
        {
            var tableId = TableId.Field;
            var expectedSingleByteColumnCount = 0;
            var expectedWordColumnCount = 1;
            var expectedDwordColumnCount = 0;
            var expectedStringsColumnCount = 1;
            var expectedBlobColumnCount = 1;
            var expectedGuidColumnCount = 0;

            TestCounts(tableId, expectedSingleByteColumnCount, expectedWordColumnCount, expectedDwordColumnCount,
                       expectedStringsColumnCount, expectedBlobColumnCount, expectedGuidColumnCount);
        }

        [Test]
        public void ShouldBeAbleToReturnCorrectColumnSizeCountsForParamTable()
        {
            var tableId = TableId.Param;
            var expectedSingleByteColumnCount = 0;
            var expectedWordColumnCount = 2;
            var expectedDwordColumnCount = 0;
            var expectedStringsColumnCount = 1;
            var expectedBlobColumnCount = 0;
            var expectedGuidColumnCount = 0;

            TestCounts(tableId, expectedSingleByteColumnCount, expectedWordColumnCount, expectedDwordColumnCount,
                       expectedStringsColumnCount, expectedBlobColumnCount, expectedGuidColumnCount);
        }

        [Test]
        public void ShouldBeAbleToReturnCorrectColumnSizeCountsForImplMapTable()
        {
            var tableId = TableId.ImplMap;
            var expectedSingleByteColumnCount = 0;
            var expectedWordColumnCount = 1;
            var expectedDwordColumnCount = 0;
            var expectedStringsColumnCount = 1;
            var expectedBlobColumnCount = 0;
            var expectedGuidColumnCount = 0;

            TestCounts(tableId, expectedSingleByteColumnCount, expectedWordColumnCount, expectedDwordColumnCount,
                       expectedStringsColumnCount, expectedBlobColumnCount, expectedGuidColumnCount);
        }

        [Test]
        public void ShouldBeAbleToReturnCorrectColumnSizeCountsForMemberRefTable()
        {
            var tableId = TableId.MemberRef;
            var expectedSingleByteColumnCount = 0;
            var expectedWordColumnCount = 0;
            var expectedDwordColumnCount = 0;
            var expectedStringsColumnCount = 1;
            var expectedBlobColumnCount = 1;
            var expectedGuidColumnCount = 0;

            TestCounts(tableId, expectedSingleByteColumnCount, expectedWordColumnCount, expectedDwordColumnCount,
                       expectedStringsColumnCount, expectedBlobColumnCount, expectedGuidColumnCount);
        }

        [Test]
        public void ShouldBeAbleToReturnCorrectColumnSizeCountsForFieldMarshalTable()
        {
            var tableId = TableId.FieldMarshal;
            var expectedSingleByteColumnCount = 0;
            var expectedWordColumnCount = 0;
            var expectedDwordColumnCount = 0;
            var expectedStringsColumnCount = 0;
            var expectedBlobColumnCount = 1;
            var expectedGuidColumnCount = 0;

            TestCounts(tableId, expectedSingleByteColumnCount, expectedWordColumnCount, expectedDwordColumnCount,
                       expectedStringsColumnCount, expectedBlobColumnCount, expectedGuidColumnCount);
        }

        [Test]
        public void ShouldBeAbleToReturnCorrectColumnSizeCountsForDeclSecurityTable()
        {
            var tableId = TableId.DeclSecurity;
            var expectedSingleByteColumnCount = 0;
            var expectedWordColumnCount = 1;
            var expectedDwordColumnCount = 0;
            var expectedStringsColumnCount = 0;
            var expectedBlobColumnCount = 1;
            var expectedGuidColumnCount = 0;

            TestCounts(tableId, expectedSingleByteColumnCount, expectedWordColumnCount, expectedDwordColumnCount,
                       expectedStringsColumnCount, expectedBlobColumnCount, expectedGuidColumnCount);
        }

        [Test]
        public void ShouldBeAbleToReturnCorrectColumnSizeCountsForFieldLayoutTable()
        {
            var tableId = TableId.FieldLayout;
            var expectedSingleByteColumnCount = 0;
            var expectedWordColumnCount = 0;
            var expectedDwordColumnCount = 1;
            var expectedStringsColumnCount = 0;
            var expectedBlobColumnCount = 0;
            var expectedGuidColumnCount = 0;

            TestCounts(tableId, expectedSingleByteColumnCount, expectedWordColumnCount, expectedDwordColumnCount,
                       expectedStringsColumnCount, expectedBlobColumnCount, expectedGuidColumnCount);
        }

        [Test]
        public void ShouldBeAbleToReturnCorrectColumnSizeCountsForStandAloneSigTable()
        {
            var tableId = TableId.StandAloneSig;
            var expectedSingleByteColumnCount = 0;
            var expectedWordColumnCount = 0;
            var expectedDwordColumnCount = 0;
            var expectedStringsColumnCount = 0;
            var expectedBlobColumnCount = 1;
            var expectedGuidColumnCount = 0;

            TestCounts(tableId, expectedSingleByteColumnCount, expectedWordColumnCount, expectedDwordColumnCount,
                       expectedStringsColumnCount, expectedBlobColumnCount, expectedGuidColumnCount);
        }

        [Test]
        public void ShouldBeAbleToReturnCorrectColumnSizeCountsForEventMapTable()
        {
            var tableId = TableId.EventMap;
            var expectedSingleByteColumnCount = 0;
            var expectedWordColumnCount = 0;
            var expectedDwordColumnCount = 0;
            var expectedStringsColumnCount = 0;
            var expectedBlobColumnCount = 0;
            var expectedGuidColumnCount = 0;

            TestCounts(tableId, expectedSingleByteColumnCount, expectedWordColumnCount, expectedDwordColumnCount,
                       expectedStringsColumnCount, expectedBlobColumnCount, expectedGuidColumnCount);
        }

        [Test]
        public void ShouldBeAbleToReturnCorrectColumnSizeCountsForEventTable()
        {
            var tableId = TableId.Event;
            var expectedSingleByteColumnCount = 0;
            var expectedWordColumnCount = 1;
            var expectedDwordColumnCount = 0;
            var expectedStringsColumnCount = 1;
            var expectedBlobColumnCount = 0;
            var expectedGuidColumnCount = 0;

            TestCounts(tableId, expectedSingleByteColumnCount, expectedWordColumnCount, expectedDwordColumnCount,
                       expectedStringsColumnCount, expectedBlobColumnCount, expectedGuidColumnCount);
        }

        [Test]
        public void ShouldBeAbleToReturnCorrectColumnSizeCountsForPropertyMapTable()
        {
            var tableId = TableId.PropertyMap;
            var expectedSingleByteColumnCount = 0;
            var expectedWordColumnCount = 0;
            var expectedDwordColumnCount = 0;
            var expectedStringsColumnCount = 0;
            var expectedBlobColumnCount = 0;
            var expectedGuidColumnCount = 0;

            TestCounts(tableId, expectedSingleByteColumnCount, expectedWordColumnCount, expectedDwordColumnCount,
                       expectedStringsColumnCount, expectedBlobColumnCount, expectedGuidColumnCount);
        }

        [Test]
        public void ShouldBeAbleToReturnCorrectColumnSizeCountsForPropertyTable()
        {
            var tableId = TableId.Property;
            var expectedSingleByteColumnCount = 0;
            var expectedWordColumnCount = 1;
            var expectedDwordColumnCount = 0;
            var expectedStringsColumnCount = 1;
            var expectedBlobColumnCount = 1;
            var expectedGuidColumnCount = 0;

            TestCounts(tableId, expectedSingleByteColumnCount, expectedWordColumnCount, expectedDwordColumnCount,
                       expectedStringsColumnCount, expectedBlobColumnCount, expectedGuidColumnCount);
        }

        [Test]
        public void ShouldBeAbleToReturnCorrectColumnSizeCountsForMethodSemanticsTable()
        {
            var tableId = TableId.MethodSemantics;
            var expectedSingleByteColumnCount = 0;
            var expectedWordColumnCount = 1;
            var expectedDwordColumnCount = 0;
            var expectedStringsColumnCount = 0;
            var expectedBlobColumnCount = 0;
            var expectedGuidColumnCount = 0;

            TestCounts(tableId, expectedSingleByteColumnCount, expectedWordColumnCount, expectedDwordColumnCount,
                       expectedStringsColumnCount, expectedBlobColumnCount, expectedGuidColumnCount);
        }

        [Test]
        public void ShouldBeAbleToReturnCorrectColumnSizeCountsForMethodImplTable()
        {
            var tableId = TableId.MethodImpl;
            var expectedSingleByteColumnCount = 0;
            var expectedWordColumnCount = 0;
            var expectedDwordColumnCount = 0;
            var expectedStringsColumnCount = 0;
            var expectedBlobColumnCount = 0;
            var expectedGuidColumnCount = 0;

            TestCounts(tableId, expectedSingleByteColumnCount, expectedWordColumnCount, expectedDwordColumnCount,
                       expectedStringsColumnCount, expectedBlobColumnCount, expectedGuidColumnCount);
        }

        [Test]
        public void ShouldBeAbleToReturnCorrectColumnSizeCountsForTypeRefTable()
        {
            var tableId = TableId.TypeRef;
            var expectedSingleByteColumnCount = 0;
            var expectedWordColumnCount = 0;
            var expectedDwordColumnCount = 0;
            var expectedStringsColumnCount = 2;
            var expectedBlobColumnCount = 0;
            var expectedGuidColumnCount = 0;

            TestCounts(tableId, expectedSingleByteColumnCount, expectedWordColumnCount, expectedDwordColumnCount,
                       expectedStringsColumnCount, expectedBlobColumnCount, expectedGuidColumnCount);
        }

        [Test]
        public void ShouldBeAbleToReturnCorrectColumnSizeCountsForTypeSpecTable()
        {
            var tableId = TableId.TypeSpec;
            var expectedSingleByteColumnCount = 0;
            var expectedWordColumnCount = 0;
            var expectedDwordColumnCount = 0;
            var expectedStringsColumnCount = 0;
            var expectedBlobColumnCount = 1;
            var expectedGuidColumnCount = 0;

            TestCounts(tableId, expectedSingleByteColumnCount, expectedWordColumnCount, expectedDwordColumnCount,
                       expectedStringsColumnCount, expectedBlobColumnCount, expectedGuidColumnCount);
        }

        [Test]
        public void ShouldBeAbleToReturnCorrectColumnSizeCountsForFieldRVATable()
        {
            var tableId = TableId.FieldRVA;
            var expectedSingleByteColumnCount = 0;
            var expectedWordColumnCount = 0;
            var expectedDwordColumnCount = 1;
            var expectedStringsColumnCount = 0;
            var expectedBlobColumnCount = 0;
            var expectedGuidColumnCount = 0;

            TestCounts(tableId, expectedSingleByteColumnCount, expectedWordColumnCount, expectedDwordColumnCount,
                       expectedStringsColumnCount, expectedBlobColumnCount, expectedGuidColumnCount);
        }

        [Test]
        public void ShouldBeAbleToReturnCorrectColumnSizeCountsForFileTable()
        {
            var tableId = TableId.File;
            var expectedSingleByteColumnCount = 0;
            var expectedWordColumnCount = 0;
            var expectedDwordColumnCount = 1;
            var expectedStringsColumnCount = 1;
            var expectedBlobColumnCount = 1;
            var expectedGuidColumnCount = 0;

            TestCounts(tableId, expectedSingleByteColumnCount, expectedWordColumnCount, expectedDwordColumnCount,
                       expectedStringsColumnCount, expectedBlobColumnCount, expectedGuidColumnCount);
        }

        [Test]
        public void ShouldBeAbleToReturnCorrectColumnSizeCountsForManifestResourceTable()
        {
            var tableId = TableId.ManifestResource;
            var expectedSingleByteColumnCount = 0;
            var expectedWordColumnCount = 0;
            var expectedDwordColumnCount = 2;
            var expectedStringsColumnCount = 1;
            var expectedBlobColumnCount = 0;
            var expectedGuidColumnCount = 0;

            TestCounts(tableId, expectedSingleByteColumnCount, expectedWordColumnCount, expectedDwordColumnCount,
                       expectedStringsColumnCount, expectedBlobColumnCount, expectedGuidColumnCount);
        }

        [Test]
        public void ShouldBeAbleToReturnCorrectColumnSizeCountsForNestedClassTable()
        {
            var tableId = TableId.NestedClass;
            var expectedSingleByteColumnCount = 0;
            var expectedWordColumnCount = 0;
            var expectedDwordColumnCount = 0;
            var expectedStringsColumnCount = 0;
            var expectedBlobColumnCount = 0;
            var expectedGuidColumnCount = 0;

            TestCounts(tableId, expectedSingleByteColumnCount, expectedWordColumnCount, expectedDwordColumnCount,
                       expectedStringsColumnCount, expectedBlobColumnCount, expectedGuidColumnCount);
        }

        [Test]
        public void ShouldBeAbleToReturnCorrectColumnSizeCountsForGenericParamTable()
        {
            var tableId = TableId.GenericParam;
            var expectedSingleByteColumnCount = 0;
            var expectedWordColumnCount = 2;
            var expectedDwordColumnCount = 0;
            var expectedStringsColumnCount = 1;
            var expectedBlobColumnCount = 0;
            var expectedGuidColumnCount = 0;

            TestCounts(tableId, expectedSingleByteColumnCount, expectedWordColumnCount, expectedDwordColumnCount,
                       expectedStringsColumnCount, expectedBlobColumnCount, expectedGuidColumnCount);
        }

        [Test]
        public void ShouldBeAbleToReturnCorrectColumnSizeCountsForGenericParamConstraintTable()
        {
            var tableId = TableId.GenericParamConstraint;
            var expectedSingleByteColumnCount = 0;
            var expectedWordColumnCount = 0;
            var expectedDwordColumnCount = 0;
            var expectedStringsColumnCount = 0;
            var expectedBlobColumnCount = 0;
            var expectedGuidColumnCount = 0;

            TestCounts(tableId, expectedSingleByteColumnCount, expectedWordColumnCount, expectedDwordColumnCount,
                       expectedStringsColumnCount, expectedBlobColumnCount, expectedGuidColumnCount);
        }

        [Test]
        public void ShouldBeAbleToReturnCorrectColumnSizeCountsForMethodSpecTable()
        {
            var tableId = TableId.MethodSpec;
            var expectedSingleByteColumnCount = 0;
            var expectedWordColumnCount = 0;
            var expectedDwordColumnCount = 0;
            var expectedStringsColumnCount = 0;
            var expectedBlobColumnCount = 1;
            var expectedGuidColumnCount = 0;

            TestCounts(tableId, expectedSingleByteColumnCount, expectedWordColumnCount, expectedDwordColumnCount,
                       expectedStringsColumnCount, expectedBlobColumnCount, expectedGuidColumnCount);
        } 
        #endregion

        #region Private Members
        private void TestCounts(TableId tableId, int expectedSingleByteColumnCount, int expectedWordColumnCount, int expectedDwordColumnCount, int expectedStringsColumnCount, int expectedBlobColumnCount, int expectedGuidColumnCount)
        {


            var tableName = Enum.GetName(typeof(TableId), tableId);
            var schemaName = string.Format("{0}RowSchema", tableName);

            // Obtain the raw schema counts
            var result = container.GetInstance<ITuple<int, int, int, int, int, int, IEnumerable<ITuple<IEnumerable<TableId>, int>>>>(schemaName);

            Assert.IsNotNull(result);

            Assert.AreEqual(expectedSingleByteColumnCount, result.Item1, "Wrong single-byte column count");
            Assert.AreEqual(expectedWordColumnCount, result.Item2, "Wrong word column count");
            Assert.AreEqual(expectedDwordColumnCount, result.Item3, "Wrong dword column count");
            Assert.AreEqual(expectedStringsColumnCount, result.Item4, "Wrong #Strings column count");
            Assert.AreEqual(expectedBlobColumnCount, result.Item5, "Wrong #Blob column count");
            Assert.AreEqual(expectedGuidColumnCount, result.Item6, "Wrong #GUID column count");
        }

        private void TestRowSizeCalculation(TableId tableId, int expectedRowSize)
        {
            var stream = GetStream();
            var heapSizes = Tao.Interfaces.Tuple.New(2, 2, 2);
            var calculator = container.GetInstance<IFunction<ITuple<ITuple<TableId, Stream>, ITuple<int, int, int>>, int>>("CalculateMetadataTableRowSize");
            Assert.IsNotNull(calculator);

            var result = calculator.Execute(Tao.Interfaces.Tuple.New(tableId, stream), heapSizes);
            Assert.AreEqual(expectedRowSize, result);
        } 
        #endregion
    }
}
