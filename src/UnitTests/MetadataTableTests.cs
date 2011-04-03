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
        public void ShouldBeAbleToReadInterfaceImplTableStreamAtCorrectPosition()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var targetFile = Path.Combine(baseDirectory, "SampleBinaries\\LinFu.Core.dll");
            var stream = new FileStream(targetFile, FileMode.Open, FileAccess.Read);

            var table = GetTable(TableId.InterfaceImpl, stream);

            var rowCount = table.Item1;
            Assert.AreEqual(rowCount, 645);

            var tableStream = table.Item2;
            var reader = new BinaryReader(tableStream);

            // Check the first row
            var classValue = reader.ReadUInt16();
            var interfaceValue = reader.ReadUInt16();

            Assert.AreEqual(4, classValue);
            Assert.AreEqual(8, interfaceValue);

            // Check a random middle row (row 355)
            reader.BaseStream.Seek(0x588, SeekOrigin.Begin);
            Assert.AreEqual(0x1dc, reader.ReadUInt16());
            Assert.AreEqual(0x568, reader.ReadUInt16());

            // Check the last row
            reader.BaseStream.Seek(0xa10, SeekOrigin.Begin);
            Assert.AreEqual(0x420, reader.ReadUInt16());
            Assert.AreEqual(0x1F1, reader.ReadUInt16());
        }

        [Test]
        public void ShouldBeAbleToReadMemberRefRowTableStreamAtCorrectPosition()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var targetFile = Path.Combine(baseDirectory, "SampleBinaries\\LinFu.Core.dll");
            var stream = new FileStream(targetFile, FileMode.Open, FileAccess.Read);

            var table = GetTable(TableId.MemberRef, stream);
            var rowCount = table.Item1;
            Assert.AreEqual(rowCount, 1613);

            var tableStream = table.Item2;
            var reader = new BinaryReader(tableStream);

            var classValue = reader.ReadUInt16();
            var nameValue = reader.ReadUInt32();
            var signature = reader.ReadUInt32();

            // Match the first row
            Assert.AreEqual(0x24, classValue);
            Assert.AreEqual(0x7EC1, nameValue);
            Assert.AreEqual(0x767E, signature);

            // Match a random middle row (row number 806)
            tableStream.Seek(0x1F7C, SeekOrigin.Begin);
            Assert.AreEqual(0x284, reader.ReadUInt16());
            Assert.AreEqual(0x897B, reader.ReadUInt32());
            Assert.AreEqual(0xBC2B, reader.ReadUInt32());

            // Match the last row
            tableStream.Seek(0x3EF8, SeekOrigin.Begin);
            Assert.AreEqual(0x1454, reader.ReadUInt16());
            Assert.AreEqual(0xFE4F, reader.ReadUInt32());
            Assert.AreEqual(0x7C42, reader.ReadUInt32());
        }

        [Test]
        public void ShouldBeAbleToReadStandAloneSigTableStreamAtCorrectPosition()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var targetFile = Path.Combine(baseDirectory, "SampleBinaries\\LinFu.Core.dll");
            var stream = new FileStream(targetFile, FileMode.Open, FileAccess.Read);

            var table = GetTable(TableId.StandAloneSig, stream);
            var rowCount = table.Item1;
            var tableStream = table.Item2;

            Assert.AreEqual(rowCount, 3832);

            // Match the first row
            var reader = new BinaryReader(tableStream);
            Assert.AreEqual(0xC982, reader.ReadUInt32());

            // Match the middle row (row 1916)
            tableStream.Seek(0x1DEC, SeekOrigin.Begin);
            Assert.AreEqual(0xE460, reader.ReadUInt32());

            // Match the last row
            tableStream.Seek(0x3BDC, SeekOrigin.Begin);
            Assert.AreEqual(0x631D, reader.ReadUInt32());
        }

        [Test]
        public void ShouldBeAbleToReadPropertyMapTableStreamAtCorrectPosition()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var targetFile = Path.Combine(baseDirectory, "SampleBinaries\\LinFu.Core.dll");
            var stream = new FileStream(targetFile, FileMode.Open, FileAccess.Read);

            var table = GetTable(TableId.PropertyMap, stream);
            var rowCount = table.Item1;
            var tableStream = table.Item2;

            Assert.AreEqual(rowCount, 296);
            Assert.AreEqual(0x4A0, tableStream.Length);

            var reader = new BinaryReader(tableStream);

            // Match the first row
            Assert.AreEqual(0xF, reader.ReadUInt16());
            Assert.AreEqual(1, reader.ReadUInt16());

            // Match the middle row (row 148)
            tableStream.Seek(0x24C, SeekOrigin.Begin);
            Assert.AreEqual(0x1BD, reader.ReadUInt16());
            Assert.AreEqual(0x1B0, reader.ReadUInt16());

            // Match the last row
            tableStream.Seek(0x49C, SeekOrigin.Begin);
            Assert.AreEqual(0x41E, reader.ReadUInt16());
            Assert.AreEqual(0x42A, reader.ReadUInt16());
        }

        [Test]
        public void ShouldBeAbleToReadPropertyTableStreamAtCorrectPosition()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var targetFile = Path.Combine(baseDirectory, "SampleBinaries\\LinFu.Core.dll");
            var stream = new FileStream(targetFile, FileMode.Open, FileAccess.Read);

            var table = GetTable(TableId.Property, stream);
            var rowCount = table.Item1;
            var tableStream = table.Item2;

            Assert.AreEqual(rowCount, 1068);
            Assert.AreEqual(0x29B8, tableStream.Length);

            // Match the first row
            var reader = new BinaryReader(tableStream);
            Assert.AreEqual(0, reader.ReadUInt16());
            Assert.AreEqual(0x3E46, reader.ReadUInt32());
            Assert.AreEqual(0xC1A7, reader.ReadUInt32());

            // Match the middle row
            tableStream.Seek(0x14D2, SeekOrigin.Begin);
            Assert.AreEqual(0, reader.ReadUInt16());
            Assert.AreEqual(0x1261E, reader.ReadUInt32());
            Assert.AreEqual(0xC26A, reader.ReadUInt32());

            // Match the last row
            tableStream.Seek(0x29AE, SeekOrigin.Begin);
            Assert.AreEqual(0, reader.ReadUInt16());
            Assert.AreEqual(0x1FD4, reader.ReadUInt32());
            Assert.AreEqual(0xC1B2, reader.ReadUInt32());
        }

        [Test]
        public void ShouldBeAbleToReadCustomAttributeTableStreamAtCorrectPosition()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var targetFile = Path.Combine(baseDirectory, "SampleBinaries\\LinFu.Core.dll");
            var stream = new FileStream(targetFile, FileMode.Open, FileAccess.Read);

            var table = GetTable(TableId.CustomAttribute, stream);
            var rowCount = table.Item1;
            Assert.AreEqual(rowCount, 1100);

            var tableStream = table.Item2;
            Assert.AreEqual(0x2AF8, tableStream.Length);

            var reader = new BinaryReader(tableStream);

            // Match the first row
            Assert.AreEqual(0x2E, reader.ReadUInt32());
            Assert.AreEqual(0x315B, reader.ReadUInt16());
            Assert.AreEqual(0x3B6A, reader.ReadUInt32());

            // Match the middle row (row 550)
            tableStream.Seek(0x1572, SeekOrigin.Begin);
            Assert.AreEqual(0x5103, reader.ReadUInt32());
            Assert.AreEqual(0x31FB, reader.ReadUInt16());
            Assert.AreEqual(0x56C9, reader.ReadUInt32());

            // Match the last row
            tableStream.Seek(0x2AEE, SeekOrigin.Begin);
            Assert.AreEqual(0x2A880, reader.ReadUInt32());
            Assert.AreEqual(0x320B, reader.ReadUInt16());
            Assert.AreEqual(0x62EA, reader.ReadUInt32());
        }

        [Test]
        public void ShouldBeAbleToReadFieldMarshalTableStreamAtCorrectPosition()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var targetFile = Path.Combine(baseDirectory, "SampleBinaries\\LinFu.Core.dll");
            var stream = new FileStream(targetFile, FileMode.Open, FileAccess.Read);

            var table = GetTable(TableId.FieldMarshal, stream);
            var rowCount = table.Item1;

            Assert.AreEqual(32, rowCount);

            var tableStream = table.Item2;
            Assert.AreEqual(0xC0, tableStream.Length);

            // Match the first row 
            var reader = new BinaryReader(tableStream);
            Assert.AreEqual(0x26ED, reader.ReadUInt16());
            Assert.AreEqual(0x72CC, reader.ReadUInt32());

            // Match the middle row (row 16)
            tableStream.Seek(0x5A, SeekOrigin.Begin);
            Assert.AreEqual(0x27D5, reader.ReadUInt16());
            Assert.AreEqual(0x72CE, reader.ReadUInt32());

            // Read the last row
            tableStream.Seek(0xBA, SeekOrigin.Begin);
            Assert.AreEqual(0x289D, reader.ReadUInt16());
            Assert.AreEqual(0x72CE, reader.ReadUInt32());
        }

        [Test]
        public void ShouldBeAbleToReadConstantTableStreamAtCorrectPosition()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var targetFile = Path.Combine(baseDirectory, "SampleBinaries\\LinFu.Core.dll");
            var stream = new FileStream(targetFile, FileMode.Open, FileAccess.Read);

            var table = GetTable(TableId.Constant, stream);
            var rowCount = table.Item1;
            Assert.AreEqual(rowCount, 1345);

            var tableStream = table.Item2;
            Assert.AreEqual(0x2A08, tableStream.Length);

            var reader = new BinaryReader(tableStream);

            // Match the first row
            Assert.AreEqual(0x8, reader.ReadByte());
            Assert.AreEqual(0, reader.ReadByte());
            Assert.AreEqual(0x414, reader.ReadUInt16());
            Assert.AreEqual(0x2039, reader.ReadUInt32());

            // Match the middle row (row 672)
            tableStream.Seek(0x14F8, SeekOrigin.Begin);
            Assert.AreEqual(0x8, reader.ReadByte());
            Assert.AreEqual(0, reader.ReadByte());
            Assert.AreEqual(0x228C, reader.ReadUInt16());

            // Mach the last row
            tableStream.Seek(0x2A00, SeekOrigin.Begin);
            Assert.AreEqual(0x5, reader.ReadByte());
            Assert.AreEqual(0, reader.ReadByte());
            Assert.AreEqual(0x3D74, reader.ReadUInt16());
            Assert.AreEqual(0x3B68, reader.ReadUInt32());
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
