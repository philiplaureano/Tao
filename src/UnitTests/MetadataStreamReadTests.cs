using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tao.Core;
using BinaryReader = Tao.Core.BinaryReader;

namespace Tao.UnitTests
{
    [TestFixture]
    public class MetadataStreamReadTests : BaseHeaderReadTest
    {
        [Test]
        public void ShouldReadMajorVersion()
        {
            AssertEquals<MetadataStream, byte?>(2, h => h.MajorVersion);
        }

        [Test]
        public void ShouldReadMinorVersion()
        {
            AssertEquals<MetadataStream, byte?>(0, h => h.MinorVersion);
        }

        [Test]
        public void ShouldReadHeapSizes()
        {
            AssertEquals<MetadataStream, byte?>(0, h => h.HeapSizes);
        }

        [Test]
        public void ShouldReadValidField()
        {
            AssertEquals<MetadataStream, ulong?>(0x0000000100000045, h => h.Valid);
        }

        [Test]
        public void ShouldReadSortedField()
        {
            AssertEquals<MetadataStream, ulong?>(0x000016003301fa00, h => h.Sorted);
        }

        [Test]
        public void ShouldListTablesThatExistWithinTheCurrentMetadata()
        {
            var stream = OpenSampleAssembly();
            SetStreamPosition(stream);
            var reader = new BinaryReader(stream);

            var metadataStream = new MetadataStream();
            metadataStream.ReadFrom(reader);

            IEnumerable<TableId> validTables = metadataStream.ValidTables;
            Assert.IsNotNull(validTables);
            Assert.IsTrue(validTables.Count() > 0);
        }

        [Test]
        public void ShouldGetRowCountForEachTableInTheSampleAssembly()
        {
            var stream = OpenSampleAssembly();
            SetStreamPosition(stream);
            var reader = new BinaryReader(stream);

            var metadataStream = new MetadataStream();
            metadataStream.ReadFrom(reader);

            Assert.AreEqual(1, metadataStream.GetRowCount(TableId.Module));
            Assert.AreEqual(1, metadataStream.GetRowCount(TableId.TypeDef));
            Assert.AreEqual(1, metadataStream.GetRowCount(TableId.Assembly));
            Assert.AreEqual(1, metadataStream.GetRowCount(TableId.MethodDef));
        }
        [Test]
        public void ShouldHaveModuleTable()
        {
            // The module table = 0x00
            const byte tableId = 0x00;

            ShouldHaveTable(tableId);
        }

        private void ShouldHaveTable(byte tableId)
        {
            var stream = OpenSampleAssembly();
            SetStreamPosition(stream);
            var reader = new BinaryReader(stream);

            var metadataStream = new MetadataStream();
            metadataStream.ReadFrom(reader);

            var hasTable = metadataStream.IsValid(tableId);
            Assert.IsTrue(hasTable);
        }

        protected override void SetStreamPosition(Stream stream)
        {
            stream.Seek(0x2CC, SeekOrigin.Begin);
        }
    }
}
