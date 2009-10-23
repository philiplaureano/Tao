using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Core
{
    /// <summary>
    /// Describes the subsystem required to run an image.
    /// </summary>
    public enum ImageSubsystem
    {
        /// <summary>
        /// Unknown subsystem.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Used for device drivers and native Windows processes.
        /// </summary>
        Native = 1,

        /// <summary>
        /// Image runs in the Windows graphical user interface subsystem.
        /// </summary>
        WindowsGui = 2,

        /// <summary>
        /// Image runs in the Windows character subsystem
        /// </summary>
        WindowsCui = 3,

        /// <summary>
        /// Image runs in the Posix character subsystem.
        /// </summary>
        PosixCui = 7,

        /// <summary>
        /// Image runs in Windows CE
        /// </summary>
        WindowsCeGui = 9,

        /// <summary>
        /// The image is an EFI application.
        /// </summary>
        EfiApplication = 10,

        /// <summary>
        /// The image is an EFI driver with boot services.
        /// </summary>
        EfiBootServiceDriver = 11,

        /// <summary>
        /// The image is an EFI driver with runtime services.
        /// </summary>
        EfiRuntimeDriver = 12,

        /// <summary>
        /// The image is an EFI ROM image.
        /// </summary>
        EfiRom = 13,

        /// <summary>
        /// The image runs in Xbox.
        /// </summary>
        Xbox = 14,

        /// <summary>
        /// (No description given).
        /// </summary>
        NexusAgent = 15
    }
}
