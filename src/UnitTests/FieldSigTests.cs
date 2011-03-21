using System.IO;
using NUnit.Framework;
using Tao.Containers;
using Tao.Interfaces;
using Tao.Model;

namespace Tao.UnitTests
{
    [TestFixture]
    public class FieldSigTests : BaseStreamTests
    {
        [Test]
        public void ShouldReadCorrectBlobDataForGivenField()
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

        // TODO: Read the field signature based on a given stream and blob stream index
        [Test]
        public void ShouldReadFieldSignatureWithClassType()
        {
            const byte token = 0x8;
            var expectedTableId = TableId.TypeDef;
            uint expectedIndex = 2;

            byte fieldSig = 0x6;
            byte classElement = (byte)ElementType.Class;
            var bytes = new[]
                            {
                                fieldSig,
                                classElement,
                                token
                            };

            var stream = new MemoryStream(bytes);

            var reader = container.GetInstance<IFunction<Stream, FieldSignature>>();
            Assert.IsNotNull(reader);

            var fieldSignature = reader.Execute(stream);
            Assert.IsNotNull(fieldSignature);
            Assert.AreEqual(0, fieldSignature.CustomMods.Count);

            TypeSignature fieldType = fieldSignature.FieldType;

            TypeDefOrRefEncodedSignature encodedSignature = fieldType as TypeDefOrRefEncodedSignature;
            Assert.IsNotNull(encodedSignature);
            Assert.AreEqual(encodedSignature.TableIndex, expectedIndex);
            Assert.AreEqual(encodedSignature.TableId, expectedTableId);
        }
        [Test]
        public void ShouldRecognizeFieldSignatureConstant()
        {
            var fieldConstantReader = container.GetInstance<IFunction<byte, bool>>("FieldSignatureConstantReader");
            Assert.IsNotNull(fieldConstantReader);

            var result = fieldConstantReader.Execute(0x6);
            Assert.IsTrue(result);
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
