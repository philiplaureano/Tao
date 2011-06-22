using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.Interfaces;

namespace Tao.Model
{
    /// <summary>
    /// Represents a single row in the MemberRef metadata table.
    /// </summary>
    public class MemberRefRow
    {
        /// <summary>
        /// Gets or sets the value indicating the Class field value from the MemberRef table.
        /// </summary>
        /// <value>The Class field value from the MemberRef table.</value>
        public ITuple<TableId, uint> Class { get; set; }

        /// <summary>
        /// Gets or sets the value indicating the index into the string heap that points to the name of the target member.
        /// </summary>
        /// <value>The index into the string heap that points to the name of the target member.</value>
        public uint Name { get; set; }

        /// <summary>
        /// Gets or sets the value indicating the index into the blob heap that points to the singature of the current member.
        /// </summary>
        /// <value>The value indicating the index into the blob heap that points to the singature of the current member.</value>
        public uint Signature { get; set; }
    }
}
