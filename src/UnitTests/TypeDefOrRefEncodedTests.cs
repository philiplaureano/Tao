using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tao.Interfaces;
using Tao.Containers;

namespace Tao.UnitTests
{
    [TestFixture]
    public class TypeDefOrRefEncodedTests : BaseStreamTests
    {
        [Test]
        public void ShouldReadTypeDefOrRefEncodedTokenForTypeDefTable()
        {
            const byte token = 0x8;
            var expectedTableId = TableId.TypeDef;
            uint expectedIndex = 2;

            TestTypeDefOrRefEncodedTokenRead(token, expectedTableId, expectedIndex);
        }

        [Test]
        public void ShouldReadTypeDefOrRefEncodedTokenForTypeSpecTable()
        {
            const byte token = 0xAA;
            var expectedTableId = TableId.TypeSpec;
            uint expectedIndex = 0x2A;

            TestTypeDefOrRefEncodedTokenRead(token, expectedTableId, expectedIndex);
        }

        [Test]
        public void ShouldReadTypeDefOrRefEncodedTokenForTypeRefTable()
        {
            const byte token = 0x49;
            var expectedTableId = TableId.TypeRef;
            uint expectedIndex = 0x12;

            TestTypeDefOrRefEncodedTokenRead(token, expectedTableId, expectedIndex);
        }

        private void TestTypeDefOrRefEncodedTokenRead(byte token, TableId expectedTableId, uint expectedIndex)
        {
            var reader = container.GetInstance<IFunction<byte, ITuple<TableId, uint>>>("TypeDefOrRefEncodedReader");
            Assert.IsNotNull(reader);

            var result = reader.Execute(token);

            var tableId = result.Item1;
            var index = result.Item2;

            Assert.AreEqual(expectedTableId, tableId);
            Assert.AreEqual(expectedIndex, index);
        }
    }
}
