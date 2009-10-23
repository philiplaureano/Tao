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
    public class CLIHeaderReadTests : BaseHeaderReadTest
    {
        [Test]
        public void ShouldBeAbleToReadCLIHeaderFromStream()
        {
            var stream = OpenSampleAssembly();
            var reader = new BinaryReader(stream);
            var header = new CLIHeader();
            header.ReadFrom(reader);

            return;
        }

        [Test]
        public void ShouldUseOptionalHeaderToFindTheCLIHeaderOffset()
        {
            var stream = OpenSampleAssembly();
            SetStreamPosition(stream);

            // Use an actual optional header instance to calculate the file position
            var actualOptionalHeader = new OptionalHeader();
            actualOptionalHeader.ReadFrom(new BinaryReader(stream));

            // Read the data directories from the given stream
            var directories = new List<IDataDirectory>(actualOptionalHeader.DataDirectories);

            // Use a dummy directory in place of the CLI header directory to ensure
            // that it's used by the CLI header itself
            var mockCLIDirectory = new Mock<IDataDirectory>();
            mockCLIDirectory.Expect(d => d.VirtualAddress).Returns(0x2008);

            directories[14] = mockCLIDirectory.Object;

            var reader = new BinaryReader(stream);
            var optionalHeader = new Mock<IOptionalHeader>();
            optionalHeader.Expect(h => h.ReadFrom(reader));
            optionalHeader.Expect(h => h.FileAlignment).Returns(actualOptionalHeader.FileAlignment);
            optionalHeader.Expect(h => h.SectionAlignment).Returns(actualOptionalHeader.SectionAlignment);
            optionalHeader.Expect(h => h.DataDirectories).Returns(directories);


            var targetHeader = new CLIHeader(optionalHeader.Object);
            targetHeader.ReadFrom(reader);

            optionalHeader.VerifyAll();
            mockCLIDirectory.VerifyAll();
        }

        [Test]
        public void ShouldReadHeaderSize()
        {
            AssertEquals<uint?>(0x48, h => h.SizeOfHeader);
        }

        [Test]
        public void ShouldReadMajorRuntimeVersion()
        {
            AssertEquals<ushort?>(0x02, h => h.MajorRuntimeVersion);
        }

        [Test]
        public void ShouldReadMinorRuntimeVersion()
        {
            AssertEquals<ushort?>(0x05, h => h.MinorRuntimeVersion);
        }

        [Test]
        public void ShouldReadMetadataRVA()
        {
            AssertEquals<uint?>(0x2060, h => h.MetadataRva);
        }

        [Test]
        public void ShouldReadSizeOfMetadata()
        {
            AssertEquals<uint?>(0x11c, h => h.SizeOfMetadata);
        }

        [Test]
        public void ShouldReadRuntimeFlags()
        {
            AssertEquals(RuntimeFlags.ILOnly, h => h.RuntimeFlags);
        }

        [Test]
        public void ShouldReadEntryPointToken()
        {
            AssertEquals<uint?>(0x06000001, h => h.EntryPointToken);
        }

        [Test]
        public void ShouldReadResources()
        {
            AssertEquals<ulong?>(0, h => h.Resources);
        }

        [Test]
        public void ShouldReadStrongNameSignature()
        {
            AssertEquals<ulong?>(0, h => h.StrongNameSignature);
        }

        [Test]
        public void ShouldReadCodeManagerTable()
        {
            AssertEquals<ulong?>(0, h => h.CodeManagerTable);
        }

        [Test]
        public void ShouldReadVTableFixups()
        {
            AssertEquals<ulong?>(0, h => h.VTableFixups);
        }

        [Test]
        public void ShouldReadExportAddressTableJumps()
        {
            AssertEquals<ulong?>(0, h => h.ExportAddressTableJumps);
        }

        [Test]
        public void ShouldReadManagedNativeHeader()
        {
            AssertEquals<ulong?>(0, h => h.ManagedNativeHeader);
        }

        private void AssertEquals<TValue>(TValue expected, Func<CLIHeader, TValue> getActualValue)
        {
            AssertEquals<CLIHeader, TValue>(expected, getActualValue);
        }

        protected override void SetStreamPosition(Stream stream)
        {
            stream.Seek(0x208, SeekOrigin.Begin);
        }
    }
}
