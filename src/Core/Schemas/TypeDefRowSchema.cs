using System;
using System.Collections.Generic;
using System.Text;
using Tao.Interfaces;

namespace Tao.Schemas
{
    /// <summary>
    /// Represents the row schema for the TypeDef table.
    /// </summary>
    public class TypeDefRowSchema : BaseRowSchema
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeDefRowSchema"/> class.
        /// </summary>
        public TypeDefRowSchema()
            : base(0, 0, 1, 2, 0, 0)
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
                // Return the Extends column references
                yield return CreateTuple(1, TableId.TypeDef, TableId.TypeRef, TableId.TypeSpec);               

                // Return the FieldList column reference
                yield return CreateTuple(1, TableId.Field);

                // Return the MethodList reference
                yield return CreateTuple(1, TableId.MethodDef);
            }
        }
    }
}