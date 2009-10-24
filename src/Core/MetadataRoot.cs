using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tao.Core
{
    /// <summary>
    /// Represents the root of the physical metadata.
    /// </summary>
    public class MetadataRoot : IMetadataRoot
    {
        private readonly ICLIHeader _cliHeader;
        private readonly IOptionalHeader _optionalHeader;

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataRoot"/> class.
        /// </summary>
        public MetadataRoot() : this(new OptionalHeader(), new CLIHeader())
        {            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataRoot"/> class.
        /// </summary>
        /// <param name="optionalHeader">The optional header.</param>
        /// <param name="cliHeader">The CLI header that points to the metadata root.</param>
        public MetadataRoot(IOptionalHeader optionalHeader, ICLIHeader cliHeader)
        {
            _optionalHeader = optionalHeader;
            _cliHeader = cliHeader;
        }

        /// <summary>
        /// Gets the value indicating the signature of the metadata root.
        /// </summary>
        /// <value>The metadata root signature.</value>
        public uint? Signature
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the major version of the metadata root
        /// </summary>
        /// <value>The major version of the metadata root.</value>
        public ushort? MajorVersion
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the minor version of the metadata root
        /// </summary>
        /// <value>The major version of the metadata root.</value>
        public ushort? MinorVersion
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the length of the version string in bytes
        /// </summary>
        /// <value>The length of the version string, in bytes.</value>
        public uint? VersionStringLength
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the version string.
        /// </summary>
        /// <value>The version string.</value>
        public string VersionString
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the number of streams that exist in the metadata.
        /// </summary>
        public ushort? NumberOfStreams
        {
            get;
            private set;
        }

        /// <summary>
        /// Reads the metadata root from the given reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public void ReadFrom(IBinaryReader reader)
        {
            if (_cliHeader != null)
                SeekCLIHeaderPosition(reader);

            Signature = reader.ReadUInt32();
            MajorVersion = reader.ReadUInt16();
            MinorVersion = reader.ReadUInt16();

            // Read the reserved data
            reader.ReadUInt32();

            VersionStringLength = reader.ReadUInt32();
            VersionString = GetVersionString(reader);

            // Read the reserved flags
            reader.ReadUInt16();

            NumberOfStreams = reader.ReadUInt16();
        }
        
        /// <summary>
        /// Ensures that the given binary reader is pointing towards the first byte of the CLI header.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        private void SeekCLIHeaderPosition(IBinaryReader reader)
        {
            _optionalHeader.ReadFrom(reader);
            _cliHeader.ReadFrom(reader);

            var sectionAlignment = _optionalHeader.SectionAlignment;
            var fileAlignment = _optionalHeader.FileAlignment;
            var rva = _cliHeader.MetadataRva;
            var fileOffset = rva.Value - sectionAlignment.Value + fileAlignment.Value;

            reader.Seek(fileOffset, SeekOrigin.Begin);
        }

        /// <summary>
        /// Reads the null-terminated version string from the given reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        /// <returns>The version string.</returns>
        private string GetVersionString(IBinaryReader reader)
        {
            var bytes = new List<byte>();
            for (uint i = 0; i < VersionStringLength.Value; i++)
            {
                var currentByte = reader.ReadByte();
                if (currentByte == 0)
                    continue;

                bytes.Add(currentByte);
            }

            return Encoding.UTF8.GetString(bytes.ToArray());
        }
    }
}
