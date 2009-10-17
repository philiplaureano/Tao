using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Core
{
    /// <summary>
    /// Describes whether or not an executable image is a 32-bit image or an image that supports a 64-bit address space.
    /// </summary>
    public enum PEFormat
    {
        /// <summary>
        /// Denotes a 32-bit image.
        /// </summary>
        PE32 = 0x10b,

        /// <summary>
        /// Denotes an image that supports addressing modes above 32-bits.
        /// </summary>
        PE32Plus = 0x20b
    }
}
