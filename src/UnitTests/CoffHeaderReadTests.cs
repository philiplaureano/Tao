using System;
using System.IO;
using System.Text;
using NUnit.Framework;
using Tao.Core;

namespace Tao.UnitTests
{
    [TestFixture]
    public class CoffHeaderReadTests
    {
        private readonly string _inputFile;
        private readonly string _fullPath;

        public CoffHeaderReadTests()
        {
            _inputFile = "skeleton.exe";
            _fullPath = Path.GetFullPath(_inputFile);
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

            header.Read(stream);

            Assert.IsTrue(header.HasPortableExecutableSignature);
        }

        [Test]
        public void ShouldReadMachineType()
        {
            using (var fileStream = OpenSampleAssembly())
            {
                fileStream.Seek(0x80, SeekOrigin.Begin);
                var header = new COFFHeader();
                header.Read(fileStream);

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
                header.Read(fileStream);    

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
                header.Read(fileStream);

                // '0x4acfddbc' is the hardcoded creation date
                // of skeleton.exe
                var expectedTimeStamp = 0x4acfddbc;
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
                header.Read(fileStream);

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
                header.Read(fileStream);

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
                header.Read(fileStream);

                var expectedSize = 0xE0;
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
                header.Read(fileStream);

                ImageFileCharacteristics expectedCharacteristics = ImageFileCharacteristics.ThirtyTwoBitMachine |
                                                           ImageFileCharacteristics.ExecutableImage;

                Assert.AreEqual(expectedCharacteristics, header.Characteristics);
            }
        }
        private FileStream OpenSampleAssembly()
        {
            var file = new FileStream(_fullPath, FileMode.Open, FileAccess.Read);

            return file;
        }
    }
}

