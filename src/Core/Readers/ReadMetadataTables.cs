using System;
using System.Collections.Generic;
using System.IO;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a class that reads the metadata tables from the given stream.
    /// </summary>
    public class ReadMetadataTables : IFunction<ITuple<Func<TableId, bool>,Stream>, IDictionary<TableId, ITuple<int, Stream>>>
    {
        private readonly IFunction<ITuple<ITuple<TableId, Stream>, ITuple<int, int, int>>, int> _calculateMetadataTableRowSize;
        private readonly IFunction<Stream, IDictionary<TableId, int>> _readMetadataTableRowCounts;
        private readonly IFunction<Stream, ITuple<int, int, int>> _readMetadataHeapIndexSizes;
        private readonly IFunction<ITuple<int, Stream>, Stream> _inMemorySubStreamReader;
        private readonly IFunction<Stream, Stream> _readMetadataStream;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadMetadataTables"/> class.
        /// </summary>
        /// <param name="calculateMetadataTableRowSize">The object that will determine the row size for each of the metadata tables.</param>
        /// <param name="readMetadataTableRowCounts">The reader that will determine the number of rows for each .NET metadata table.</param>
        /// <param name="inMemorySubStreamReader">The reader that will extract the table streams from the input stream.</param>
        /// <param name="readMetadataHeapIndexSizes">The reader that will determine the heap index sizes for the current .NET assembly stream.</param>
        /// <param name="readMetadataStream">The reader that will read the contents of the .NET metadata stream header.</param>
        public ReadMetadataTables(IFunction<ITuple<ITuple<TableId, Stream>, ITuple<int, int, int>>, int> calculateMetadataTableRowSize, IFunction<Stream, IDictionary<TableId, int>> readMetadataTableRowCounts, IFunction<Stream, ITuple<int, int, int>> readMetadataHeapIndexSizes, IFunction<ITuple<int, Stream>, Stream> inMemorySubStreamReader, IFunction<Stream, Stream> readMetadataStream)
        {
            _calculateMetadataTableRowSize = calculateMetadataTableRowSize;
            _readMetadataTableRowCounts = readMetadataTableRowCounts;
            _readMetadataHeapIndexSizes = readMetadataHeapIndexSizes;
            _inMemorySubStreamReader = inMemorySubStreamReader;
            _readMetadataStream = readMetadataStream;
        }

        /// <summary>
        /// Reads the metadata tables from the given stream that matches the given predicate.
        /// </summary>
        /// <param name="input">The predicate that determines which tables should be read and the stream that contains the metadata tables.</param>
        /// <returns>A dictionary containing all the metadata table streams and row counts for each table in the given assembly.</returns>
        public IDictionary<TableId, ITuple<int, Stream>> Execute(ITuple<Func<TableId, bool>, Stream> input)
        {
            var shouldReadTargetTable = input.Item1;
            var stream = input.Item2;

            var heapSizes = _readMetadataHeapIndexSizes.Execute(stream);
            var metadataHeap = _readMetadataStream.Execute(stream);
            var rowCounts = _readMetadataTableRowCounts.Execute(stream);

            // Seek the end of the #~ Header
            const int baseHeaderSize = 0x18;
            var tableCount = rowCounts.Count;
            var offset = baseHeaderSize + tableCount * 4;
            metadataHeap.Seek(offset, SeekOrigin.Begin);

            var results = new Dictionary<TableId, ITuple<int, Stream>>();
            foreach (var tableId in rowCounts.Keys)
            {
                var rowCount = rowCounts[tableId];
                var rowSize = _calculateMetadataTableRowSize.Execute(Tuple.New(tableId, stream), heapSizes);

                var streamLength = rowCount * rowSize;

                Stream currentStream = null;
                if (shouldReadTargetTable(tableId))
                {
                    currentStream = _inMemorySubStreamReader.Execute(streamLength, metadataHeap);
                    results[tableId] = Tuple.New(rowCount, currentStream);
                    continue;
                }

                // Skip the current table stream
                metadataHeap.Seek(streamLength, SeekOrigin.Current);
                currentStream = new MemoryStream();
                results[tableId] = Tuple.New(0, currentStream);
            }

            return results;
        }
    }
}
