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
    public class ModuleTableReadTests : BaseHeaderReadTest
    {
        [Test]
        public void ShouldReadGeneration()
        {
            AssertEquals<ModuleTable, ushort?>(0, h=>h.Generation);
        }

        protected override void SetStreamPosition(Stream stream)
        {
            stream.Seek(0x2f4, SeekOrigin.Begin);
        }
    }
}
