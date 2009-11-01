using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Core
{
    /// <summary>
    /// Represents the optional header of a given portable executable file.
    /// </summary>
    public interface IOptionalHeader : IReader
    {
        /// <summary>
        /// Gets the value indicating the <see cref="PEFormat"/> of the PE file.
        /// </summary>
        /// <value>The enumeration that describes the PE file type.</value>
        PEFormat? MagicNumber { get; }

        /// <summary>
        /// Gets the value indicating the major linker version.
        /// </summary>
        /// <value>The major linker version</value>
        byte? MajorLinkerVersion { get; }

        /// <summary>
        /// Gets the value indicating the minor linker version.
        /// </summary>
        /// <value>The minor linker version.</value>
        byte? MinorLinkerVersion { get; }

        /// <summary>
        /// Gets the value indicating the size of the code section, or the sum of all code sections if there are multiple sections.
        /// </summary>
        /// <value>The size of the code section.</value>
        uint? SizeOfCode { get; }

        /// <summary>
        /// Gets the value indicating the size of the initialized data section, or the sum of all sections if there are multiple data sections.
        /// </summary>
        /// <value>The size of the data section.</value>
        uint? SizeOfInitializedData { get; }

        /// <summary>
        /// Gets the value indicating the size of the uninitialized data section, or the sum of all such sections if there are multiple BSS sections.
        /// </summary>
        /// <value>The size of the uninitialized data.</value>
        uint? SizeOfUninitializedData { get; }

        /// <summary>
        /// Gets the value indicating the address of the entry point relative to the image base when the executable file is loaded into memory.
        /// </summary>
        /// <value>The address of the program entry point.</value>
        uint? AddressOfEntryPoint { get; }

        /// <summary>
        /// The address, relative to the image base of the beginning of code section when loaded into memory.
        /// </summary>
        /// <value>The base of code address.</value>
        uint? BaseOfCode { get; }

        /// <summary>
        /// The address, relative to the image base, of the beginning of the data section, when loaded
        /// into memory.
        /// </summary>
        /// <value>The base of data address.</value>
        uint? BaseOfData { get; }

        /// <summary>
        /// Gets the value indicating the preferred address of the first byte of the image when loaded into memory.
        /// </summary>
        /// <value>The preferred address of the first byte once it's loaded into memory.</value>
        uint? ImageBase { get; }

        /// <summary>
        /// Gets the value indicating the alignment (in bytes) of sections when loaded into memory. 
        /// </summary>
        /// <value>The section alignment, in bytes.</value>
        uint? SectionAlignment { get; }

        /// <summary>
        /// Gets the value indicating the alignment factor (in bytes) used to align the raw data of sections in the image file.
        /// </summary>
        /// <value>The file alignment factor, expressed in bytes.</value>
        uint? FileAlignment { get; }

        /// <summary>
        /// Gets the value indicating the major version number of the required OS.
        /// </summary>
        /// <value>The major version number of the required operating system.</value>
        ushort? MajorOSVersion { get; }

        /// <summary>
        /// Gets the value indicating the minor version number of the required OS.
        /// </summary>
        /// <value>The minor version number of the required operating system.</value>
        ushort? MinorOSVersion { get; }

        /// <summary>
        /// Gets the value indicating the major version number of the image.
        /// </summary>
        /// <value>The major version number of the target image.</value>
        ushort? MajorImageVersion { get; }

        /// <summary>
        /// Gets the value indicating the minor version number of the image.
        /// </summary>
        /// <value>The minor version number of the target image.</value>
        ushort? MinorImageVersion { get; }

        /// <summary>
        /// Gets the value indicating the major version number of the subsystem.
        /// </summary>
        /// <value>The major version number of the target subsystem.</value>
        ushort? MajorSubsystemVersion { get; }

        /// <summary>
        /// Gets the value indicating the minor version number of the subsystem.
        /// </summary>
        /// <value>The minor version number of the target subsystem.</value>
        ushort? MinorSubsystemVersion { get; }

        /// <summary>
        /// Gets the value indicating the Win32VersionValue.
        /// </summary>
        /// <value>This value is reserved and must be zero.</value>
        uint? Win32VersionValue { get; }

        /// <summary>
        /// Gets the value indicating the size of the image.
        /// </summary>
        /// <value>The size of the image.</value>
        uint? SizeOfImage { get; }

        /// <summary>
        /// Gets the value indicating the combined size of the MS-DOS stub, PE header, and section headers rounded up to a multiple of <see cref="FileAlignment"/>.
        /// </summary>
        /// <value>The combined size of all file headers.</value>
        uint? SizeOfHeaders { get; }

        /// <summary>
        /// Gets the value indicating the image file checksum.
        /// </summary>
        /// <value>The image file checksum.</value>
        uint? CheckSum { get; }

        /// <summary>
        /// Gets the value indicating the windows subsystem required to run the target image.
        /// </summary>
        /// <value>The windows subsystem required to run the target image.</value>
        ImageSubsystem? Subsystem { get; }

        /// <summary>
        /// Gets the value indicating the <see cref="DLLCharacteristics"/> for the target image.
        /// </summary>
        /// <value>The <see cref="DllCharacteristics"/> of the target image.</value>
        DllCharacteristics? DLLCharacteristics { get; }

        /// <summary>
        /// Gets the value indicating the size of the stack to reserve.
        /// </summary>
        /// <remarks>Only the Stack Commit Size is committed; the rest is made available one page at a time, until reserve size is reached.</remarks>
        uint? SizeOfStackReserve { get; }

        /// <summary>
        /// Gets the value indicating the size of local heap space to reserve.
        /// </summary>
        /// <value>The size of the local heap to reserve.</value>
        uint? SizeOfHeapReserve { get; }

        /// <summary>
        /// Gets the value indicating the size of local heap space to commit.
        /// </summary>
        /// <value>The size of local heap space to commit.</value>
        uint? SizeOfHeapCommit { get; }

        /// <summary>
        /// Gets the value indicating the size of stack to commit.
        /// </summary>
        /// <value>The stack size to commit.</value>
        uint? SizeOfStackCommit { get; }

        /// <summary>
        /// Gets the value indicating the loader flags for the current image.
        /// </summary>
        /// <value>Reserved. This value must be zero.</value>
        uint? LoaderFlags { get; }

        /// <summary>
        /// Gets the value indicating the number of data directories that the image contains.
        /// </summary>
        /// <value>The number of data directories.</value>
        uint? NumberOfDirectories { get; }

        /// <summary>
        /// Gets the value indicating the data directories that currently reside within the image.
        /// </summary>
        /// <value>The list of data directories.</value>
        IEnumerable<IDataDirectory> DataDirectories { get; }
    }
}
