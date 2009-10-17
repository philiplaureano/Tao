using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tao.Core
{
    /// <summary>
    /// Describes the target machine for a given image file.
    /// </summary>
    public enum ImageFileMachineType : ushort 
    {
        /// <summary>
        /// Contents assumed to be applicable to any machine type.
        /// </summary>
        MachineUnknown = 0x0,

        /// <summary>
        /// Matsushita AM33
        /// </summary>
        MachineAm33 = 0x1d3,

        /// <summary>
        /// ARM little endian
        /// </summary>
        Arm = 0x1c0,

        /// <summary>
        /// AMD AMD64
        /// </summary>
        Amd64 = 0x8664,

        /// <summary>
        /// CLR pure MSIL (object only)
        /// </summary>
        Cee = 0xc0ee,

        /// <summary>
        /// EFI Byte Code
        /// </summary>
        Ebc = 0xebc,

        /// <summary>
        /// Intel 386 or later, and compatible processors
        /// </summary>
        I386 = 0x14c,

        /// <summary>
        /// Intel IA64
        /// </summary>
        Ia64 = 0x200,

        /// <summary>
        /// Mitsubishi M32R little endian
        /// </summary>
        M32R = 0x9041,

        /// <summary>
        /// (No description given)
        /// </summary>
        Mips16 = 0x266,

        /// <summary>
        /// MIPS with FPU
        /// </summary>
        MipsFpu = 0x366,


        /// <summary>
        /// MIPS16 with FPU
        /// </summary>
        MipsFpu16 = 0x466,

        /// <summary>
        /// PowerPC
        /// </summary>
        PowerPc = 0x1f0,

        /// <summary>
        /// PowerPC with FPU support
        /// </summary>
        PowerPcFp = 0x1f1,

        /// <summary>
        /// MIPS little endian
        /// </summary>
        R4000 = 0x166,

        /// <summary>
        /// Hitachi SH3
        /// </summary>
        Sh3 = 0x1a2,

        /// <summary>
        /// Hitachi SH3 DSP
        /// </summary>
        Sh3Dsp = 0x1a3,

        /// <summary>
        /// Hitachi SH4
        /// </summary>
        Sh4 = 0x1a6,

        /// <summary>
        /// Hitachi SH5
        /// </summary>
        Sh5 = 0x1a8,

        /// <summary>
        /// Thumb
        /// </summary>
        Thumb = 0x1c2,

        /// <summary>
        /// MIPS little endian WCE v2
        /// </summary>
        WceMipsV2 = 0x169
    }
}
