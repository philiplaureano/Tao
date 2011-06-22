using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.Interfaces;

namespace Tao.Schemas
{
    /// <summary>
    /// Represents the row schema for the ExportedType table.
    /// </summary>
    public class ExportedTypeRowSchema : BaseRowSchema
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExportedTypeRowSchema"/> class.
        /// </summary>
        public ExportedTypeRowSchema()
            : base(0, 0, 2, 2, 0, 0)
        {
        }

        /// <summary>
        /// Gets the value indicating the list of table indexes and the number of indexes that point to each table type.
        /// </summary>
        /// <value>The number of table indexes.</value>
        public override IEnumerable<ITuple<IEnumerable<TableId>, int>> Item7
        {
            get
            {
                IEnumerable<TableId> tableIds = new TableId[] { TableId.Field, TableId.ExportedType, TableId.AssemblyRef };
                yield return Tuple.New(tableIds, 1);
            }
        }
    }
}
