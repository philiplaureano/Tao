using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tao.Model
{
    /// <summary>
    /// Represents an table index that points to a single row in a .NET metadata table.
    /// </summary>
    public class TableIndex
    {
        /// <summary>
        /// Gets or sets the value indicating the <see cref="TableId"/> of the target table.
        /// </summary>
        /// <value>The <see cref="TableId"/> of the target table.</value>
        public TableId TableId { get; set; }

        /// <summary>
        /// Gets or sets the value indicating the target metadata table row.
        /// </summary>
        /// <value>The target metadata table row.</value>
        public uint Row { get; set; }
    }
}
