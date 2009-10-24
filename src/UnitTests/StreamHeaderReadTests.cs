using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;
using Tao.Core;

using BinaryReader = Tao.Core.BinaryReader;

namespace Tao.UnitTests
{
    [TestFixture]
    public class StreamHeaderReadTests : BaseHeaderReadTest
    {
        [Test]
        public void ShouldBeAbleToUseStreamHeadersWithoutExplicitlyInstantiatingOtherHeaders()
        {
            var reader = new BinaryReader(OpenSampleAssembly());
            var headers = new StreamHeaders();

            headers.ReadFrom(reader);
            Verify(headers);
        }

        [Test]
        public void ShouldBeAbleToUseMetadataRootToGetMultipleStreamHeaders()
        {
            var reader = new BinaryReader(OpenSampleAssembly());
            var root = new Mock<IMetadataRoot>();
            root.Expect(r => r.NumberOfStreams).Returns(5);
            root.Expect(r => r.ReadFrom(reader));

            var headers = new StreamHeaders(root.Object);
            headers.ReadFrom(reader);

            root.VerifyAll();
        }

        [Test]
        public void ShouldBeAbleToReadOffset()
        {
            AssertEquals<StreamHeader, uint?>(0x6c, header => header.Offset);
        }

        [Test]
        public void ShouldBeAbleToReadSize()
        {
            AssertEquals<StreamHeader, uint?>(0x68, header => header.Size);
        }

        [Test]
        public void ShouldBeAbleToReadName()
        {
            AssertEquals<StreamHeader, string>("#~", header=>header.Name);
        }

        [Test]
        public void ShouldBeAbleToReadMultipleStreamHeaders()
        {
            // Point the stream towards the first stream header
            var stream = OpenSampleAssembly();
            stream.Seek(0x280, SeekOrigin.Begin);

            var reader = new BinaryReader(stream);

            var streamReader = new StreamHeaderReader();
            const int headerCount = 5;
            var streamHeadersRead = streamReader.ReadFrom(headerCount, reader);
            var streamHeaders = new List<IStreamHeader>(streamHeadersRead);

            Assert.IsNotNull(streamHeaders);
            Verify(streamHeaders);
        }

        private void Verify(IList<IStreamHeader> streamHeaders)
        {
            Assert.IsTrue(streamHeaders.Count == 5);

            // Check the ~ stream header
            var tildeStreamHeader = streamHeaders[0];
            Assert.AreEqual(0x6c, tildeStreamHeader.Offset);
            Assert.AreEqual(0x68, tildeStreamHeader.Size);
            Assert.AreEqual("#~", tildeStreamHeader.Name);

            // Check the string stream header
            var stringsStreamHeader = streamHeaders[1];
            Assert.AreEqual(0xd4, stringsStreamHeader.Offset);
            Assert.AreEqual(0x28, stringsStreamHeader.Size);
            Assert.AreEqual("#Strings", stringsStreamHeader.Name);

            // Check the #US stream header
            var usStreamHeader = streamHeaders[2];
            Assert.AreEqual(0xFc, usStreamHeader.Offset);
            Assert.AreEqual(0x08, usStreamHeader.Size);
            Assert.AreEqual("#US", usStreamHeader.Name);

            var guidStreamHeader = streamHeaders[3];
            Assert.AreEqual(0x104, guidStreamHeader.Offset);
            Assert.AreEqual(0x10, guidStreamHeader.Size);
            Assert.AreEqual("#GUID", guidStreamHeader.Name);

            var blobStreamHeader = streamHeaders[4];
            Assert.AreEqual(0x114, blobStreamHeader.Offset);
            Assert.AreEqual(0x8, blobStreamHeader.Size);
            Assert.AreEqual("#Blob", blobStreamHeader.Name);
        }

        protected override void SetStreamPosition(Stream stream)
        {
            stream.Seek(0x280, SeekOrigin.Begin);
        }
    }
}
