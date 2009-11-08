using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tao.Core;
using BinaryReader=Tao.Core.BinaryReader;

namespace Tao.UnitTests
{
    [TestFixture]
    public class ImportTableReadTests : BaseHeaderReadTest
    {
        [Test]
        public void ShouldBeAbleToReadImportTableWithoutUsingHavingToManuallySeekTheImportTablePosition()
        {
            var reader = new BinaryReader(OpenSampleAssembly());
            var table = new ImportTable();
            table.ReadFrom(reader);

            Assert.AreEqual(0x21a4, table.ImportLookupTableRva);
            Assert.AreEqual(0, table.DateTimeStamp);
            Assert.AreEqual(0, table.ForwarderChain);
            Assert.AreEqual(0x2000, table.ImportAddressTableRva);
        }

        [Test]
        public void ShouldReadImportLookupTableRVA()
        {
            AssertEquals<ImportTable, uint?>(0x21a4, h => h.ImportLookupTableRva);
        }

        [Test]
        public void ShouldReadDateTimeStamp()
        {
            AssertEquals<ImportTable, uint?>(0, h => h.DateTimeStamp);
        }

        [Test]
        public void ShouldReadForwarderChain()
        {
            AssertEquals<ImportTable, uint?>(0, h => h.ForwarderChain);
        }

        [Test]
        public void ShouldReadNameRva()
        {
            AssertEquals<ImportTable, uint?>(0x21be, h => h.NameRva);
        }

        [Test]
        public void ShouldReadIATRva()
        {
            AssertEquals<ImportTable, uint?>(0x2000, h=>h.ImportAddressTableRva);
        }

        protected override void SetStreamPosition(Stream stream)
        {
            stream.Seek(0x37c, SeekOrigin.Begin);
        }
    }
}
