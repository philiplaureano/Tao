using System;
using System.IO;
using System;
using Moq;
using NUnit.Framework;
using Tao.Core;
using BinaryReader = Tao.Core.BinaryReader;

namespace Tao.UnitTests
{
    [TestFixture]
    public class CoffHeaderReadTests : BaseHeaderReadTest
    {
        [Test]
        public void ShouldReadFromDOSHeaderIfDOSHeaderIsPresent()
        {
            var stream = OpenSampleAssembly();
            SetStreamPosition(stream);

            var reader = new BinaryReader(stream);
            var dosHeader = new Mock<IHeader>();
            dosHeader.Expect(h => h.ReadFrom(reader));

            var header = new COFFHeader(dosHeader.Object);
            header.ReadFrom(reader);

            dosHeader.VerifyAll();
        }

        [Test]
        public void ShouldBeAbleToFindSampleAssemblyImage()
        {
            Console.WriteLine("Input file location = {0}", _fullPath);
            Assert.IsTrue(File.Exists(_fullPath), "Input file '{0}' not found", _fullPath);
        }

        [Test]
        public void ShouldBeAbleToReadPESignature()
        {
            var stream = OpenSampleAssembly();
            var header = new COFFHeader();
            stream.Seek(0x80, SeekOrigin.Begin);

            var reader = new BinaryReader(stream);
            header.ReadFrom(reader);

            Assert.IsTrue(header.HasPortableExecutableSignature);
        }

        [Test]
        public void ShouldReadMachineType()
        {
            using (var fileStream = OpenSampleAssembly())
            {
                fileStream.Seek(0x80, SeekOrigin.Begin);
                var header = new COFFHeader();
                var reader = new BinaryReader(fileStream);

                header.ReadFrom(reader);

                Assert.AreEqual(ImageFileMachineType.I386, header.MachineType);
            }
        }

        [Test]
        public void ShouldReadNumberOfSections()
        {

            using (var fileStream = OpenSampleAssembly())
            {
                fileStream.Seek(0x80, SeekOrigin.Begin);
                var header = new COFFHeader();
                var reader = new BinaryReader(fileStream);

                header.ReadFrom(reader);

                Assert.AreEqual(2, header.NumberOfSections);
            }
        }

        [Test]
        public void ShouldReadTimeDateStamp()
        {
            using (var fileStream = OpenSampleAssembly())
            {
                fileStream.Seek(0x80, SeekOrigin.Begin);
                var header = new COFFHeader();
                var reader = new BinaryReader(fileStream);

                header.ReadFrom(reader);

                // Use the hardcoded creation date
                // of skeleton.exe
                const int expectedTimeStamp = 0x4ad286dd;
                Assert.AreEqual(expectedTimeStamp, header.TimeDateStamp);
            }
        }

        [Test]
        public void ShouldReadPointerToSymbolTable()
        {
            using (var fileStream = OpenSampleAssembly())
            {
                fileStream.Seek(0x80, SeekOrigin.Begin);
                var header = new COFFHeader();
                var reader = new BinaryReader(fileStream);

                header.ReadFrom(reader);

                Assert.AreEqual(0, header.PointerToSymbolTable);
            }
        }

        [Test]
        public void ShouldReadNumberOfSymbols()
        {
            using (var fileStream = OpenSampleAssembly())
            {
                fileStream.Seek(0x80, SeekOrigin.Begin);
                var header = new COFFHeader();
                var reader = new BinaryReader(fileStream);

                header.ReadFrom(reader);

                Assert.AreEqual(0, header.NumberOfSymbols);
            }
        }

        [Test]
        public void ShouldReadSizeOfOptionalHeader()
        {
            using (var fileStream = OpenSampleAssembly())
            {
                fileStream.Seek(0x80, SeekOrigin.Begin);
                var header = new COFFHeader();
                var reader = new BinaryReader(fileStream);

                header.ReadFrom(reader);

                const int expectedSize = 0xE0;
                Assert.AreEqual(expectedSize, header.OptionalHeaderSize);
            }
        }

        [Test]
        public void ShouldReadCharacteristics()
        {
            using (var fileStream = OpenSampleAssembly())
            {
                fileStream.Seek(0x80, SeekOrigin.Begin);
                var header = new COFFHeader();
                var reader = new BinaryReader(fileStream);

                header.ReadFrom(reader);

                const ImageFileCharacteristics expectedCharacteristics = ImageFileCharacteristics.ThirtyTwoBitMachine |
                                                                         ImageFileCharacteristics.ExecutableImage;

                Assert.AreEqual(expectedCharacteristics, header.Characteristics);
            }
        }

        protected override void SetStreamPosition(Stream stream)
        {
            stream.Seek(0x80, SeekOrigin.Begin);
        }
    }
}

