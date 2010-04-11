using System;
using System.Collections.Generic;
using System.Text;
using Tao.Interfaces;

namespace Tao.Schemas
{
    /// <summary>
    /// Represents the row schema for the Module table.
    /// </summary>
    public class ModuleRowSchema : BaseRowSchema
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleRowSchema"/> class.
        /// </summary>
        public ModuleRowSchema() : base(0, 1, 0, 1, 0, 3)
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