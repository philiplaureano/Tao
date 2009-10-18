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
    public class ImportAddressTableReadTests : BaseHeaderReadTest
    {
        [Test]
        public void ShouldBeAbleToReadIAT()
        {
            var stream = OpenSampleAssembly();
            stream.Seek(0x200, SeekOrigin.Begin);

            var reader = new BinaryReader(stream);
            
            var importAddressTable = new ImportAddressTable();
            importAddressTable.ReadFrom(reader);

            Assert.AreEqual(0x21b0, importAddressTable.HintNameTableRVA);            
        }
    }
}
