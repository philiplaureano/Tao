using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tao.Model
{
    /// <summary>
    /// Represents a class that represents a single row in the TypeRef table.
    /// </summary>
    public class TypeRefRow
    {
        /// <summary>
        /// Gets or sets the value indicating the string heap index that points to the name of the current TypeRef.
        /// </summary>
        /// <value>The name of the current TypeRef.</value>
        public uint Name { get; set; }

        /// <summary>
        /// Gets or sets the value indicating the string heap index that points to the namespace of the current TypeRef.
        /// </summary>
        /// <value>The namespace of the current TypeRef.</value>
        public uint Namespace { get; set; }

        /// <summary>
        /// Gets or sets the value indicating the table index that points to the target metadata table row.
        /// </summary>
        /// <value>The table index that points to the target metadata table row.</value>
        public TableIndex ResolutionScope { get; set; }
    }
}
