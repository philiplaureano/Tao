using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tao.Model
{
    /// <summary>
    /// Represents a single row in the Param table.
    /// </summary>
    public class ParamRow
    {
        /// <summary>
        /// Gets or sets the value indicating the <see cref="ParamAttributes"/> for the current parameter.
        /// </summary>
        /// <value>The <see cref="ParamAttributes"/> for the current parameter.</value>
        public ParamAttributes Flags { get; set; }

        /// <summary>
        /// Gets or sets the value indicating the orginal of the current parameter.
        /// </summary>
        /// <value>The value indicating the orginal of the current parameter.</value>
        public ushort Sequence { get; set; }

        /// <summary>
        /// Gets or sets the value indicating the string heap index that points to the name of the current parameter.
        /// </summary>
        public uint Name { get; set; }
    }
}
