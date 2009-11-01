using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;
using Tao.Core;

namespace Tao.UnitTests
{
    public partial class OptionalHeaderReadTests
    {
        #region Standard Fields
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
            IHeader dummyHeader = null;
            var reader = new Mock<IBinaryReader>();
            var header = new OptionalHeader(dummyHeader);


            // Return an invalid magic value
            reader.Expect(r => r.ReadUInt16()).Returns(0xFF);
            header.ReadFrom(reader.Object);
            reader.VerifyAll();
        }

        [Test]
        [ExpectedException(typeof(NotSupportedException))]
        public void ShouldThrowNotSupportedExceptionIfImageMagicNumberIsPE32Plus()
        {
            IHeader dummyHeader = null;            
            var reader = new Mock<IBinaryReader>();
            var header = new OptionalHeader(dummyHeader);

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
    }
}
