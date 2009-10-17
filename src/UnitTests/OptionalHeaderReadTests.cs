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
    public class OptionalHeaderReadTests : BaseHeaderReadTest
    {
        #region Standard Field Headers
        [Test]
        public void ShouldBeAbleToReadMagicNumber()
        {
            OptionalHeader header = GetHeader();

            Assert.AreEqual(header.MagicNumber, PEFormat.PE32);
        }

        [Test]
        [ExpectedException(typeof(NotSupportedException))]
        public void ShouldThrowNotSupportedExceptionIfImageHasInvalidMagicNumber()
        {
            var reader = new Mock<IBinaryReader>();
            var header = new OptionalHeader();

            // Return an invalid magic value
            reader.Expect(r => r.ReadUInt16()).Returns(0xFF);
            header.ReadFrom(reader.Object);
            reader.VerifyAll();
        }

        [Test]
        [ExpectedException(typeof(NotSupportedException))]
        public void ShouldThrowNotSupportedExceptionIfImageMagicNumberIsPE32Plus()
        {
            var reader = new Mock<IBinaryReader>();
            var header = new OptionalHeader();

            // Return an invalid magic value
            reader.Expect(r => r.ReadUInt16()).Returns(Convert.ToUInt16(PEFormat.PE32Plus));
            header.ReadFrom(reader.Object);
            reader.VerifyAll();
        }

        [Test]
        public void ShouldReadMajorLinkerVersion()
        {
            var header = GetHeader();

            Assert.AreEqual(0x08, header.MajorLinkerVersion);
        }

        [Test]
        public void ShouldReadMinorLinkerVersion()
        {
            var header = GetHeader();

            Assert.AreEqual(0, header.MinorLinkerVersion);
        }

        [Test]
        public void ShouldReadSizeOfCode()
        {
            var header = GetHeader();
            Assert.AreEqual(0x200, header.SizeOfCode);
        }

        [Test]
        public void ShouldReadSizeOfInitializedData()
        {
            var header = GetHeader();
            Assert.AreEqual(0x200, header.SizeOfInitializedData);
        }

        [Test]
        public void ShouldReadSizeOfUninitializedData()
        {
            var header = GetHeader();
            Assert.AreEqual(0, header.SizeOfUninitializedData);
        }

        [Test]
        public void ShouldReadAddressOfEntryPoint()
        {
            var header = GetHeader();

            Assert.AreEqual(0x000021ce, header.AddressOfEntryPoint);
        }

        [Test]
        public void ShouldReadBaseOfCode()
        {
            var header = GetHeader();
            Assert.AreEqual(0x00002000, header.BaseOfCode);
        }

        [Test]
        public void ShouldReadBaseOfDataIfHeaderImageIsPE32()
        {
            var header = GetHeader();

            if (header.MagicNumber != PEFormat.PE32)
                return;

            Assert.AreEqual(0x4000, header.BaseOfData);
        }
        #endregion

        [Test]
        public void ShouldBeAbleToReadImageBase()
        {
            var header = GetHeader();
            Assert.AreEqual(0x00400000, header.ImageBase);
        }

        [Test]
        public void ShouldBeAbleToReadSectionAlignment()
        {
            var header = GetHeader();
            Assert.AreEqual(0x00002000, header.SectionAlignment);
        }

        [Test]
        public void ShouldBeAbleToReadFileAlignment()
        {
            var header = GetHeader();
            Assert.AreEqual(0x00000200, header.FileAlignment);
        }

        [Test]
        public void ShouldBeAbleToReadMajorOSVersion()
        {
            var header = GetHeader();
            Assert.AreEqual(0x0004, header.MajorOSVersion);
        }

        [Test]
        public void ShouldBeAbleToReadMinorOSVersion()
        {
            var header = GetHeader();
            Assert.AreEqual(0x0000, header.MinorOSVersion);
        }

        [Test]
        public void ShouldBeAbleToReadMajorImageVersion()
        {
            var header = GetHeader();
            Assert.AreEqual(0x0000, header.MajorImageVersion);
        }

        [Test]
        public void ShouldBeAbleToReadMinorImageVersion()
        {
            var header = GetHeader();
            Assert.AreEqual(0x0000, header.MinorImageVersion);
        }

        [Test]
        public void ShouldBeAbleToReadMajorSubsystemVersion()
        {
            var header = GetHeader();
            Assert.AreEqual(0x0004, header.MajorSubsystemVersion);
        }

        [Test]
        public void ShouldBeAbleToReadMinorSubsystemVersion()
        {
            var header = GetHeader();
            Assert.AreEqual(0x0000, header.MinorSubsystemVersion);
        }

        [Test]
        public void ShouldBeAbleToReadWin32VersionValue()
        {
            var header = GetHeader();
            Assert.AreEqual(0, header.Win32VersionValue);
        }

        [Test]
        public void ShouldBeAbleToReadSizeOfImage()
        {
            var header = GetHeader();
            Assert.AreEqual(0x00006000, header.SizeOfImage);
        }

        [Test]
        public void ShouldBeAbleToReadSizeOfHeaders()
        {
            var header = GetHeader();
            Assert.AreEqual(0x00000200, header.SizeOfHeaders);
        }

        [Test]
        public void ShouldBeAbleToReadCheckSum()
        {
            var header = GetHeader();
            Assert.AreEqual(0, header.CheckSum);
        }

        [Test]
        public void ShouldBeAbleToReadSubsystem()
        {
            var header = GetHeader();
            Assert.AreEqual(ImageSubsystem.WindowsCui, header.Subsystem);
        }

        [Test]
        public void ShouldBeAbleToReadDllCharacteristics()
        {
            var header = GetHeader();
            Assert.AreEqual(0x8540, Convert.ToUInt16(header.DLLCharacteristics));
        }

        [Test]
        public void ShouldBeAbleToReadSizeOfStackReserve()
        {
            var header = GetHeader();
            Assert.AreEqual(0x00100000, header.SizeOfStackReserve);
        }

        [Test]
        public void ShouldBeAbleToReadSizeOfStackCommit()
        {
            var header = GetHeader();
            Assert.AreEqual(0x00001000, header.SizeOfStackCommit);
        }

        [Test]
        public void ShouldBeAbleToReadSizeOfHeapReserve()
        {
            var header = GetHeader();
            Assert.AreEqual(0x00100000, header.SizeOfHeapReserve);
        }

        [Test]
        public void ShouldBeAbleToReadSizeOfHeapCommit()
        {
            var header = GetHeader();
            Assert.AreEqual(0x00001000, header.SizeOfHeapCommit);
        }

        [Test]
        public void ShouldBeAbleToReadLoaderFlags()
        {
            var header = GetHeader();
            Assert.AreEqual(0, header.LoaderFlags);
        }

        [Test]
        public void ShouldBeAbleToReadNumberOfDirectories()
        {
            var header = GetHeader();
            Assert.AreEqual(0x10, header.NumberOfDirectories);
        }

        private OptionalHeader GetHeader()
        {
            var stream = OpenSampleAssembly();
            var reader = new BinaryReader(stream);

            const int optionalHeaderOffset = 0x98;
            stream.Seek(optionalHeaderOffset, SeekOrigin.Begin);

            var header = new OptionalHeader();

            header.ReadFrom(reader);
            return header;
        }
    }
}
