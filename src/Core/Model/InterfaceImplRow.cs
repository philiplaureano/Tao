using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.Interfaces;

namespace Tao.Model
{
    /// <summary>
    /// Represents a single row in the InterfaceImpl metadata table.
    /// </summary>
    public class InterfaceImplRow
    {
        /// <summary>
        /// Gets or sets the value indicating the table index of the current parent class.
        /// </summary>
        /// <value>The value indicating the the table index of the current parent class.</value>
        public ITuple<TableId, uint> Class { get; set; }

        /// <summary>
        /// Gets or sets the value indicating the table index of the current interface.
        /// </summary>
        /// <value>The value indicating the table index of the current interface.</value>
        public ITuple<TableId, uint> Interface { get; set; }
    }
}
