using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tao.Interfaces;
using Tao.Containers;
using System.Net;

namespace Tao.UnitTests
{
    [TestFixture]
    public class ReadCompressedIntegerTests : BaseStreamTests
    {
        [Test]
        public void ShouldBeAbleToReadCompressedSingleByteValue()
        {
            byte value = 0x3;
            var expectedStreamPosition = 1;

            var reader = container.GetInstance<IFunction<Stream, uint>>("ReadCompressedInteger");
            Assert.IsNotNull(reader);

            var bytes = new byte[] { value };
            var stream = new MemoryStream(bytes);

            var result = reader.Execute(stream);
            Assert.AreEqual(value, result);
            Assert.AreEqual(expectedStreamPosition, stream.Position);
        }
        [Test]
        public void ShouldBeAbleToReadCompressedWordValue()
        {
            uint expectedValue = 0x2E57;
            var expectedStreamPosition = 2;

            var reader = container.GetInstance<IFunction<Stream, uint>>("ReadCompressedInteger");
            Assert.IsNotNull(reader);

            var bytes = new byte[] {0xAE, 0x57};
            var stream = new MemoryStream(bytes);

            var result = reader.Execute(stream);
            Assert.AreEqual(expectedValue, result);
            Assert.AreEqual(expectedStreamPosition, stream.Position);
        }

        [Test]
        public void ShouldBeAbleToReadCompressedDoubleWordValue()
        {
            uint compressedValue = 0xC0004000;
           
            uint expectedValue = 0x4000;
            var expectedStreamPosition = 4;

            TestCompressedIntegerRead(compressedValue, expectedValue, expectedStreamPosition);
        }

        private void TestCompressedIntegerRead(uint compressedValue, uint expectedResult, int expectedStreamPosition)
        {
            var stream = new MemoryStream();
            var writer = new BinaryWriter(stream);
            writer.Write(compressedValue);

            var reader = container.GetInstance<IFunction<Stream, uint>>("ReadCompressedInteger");
            Assert.IsNotNull(reader);

            var streamContents = stream.ToArray();
            var bigEndianContents = new List<byte>(streamContents);
            bigEndianContents.Reverse();

            stream = new MemoryStream(bigEndianContents.ToArray());

            // Reset the stream pointer to point to the beginning of the stream
            stream.Seek(0, SeekOrigin.Begin);
            var result = reader.Execute(stream);
            Assert.AreEqual(expectedResult, result);

            Assert.AreEqual(expectedStreamPosition, stream.Position);
        }
    }
}