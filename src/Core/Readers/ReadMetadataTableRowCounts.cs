using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a class that reads the number of rows that exist for each existing metadata table within the portable executable stream.
    /// </summary>
    public class ReadMetadataTableRowCounts : IFunction<Stream, IDictionary<TableId, int>>
    {
        private readonly IFunction<Stream, Stream> _readMetadataStream;
        private readonly IFunction<Stream, IEnumerable<TableId>> _enumerateExistingMetadataTables;
        private readonly IFunction<ITuple<Stream, IDictionary<TableId, int>>> _readRowCountsFromMetadataStream;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadMetadataTableRowCounts"/> class.
        /// </summary>
        public ReadMetadataTableRowCounts(IFunction<Stream, Stream> readMetadataStream, IFunction<Stream, IEnumerable<TableId>> enumerateExistingMetadataTables, IFunction<ITuple<Stream, IDictionary<TableId, int>>> readRowCountsFromMetadataStream)
        {
            _readMetadataStream = readMetadataStream;
            _readRowCountsFromMetadataStream = readRowCountsFromMetadataStream;
            _enumerateExistingMetadataTables = enumerateExistingMetadataTables;
        }

        /// <summary>
        /// Reads the number of rows that exist for each existing metadata table within the portable executable stream.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <returns>The dictionary containing the row counts for each existing metadata table.</returns>
        public IDictionary<TableId, int> Execute(Stream input)
        {
            var metadataStream = _readMetadataStream.Execute(input);
            var existingTables = _enumerateExistingMetadataTables.Execute(input);

            var results = new Dictionary<TableId, int>();
            foreach(var tableId in existingTables)
            {
                results[tableId] = 0;
            }

            _readRowCountsFromMetadataStream.Execute(metadataStream, results);

            return results;
        }       
    }
}
