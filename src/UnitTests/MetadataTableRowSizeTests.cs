using System;
using System.Collections.Generic;
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
            var container = CreateContainer();
            var heapSizes = Tuple.New(2, 2, 2);
            var calculator = container.GetInstance<IFunction<ITuple<TableId, ITuple<int, int, int>>, int>>("CalculateMetadataTableRowSize");
            Assert.IsNotNull(calculator);

            var result = calculator.Execute(TableId.Assembly, heapSizes);
            Assert.AreEqual(22, result);
        }

        [Test]
        public void ShouldBeAbleToReturnCorrectColumnSizeCountsForAssemblyTable()
        {
            var container = CreateContainer();

            var getRowSchema = container.GetInstance<IFunction<TableId, ITuple<int, int, int, int, int>>>("GetMetadataTableColumnSizeCounts");
            Assert.IsNotNull(getRowSchema);

            var result = getRowSchema.Execute(TableId.Assembly);
            Assert.IsNotNull(result);

            Assert.AreEqual(4, result.Item1, "Wrong word column count");
            Assert.AreEqual(2, result.Item2, "Wrong dword column count");
            Assert.AreEqual(2, result.Item3, "Wrong #Strings column count");
            Assert.AreEqual(1, result.Item4, "Wrong #Blob column count");
            Assert.AreEqual(0, result.Item5, "Wrong #GUID column count");
        }
    }
}
