using System.Collections.Generic;
using Tao.Interfaces;

namespace Tao.Schemas
{
    /// <summary>
    /// Represents the row schema for the MemberRef table.
    /// </summary>
    public class MemberRefRowSchema : BaseRowSchema
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemberRefRowSchema"/> class.
        /// </summary>
        public MemberRefRowSchema() : base(0, 0, 0, 1, 1, 0)
        {
        }

        /// <summary>
        /// Gets the value indicating the list of table indices and the number of indexes that point to each table type.
        /// </summary>
        /// <value>The number of table indices.</value>
        public override IEnumerable<ITuple<IEnumerable<TableId>, int>> Item7
        {
            get
            {
                yield return CreateTuple(1, TableId.TypeRef, TableId.ModuleRef, TableId.MethodDef, TableId.TypeSpec, TableId.TypeDef);
            }
        }
    }
}