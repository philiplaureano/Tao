using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao.Core
{
    /// <summary>
    /// Represents a class that enumerates the list of tables that currently exist in the metadata stream.
    /// </summary>
    public class EnumerateExistingMetadataTables : IFunction<Stream, IEnumerable<TableId>>
    {
        private readonly IFunction<Stream, Stream> _readMetadataStream;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumerateExistingMetadataTables"/> class.
        /// </summary>
        public EnumerateExistingMetadataTables(IFunction<Stream, Stream> readMetadataStream)
        {
            _readMetadataStream = readMetadataStream;
        }

        /// <summary>
        /// Enumerates the list of tables that currently exist in the metadata stream.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <returns>The list of <see cref="TableId"/> values that currently exist within the metadata stream.</returns>
        public IEnumerable<TableId> Execute(Stream input)
        {
            var metadataStream = _readMetadataStream.Execute(input);

            // Read the valid bit vector
            metadataStream.Seek(8, SeekOrigin.Current);
            UInt64 bitVector;
            using (var reader = new BinaryReader(metadataStream))
            {
                bitVector = reader.ReadUInt64();
            }
            
            var values = Enum.GetValues(typeof(TableId));
            foreach (var value in values)
            {
                var currentId = Convert.ToByte(value);
                if (!IsValid(currentId, bitVector))
                    continue;

                yield return (TableId)currentId;
            }
        }

        /// <summary>
        /// Determines whether or not the metadata table with the given <paramref name="tableId"/> exists within the current .NET executable image.
        /// </summary>
        /// <param name="tableId">The ID of the target metadata table.</param>
        /// <param name="bitVector">The bit vector that denotes the list of metadata tables.</param>
        /// <returns>Returns <c>true</c> if the table exists; otherwise, it will return <c>false</c>.</returns>
        private bool IsValid(byte tableId, UInt64 bitVector)
        {
            var targetBit = bitVector >> tableId;

            return Convert.ToBoolean(targetBit & 1);
        }
    }
}
