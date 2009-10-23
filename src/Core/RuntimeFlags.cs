using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Core
{
    /// <summary>
    /// Flags that describe the runtime image.
    /// </summary>
    [Flags]
    public enum RuntimeFlags : uint
    {
        /// <summary>
        /// Always 1.
        /// </summary>
        ILOnly = 0x00000001,

        /// <summary>
        /// The image may only be loaded into a 32-bit process.
        /// </summary>
        Image32BitRequired = 0x00000002,

        /// <summary>
        /// The image has a strong name signature.
        /// </summary>
        StrongNameSigned = 0x00000008,

        /// <summary>
        /// Always zero.
        /// </summary>
        TrackDebugData = 0x00010000
    }
}
