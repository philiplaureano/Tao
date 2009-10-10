using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tao.Core
{
    public enum MachineType : short 
    {
        ImageFileMachineUnknown = 0x0,
        ImageFileMachineAm33 = 0x1d3,
        ImageFileMachineI386 = 0x14c,
        ImageFileMachineArm = 0x1c0,
        ImageFileMachineEbc = 0xebc,
        ImageFileMachineIa64 = 0x200,
    }
}
