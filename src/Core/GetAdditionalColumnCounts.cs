using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao.Core
{
    /// <summary>
    /// Represents a class that automatically widens a metadata table row's column width to accommodate
    /// tables with over 16K rows.
    /// </summary>
    public class GetAdditionalColumnCounts : IFunction<ITuple<ITuple<int, int, int, int, int, int, IEnumerable<ITuple<IEnumerable<TableId>, int>>>, Stream>, ITuple<int, int>>
    {
        private readonly IFunction<Stream, IDictionary<TableId, int>> _readMetadataTableRowCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAdditionalColumnCounts"/> class.
        /// </summary>
        public GetAdditionalColumnCounts(IFunction<Stream, IDictionary<TableId, int>> readMetadataTableRowCount)
        {
            _readMetadataTableRowCount = readMetadataTableRowCount;
        }

        /// <summary>
        /// Widens a metadata table row's column width to accommodate
        /// tables with over 16K rows.
        /// </summary>
        /// <param name="input">The input schema and the target stream containing the metadata.</param>
        /// <returns>A tuple containing the additional word columns and the additional double-word columns, respectively.</returns>
        public ITuple<int, int> Execute(ITuple<ITuple<int, int, int, int, int, int, IEnumerable<ITuple<IEnumerable<TableId>, int>>>, Stream> input)
        {
            var schema = input.Item1;
            var stream = input.Item2;

            var foreignTableReferences = schema.Item7;
            var rowCounts = _readMetadataTableRowCount.Execute(stream);
            var additionalDwordColumns = 0;

            var additionalWordCount = 0;
            foreach (var tableReferences in foreignTableReferences)
            {
                additionalWordCount++;
                var tableIds = tableReferences.Item1;
                foreach (var foreignTableId in tableIds)
                {
                    var rowCount = rowCounts.ContainsKey(foreignTableId) ? rowCounts[foreignTableId] : 0;

                    // Widen the column to 32 bits if necessary
                    if (rowCount <= UInt16.MaxValue)
                        continue;

                    additionalWordCount--;
                    additionalDwordColumns++;
                    break;
                }
            }

            return Tuple.New(additionalWordCount, additionalDwordColumns);
        }
    }
}
