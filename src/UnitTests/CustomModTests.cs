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
    public class CustomModTests : BaseStreamTests
    {
        [Test]
        public void ShouldReadCustomModOptSignature()
        {
            var elementType = ElementType.CMOD_OPT;
            byte codedToken = 0x49;
            var expectedTableId = TableId.TypeRef;

            TestCustomModRead(elementType, codedToken, expectedTableId);
        }

        [Test]
        public void ShouldReadCustomModReqSignature()
        {
            var elementType = ElementType.CMOD_REQD;
            byte codedToken = 0x49;
            var expectedTableId = TableId.TypeRef;

            TestCustomModRead(elementType, codedToken, expectedTableId);
        }

        private void TestCustomModRead(ElementType elementType, byte codedToken, TableId expectedTableId)
        {
            var bytes = new byte[] { Convert.ToByte(elementType), codedToken };
            var modReader = container.GetInstance<IFunction<IEnumerable<byte>, ITuple<ElementType, TableId, uint>>>("CustomModReader");
            Assert.IsNotNull(modReader);

            var result = modReader.Execute(bytes);

            Assert.AreEqual(elementType, result.Item1);

            Assert.AreEqual(result.Item2, expectedTableId);
            Assert.AreEqual(0x12, result.Item3);
        }      
    }
}
