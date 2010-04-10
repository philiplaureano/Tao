using System;
using System.Collections.Generic;
using System.Text;
using Tao.Interfaces;

namespace Tao.Core.Schemas
{
    /// <summary>
    /// Represents the row schema for the NestedClass table.
    /// </summary>
    public class NestedClassRowSchema : BaseRowSchema
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NestedClassRowSchema"/> class.
        /// </summary>
        public NestedClassRowSchema() : base(0, 0, 0, 0, 0, 0)
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
                yield return CreateTuple(2, TableId.TypeDef);
            }
        }
    }
}