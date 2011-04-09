using System.Collections.Generic;
using Tao.Interfaces;

namespace Tao.Schemas
{
    /// <summary>
    /// Represents the row schema for the GenericParamConstraint table.
    /// </summary>
    public class GenericParamConstraintRowSchema : BaseRowSchema
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericParamConstraintRowSchema"/> class.
        /// </summary>
        public GenericParamConstraintRowSchema() : base(0, 0, 0, 0, 0, 0)
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
                yield return CreateTuple(1, TableId.GenericParam);
                yield return CreateTuple(1, TableId.TypeDef, TableId.TypeRef, TableId.TypeSpec);
            }
        }
    }
}