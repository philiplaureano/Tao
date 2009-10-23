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
    [TestFixture]
    public class SectionHeaderReadTests : BaseHeaderReadTest
    {
        #region Section Header Field Read Tests
        [Test]
        public void ShouldReadSectionName()
        {            
            var sectionHeader = GetSectionHeader();
            Assert.AreEqual(".text", sectionHeader.SectionName);
        }

        [Test]
        public void ShouldReadVirtualSize()
        {
            TestHeader(header => Assert.AreEqual(0x1d4, header.VirtualSize));
        }

        [Test]
        public void ShouldReadVirtualAddress()
        {
            TestHeader(header => Assert.AreEqual(0x2000, header.VirtualAddress));
        }

        [Test]
        public void ShouldReadSizeOfRawData()
        {
            TestHeader(header => Assert.AreEqual(0x200, header.SizeOfRawData));
        }

        [Test]
        public void ShouldReadPointerToRawData()
        {
            TestHeader(header => Assert.AreEqual(0x200, header.PointerToRawData));
        }

        [Test]
        public void ShouldReadPointerToRelocations()
        {
            TestHeader(header => Assert.AreEqual(0, header.PointerToRelocations));
        }

        [Test]
        public void ShouldReadPointerToLineNumbers()
        {
            TestHeader(header => Assert.AreEqual(0, header.PointerToLineNumbers));
        }

        [Test]
        public void ShouldReadNumberOfRelocations()
        {
            TestHeader(header=>Assert.AreEqual(0, header.NumberOfRelocations));
        }

        [Test]
        public void ShouldReadNumberOfLineNumbers()
        {
            TestHeader(header => Assert.AreEqual(0, header.NumberOfLineNumbers));
        }

        [Test]
        public void ShouldReadSectionCharacteristics()
        {
            const SectionFlags expectedFlags = SectionFlags.ContainsCode | SectionFlags.MemExecute | SectionFlags.MemRead;
            TestHeader(header => Assert.AreEqual(expectedFlags, header.Characteristics));
        }
        #endregion

        [Test]
        public void ShouldReadSectionHeadersFromSkeletonFile()
        {
            const int sectionCount = 2;
            const int sectionOffset = 0x178;
            var stream = OpenSampleAssembly();
            stream.Seek(sectionOffset, SeekOrigin.Begin);
            var reader = new BinaryReader(stream);
            var sectionHeaderReader = new SectionHeaderReader();

            var sections = sectionHeaderReader.ReadFrom(sectionCount, reader);

            VerifySectionHeaders(sections);
        }

        private void VerifySectionHeaders(IEnumerable<ISectionHeader> targetSections)
        {
            var sections = new List<ISectionHeader>(targetSections);
            Assert.IsNotNull(sections);
            Assert.IsTrue(sections.Count == 2);

            // Verify the first section
            var textSection = sections[0];
            Assert.AreEqual(0x1d4, textSection.VirtualSize);
            Assert.AreEqual(0x2000, textSection.VirtualAddress);
            Assert.AreEqual(0x200, textSection.SizeOfRawData);
            Assert.AreEqual(0x200, textSection.PointerToRawData);
            Assert.AreEqual(0, textSection.PointerToRelocations);
            Assert.AreEqual(0, textSection.PointerToLineNumbers);
            Assert.AreEqual(0, textSection.NumberOfRelocations);
            Assert.AreEqual(0, textSection.NumberOfLineNumbers);

            const SectionFlags expectedTextSectionCharacteristics = SectionFlags.ContainsCode | SectionFlags.MemExecute | SectionFlags.MemRead;
            Assert.AreEqual(expectedTextSectionCharacteristics, textSection.Characteristics);


            // Verify the section section
            var relocSection = sections[1];
            Assert.AreEqual(0xC, relocSection.VirtualSize);
            Assert.AreEqual(0x4000, relocSection.VirtualAddress);
            Assert.AreEqual(0x200, relocSection.SizeOfRawData);
            Assert.AreEqual(0x400, relocSection.PointerToRawData);
            Assert.AreEqual(0, relocSection.PointerToRelocations);
            Assert.AreEqual(0, relocSection.PointerToLineNumbers);
            Assert.AreEqual(0, relocSection.NumberOfRelocations);
            Assert.AreEqual(0, relocSection.NumberOfLineNumbers);

            const SectionFlags expectedRelocSectionCharacteristics =
                SectionFlags.ContainsInitializedData | SectionFlags.MemDiscardable | SectionFlags.MemRead;
            Assert.AreEqual(expectedRelocSectionCharacteristics, relocSection.Characteristics);
        }

        public void TestHeader(Action<SectionHeader> doTest)
        {
            var sectionHeader = GetSectionHeader();
            doTest(sectionHeader);
        }
        #region Helper methods
        private SectionHeader GetSectionHeader()
        {
            const int startingOffset = 0x178;
            return GetSectionHeader(startingOffset);
        }

        private SectionHeader GetSectionHeader(int startingOffset)
        {
            var stream = OpenSampleAssembly();
            stream.Seek(startingOffset, SeekOrigin.Begin);
            var reader = new BinaryReader(stream);

            var sectionHeader = new SectionHeader();
            sectionHeader.ReadFrom(reader);
            return sectionHeader;
        }

        protected override void SetStreamPosition(Stream stream)
        {
            stream.Seek(0x178, SeekOrigin.Begin);
        }
        #endregion
    }
}
