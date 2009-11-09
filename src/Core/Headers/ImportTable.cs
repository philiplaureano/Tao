using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Core
{
    /// <summary>
    /// Represents the Import Table in a Portable Executable image.
    /// </summary>
    public class ImportTable : BaseHeader
    {
        private IOptionalHeader _optionalHeader;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportTable"/> class.
        /// </summary>
        public ImportTable()
            : this(new OptionalHeader())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportTable"/> class.
        /// </summary>
        /// <param name="optionalHeader">The optional header.</param>
        public ImportTable(IOptionalHeader optionalHeader)
        {
            if (optionalHeader == null)
                throw new ArgumentNullException("optionalHeader");

            _optionalHeader = optionalHeader;
        }

        /// <summary>
        /// Gets the value indicating the relative virtual address of the Import Lookup Table.
        /// </summary>
        /// <value>The Import Lookup Table RVA.</value>
        public uint? ImportLookupTableRva
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the name of the library that will be imported by the image.
        /// </summary>
        /// <value>The RVA of the library name.</value>
        public uint? NameRva
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the RVA of the Import Address Table.
        /// </summary>
        /// <value>The RVA of the Import Address Table.</value>
        public uint? ImportAddressTableRva
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the DateTimeStamp of the 
        /// </summary>
        public uint? DateTimeStamp
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the ForwarderChain.
        /// </summary>
        /// <value>The Forwarder Chain.</value>
        public uint? ForwarderChain
        {
            get;
            private set;
        }

        /// <summary>
        /// Reads the Import Table from the binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public override void ReadFrom(IBinaryReader reader)
        {
            SeekImportTablePosition(reader);

            ImportLookupTableRva = reader.ReadUInt32();
            DateTimeStamp = reader.ReadUInt32();
            ForwarderChain = reader.ReadUInt32();
            NameRva = reader.ReadUInt32();
            ImportAddressTableRva = reader.ReadUInt32();
        }

        /// <summary>
        /// Seeks the position of the Import Table.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        private void SeekImportTablePosition(IBinaryReader reader)
        {
            // Seek the position of the import table
            if (_optionalHeader == null)
                return;

            _optionalHeader.ReadFrom(reader);
            SeekHeader(_optionalHeader, reader, 1);            
        }
    }
}
