using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tao.Core
{
    /// <summary>
    /// Represents the CLI header that contains all of the runtime-specific data entries to run managed code.
    /// </summary>
    public class CLIHeader : BaseHeader, ICLIHeader
    {
        private readonly IOptionalHeader _optionalHeader;

        /// <summary>
        /// Initializes a new instance of the <see cref="CLIHeader"/> class.
        /// </summary>
        public CLIHeader()
            : this(new OptionalHeader())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CLIHeader"/> class.
        /// </summary>
        /// <param name="optionalHeader">The optional header that will determine the position of the CLI header.</param>
        public CLIHeader(IOptionalHeader optionalHeader)
        {
            _optionalHeader = optionalHeader;
        }

        /// <summary>
        /// Gets the value indicating the size of the header in bytes.
        /// </summary>
        /// <value>The size of the header.</value>
        public uint? SizeOfHeader
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the major runtime version.
        /// </summary>
        /// <value>The major runtime version.</value>
        public ushort? MajorRuntimeVersion
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the major runtime version.
        /// </summary>
        /// <value>The major runtime version.</value>
        public ushort? MinorRuntimeVersion
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the RVA of the metadata directory.
        /// </summary>
        /// <value>The relative virtual address (RVA) of the metadata.</value>
        public uint? MetadataRva
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the size of the metadata directory.
        /// </summary>
        public uint? SizeOfMetadata
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the <see cref="RuntimeFlags"/> that describe the current image.
        /// </summary>
        /// <value>The runtime flags.</value>
        public RuntimeFlags? RuntimeFlags
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the token for the MethodDef or File of the entry point for the image.
        /// </summary>
        /// <value>The entry point for the given image.</value>
        public uint? EntryPointToken
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the location of CLI resources.
        /// </summary>
        /// <value>The location of the CLI resources.</value>
        public ulong? Resources
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the strong name signature for the given image.
        /// </summary>
        /// <value>The strong name signature.</value>
        public ulong? StrongNameSignature
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the code manager table.
        /// </summary>
        /// <remarks>This should always be zero.</remarks>
        /// <value>The code manager table.</value>
        public ulong? CodeManagerTable
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the RVA of an array of locations in the file that contain an array of function pointers.
        /// </summary>
        /// <value>The the value indicating the RVA of an array of locations in the file that contain an array of function pointers.</value>
        public ulong? VTableFixups
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the export address table jumps.
        /// </summary>
        /// <value>The export address table jumps.</value>
        public ulong? ExportAddressTableJumps
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the managed native header.
        /// </summary>
        /// <remarks>This should always be zero.</remarks>
        /// <value>The managed native header.</value>
        public ulong? ManagedNativeHeader
        {
            get;
            private set;
        }

        /// <summary>
        /// Reads the CLI header from the given <paramref name="reader"/>.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void ReadFrom(IBinaryReader reader)
        {
            GetCLIHeaderPosition(reader);

            SizeOfHeader = reader.ReadUInt32();
            MajorRuntimeVersion = reader.ReadUInt16();
            MinorRuntimeVersion = reader.ReadUInt16();

            MetadataRva = reader.ReadUInt32();
            SizeOfMetadata = reader.ReadUInt32();

            RuntimeFlags = (RuntimeFlags)reader.ReadUInt32();
            EntryPointToken = reader.ReadUInt32();
            Resources = reader.ReadUInt64();

            StrongNameSignature = reader.ReadUInt64();
            CodeManagerTable = reader.ReadUInt64();
            VTableFixups = reader.ReadUInt64();

            ExportAddressTableJumps = reader.ReadUInt64();
            ManagedNativeHeader = reader.ReadUInt64();
        }

        /// <summary>
        /// Determines the CLI header position using the data from the given binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        private void GetCLIHeaderPosition(IBinaryReader reader)
        {
            // Use the optional header to find the CLI header
            // file position
            if (_optionalHeader == null)
                return;

            var optionalHeader = _optionalHeader;
            optionalHeader.ReadFrom(reader);

            const int headerIndex = 14;
            SeekHeader(optionalHeader, reader, headerIndex);
        }        
    }
}
