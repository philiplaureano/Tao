using System;
using System.Collections.Generic;
using System.Text;

namespace Tao
{
    public enum ElementType : byte
    {
        End = 0x0,
        Void = 0x1,
        Boolean = 0x2,
        Char = 0x3,
        I1 = 0x4,
        U1 = 0x5,
        I2 = 0x6,
        I4 = 0x8,
        U4 = 0x9,
        I8 = 0xA,
        U8 = 0xB,
        R4 = 0xC,
        R8 = 0xD,
        String = 0xE,
        Ptr = 0xF,
        ByRef = 0x10,
        ValueType = 0x11,
        Class = 0x12,
        Array = 0x13,
        TypedByRef = 0x16,
        IntPtr = 0x18,
        UIntPtr = 0x19,
        FnPtr = 0x1B,
        Object = 0x1C,
        SzArray = 0x1D,
        CMOD_REQD = 0x1f,
        CMOD_OPT = 0x20,
        Internal = 0x21,
        Modifier = 0x40,
        Sentinel = 0x41,
        Pinned = 0x45
    }
}
