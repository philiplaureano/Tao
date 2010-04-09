using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tao.Core;
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
            var stream = GetStream();
            var container = CreateContainer();
            var heapSizes = Tuple.New(2, 2, 2);
            var calculator = container.GetInstance<IFunction<ITuple<ITuple<TableId, Stream>, ITuple<int, int, int>>, int>>("CalculateMetadataTableRowSize");
            Assert.IsNotNull(calculator);

            var result = calculator.Execute(Tuple.New(TableId.Assembly, stream), heapSizes);
            Assert.AreEqual(22, result);
        }

        [Test]
        public void ShouldBeAbleToReturnCorrectColumnSizeCountsForAssemblyTable()
        {
            var tableId = TableId.Assembly;
            var expectedWordColumnCount = 4;
            var expectedDwordColumnCount = 2;
            var expectedStringsColumnCount = 2;
            var expectedBlobColumnCount = 1;
            var expectedGuidColumnCount = 0;

            TestCounts(tableId, expectedWordColumnCount, expectedDwordColumnCount,
                expectedStringsColumnCount, expectedBlobColumnCount, expectedGuidColumnCount);
        }

        [Test]
        public void ShouldBeAbleToReturnCorrectColumnSizeCountsForAssemblyRefTable()
        {
            var tableId = TableId.AssemblyRef;
            var expectedWordColumnCount = 4;
            var expectedDwordColumnCount = 1;
            var expectedStringsColumnCount = 2;
            var expectedBlobColumnCount = 1;
            var expectedGuidColumnCount = 0;

            TestCounts(tableId, expectedWordColumnCount, expectedDwordColumnCount,
                expectedStringsColumnCount, expectedBlobColumnCount, expectedGuidColumnCount);
        }

        private void TestCounts(TableId tableId, int expectedWordColumnCount, int expectedDwordColumnCount, int expectedStringsColumnCount, int expectedBlobColumnCount, int expectedGuidColumnCount)
        {
            var stream = GetStream();
            var container = CreateContainer();
            var getRowSchema = container.GetInstance<IFunction<ITuple<TableId, Stream>, ITuple<int, int, int, int, int>>>("GetMetadataTableColumnSizeCounts");
            Assert.IsNotNull(getRowSchema);

            var result = getRowSchema.Execute(tableId, stream);
            Assert.IsNotNull(result);

            Assert.AreEqual(expectedWordColumnCount, result.Item1, "Wrong word column count");
            Assert.AreEqual(expectedDwordColumnCount, result.Item2, "Wrong dword column count");
            Assert.AreEqual(expectedStringsColumnCount, result.Item3, "Wrong #Strings column count");
            Assert.AreEqual(expectedBlobColumnCount, result.Item4, "Wrong #Blob column count");
            Assert.AreEqual(expectedGuidColumnCount, result.Item5, "Wrong #GUID column count");
        }
    }
}
