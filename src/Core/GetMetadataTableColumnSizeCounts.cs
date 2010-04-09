using System;
using System.Collections.Generic;
using System.Text;
using Hiro.Containers;
using Tao.Interfaces;
using Tao.Containers;

namespace Tao.Core
{
    /// <summary>
    /// Represents a class that returns the column width schema for a given metadata table.
    /// </summary>
    public class GetMetadataTableColumnSizeCounts : IFunction<TableId, ITuple<int, int, int, int, int>>
    {
        private readonly IMicroContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public GetMetadataTableColumnSizeCounts(IMicroContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// Returns the column width schema for a given metadata table.
        /// </summary>
        /// <param name="input">The taret table identifier.</param>
        /// <returns>The tuple that describes the table's schema.</returns>
        public ITuple<int, int, int, int, int> Execute(TableId input)
        {
            var tableName = Enum.GetName(typeof (TableId), input);
            var schemaName = string.Format("{0}RowSchema", tableName);

            return _container.GetInstance<ITuple<int, int, int, int, int>>(schemaName);
        }
    }
}
