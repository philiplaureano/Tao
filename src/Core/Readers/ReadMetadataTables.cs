using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao.Core.Readers
{
    /// <summary>
    /// Represents a class that reads all the metadata tables from the given stream.
    /// </summary>
    public class ReadMetadataTables : IFunction<Stream, IDictionary<TableId, ITuple<int, Stream>>>
    {
        private readonly IFunction<ITuple<ITuple<TableId, Stream>, ITuple<int, int, int>>, int> _calculateMetadataTableRowSize;
        private readonly IFunction<Stream, IDictionary<TableId, int>> _readMetadataTableRowCounts;
        private readonly IFunction<Stream, ITuple<int, int, int>> _readMetadataHeapIndexSizes;
        private readonly IFunction<ITuple<int, Stream>, Stream> _inMemorySubStreamReader;
        private readonly IFunction<Stream, Stream> _readMetadataStream;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadMetadataTable"/> class.
        /// </summary>
        public ReadMetadataTables(IFunction<ITuple<ITuple<TableId, Stream>, ITuple<int, int, int>>, int> calculateMetadataTableRowSize, IFunction<Stream, IDictionary<TableId, int>> readMetadataTableRowCounts, IFunction<Stream, ITuple<int, int, int>> readMetadataHeapIndexSizes, IFunction<ITuple<int, Stream>, Stream> inMemorySubStreamReader, IFunction<Stream, Stream> readMetadataStream)
        {
            _calculateMetadataTableRowSize = calculateMetadataTableRowSize;
            _readMetadataTableRowCounts = readMetadataTableRowCounts;
            _readMetadataHeapIndexSizes = readMetadataHeapIndexSizes;
            _inMemorySubStreamReader = inMemorySubStreamReader;
            _readMetadataStream = readMetadataStream;
        }

        /// <summary>
        /// Reads all the metadata tables from the given stream.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <returns>A dictionary containing the <see cref="TableId"/> row count, and stream for each metadata table.</returns>
        public IDictionary<TableId, ITuple<int, Stream>> Execute(Stream input)
        {
            var heapSizes = _readMetadataHeapIndexSizes.Execute(input);
            var metadataHeap = _readMetadataStream.Execute(input);
            var rowCounts = _readMetadataTableRowCounts.Execute(input);

            var results = new Dictionary<TableId, ITuple<int, Stream>>();
            foreach (var tableId in rowCounts.Keys)
            {
                var rowCount = rowCounts[tableId];
                var rowSize = _calculateMetadataTableRowSize.Execute(Tuple.New(tableId, input), heapSizes);

                var streamLength = rowCount * rowSize;
                var currentStream = _inMemorySubStreamReader.Execute(streamLength, metadataHeap);
                results[tableId] = Tuple.New(rowCount, currentStream);
            }

            return results;
        }
    }
}
