using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Tao.Containers;
using Tao.Interfaces;

namespace Tao.UnitTests
{
    [TestFixture]
    public class MetadataTableTests : BaseStreamTests
    {
        [Test]
        public void ShouldBeAbleToEnumerateTablesThatAlreadyExist()
        {
            var stream = GetStream();


            var enumerator = container.GetInstance<IFunction<Stream, IEnumerable<TableId>>>("EnumerateExistingMetadataTables");
            Assert.IsNotNull(enumerator);

            var results = enumerator.Execute(stream);
            var tableIds = new List<TableId>(results);
            Assert.IsTrue(tableIds.Count > 0);

            Assert.IsTrue(tableIds.Contains(TableId.Assembly));
            Assert.IsTrue(tableIds.Contains(TableId.Module));
            Assert.IsTrue(tableIds.Contains(TableId.MethodDef));
            Assert.IsTrue(tableIds.Contains(TableId.TypeDef));
        }

        [Test]
        public void ShouldBeAbleToGetRowCountsForAllMetadataTables()
        {
            var stream = GetStream();
            var counter = container.GetInstance<IFunction<Stream, IDictionary<TableId, int>>>("ReadMetadataTableRowCounts");
            Assert.IsNotNull(counter);

            var result = counter.Execute(stream);

            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Count);

            Assert.AreEqual(1, result[TableId.Module]);
            Assert.AreEqual(1, result[TableId.TypeDef]);
            Assert.AreEqual(1, result[TableId.MethodDef]);
            Assert.AreEqual(1, result[TableId.Assembly]);
        }

        [Test]
        public void ShouldBeAbleToReadHeapSizesField()
        {
            var stream = GetStream();

            var reader = container.GetInstance<IFunction<Stream, byte?>>("ReadHeapSizesField");
            Assert.IsNotNull(reader);

            var result = reader.Execute(stream);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result);
        }

        [Test]
        public void ShouldBeAbleToReadHeapSizes()
        {
            var stream = GetStream();

            var reader = container.GetInstance<IFunction<Stream, ITuple<int, int, int>>>("ReadMetadataHeapIndexSizes");
            Assert.IsNotNull(reader);

            var results = reader.Execute(stream);
            Assert.IsNotNull(results);

            Assert.AreEqual(results.Item1, 2, "Incorrect #Strings heap index size");
            Assert.AreEqual(results.Item2, 2, "Incorrect #Blob heap index size");
            Assert.AreEqual(results.Item3, 2, "Incorrect #Blob heap index size");
        }

        [Test]
        public void ShouldBeAbleToReadAllMetadataTableRowCounts()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var targetFile = Path.Combine(baseDirectory, "SampleBinaries\\LinFu.Core.dll");
            var stream = new FileStream(targetFile, FileMode.Open, FileAccess.Read);
            var reader = container.GetInstance<IFunction<ITuple<Func<TableId, bool>, Stream>, IDictionary<TableId, ITuple<int, Stream>>>>();
            Assert.IsNotNull(reader);

            Func<TableId, bool> predicate = tableId => true;
            IDictionary<TableId, ITuple<int, Stream>> tables;
            tables = reader.Execute(predicate, stream);
            Assert.IsNotNull(tables);

            var expectedRowCounts = new Dictionary<TableId, int>();
            expectedRowCounts[TableId.Module] = 1;
            expectedRowCounts[TableId.TypeRef] = 222;
            expectedRowCounts[TableId.TypeDef] = 1061;
            expectedRowCounts[TableId.Field] = 3964;
            expectedRowCounts[TableId.MethodDef] = 5568;
            expectedRowCounts[TableId.Param] = 5401;
            expectedRowCounts[TableId.InterfaceImpl] = 645;
            expectedRowCounts[TableId.MemberRef] = 1613;
            expectedRowCounts[TableId.Constant] = 1345;
            expectedRowCounts[TableId.CustomAttribute] = 1100;
            expectedRowCounts[TableId.FieldMarshal] = 32;
            expectedRowCounts[TableId.ClassLayout] = 8;
            expectedRowCounts[TableId.StandAloneSig] = 3832;
            expectedRowCounts[TableId.PropertyMap] = 296;
            expectedRowCounts[TableId.Property] = 1068;
            expectedRowCounts[TableId.MethodSemantics] = 1605;
            expectedRowCounts[TableId.MethodImpl] = 56;
            expectedRowCounts[TableId.ModuleRef] = 1;
            expectedRowCounts[TableId.TypeSpec] = 894;
            expectedRowCounts[TableId.ImplMap] = 2;
            expectedRowCounts[TableId.FieldRVA] = 4;
            expectedRowCounts[TableId.Assembly] = 1;
            expectedRowCounts[TableId.AssemblyRef] = 6;
            expectedRowCounts[TableId.NestedClass] = 115;
            expectedRowCounts[TableId.GenericParam] = 229;
            expectedRowCounts[TableId.MethodSpec] = 233;

            foreach (var tableId in expectedRowCounts.Keys)
            {
                var rowCount = expectedRowCounts[tableId];
                tables.ShouldHaveExpectedRowCount(tableId, rowCount);
            }
        }

        [Test]
        public void ShouldBeAbleToReadClassLayoutTableStream()
        {
            var tableId = TableId.ClassLayout;
            var expectedStreamLength = 0x40;
            var expectedRowCount = 8;

            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var targetFile = Path.Combine(baseDirectory, "SampleBinaries\\LinFu.Core.dll");
            var stream = new FileStream(targetFile, FileMode.Open, FileAccess.Read);

            TestTableRead(id => GetTable(id, stream), tableId, expectedRowCount, expectedStreamLength);
        }

        [Test]
        public void ShouldBeAbleToReadAssemblyTableStream()
        {
            var tableId = TableId.Assembly;
            var expectedStreamLength = 22;
            var expectedRowCount = 1;

            TestTableRead(tableId, expectedRowCount, expectedStreamLength);
        }

        [Test]
        public void ShouldBeAbleToReadModuleTableStream()
        {
            var tableId = TableId.Module;
            var result = GetTable(tableId);
            var tableStream = result.Item2;

            Assert.AreEqual(1, result.Item1, "Wrong row count");
            Assert.AreEqual(0xA, tableStream.Length, "Wrong Stream Length");
            return;
        }


        private void TestTableRead(Func<TableId, ITuple<int, Stream>> getTable, TableId tableId, int expectedRowCount, int expectedStreamLength)
        {
            var result = getTable(tableId);
            var tableStream = result.Item2;

            Assert.AreEqual(expectedRowCount, result.Item1, "Wrong row count");
            Assert.AreEqual(expectedStreamLength, tableStream.Length, "Wrong Stream Length");
        }

        private void TestTableRead(TableId tableId, int expectedRowCount, int expectedStreamLength)
        {
            TestTableRead(GetTable, tableId, expectedRowCount, expectedStreamLength);
        }
        private ITuple<int, Stream> GetTable(TableId tableId)
        {
            return GetTable(tableId, GetStream());
        }

        private ITuple<int, Stream> GetTable(TableId tableId, Stream stream)
        {
            var reader = container.GetInstance<IFunction<Stream, IDictionary<TableId, ITuple<int, Stream>>>>("ReadAllMetadataTables");
            Assert.IsNotNull(reader);

            var tables = reader.Execute(stream);
            Assert.IsNotNull(tables);
            Assert.IsTrue(tables.ContainsKey(tableId));

            return tables[tableId];
        }
    }
}
