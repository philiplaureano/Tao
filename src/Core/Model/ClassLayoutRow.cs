using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tao.Model
{
    /// <summary>
    /// Represents a single row from the ClassLayout metadata table.
    /// </summary>
    public class ClassLayoutRow
    {
        /// <summary>
        /// Gets or sets the value indicating the packing size for the given class layout.
        /// </summary>
        /// <value>The packing size for the given class layout.</value>
        public ushort PackingSize { get; set; }

        /// <summary>
        /// Gets or sets the value indicating the class size.
        /// </summary>
        /// <value>The class size.</value>
        public uint ClassSize { get; set; }

        /// <summary>
        /// Gets or sets the value indicating the index that points to the parent type in the TypeDef table.
        /// </summary>
        /// <value>The index that points to the parent type in the TypeDef table.</value>
        public uint Parent { get; set; }
    }
}
