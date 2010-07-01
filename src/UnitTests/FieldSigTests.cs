using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FieldSigAssembly;
using NUnit.Framework;
using Hiro.Containers;
using Tao.Interfaces;
using Tao.Containers;

namespace Tao.UnitTests
{
    [TestFixture]
    public class FieldSigTests : BaseStreamTests
    {
        [Test]
        public void ShouldReadCorrectBlobData()
        {
            var blobReader = container.GetInstance<IFunction<ITuple<uint, Stream>, byte[]>>();

            var stream = GetStream();

            // Read the first field blob 
            uint index = 0xA;
            var blob = blobReader.Execute(index, stream);
            Assert.AreEqual(2, blob.Length);
            Assert.AreEqual(0x6, blob[0]);
            Assert.AreEqual(0x8, blob[1]);
        }

        [Test]
        public void ShouldRecognizeFieldSignatureConstant()
        {
            var fieldConstantReader = container.GetInstance<IFunction<byte, bool>>("FieldSignatureConstantReader");
            Assert.IsNotNull(fieldConstantReader);

            var result = fieldConstantReader.Execute(0x6);
            Assert.IsTrue(result);
        }

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

        protected override Stream GetStream()
        {
            var targetAssemblyLocation = "FieldSigAssembly.dll";
            var streamFactory = container.GetInstance<IFunction<string, Stream>>();
            Assert.IsNotNull(streamFactory);

            var result = streamFactory.Execute(targetAssemblyLocation);

            return result;
        }
    }
}
