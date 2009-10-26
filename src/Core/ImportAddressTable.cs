using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tao.Core
{
    /// <summary>
    /// Represents the IAT in a portable executable file.
    /// </summary>
    public class ImportAddressTable : IImportAddressTable
    {
        private readonly IOptionalHeader _optionalHeader;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportAddressTable"/> class.
        /// </summary>
        public ImportAddressTable()
            : this(new OptionalHeader())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportAddressTable"/> class.
        /// </summary>
        public ImportAddressTable(IOptionalHeader optionalHeader)
        {
            if (optionalHeader == null)
                throw new ArgumentNullException("optionalHeader");

            _optionalHeader = optionalHeader;
        }

        /// <summary>
        /// Gets the value indicating the relative virtual address that points to the Hint/Name Table.
        /// </summary>
        /// <value>The RVA of the Hint/Name table.</value>
        public uint? HintNameTableRVA
        {
            get;
            private set;
        }

        /// <summary>
        /// Reads the IAT from the given <paramref name="reader"/>.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public void ReadFrom(IBinaryReader reader)
        {
            _optionalHeader.ReadFrom(reader);

            var directories = _optionalHeader.DataDirectories;
            var directoryList = new List<IDataDirectory>(directories);

            // Find the IAT data directory
            if (directoryList.Count < 13)
                throw new InvalidOperationException("Unable to find the IAT data directory");

            // The IAT data directory should be at index 12 in
            // the list of data directories
            SeekTargetOffset(reader, directoryList);

            HintNameTableRVA = reader.ReadUInt32();
        }

        /// <summary>
        /// Sets the reader position to point to the IAT table.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        /// <param name="directoryList">The list of data directories that contains the IAT data directory.</param>
        private void SeekTargetOffset(IBinaryReader reader, IList<IDataDirectory> directoryList)
        {
            var iatDataDirectory = directoryList[12];

            // Calculate the file offset of the IAT table
            var sectionAlignment = _optionalHeader.SectionAlignment;
            var fileAlignment = _optionalHeader.FileAlignment;
            var iatRva = iatDataDirectory.VirtualAddress;

            var targetAddress = iatRva % sectionAlignment + fileAlignment;
            var targetOffset = Convert.ToInt64(targetAddress.GetValueOrDefault());
            reader.Seek(targetOffset, SeekOrigin.Begin);
        }
    }
}
