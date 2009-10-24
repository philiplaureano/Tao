using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;
using Tao.Core;
using BinaryReader=Tao.Core.BinaryReader;

namespace Tao.UnitTests
{
    [TestFixture]
    public class MetadataRootReadTests : BaseHeaderReadTest
    {
        [Test]
        public void ShouldBeAbleToReadMetadataRootWithoutExplicitlyInstantiatingOtherHeaders()
        {
            var stream = OpenSampleAssembly();
            stream.Seek(0, SeekOrigin.Begin);

            var root = new MetadataRoot();
            root.ReadFrom(new BinaryReader(stream));

            return;
        }

        [Test]
        public void ShouldUseCLIHeaderToFindTheMetadataRootPosition()
        {            
            var stream = OpenSampleAssembly();
            var reader = new BinaryReader(stream);

            SetStreamPosition(stream);

            // Ensure that the metadata root reads from the optional header
            var optionalHeader = new Mock<IOptionalHeader>();
            optionalHeader.Expect(h => h.SectionAlignment).Returns(0x2000);
            optionalHeader.Expect(h => h.FileAlignment).Returns(0x200);
            optionalHeader.Expect(h => h.ReadFrom(reader));

            var cliHeader = new Mock<ICLIHeader>();
            cliHeader.Expect(h => h.MetadataRva).Returns(0x2060);
            cliHeader.Expect(h => h.ReadFrom(reader));
            var root = new MetadataRoot(optionalHeader.Object, cliHeader.Object);

            root.ReadFrom(reader);

            cliHeader.VerifyAll();
            optionalHeader.VerifyAll();
        }
        [Test]
        public void ShouldBeAbleToReadMetadataSignature()
        {            
            AssertEquals<MetadataRoot, uint?>(0x424A5342, h=>h.Signature);
        }

        [Test]
        public void ShouldBeAbleToReadMajorVersion()
        {
            AssertEquals<MetadataRoot, ushort?>(1, h => h.MajorVersion);
        }

        [Test]
        public void ShouldBeAbleToReadVersionStringLength()
        {
            AssertEquals<MetadataRoot, ulong?>(0xC, h=>h.VersionStringLength);
        }

        [Test]
        public void ShouldBeAbleToReadVersionString()
        {
            AssertEquals<MetadataRoot, string>("v2.0.50727", h => h.VersionString);
        }

        [Test]
        public void ShouldBeAbleToReadMinorVersion()
        {
            AssertEquals<MetadataRoot, ushort?>(1, h => h.MinorVersion);
        }

        [Test]
        public void ShouldBeAbleToReadNumberOfStreams()
        {
            AssertEquals<MetadataRoot, ushort?>(5, h => h.NumberOfStreams);
        }

        protected override void SetStreamPosition(System.IO.Stream stream)
        {
            stream.Seek(0x260, SeekOrigin.Begin);
        }
    }
}
