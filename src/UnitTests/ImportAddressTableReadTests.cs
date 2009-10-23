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
            Func<ImportAddressTable, uint?> getActualValue = h => h.HintNameTableRVA;
            AssertEquals<ImportAddressTable,uint?>(0x21b0, getActualValue);
        }

        protected override void SetStreamPosition(Stream stream)
        {
            stream.Seek(0x200, SeekOrigin.Begin);
        }
    }
}
