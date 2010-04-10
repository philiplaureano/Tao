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
        private readonly IFunction<ITuple<ITuple<int, int, int, int, int, int, IEnumerable<ITuple<IEnumerable<TableId>, int>>>, Stream>,
                    ITuple<int, int>> _getAdditionalColumnCounts;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetMetadataTableColumnSizeCounts"/> class.
        /// </summary>
        public GetMetadataTableColumnSizeCounts(IMicroContainer container, IFunction<ITuple<ITuple<int, int, int, int, int, int, IEnumerable<ITuple<IEnumerable<TableId>, int>>>, Stream>, ITuple<int, int>> getAdditionalColumnCounts)
        {
            _container = container;
            _getAdditionalColumnCounts = getAdditionalColumnCounts;
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

            if (schema == null)
                throw new SchemaNotFoundException(tableId);

            var additionalCounts = _getAdditionalColumnCounts.Execute(schema, stream);
            int additionalDwordColumns = additionalCounts.Item1;
            int additionalWordCount = additionalCounts.Item2;


            var result = Tuple.New(schema.Item1, schema.Item2 + additionalWordCount, schema.Item3 + additionalDwordColumns, schema.Item4, schema.Item5,
                                   schema.Item6);

            return result;
        }        
    }
}
