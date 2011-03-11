using System.Collections.Generic;
using Tao.Interfaces;

namespace Tao.Schemas
{
    /// <summary>
    /// Represents the row schema for the Assembly table.
    /// </summary>
    public class ClassLayoutRowSchema : BaseRowSchema
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClassLayoutRowSchema"/> class.
        /// </summary>
        public ClassLayoutRowSchema() : base(0, 1, 1, 0, 0, 0)
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
                IEnumerable<TableId> tableIds = new[]
                                                    {
                                                        TableId.TypeDef
                                                    };

                yield return Tuple.New(tableIds, 1);
            }
        }
    }
}
