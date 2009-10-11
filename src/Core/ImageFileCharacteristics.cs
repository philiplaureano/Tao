using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tao.Core
{
    [Flags]
    public enum ImageFileCharacteristics : ushort 
    {
        RelocsStripped = 0x0001,
        ExecutableImage = 0x0002,
        LineNumbersStripped = 0x0004,
        LocalSymbolsStripped = 0x0008,
        AggressiveWsTrim = 0x0010,
        LargeAddressAware = 0x0020,
        Reserved = 0x0040,
        BytesReversedLo = 0x0080,
        ThirtyTwoBitMachine = 0x0100,
        DebugInformationStripped = 0x0200,
        RemovableRunFromSwap = 0x0400,
        NetRunFromSwap = 0x0800,
        SystemFile = 0x1000,
        Dll = 0x2000,
        UpSystemOnly = 0x4000,
        BytesReversedHi = 0x8000
    }
}
