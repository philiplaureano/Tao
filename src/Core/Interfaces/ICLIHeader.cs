namespace Tao.Core
{
    /// <summary>
    /// Represents a CLI header type.
    /// </summary>
    public interface ICLIHeader : IReader
    {
        /// <summary>
        /// Gets the value indicating the size of the header in bytes.
        /// </summary>
        /// <value>The size of the header.</value>
        uint? SizeOfHeader { get; }

        /// <summary>
        /// Gets the value indicating the major runtime version.
        /// </summary>
        /// <value>The major runtime version.</value>
        ushort? MajorRuntimeVersion { get; }

        /// <summary>
        /// Gets the value indicating the major runtime version.
        /// </summary>
        /// <value>The major runtime version.</value>
        ushort? MinorRuntimeVersion { get; }

        /// <summary>
        /// Gets the value indicating the RVA of the metadata directory.
        /// </summary>
        /// <value>The relative virtual address (RVA) of the metadata.</value>
        uint? MetadataRva { get; }

        /// <summary>
        /// Gets the value indicating the size of the metadata directory.
        /// </summary>
        uint? SizeOfMetadata { get; }

        /// <summary>
        /// Gets the value indicating the <see cref="RuntimeFlags"/> that describe the current image.
        /// </summary>
        /// <value>The runtime flags.</value>
        RuntimeFlags? RuntimeFlags { get; }

        /// <summary>
        /// Gets the value indicating the token for the MethodDef or File of the entry point for the image.
        /// </summary>
        /// <value>The entry point for the given image.</value>
        uint? EntryPointToken { get; }

        /// <summary>
        /// Gets the value indicating the location of CLI resources.
        /// </summary>
        /// <value>The location of the CLI resources.</value>
        ulong? Resources { get; }

        /// <summary>
        /// Gets the value indicating the strong name signature for the given image.
        /// </summary>
        /// <value>The strong name signature.</value>
        ulong? StrongNameSignature { get; }

        /// <summary>
        /// Gets the value indicating the code manager table.
        /// </summary>
        /// <remarks>This should always be zero.</remarks>
        /// <value>The code manager table.</value>
        ulong? CodeManagerTable { get; }

        /// <summary>
        /// Gets the value indicating the RVA of an array of locations in the file that contain an array of function pointers.
        /// </summary>
        /// <value>The the value indicating the RVA of an array of locations in the file that contain an array of function pointers.</value>
        ulong? VTableFixups { get; }

        /// <summary>
        /// Gets the value indicating the export address table jumps.
        /// </summary>
        /// <value>The export address table jumps.</value>
        ulong? ExportAddressTableJumps { get; }

        /// <summary>
        /// Gets the value indicating the managed native header.
        /// </summary>
        /// <remarks>This should always be zero.</remarks>
        /// <value>The managed native header.</value>
        ulong? ManagedNativeHeader { get; }
    }
}