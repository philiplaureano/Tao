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

        // TODO: Finish the FieldSig read implementation
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
