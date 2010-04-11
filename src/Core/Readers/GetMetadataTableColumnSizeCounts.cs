using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Hiro.Containers;
using Tao.Interfaces;
using Tao.Containers;

namespace Tao
{
    /// <summary>
    /// Represents a class that returns the column width schema for a given metadata table.
    /// </summary>
    public class GetMetadataTableColumnSizeCounts : IFunction<ITuple<TableId, Stream>, ITuple<int, int, int, int, int, int>>
    {
        private readonly IMicroContainer _container;

        private readonly IFunction < ITuple <TableId, ITuple<int, int, int, int, int, int, IEnumerable<ITuple<IEnumerable<TableId>, int>>>, Stream>, ITuple<int, int, int, int, int, int>>
            _getAdjustedSchemaCounts;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetMetadataTableColumnSizeCounts"/> class.
        /// </summary>
        public GetMetadataTableColumnSizeCounts(IMicroContainer container, IFunction<ITuple<ITuple<int, int, int, int, int, int, IEnumerable<ITuple<IEnumerable<TableId>, int>>>, Stream>, ITuple<int, int>> getAdditionalColumnCounts, IFunction<ITuple<TableId, ITuple<int, int, int, int, int, int, IEnumerable<ITuple<IEnumerable<TableId>, int>>>, Stream>, ITuple<int, int, int, int, int, int>> getAdjustedSchemaCounts)
        {
            _container = container;
            _getAdjustedSchemaCounts = getAdjustedSchemaCounts;
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
                _container.GetInstance<ITuple<int, int, int, int, int, int, IEnumerable<ITuple<IEnumerable<TableId>, int>>>>(schemaName);

            var result = _getAdjustedSchemaCounts.Execute(tableId, schema, stream);

            return result;
        }
    }
}
