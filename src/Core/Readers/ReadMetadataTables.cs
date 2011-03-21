using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
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
        private readonly IFunction<Stream, IEnumerable<ITuple<int, int, string>>> _readMetadataStreamHeaders;
        private readonly IFunction<Stream> _seekMetadataRootPosition;
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadMetadataTables"/> class.
        /// </summary>
        /// <param name="calculateMetadataTableRowSize">The object that will determine the row size for each of the metadata tables.</param>
        /// <param name="readMetadataTableRowCounts">The reader that will determine the number of rows for each .NET metadata table.</param>
        /// <param name="inMemorySubStreamReader">The reader that will extract the table streams from the input stream.</param>
        /// <param name="readMetadataHeapIndexSizes">The reader that will determine the heap index sizes for the current .NET assembly stream.</param>
        public ReadMetadataTables(IFunction<ITuple<ITuple<TableId, Stream>, ITuple<int, int, int>>, int> calculateMetadataTableRowSize, IFunction<Stream, IDictionary<TableId, int>> readMetadataTableRowCounts, IFunction<Stream, ITuple<int, int, int>> readMetadataHeapIndexSizes, IFunction<ITuple<int, Stream>, Stream> inMemorySubStreamReader, IFunction<Stream, IEnumerable<ITuple<int, int, string>>> readMetadataStreamHeaders, IFunction<Stream> seekMetadataRootPosition)
        {
            _calculateMetadataTableRowSize = calculateMetadataTableRowSize;
            _seekMetadataRootPosition = seekMetadataRootPosition;
            _readMetadataStreamHeaders = readMetadataStreamHeaders;
            _readMetadataTableRowCounts = readMetadataTableRowCounts;
            _readMetadataHeapIndexSizes = readMetadataHeapIndexSizes;
            _inMemorySubStreamReader = inMemorySubStreamReader;
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
            var rowCounts = _readMetadataTableRowCounts.Execute(stream);

            // Seek the end of the #~ Header
            var headers = _readMetadataStreamHeaders.Execute(stream);
            int? metadataStreamOffset = null;
            int? streamSize = null;
            var headerCount = 0;
            foreach(var header in headers)
            {
                var name = header.Item3;
                if (name != "#~") 
                    continue;

                metadataStreamOffset = header.Item1;
                streamSize = header.Item2;
                headerCount++;
            }

            if (metadataStreamOffset == null || streamSize == null)
                throw new VerificationException("Unable to find the '#~' stream");

            _seekMetadataRootPosition.Execute(stream);
            stream.Seek(metadataStreamOffset.Value, SeekOrigin.Current);
            
            const int baseHeaderSize = 0x18;
            var headerStartPosition = stream.Position;            
            var tableCount = rowCounts.Count;
            var offset = baseHeaderSize + tableCount * 4;
            var tableStartPosition = headerStartPosition + offset;

            stream.Seek(tableStartPosition, SeekOrigin.Begin);
            var results = new Dictionary<TableId, ITuple<int, Stream>>();
            foreach (var tableId in rowCounts.Keys)
            {
                var currentPosition = stream.Position;
                var rowCount = rowCounts[tableId];
                var rowSize = _calculateMetadataTableRowSize.Execute(Tuple.New(tableId, stream), heapSizes);

                var streamLength = rowCount * rowSize;

                Stream currentStream = null;
                if (shouldReadTargetTable(tableId))
                {
                    stream.Seek(currentPosition, SeekOrigin.Begin);
                    currentStream = _inMemorySubStreamReader.Execute(streamLength, stream);
                    results[tableId] = Tuple.New(rowCount, currentStream);
                    continue;
                }

                // Skip the current table stream
                var nextPosition = currentPosition + streamLength;
                stream.Seek(nextPosition, SeekOrigin.Begin);
                currentStream = new MemoryStream();
                results[tableId] = Tuple.New(0, currentStream);
            }

            return results;
        }
    }
}
