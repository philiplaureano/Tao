using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.ComponentModel;

namespace Tao.Core
{
    /// <summary>
    /// Represents the metadata stream of a .NET executable file.
    /// </summary>
    public class MetadataStream : IMetadataStream
    {
        private readonly Dictionary<TableId, uint> _rowCounts = new Dictionary<TableId, uint>();
        private readonly IOptionalHeader _optionalHeader;
        private readonly ICLIHeader _cliHeader;
        private readonly IStreamHeaders _streamHeaders;

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataStream"/> class.
        /// </summary>
        public MetadataStream()
            : this(new OptionalHeader(), new CLIHeader(), new StreamHeaders())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataStream"/> class.
        /// </summary>
        /// <param name="optionalHeader">The optional header.</param>
        /// <param name="cliHeader">The CLI header.</param>
        /// <param name="streamHeaders">The stream headers that contain the metadata stream.</param>
        public MetadataStream(IOptionalHeader optionalHeader, ICLIHeader cliHeader, IStreamHeaders streamHeaders)
        {
            if (optionalHeader == null)
                throw new ArgumentNullException("optionalHeader");

            if (cliHeader == null)
                throw new ArgumentNullException("cliHeader");

            if (streamHeaders == null)
                throw new ArgumentNullException("streamHeaders");

            _optionalHeader = optionalHeader;
            _cliHeader = cliHeader;
            _streamHeaders = streamHeaders;
        }

        /// <summary>
        /// Gets the value indicating the major version of the table schemata
        /// </summary>
        /// <value>The major version of the table schemata.</value>
        public byte? MajorVersion
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the minor version of the table schemata.
        /// </summary>
        public byte? MinorVersion
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the bit vector for the heap sizes.
        /// </summary>
        /// <value>The bit vector for the heap sizes.</value>
        public byte? HeapSizes
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the bit vector for the valid tables
        /// </summary>
        /// <value>The bit vector for the valid tables.</value>
        public ulong? Valid
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the bit vector for the sorted tables.
        /// </summary>
        /// <value>The bit vector for the sorted tables.</value>
        public ulong? Sorted
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the list of <see cref="TableId">metadata tables</see> that currently exist within the .NET executable image.
        /// </summary>
        /// <value>A list of TableIds that enumerate the existing tables in the current .NET executable</value>
        public IEnumerable<TableId> ValidTables
        {
            get
            {
                var values = Enum.GetValues(typeof(TableId));
                foreach (var value in values)
                {
                    var currentId = Convert.ToByte(value);
                    if (!IsValid(currentId))
                        continue;

                    yield return (TableId)currentId;
                }
            }
        }

        /// <summary>
        /// Reads the metadata stream from the given reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public void ReadFrom(IBinaryReader reader)
        {
            _optionalHeader.ReadFrom(reader);
            _cliHeader.ReadFrom(reader);

            // Determine the position of the metadata root
            SeekMetadataStreamPosition(reader);

            // Skip the reserved field
            reader.ReadUInt32();
            MajorVersion = reader.ReadByte();
            MinorVersion = reader.ReadByte();
            HeapSizes = reader.ReadByte();

            // Skip another reserved field
            reader.ReadByte();

            Valid = reader.ReadUInt64();
            Sorted = reader.ReadUInt64();

            // Get the row count for each table
            var validTables = ValidTables;

            // Clear the existing row counts
            _rowCounts.Clear();
            foreach (var tableId in validTables)
            {
                var currentId = tableId;
                var rowCount = reader.ReadUInt32();
                _rowCounts[currentId] = rowCount;
            }
        }

        /// <summary>
        /// Determines the position of the metadata stream header and sets the file pointer to point
        /// to the position where the metadata stream is found.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        private void SeekMetadataStreamPosition(IBinaryReader reader)
        {
            var rootRva = _cliHeader.MetadataRva;
            var sectionAlignment = _optionalHeader.SectionAlignment;
            var fileAlignment = _optionalHeader.FileAlignment;

            var rootFileOffset = rootRva.Value % sectionAlignment.Value + fileAlignment.Value;

            // Get the offset of the #~ stream
            var targetStreamHeader = GetMetadataStreamHeader(reader);
            var streamOffset = targetStreamHeader.Offset;

            // Seek the stream header
            var targetFileOffset = Convert.ToInt64(rootFileOffset + streamOffset);
            reader.Seek(targetFileOffset, SeekOrigin.Begin);
        }

        /// <summary>
        /// Locates the metadata stream header.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        /// <returns>The metadata stream header.</returns>
        private IStreamHeader GetMetadataStreamHeader(IBinaryReader reader)
        {
            _streamHeaders.ReadFrom(reader);

            IStreamHeader targetStreamHeader = null;
            foreach (var streamHeader in _streamHeaders)
            {
                if (streamHeader == null || streamHeader.Name != "#~")
                    continue;

                targetStreamHeader = streamHeader;
                break;
            }

            if (targetStreamHeader == null)
                throw new InvalidOperationException("Metadata Stream Not Found");

            return targetStreamHeader;
        }

        /// <summary>
        /// Determines whether or not the metadata table with the given <paramref name="tableId"/> exists within the current .NET executable image.
        /// </summary>
        /// <param name="tableId">The ID of the target metadata table.</param>
        /// <returns>Returns <c>true</c> if the table exists; otherwise, it will return <c>false</c>.</returns>
        public bool IsValid(byte tableId)
        {
            if (Valid == null)
                throw new InvalidOperationException("The metadata stream hasn't been initialized yet");

            var bitVector = Valid.Value;
            var targetBit = bitVector >> tableId;

            return Convert.ToBoolean(targetBit & 1);
        }

        /// <summary>
        /// Gets the row counts for the given <paramref name="tableId"/>.
        /// </summary>
        /// <param name="tableId">The tableId.</param>
        /// <returns>The number of rows for the given table.</returns>
        public uint GetRowCount(TableId tableId)
        {
            if (!_rowCounts.ContainsKey(tableId))
                return 0;

            return _rowCounts[tableId];
        }
    }
}
