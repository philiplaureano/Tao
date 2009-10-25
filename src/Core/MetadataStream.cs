using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Tao.Core
{
    /// <summary>
    /// Represents the metadata stream of a .NET executable file.
    /// </summary>
    public class MetadataStream : IHeader
    {
        private readonly Dictionary<TableId, uint> _rowCounts = new Dictionary<TableId, uint>();

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
            get; private set;
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
                foreach(var value in values)
                {
                    var currentId = Convert.ToByte(value);
                    if (!IsValid(currentId))
                        continue;

                    yield return (TableId) currentId;
                }
            }
        }
    
        /// <summary>
        /// Reads the metadata stream from the given reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public void ReadFrom(IBinaryReader reader)
        {
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
            foreach(var tableId in validTables)
            {
                var currentId = tableId;
                var rowCount = reader.ReadUInt32();
                _rowCounts[currentId] = rowCount;
            }
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
            var targetBit = bitVector >> tableId ;

            return Convert.ToBoolean(targetBit & 1);
        }

        /// <summary>
        /// Gets the row counts for the given <paramref name="tableId"/>.
        /// </summary>
        /// <param name="tableId">The tableId.</param>
        /// <returns>The number of rows for the given table.</returns>
        public uint GetRowCount(TableId tableId)
        {
            return _rowCounts[tableId];
        }
    }
}
