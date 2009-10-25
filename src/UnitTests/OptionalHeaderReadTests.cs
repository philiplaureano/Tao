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
    public partial class OptionalHeaderReadTests : BaseHeaderReadTest
    {
        [Test]
        public void ShouldBeAbleToUseOptionalHeaderWithoutExplicitlyCreatingOtherHeaders()
        {
            var binaryReader = new BinaryReader(OpenSampleAssembly());
            var optionalHeader = new OptionalHeader();
            optionalHeader.ReadFrom(binaryReader);

            VerifyDataDirectories(optionalHeader.DataDirectories);
        }

        [Test]
        public void ShouldUseCOFFHeaderWhenReadingTheOptionalHeader()
        {
            var stream = OpenSampleAssembly();
            stream.Seek(0x98, SeekOrigin.Begin);

            var binaryReader = new BinaryReader(stream);
            var coffHeader = new Mock<IHeader>();
            coffHeader.Expect(h => h.ReadFrom(binaryReader));

            var optionalHeader = new OptionalHeader(coffHeader.Object);

            optionalHeader.ReadFrom(binaryReader);
            coffHeader.VerifyAll();
        }

        #region Data Directory Tests
        [Test]
        public void ShouldReadExportDataDirectory()
        {
            TestEmptyDirectoryRead(0xF8);
        }

        [Test]
        public void ShouldReadImportTableDataDirectory()
        {
            TestDirectoryRead(0x100, 0x0000217c, 0x0000004f);
        }

        [Test]
        public void ShouldReadResourceDirectory()
        {
            TestEmptyDirectoryRead(0x108);
        }

        [Test]
        public void ShouldReadExceptionDirectory()
        {
            TestEmptyDirectoryRead(0x110);
        }

        [Test]
        public void ShouldReadSecurityDirectory()
        {
            TestEmptyDirectoryRead(0x110);
        }

        [Test]
        public void ShouldReadCertificateDirectory()
        {
            TestEmptyDirectoryRead(0x118);
        }

        [Test]
        public void ShouldReadBaseRelocationTableDirectory()
        {
            TestDirectoryRead(0x120, 0x4000, 0xC);
        }

        [Test]
        public void ShouldReadDebugDirectory()
        {
            TestEmptyDirectoryRead(0x128);
        }

        [Test]
        public void ShouldReadCopyrightDirectory()
        {
            TestEmptyDirectoryRead(0x130);
        }

        [Test]
        public void ShouldReadGlobalPtrDirectory()
        {
            TestEmptyDirectoryRead(0x138);
        }

        [Test]
        public void ShouldReadTLSTableDirectory()
        {
            TestEmptyDirectoryRead(0x140);
        }

        [Test]
        public void ShouldReadLoadConfigDirectory()
        {
            TestEmptyDirectoryRead(0x148);
        }

        [Test]
        public void ShouldReadBoundImportDirectory()
        {
            TestEmptyDirectoryRead(0x150);
        }

        [Test]
        public void ShouldReadIATDirectory()
        {
            TestDirectoryRead(0x158, 0x2000, 8);
        }

        [Test]
        public void ShouldReadDelayImportDescriptorDirectory()
        {
            TestEmptyDirectoryRead(0x160);
        }

        [Test]
        public void ShouldReadCLIHeaderDirectory()
        {
            TestDirectoryRead(0x168, 0x2008, 0x48);
        }

        [Test]
        public void ShouldReadReservedDirectory()
        {
            TestEmptyDirectoryRead(0x170);
        }

        [Test]
        public void ShouldReadAllDataDirectories()
        {
            var stream = OpenSampleAssembly();
            var reader = new BinaryReader(stream);
            const int startingOffset = 0xF8;

            stream.Seek(startingOffset, SeekOrigin.Begin);

            const int directoryCount = 0x10;
            var directoryReader = new DataDirectoryReader();
            var directories = directoryReader.ReadFrom(directoryCount, reader);

            VerifyDataDirectories(directories);
        }

        [Test]
        public void ShouldUseDataDirectoryReaderInterfaceWhenReadingOptionalHeader()
        {
            var directoryReader = new Mock<IHeaderReader<IDataDirectory>>();
            directoryReader.Expect(reader => reader.ReadFrom(0x10, It.IsAny<IBinaryReader>())).Returns(new List<IDataDirectory>());

            var header = new OptionalHeader(directoryReader.Object);
            ReadOptionalHeaderFromSampleAssembly(header);

            directoryReader.VerifyAll();
        }

        [Test]
        public void ShouldReadDataDirectoriesUsingOptionalHeader()
        {
            var header = GetHeader();

            VerifyDataDirectories(header.DataDirectories);
        }
        #endregion
        #region Helper methods

        protected override void SetStreamPosition(Stream stream)
        {
            stream.Seek(0xF8, SeekOrigin.Begin);
        }

        private void TestEmptyDirectoryRead(int startingOffset)
        {
            TestDirectoryRead(startingOffset, 0, 0);
        }

        private void TestDirectoryRead(int startingOffset, uint virtualAddress, uint size)
        {
            var stream = OpenSampleAssembly();
            var reader = new BinaryReader(stream);

            stream.Seek(startingOffset, SeekOrigin.Begin);

            var directory = new DataDirectory();
            directory.ReadFrom(reader);

            Assert.AreEqual(virtualAddress, directory.VirtualAddress);
            Assert.AreEqual(size, directory.Size);
        }

        private void VerifyDataDirectories(IEnumerable<IDataDirectory> directoryEntries)
        {
            var directories = new List<IDataDirectory>(directoryEntries);
            var sizes = new Dictionary<int, int>();
            sizes[1] = 0x4f;
            sizes[5] = 0xC;
            sizes[12] = 8;
            sizes[14] = 0x48;

            // Compare the RVAs of the data directories read from the file
            // versus the values read from the assembly using ILdasm
            var virtualAddresses = new Dictionary<int, int>();
            virtualAddresses[1] = 0x217c;
            virtualAddresses[5] = 0x4000;
            virtualAddresses[12] = 0x2000;
            virtualAddresses[14] = 0x2008;


            var currentIndex = 0;
            foreach (var directory in directories)
            {
                var isEmpty = !sizes.ContainsKey(currentIndex);
                var expectedRVA = isEmpty ? 0 : virtualAddresses[currentIndex];
                var expectedSize = isEmpty ? 0 : sizes[currentIndex];

                Assert.AreEqual(expectedRVA, directory.VirtualAddress);
                Assert.AreEqual(expectedSize, directory.Size);

                currentIndex++;
            }
        }

        protected virtual OptionalHeader GetHeader()
        {
            var header = new OptionalHeader();

            ReadOptionalHeaderFromSampleAssembly(header);

            return header;
        }

        private void ReadOptionalHeaderFromSampleAssembly(OptionalHeader header)
        {
            var stream = OpenSampleAssembly();
            var reader = new BinaryReader(stream);

            const int optionalHeaderOffset = 0x98;
            stream.Seek(optionalHeaderOffset, SeekOrigin.Begin);

            header.ReadFrom(reader);
        }
        #endregion
    }
}
