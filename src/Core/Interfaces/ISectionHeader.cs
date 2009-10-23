namespace Tao.Core
{
    /// <summary>
    /// Represents the section header of a portable executable file.
    /// </summary>
    public interface ISectionHeader : IHeader
    {
        /// <summary>
        /// Gets the value indicating the name of the current section.
        /// </summary>
        /// <value>The name of the current section.</value>
        string SectionName { get; }

        /// <summary>
        /// Gets the value indicating the total size of the section when loaded into memory.
        /// </summary>
        /// <value>The total size of the section when loaded into memory.</value>
        uint? VirtualSize { get; }

        /// <summary>
        /// Gets the value indicating the address of the first byte of the section, when loaded into memory, relative to the image base.
        /// </summary>
        /// <value>The address of the first byte of the section.</value>
        uint? VirtualAddress { get; }

        /// <summary>
        /// Gets the value indicating the size of the section or size of the initialized data on disk.
        /// </summary>
        /// <value>The section size or the size of the initialized data on disk.</value>
        uint? SizeOfRawData { get; }

        /// <summary>
        /// Gets the value indicating the file pointer to the section's first page within the COFF file.
        /// </summary>
        /// <value>The file pointer to the section's first page within the COFF file.</value>
        uint? PointerToRawData { get; }

        /// <summary>
        /// Gets the value indicating the file pointer to the beginning of relocation entries for the section.
        /// </summary>
        /// <value>The file pointer to the beginning of relocation entries for the section.</value>
        uint? PointerToRelocations { get; }

        /// <summary>
        /// Gets the value indicating the file pointer to the beginning of line-number entries for the section.
        /// </summary>
        /// <value>The file pointer to the beginning of line-number entries for the section.</value>
        /// <remarks>This should be zero for an image as COFF debugging information is deprecated.</remarks>
        uint? PointerToLineNumbers { get; }

        /// <summary>
        /// Gets the value indicating the number of relocation entries for the section.
        /// </summary>
        /// <value>The number of relocation entires for the section.</value>
        ushort? NumberOfRelocations { get; }

        /// <summary>
        /// Gets the value indicating the number of line-number entires for the section.
        /// </summary>
        /// <value>The number of line-number entires for the section.</value>
        /// <value>This should be zero for an image as COFF debuggin information is deprecated.</value>
        ushort? NumberOfLineNumbers { get; }

        /// <summary>
        /// Gets the value indicating the <see cref="SectionFlags"/> which describe the characteristics for the given section.
        /// </summary>
        /// <value>The section characteristics.</value>
        SectionFlags Characteristics { get; }
    }
}