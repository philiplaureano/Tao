namespace Tao.Core
{
    /// <summary>
    /// Represents the Coff header in a portable executable file.
    /// </summary>
    public interface ICOFFHeader : IReader
    {
        /// <summary>
        /// Gets the value indicating whether or not the previously read stream contains a PE signature.
        /// </summary>
        /// <value>A boolean flag that determines whether or not the previously read stream contains a PE signature.</value>
        bool HasPortableExecutableSignature { get; }

        /// <summary>
        /// Gets the value indicating the size of the section table.
        /// </summary>
        /// <value>The size of the section table.</value>
        uint? NumberOfSections { get; }

        /// <summary>
        /// Gets the value indicating the target <see cref="MachineType"/> for the given PE header.
        /// </summary>
        /// <value>The machine type of the target image.</value>
        ImageFileMachineType? MachineType { get; }

        /// <summary>
        /// Gets the value indicating the number of seconds since 00:00 January 1, 1970 when the file was created.
        /// </summary>
        /// <value>The number of seconds that have elapsed since 00:00 January 1, 1970.</value>
        uint? TimeDateStamp { get; }

        /// <summary>
        /// Gets the value indicating the pointer to the symbol table.
        /// </summary>
        /// <value>The pointer to the symbol table.</value>
        uint? PointerToSymbolTable { get; }

        /// <summary>
        /// Gets the value indicating the number of entries in the symbol table.
        /// </summary>
        /// <value>The number of entries in the symbol table.</value>
        uint? NumberOfSymbols { get; }

        /// <summary>
        /// Gets the value indicating the size of the optional header.
        /// </summary>
        /// <value>The optional header size.</value>
        uint? OptionalHeaderSize { get; }

        /// <summary>
        /// Gets or sets the value indicating the <see cref="ImageFileCharacteristics"/> of the COFF header.
        /// </summary>
        ImageFileCharacteristics? Characteristics { get; }
    }
}