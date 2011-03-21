using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tao.Model
{
    /// <summary>
    /// Represents a single row in the Field metadata table.
    /// </summary>
    public class FieldRow
    {
        /// <summary>
        /// Gets or sets the value indicating the <see cref="FieldAttributes"/> of the current field.
        /// </summary>
        /// <value>The <see cref="FieldAttributes"/> of the current field.</value>
        public FieldAttributes Flags { get; set; }

        /// <summary>
        /// Gets or sets the value indicating the string heap index pointing to the name of the current field.
        /// </summary>
        /// <value>The value indicating the string heap index pointing to the name of the current field.</value>
        public uint Name { get; set; }

        /// <summary>
        /// Gets or sets the value indicating the blob heap index pointing to the <see cref="FieldSignature"/> of the current field.
        /// </summary>
        /// <value>The value indicating the blob heap index pointing to the <see cref="FieldSignature"/> of the current field.</value>
        public uint Signature { get; set; }
    }
}
