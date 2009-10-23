using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tao.Core;
using BinaryReader=Tao.Core.BinaryReader;

namespace Tao.UnitTests
{
    public partial class OptionalHeaderReadTests
    {
        #region Windows-Specific Fields
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
        #endregion        
    }
}
