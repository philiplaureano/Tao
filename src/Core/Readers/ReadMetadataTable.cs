using System;
using System.Collections.Generic;
using System.IO;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a class that reads a metadata table from a given stream.
    /// </summary>
    public class ReadMetadataTable : IFunction<ITuple<TableId, Stream>,ITuple<int, Stream>>
    {
        private readonly IFunction<ITuple<Func<TableId, bool>, Stream>, IDictionary<TableId, ITuple<int, Stream>>> _readMetadataTables;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadMetadataTable"/> class.
        /// </summary>
        /// <param name="readMetadataTables">The metadata table stream reader.</param>
        public ReadMetadataTable(IFunction<ITuple<Func<TableId, bool>, Stream>, IDictionary<TableId, ITuple<int, Stream>>> readMetadataTables)
        {
            _readMetadataTables = readMetadataTables;
        }

        /// <summary>
        /// Reads the metadata table with the given <see cref="TableId"/> and input <see cref="Stream"/>.
        /// </summary>
        /// <param name="input">The <see cref="TableId"/> and input <see cref="Stream"/>.</param>
        /// <returns>A stream containing the metadata table and the number of rows that the table currently contains.</returns>
        public ITuple<int, Stream> Execute(ITuple<TableId, Stream> input)
        {
            var targetTableId = input.Item1;
            var stream = input.Item2;

            Func<TableId, bool> shouldReadTargetTable = currentTableId => targetTableId == currentTableId;
            var tables = _readMetadataTables.Execute(shouldReadTargetTable, stream);
            return tables[targetTableId];
        }
    }
}
