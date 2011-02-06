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
            uint value = 0x3;
            var expectedStreamPosition = 1;

            TestCompressedIntegerRead(value, value, expectedStreamPosition);
        }
        [Test]
        public void ShouldBeAbleToReadCompressedWordValue()
        {
            uint compressedValue = 0xAE57;
            uint expectedValue = 0x2E57;
            var expectedStreamPosition = 2;

            TestCompressedIntegerRead(compressedValue, expectedValue, expectedStreamPosition);
        }

        [Test]
        public void ShouldBeAbleToReadCompressedDoubleWordValue()
        {
            uint compressedValue = 0xC0004000;
           
            uint expectedValue = 0x2000;
            var expectedStreamPosition = 2;

            TestCompressedIntegerRead(compressedValue, expectedValue, expectedStreamPosition);
        }

        private void TestCompressedIntegerRead(uint compressedValue, uint expectedResult, int expectedStreamPosition)
        {
            var stream = new MemoryStream();
            var writer = new BinaryWriter(stream);
            writer.Write(compressedValue);

            var reader = container.GetInstance<IFunction<Stream, uint>>("ReadCompressedInteger");
            Assert.IsNotNull(reader);

            // Reset the stream pointer to point to the beginning of the stream
            stream.Seek(0, SeekOrigin.Begin);
            var result = reader.Execute(stream);
            Assert.AreEqual(expectedResult, result);

            Assert.AreEqual(expectedStreamPosition, stream.Position);
        }
    }
}