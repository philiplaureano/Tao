using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a class that calculates the row size for the given metdata table.
    /// </summary>
    public class GetTableRowSize : IFunction<ITuple<TableId, Stream>, int>
    {
        private readonly IFunction<ITuple<ITuple<TableId, Stream>, ITuple<int, int, int>>, int>
            _calculateMetadataTableRowSize;
        private readonly IFunction<Stream, ITuple<int, int, int>> _readMetadataHeapIndexSizes;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetTableRowSize"/> class.
        /// </summary>
        public GetTableRowSize(IFunction<ITuple<ITuple<TableId, Stream>, ITuple<int, int, int>>, int> calculateMetadataTableRowSize, IFunction<Stream, ITuple<int, int, int>> readMetadataHeapIndexSizes)
        {
            _calculateMetadataTableRowSize = calculateMetadataTableRowSize;
            _readMetadataHeapIndexSizes = readMetadataHeapIndexSizes;
        }

        /// <summary>
        /// Calculates the row size for the given metdata table.
        /// </summary>
        /// <param name="input">The <see cref="TableId"/> and the input stream.</param>
        /// <returns>The size of the metadata table row.</returns>
        public int Execute(ITuple<TableId, Stream> input)
        {
            var tableId = input.Item1;
            var stream = input.Item2;
            var indexSizes = _readMetadataHeapIndexSizes.Execute(stream);

            return _calculateMetadataTableRowSize.Execute(Tuple.New(tableId, stream), indexSizes);
        }
    }
}
