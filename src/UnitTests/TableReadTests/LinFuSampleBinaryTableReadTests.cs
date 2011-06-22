using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tao.Interfaces;
using Tao.Containers;

namespace Tao.UnitTests.TableReadTests
{
    [TestFixture]
    public class LinFuSampleBinaryTableReadTests : BaseMetadataTableTests
    {
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
        public void ShouldBeAbleToReadMethodSemanticsTableStreamAtCorrectPosition()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var targetFile = Path.Combine(baseDirectory, "SampleBinaries\\LinFu.Core.dll");
            var stream = new FileStream(targetFile, FileMode.Open, FileAccess.Read);

            var table = GetTable(TableId.MethodSemantics, stream);
            var rowCount = table.Item1;
            var tableStream = table.Item2;
            Assert.AreEqual(rowCount, 1605);

            Assert.AreEqual(0x259E, tableStream.Length);

            var reader = new BinaryReader(tableStream);

            // Match the first row
            Assert.AreEqual(2, reader.ReadUInt16());
            Assert.AreEqual(0x52, reader.ReadUInt16());
            Assert.AreEqual(3, reader.ReadUInt16());

            // Match the middle row (row 803)
            tableStream.Seek(0x12CC, SeekOrigin.Begin);
            Assert.AreEqual(2, reader.ReadUInt16());
            Assert.AreEqual(0xAB8, reader.ReadUInt16());
            Assert.AreEqual(0x44F, reader.ReadUInt16());

            // Match the last row
            tableStream.Seek(0x2598, SeekOrigin.Begin);
            Assert.AreEqual(2, reader.ReadUInt16());
            Assert.AreEqual(0x15B4, reader.ReadUInt16());
            Assert.AreEqual(0x859, reader.ReadUInt16());
        }

        [Test]
        public void ShouldBeAbleToReadMethodImplTableStreamAtCorrectPosition()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var targetFile = Path.Combine(baseDirectory, "SampleBinaries\\LinFu.Core.dll");
            var stream = new FileStream(targetFile, FileMode.Open, FileAccess.Read);

            var table = GetTable(TableId.MethodImpl, stream);
            var rowCount = table.Item1;

            var tableStream = table.Item2;
            Assert.AreEqual(rowCount, 56);
            Assert.AreEqual(0x150, tableStream.Length);

            // Match the first row
            var reader = new BinaryReader(tableStream);
            Assert.AreEqual(0xE5, reader.ReadUInt16());
            Assert.AreEqual(0x7C0, reader.ReadUInt16());
            Assert.AreEqual(0x48A, reader.ReadUInt16());

            // Match a middle row
            tableStream.Seek(0xA2, SeekOrigin.Begin);
            Assert.AreEqual(0x293, reader.ReadUInt16());
            Assert.AreEqual(0x24AA, reader.ReadUInt16());
            Assert.AreEqual(0x983, reader.ReadUInt16());

            // Match the last row
            tableStream.Seek(0x14A, SeekOrigin.Begin);
            Assert.AreEqual(0x3F1, reader.ReadUInt16());
            Assert.AreEqual(0x2A86, reader.ReadUInt16());
            Assert.AreEqual(0x002B, reader.ReadUInt16());
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
        public void ShouldBeAbleToReadModuleRefStream()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var targetFile = Path.Combine(baseDirectory, "SampleBinaries\\LinFu.Core.dll");
            var stream = new FileStream(targetFile, FileMode.Open, FileAccess.Read);

            var table = GetTable(TableId.ModuleRef, stream);
            var rowCount = table.Item1;
            var tableStream = table.Item2;

            var reader = new BinaryReader(tableStream);

            Assert.AreEqual(1, rowCount);
            Assert.AreEqual(4, tableStream.Length);
            Assert.AreEqual(0x1077C, reader.ReadUInt32());
        }


        [Test]
        public void ShouldBeAbleToReadClassLayoutTableStreamAtCorrectPosition()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var targetFile = Path.Combine(baseDirectory, "SampleBinaries\\LinFu.Core.dll");
            var stream = new FileStream(targetFile, FileMode.Open, FileAccess.Read);

            var table = GetTable(TableId.ClassLayout, stream);
            var rowCount = table.Item1;
            var tableStream = table.Item2;

            Assert.AreEqual(8, rowCount);
            Assert.AreEqual(0x40, tableStream.Length);

            var reader = new BinaryReader(tableStream);

            // Match the first row
            Assert.AreEqual(0, reader.ReadUInt16());
            Assert.AreEqual(1, reader.ReadUInt32());
            Assert.AreEqual(0x2D9, reader.ReadUInt16());

            // Match the middle row (row 4)
            tableStream.Seek(0x18, SeekOrigin.Begin);
            Assert.AreEqual(0, reader.ReadUInt16());
            Assert.AreEqual(1, reader.ReadUInt32());
            Assert.AreEqual(0x36A, reader.ReadUInt16());

            // Match the last row
            tableStream.Seek(0x38, SeekOrigin.Begin);
            Assert.AreEqual(1, reader.ReadUInt16());
            Assert.AreEqual(0x20, reader.ReadUInt32());
            Assert.AreEqual(0x422, reader.ReadUInt16());
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
        public void ShouldBeAbleToReadMethodSpecTableAtCorrectPosition()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var targetFile = Path.Combine(baseDirectory, "SampleBinaries\\LinFu.Core.dll");
            var stream = new FileStream(targetFile, FileMode.Open, FileAccess.Read);

            var table = GetTable(TableId.MethodSpec, stream);
            var rowCount = table.Item1;
            Assert.AreEqual(rowCount, 233);

            var tableStream = table.Item2;
            Assert.AreEqual(0x576, tableStream.Length);

            // Match the first row
            var reader = new BinaryReader(tableStream);
            Assert.AreEqual(0xB88, reader.ReadInt16());
            Assert.AreEqual(0x19, reader.ReadUInt32());

            // Match the middle row (row 116)
            tableStream.Seek(0x2B2, SeekOrigin.Begin);
            Assert.AreEqual(0x5DE, reader.ReadUInt16());
            Assert.AreEqual(0x8DD, reader.ReadUInt16());

            tableStream.Seek(0x570, SeekOrigin.Begin);
            Assert.AreEqual(0x5A6, reader.ReadUInt16());
            Assert.AreEqual(0x945, reader.ReadUInt32());
        }

        [Test]
        public void ShouldHaveGenericParamConstraintTable()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var targetFile = Path.Combine(baseDirectory, "SampleBinaries\\LinFu.Core.dll");
            var stream = new FileStream(targetFile, FileMode.Open, FileAccess.Read);

            var enumerator = container.GetInstance<IFunction<Stream, IEnumerable<TableId>>>();
            var tables = enumerator.Execute(stream).ToArray();

            Assert.IsTrue(tables.Contains(TableId.GenericParamConstraint));
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
        public void ShouldBeAbleToReadTypeSpecTableStreamAtCorrectPosition()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var targetFile = Path.Combine(baseDirectory, "SampleBinaries\\LinFu.Core.dll");
            var stream = new FileStream(targetFile, FileMode.Open, FileAccess.Read);

            var table = GetTable(TableId.TypeSpec, stream);
            var rowCount = table.Item1;
            Assert.AreEqual(rowCount, 894);

            var tableStream = table.Item2;
            Assert.AreEqual(0xDF8, tableStream.Length);

            var reader = new BinaryReader(tableStream);

            // Match the first row
            Assert.AreEqual(1, reader.ReadUInt32());

            // Match the middle row (row 447)
            tableStream.Seek(0x6F8, SeekOrigin.Begin);
            Assert.AreEqual(0x167F, reader.ReadUInt32());

            // Match the last row
            tableStream.Seek(0xDF4, SeekOrigin.Begin);
            Assert.AreEqual(0x10C38, reader.ReadUInt32());
        }

        [Test]
        public void ShouldBeAbleToReadImplMapTableStreamAtCorrectPosition()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var targetFile = Path.Combine(baseDirectory, "SampleBinaries\\LinFu.Core.dll");
            var stream = new FileStream(targetFile, FileMode.Open, FileAccess.Read);

            var table = GetTable(TableId.ImplMap, stream);
            var rowCount = table.Item1;
            Assert.AreEqual(rowCount, 2);

            var tableStream = table.Item2;
            Assert.AreEqual(0x14, tableStream.Length);

            var reader = new BinaryReader(tableStream);

            // Match the first row
            Assert.AreEqual(0x100, reader.ReadUInt16());
            Assert.AreEqual(0x27FF, reader.ReadInt16());
            Assert.AreEqual(1, reader.ReadUInt32());
            Assert.AreEqual(1, reader.ReadUInt16());

            // Match the last row
            Assert.AreEqual(0x100, reader.ReadUInt16());
            Assert.AreEqual(0x2929, reader.ReadInt16());
            Assert.AreEqual(1, reader.ReadUInt32());
            Assert.AreEqual(1, reader.ReadUInt16());
        }

        [Test]
        public void ShouldBeAbleToReadFieldRvaTableStreamAtCorrectPosition()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var targetFile = Path.Combine(baseDirectory, "SampleBinaries\\LinFu.Core.dll");
            var stream = new FileStream(targetFile, FileMode.Open, FileAccess.Read);

            var table = GetTable(TableId.FieldRVA, stream);
            var rowCount = table.Item1;
            Assert.AreEqual(rowCount, 4);

            var tableStream = table.Item2;
            Assert.AreEqual(0x18, tableStream.Length);

            var reader = new BinaryReader(tableStream);

            // Match the first row
            Assert.AreEqual(0x4CF08, reader.ReadUInt32());
            Assert.AreEqual(0x987, reader.ReadUInt16());

            // Match the middle row
            Assert.AreEqual(0x4CF44, reader.ReadUInt32());
            Assert.AreEqual(0x988, reader.ReadUInt16());

            // Match the last row
            tableStream.Seek(0x12, SeekOrigin.Begin);
            Assert.AreEqual(0x4D0A4, reader.ReadInt32());
            Assert.AreEqual(0xE81, reader.ReadUInt16());
        }

        [Test]
        public void ShouldBeAbleToReadAssemblyTableStreamAtCorrectPosition()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var targetFile = Path.Combine(baseDirectory, "SampleBinaries\\LinFu.Core.dll");
            var stream = new FileStream(targetFile, FileMode.Open, FileAccess.Read);

            var table = GetTable(TableId.Assembly, stream);
            var rowCount = table.Item1;
            Assert.AreEqual(rowCount, 1);

            var reader = new BinaryReader(table.Item2);
            Assert.AreEqual(0x8004, reader.ReadUInt32());
            Assert.AreEqual(2, reader.ReadUInt16());
            Assert.AreEqual(3, reader.ReadUInt16());
            Assert.AreEqual(0, reader.ReadUInt16());
            Assert.AreEqual(0x2FD6, reader.ReadUInt16());
            Assert.AreEqual(0, reader.ReadUInt32());
            Assert.AreEqual(0x1F97, reader.ReadUInt32());
            Assert.AreEqual(0x12, reader.ReadUInt32());
            Assert.AreEqual(0, reader.ReadUInt32());
        }

        [Test]
        public void ShouldBeAbleToReadAssemblyRefStreamAtCorrectPosition()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var targetFile = Path.Combine(baseDirectory, "SampleBinaries\\LinFu.Core.dll");
            var stream = new FileStream(targetFile, FileMode.Open, FileAccess.Read);

            var table = GetTable(TableId.AssemblyRef, stream);
            var rowCount = table.Item1;
            var tableStream = table.Item2;

            Assert.AreEqual(6, rowCount);
            Assert.AreEqual(0xA8, tableStream.Length);

            var reader = new BinaryReader(tableStream);

            // Match the first row
            Assert.AreEqual(2, reader.ReadUInt16());
            Assert.AreEqual(0, reader.ReadUInt16());
            Assert.AreEqual(0, reader.ReadUInt16());
            Assert.AreEqual(0, reader.ReadUInt16());

            Assert.AreEqual(0, reader.ReadUInt32());
            Assert.AreEqual(0x10D73, reader.ReadUInt32());
            Assert.AreEqual(0x181C5, reader.ReadUInt32());
            Assert.AreEqual(0, reader.ReadUInt32());
            Assert.AreEqual(0, reader.ReadUInt32());

            // Match the middle row
            tableStream.Seek(0x38, SeekOrigin.Begin);
            Assert.AreEqual(2, reader.ReadUInt16());
            Assert.AreEqual(0, reader.ReadUInt16());
            Assert.AreEqual(0, reader.ReadUInt16());
            Assert.AreEqual(0, reader.ReadUInt16());

            Assert.AreEqual(0, reader.ReadUInt32());
            Assert.AreEqual(0x10D73, reader.ReadUInt32());
            Assert.AreEqual(0x2E77, reader.ReadUInt32());
            Assert.AreEqual(0, reader.ReadUInt32());
            Assert.AreEqual(0, reader.ReadUInt32());

            // Match the last row
            tableStream.Seek(0x8C, SeekOrigin.Begin);
            Assert.AreEqual(2, reader.ReadUInt16());
            Assert.AreEqual(0, reader.ReadUInt16());
            Assert.AreEqual(0, reader.ReadUInt16());
            Assert.AreEqual(0, reader.ReadUInt16());

            Assert.AreEqual(0, reader.ReadUInt32());
            Assert.AreEqual(0x10D7C, reader.ReadUInt32());
            Assert.AreEqual(0x1801E, reader.ReadUInt32());
            Assert.AreEqual(0, reader.ReadUInt32());
            Assert.AreEqual(0, reader.ReadUInt32());
        }

        [Test]
        public void ShouldBeAbleToReadMethodSpecTableStreamAtCorrectPosition()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var targetFile = Path.Combine(baseDirectory, "SampleBinaries\\LinFu.Core.dll");
            var stream = new FileStream(targetFile, FileMode.Open, FileAccess.Read);

            var table = GetTable(TableId.MethodSpec, stream);

            var rowCount = table.Item1;
            var tableStream = table.Item2;

            Assert.AreEqual(233, rowCount);
            Assert.AreEqual(0x576, tableStream.Length);

            var reader = new BinaryReader(tableStream);
            // Match the first row
            Assert.AreEqual(0xB88, reader.ReadUInt16());
            Assert.AreEqual(0x19, reader.ReadUInt32());

            // Match the middle row (row 116)
            tableStream.Seek(0x2B2, SeekOrigin.Begin);
            Assert.AreEqual(0x5DE, reader.ReadUInt16());
            Assert.AreEqual(0x8DD, reader.ReadUInt32());

            // Match the last row
            tableStream.Seek(0x570, SeekOrigin.Begin);
            Assert.AreEqual(0x5A6, reader.ReadUInt16());
            Assert.AreEqual(0x945, reader.ReadUInt32());
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
        public void ShouldBeAbleToReadNestedClassTableStreamAtCorrectPosition()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var targetFile = Path.Combine(baseDirectory, "SampleBinaries\\LinFu.Core.dll");
            var stream = new FileStream(targetFile, FileMode.Open, FileAccess.Read);

            var table = GetTable(TableId.NestedClass, stream);
            var rowCount = table.Item1;
            Assert.AreEqual(rowCount, 115);

            var tableStream = table.Item2;
            Assert.AreEqual(0x1CC, tableStream.Length);

            var reader = new BinaryReader(tableStream);

            // Match the first row
            Assert.AreEqual(0x3B3, reader.ReadUInt16());
            Assert.AreEqual(0x11, reader.ReadUInt16());

            // Match the middle row (row 57)
            tableStream.Seek(0xE0, SeekOrigin.Begin);
            Assert.AreEqual(0x3EB, reader.ReadUInt16());
            Assert.AreEqual(0x125, reader.ReadUInt16());

            // Match the last row
            tableStream.Seek(0x1C8, SeekOrigin.Begin);
            Assert.AreEqual(0x425, reader.ReadUInt16());
            Assert.AreEqual(0x3E8, reader.ReadUInt16());
        }

        [Test]
        public void ShouldBeAbleToReadGenericParamTableStreamAtCorrectPosition()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var targetFile = Path.Combine(baseDirectory, "SampleBinaries\\LinFu.Core.dll");
            var stream = new FileStream(targetFile, FileMode.Open, FileAccess.Read);

            var table = GetTable(TableId.GenericParam, stream);
            var rowCount = table.Item1;
            Assert.AreEqual(rowCount, 229);

            var tableStream = table.Item2;
            Assert.AreEqual(0x8F2, tableStream.Length);

            var reader = new BinaryReader(tableStream);

            // Match the first row
            Assert.AreEqual(0, reader.ReadUInt16());
            Assert.AreEqual(0, reader.ReadUInt16());
            Assert.AreEqual(0x6, reader.ReadUInt16());
            Assert.AreEqual(0x1D, reader.ReadUInt32());

            // Match the middle row (row 114)
            tableStream.Seek(0x46A, SeekOrigin.Begin);
            Assert.AreEqual(0, reader.ReadUInt16());
            Assert.AreEqual(0, reader.ReadUInt16());
            Assert.AreEqual(0x288, reader.ReadUInt16());
            Assert.AreEqual(0x3B4, reader.ReadUInt32());

            tableStream.Seek(0x8E8, SeekOrigin.Begin);
            Assert.AreEqual(0, reader.ReadUInt16());
            Assert.AreEqual(0, reader.ReadUInt16());
            Assert.AreEqual(0xB9B, reader.ReadUInt16());
            Assert.AreEqual(0x23, reader.ReadUInt32());
        }

        [Test]
        public void ShouldBeAbleToReadGenericParamConstraintTableAtCorrectPosition()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var targetFile = Path.Combine(baseDirectory, "SampleBinaries\\LinFu.Core.dll");
            var stream = new FileStream(targetFile, FileMode.Open, FileAccess.Read);

            var table = GetTable(TableId.GenericParamConstraint, stream);
            var rowCount = table.Item1;
            Assert.AreEqual(rowCount, 0x1e);

            var tableStream = table.Item2;
            Assert.AreEqual(0x78, tableStream.Length);

            // Match the first row
            var reader = new BinaryReader(tableStream);
            Assert.AreEqual(5, reader.ReadUInt16());
            Assert.AreEqual(0x1A4, reader.ReadUInt16());

            // Match the last row
            tableStream.Seek(0x74, SeekOrigin.Begin);
            Assert.AreEqual(0xCD, reader.ReadUInt16());
        }
    }
}
