using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tao.Core
{
    public enum ImageFileMachineType : ushort 
    {
        MachineUnknown = 0x0,
        MachineAm33 = 0x1d3,
        Arm = 0x1c0,
        Amd64 = 0x8664,
        Cee = 0xc0ee,
        Ebc = 0xebc,
        I386 = 0x14c,
        Ia64 = 0x200,
        M32R = 0x9041,
        Mips16 = 0x266,
        MipsFpu = 0x366,
        MipsFpu16 = 0x466,
        PowerPc = 0x1f0,
        PowerPcFp = 0x1f1,
        R4000 = 0x166,
        Sh3 = 0x1a2,
        Sh3Dsp = 0x1a3,
        Sh4 = 0x1a6,
        Sh5 = 0x1a8,
        Thumb = 0x1c2,
        WceMipsV2 = 0x169
    }
}
