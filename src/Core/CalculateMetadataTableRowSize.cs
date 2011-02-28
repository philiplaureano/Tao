using System.IO;
using Tao.Interfaces;

namespace Tao
{
    /// <summary>
    /// Represents a class that determines the size of a single metadata table row from the given <see cref="TableId"/>
    /// and tuple containing the size of each metadata stream heap index.
    /// </summary>
    public class CalculateMetadataTableRowSize : IFunction<ITuple<ITuple<TableId, Stream>, ITuple<int, int, int>>, int>
    {
        private readonly IFunction<ITuple<TableId, Stream>, ITuple<int, int, int, int, int, int>> _getMetadataTableSizeCounts;

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculateMetadataTableRowSize"/> class.
        /// </summary>
        public CalculateMetadataTableRowSize(IFunction<ITuple<TableId, Stream>, ITuple<int, int, int, int, int, int>> getMetadataTableSizeCounts)
        {
            _getMetadataTableSizeCounts = getMetadataTableSizeCounts;
        }

        /// <summary>
        /// Represents a class that determines the size of a single metadata table row from the given <see cref="TableId"/>
        /// and returns the tuple containing the size of each metadata stream heap index.
        /// </summary>
        /// <param name="input">The tuple containing the <see cref="TableId"/> and the size of each metadata stream heap index.</param>
        /// <returns></returns>
        public int Execute(ITuple<ITuple<TableId, Stream>, ITuple<int, int, int>> input)
        {
            var firstTuple = input.Item1;
            var tableId = firstTuple.Item1;
            var stream = firstTuple.Item2;

            var heapSizes = input.Item2;

            var columnSizeCounts = _getMetadataTableSizeCounts.Execute(tableId, stream);

            var stringIndexSize = heapSizes.Item1;
            var blobIndexSize = heapSizes.Item2;
            var guidIndexSize = heapSizes.Item3;

            var singleByteColumns = columnSizeCounts.Item1;
            var wordColumns = columnSizeCounts.Item2;
            var dwordColumns = columnSizeCounts.Item3;
            var stringColumns = columnSizeCounts.Item4;
            var blobColumns = columnSizeCounts.Item5;
            var guidColumns = columnSizeCounts.Item6;

            var result = singleByteColumns + wordColumns * 2 + dwordColumns * 4 + stringColumns * stringIndexSize + blobColumns * blobIndexSize +
                         guidColumns * guidIndexSize;

            return result;
        }
    }
}
