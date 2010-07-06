using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tao.Containers;
using Tao.Interfaces;
using Tao.Model;

namespace Tao.UnitTests
{
    [TestFixture]
    public class TypeSignatureTests : BaseStreamTests
    {
        [Test]
        public void ShouldReadBooleanType()
        {
            var elementType = ElementType.Boolean;
            TestElementTypeRead(elementType);
        }

        [Test]
        public void ShouldReadCharType()
        {
            var elementType = ElementType.Char;
            TestElementTypeRead(elementType);
        }
        [Test]
        public void ShouldReadI1Type()
        {
            var elementType = ElementType.I1;
            TestElementTypeRead(elementType);
        }

        [Test]
        public void ShouldReadI2Type()
        {
            var elementType = ElementType.I2;
            TestElementTypeRead(elementType);
        }
        [Test]
        public void ShouldReadI4Type()
        {
            var elementType = ElementType.I4;
            TestElementTypeRead(elementType);
        }
        [Test]
        public void ShouldReadI8Type()
        {
            var elementType = ElementType.I8;
            TestElementTypeRead(elementType);
        }
        [Test]
        public void ShouldReadU1Type()
        {
            var elementType = ElementType.U1;
            TestElementTypeRead(elementType);
        }

        [Test]
        public void ShouldReadU4Type()
        {
            var elementType = ElementType.U4;
            TestElementTypeRead(elementType);
        }
        [Test]
        public void ShouldReadU8Type()
        {
            var elementType = ElementType.U8;
            TestElementTypeRead(elementType);
        }

        [Test]
        public void ShouldReadR4Type()
        {
            var elementType = ElementType.R4;
            TestElementTypeRead(elementType);
        }

        [Test]
        public void ShouldReadR8Type()
        {
            var elementType = ElementType.R8;
            TestElementTypeRead(elementType);
        }

        [Test]
        public void ShouldReadStringType()
        {
            var elementType = ElementType.String;
            TestElementTypeRead(elementType);
        }
        [Test]
        public void ShouldReadObjectType()
        {
            var elementType = ElementType.Object;
            TestElementTypeRead(elementType);
        }

        private void TestElementTypeRead(ElementType elementType)
        {
            var bytes = new byte[] { Convert.ToByte(elementType) };
            
            var reader = container.GetInstance<IFunction<IEnumerable<byte>, TypeSignature>>();
            Assert.IsNotNull(reader);
            var result = reader.Execute(bytes);

            Assert.AreEqual(elementType, result.ElementType);
        }
    }
}
