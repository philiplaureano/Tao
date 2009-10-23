using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Core
{
    /// <summary>
    /// Describes the DLL characteristics for the current image.
    /// </summary>
    [Flags]
    public enum DllCharacteristics : ushort 
    {
        /// <summary>
        /// Reserved.
        /// </summary>
        Reserved1 = 0x0001,

        /// <summary>
        /// Reserved.
        /// </summary>
        Reserved2 = 0x0002,

        /// <summary>
        /// Reserved.
        /// </summary>
        Reserved3 = 0x0004,

        /// <summary>
        /// Reserved.
        /// </summary>
        Reserved4 = 0x0008,
        
        /// <summary>
        /// The image understands isolation and doesn't want it.
        /// </summary>
        NoIsolation = 0x0200,

        /// <summary>
        /// The image does not use SEH.
        /// </summary>
        /// <remarks>No SE handler may be called in this image.</remarks>
        NoSEH = 0x0400,

        /// <summary>
        /// Do not bind the image.
        /// </summary>
        NoBind = 0x0800,

        /// <summary>
        /// Reserved, must be zero.
        /// </summary>
        Reserved5 = 0x1000,

        /// <summary>
        /// Driver is a WDM driver.
        /// </summary>
        WDMDriver = 0x2000,

        /// <summary>
        /// Image is Terminal Server aware.
        /// </summary>
        TerminalServerAware = 0x8000
    }
}
