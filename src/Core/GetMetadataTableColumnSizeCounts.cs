using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Hiro.Containers;
using Tao.Interfaces;
using Tao.Containers;

namespace Tao.Core
{
    /// <summary>
    /// Represents a class that returns the column width schema for a given metadata table.
    /// </summary>
    public class GetMetadataTableColumnSizeCounts : IFunction<ITuple<TableId, Stream>, ITuple<int, int, int, int, int, int>>
    {
        private readonly IMicroContainer _container;
        private readonly IFunction<Stream, IDictionary<TableId, int>> _readMetadataTableRowCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetMetadataTableColumnSizeCounts"/> class.
        /// </summary>
        public GetMetadataTableColumnSizeCounts(IMicroContainer container, IFunction<Stream, IDictionary<TableId, int>> readMetadataTableRowCount)
        {
            _container = container;
            _readMetadataTableRowCount = readMetadataTableRowCount;
        }

        /// <summary>
        /// Returns the column width schema for a given metadata table.
        /// </summary>
        /// <param name="input">The taret table identifier.</param>
        /// <returns>The tuple that describes the table's schema.</returns>
        public ITuple<int, int, int, int, int, int> Execute(ITuple<TableId, Stream> input)
        {
            var tableId = input.Item1;
            var stream = input.Item2;
            var tableName = Enum.GetName(typeof(TableId), tableId);
            var schemaName = string.Format("{0}RowSchema", tableName);

            // Obtain the raw schema counts
            var schema =
                _container.GetInstance<ITuple<int, int, int, int, int, int, IEnumerable<ITuple<TableId, int>>>>(schemaName);


            var foreignTableReferences = schema.Item7;
            var rowCounts = _readMetadataTableRowCount.Execute(stream);
            var additionalDwordColumns = 0;

            foreach (var tableReference in foreignTableReferences)
            {
                var foreignTableId = tableReference.Item1;
                var rowCount = rowCounts.ContainsKey(foreignTableId) ? rowCounts[foreignTableId] : 0;

                // Widen the column to 32 bits if necessary
                if (rowCount > UInt16.MaxValue)
                    additionalDwordColumns++;
            }

            var result = Tuple.New(schema.Item1, schema.Item2, schema.Item3 + additionalDwordColumns, schema.Item4, schema.Item5,
                                   schema.Item6);

            return result;
        }
    }
}
