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

        [Test]
        public void ShouldReadTypeDefOrRefEncodedSignature()
        {
            const byte token = 0x49;
            var expectedTableId = TableId.TypeRef;
            uint expectedIndex = 0x12;
            var elementType = ElementType.Class;

            var bytes = new byte[] { Convert.ToByte(elementType), token };

            var reader = container.GetInstance<IFunction<IEnumerable<byte>, TypeSignature>>();
            var result = reader.Execute(bytes);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(typeof(TypeDefOrRefEncodedSignature), result);

            var encodedSignature = (TypeDefOrRefEncodedSignature)result;
            Assert.AreEqual(elementType, encodedSignature.ElementType);
            Assert.AreEqual(expectedTableId, encodedSignature.TableId);
            Assert.AreEqual(expectedIndex, encodedSignature.TableIndex);
        }

        [Test]
        public void ShouldReadVoidPointerSignature()
        {
            var bytes = new byte[] { Convert.ToByte(ElementType.Ptr), Convert.ToByte(ElementType.Void) };

            var reader = container.GetInstance<IFunction<IEnumerable<byte>, TypeSignature>>();
            var result = reader.Execute(bytes);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(typeof(VoidPointerSignature), result);

            var signature = (VoidPointerSignature)result;
            Assert.AreEqual(signature.ElementType, ElementType.Ptr);
        }

        [Test]
        public void ShouldReadVoidPointerSignatureWithCustomModSignaturesAttached()
        {
            var customModBytes = GetCustomModBytes(ElementType.CMOD_REQD, 0x49);

            // Add the PTR head element
            var byteList = new List<byte> { Convert.ToByte(ElementType.Ptr) };

            // Add the custom signatures
            byteList.AddRange(customModBytes);
            byteList.AddRange(customModBytes);
            byteList.Add(Convert.ToByte(ElementType.Void));

            var bytes = byteList.ToArray();

            var reader = container.GetInstance<IFunction<IEnumerable<byte>, TypeSignature>>();
            var result = reader.Execute(bytes);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(typeof(VoidPointerSignature), result);

            var signature = (VoidPointerSignature)result;
            Assert.AreEqual(signature.ElementType, ElementType.Ptr);

            var customMods = signature.CustomMods;
            Assert.IsTrue(customMods.Count == 2);

            var firstMod = customMods[0];

            Assert.AreEqual(firstMod.ElementType, ElementType.CMOD_REQD);
            Assert.AreEqual(firstMod.TableId, TableId.TypeRef);
            Assert.AreEqual(firstMod.RowIndex, 0x12);
        }

        private byte[] GetCustomModBytes(ElementType elementType, byte codedToken)
        {
            return new byte[] { Convert.ToByte(elementType), Convert.ToByte(codedToken) };
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
