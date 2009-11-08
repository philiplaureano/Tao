namespace Tao.Core
{
    /// <summary>
    /// Represents a data directory in a portable executable file.
    /// </summary>
    public interface IDataDirectory : IReader
    {
        /// <summary>
        /// Gets the value indicating the RVA of the data directory.
        /// </summary>
        /// <value>The virtual address of the target data directory.</value>
        uint? VirtualAddress { get; }

        /// <summary>
        /// Gets the value indicating the size of the target data directory.
        /// </summary>
        /// <value>The size of the target data directory.</value>
        uint? Size { get; }
    }
}