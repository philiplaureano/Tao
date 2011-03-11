using System.Collections.Generic;
using Tao.Interfaces;

namespace Tao.Schemas
{
    /// <summary>
    /// Represents the row schema for the CustomAttribute table.
    /// </summary>
    public class CustomAttributeRowSchema : BaseRowSchema
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomAttributeRowSchema"/> class.
        /// </summary>
        public CustomAttributeRowSchema()
            : base(0, 0, 1, 0, 1, 0)
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
                // Return one reference to the Type field
                IEnumerable<TableId> tableIds = new[]
                                   {
                                       TableId.MethodDef
                                   };

                yield return Tuple.New(tableIds, 1);
            }
        }
    }
}