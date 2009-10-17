using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tao.Core
{
    /// <summary>
    /// The flags that indicate the attributes of an object or image file.
    /// </summary>
    [Flags]
    public enum ImageFileCharacteristics : ushort 
    {
        /// <summary>
        /// Indicates that the file does not contain base relocations and 
        /// must therefore be loaded at its preferred base adrdress.
        /// </summary>
        RelocsStripped = 0x0001,

        /// <summary>
        /// Indicates that the image file is valid and can be run.
        /// </summary>
        ExecutableImage = 0x0002,
        
        /// <summary>
        /// COFF line numbers have been removed.
        /// </summary>
        /// <remarks>This is deprecated and should be zero.</remarks>
        LineNumbersStripped = 0x0004,

        /// <summary>
        /// COFF symbol table entries for local symbols have been removed.
        /// </summary>
        /// <remarks>This is deprecated and should be zero.</remarks>
        LocalSymbolsStripped = 0x0008,

        /// <summary>
        /// Aggressively trim working set.
        /// </summary>
        /// <remarks>Obsolete. Deprecated in Windows 2000 and later. Must be zero.</remarks>
        AggressiveWsTrim = 0x0010,

        /// <summary>
        /// The app can handle > 2GB addresses.
        /// </summary>
        LargeAddressAware = 0x0020,

        /// <summary>
        /// Reserved.
        /// </summary>
        Reserved = 0x0040,

        /// <summary>
        /// Little endian: LSB precedes MSB in memory.
        /// </summary>
        /// <remarks>Deprecated and should be zero.</remarks>
        BytesReversedLo = 0x0080,

        /// <summary>
        /// Machine based on 32-bit word architecture.
        /// </summary>
        ThirtyTwoBitMachine = 0x0100,

        /// <summary>
        /// Debugging information removed from image file.
        /// </summary>
        DebugInformationStripped = 0x0200,

        /// <summary>
        /// If image is on removable media, fully load it and copy it to the swap file.
        /// </summary>
        RemovableRunFromSwap = 0x0400,

        /// <summary>
        /// If image is on network media, fully load it and copy it to the swap file.
        /// </summary>
        NetRunFromSwap = 0x0800,

        /// <summary>
        /// The image is a system file, not a user program.
        /// </summary>
        SystemFile = 0x1000,

        /// <summary>
        /// The image file is a dynamic-link library (DLL).
        /// </summary>
        Dll = 0x2000,

        /// <summary>
        /// File should be run only on a UP machine.
        /// </summary>
        UpSystemOnly = 0x4000,

        /// <summary>
        /// Big endian: MSB precedes LSB in memory. 
        /// </summary>
        /// <remarks>Deprecated and should be zero.</remarks>
        BytesReversedHi = 0x8000
    }
}
