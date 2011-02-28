using System.Collections.Generic;
using Tao.Interfaces;

namespace Tao.Schemas
{
    /// <summary>
    /// Represents the row schema for the TypeSpec table.
    /// </summary>
    public class TypeSpecRowSchema : BaseRowSchema
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeSpecRowSchema"/> class.
        /// </summary>
        public TypeSpecRowSchema() : base(0, 0, 0, 0, 1, 0)
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
                yield break;
            }
        }
    }
}