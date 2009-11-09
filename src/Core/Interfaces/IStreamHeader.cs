namespace Tao.Core
{
    /// <summary>
    /// Represents a metadata stream header in a .NET portable executable file.
    /// </summary>
    public interface IStreamHeader : IReader
    {
        /// <summary>
        /// Gets the value indicating the memory offset from the start of the metadata root.
        /// </summary>
        /// <value>The memory offset from the start of the metadata root.</value>
        uint? Offset { get; }

        /// <summary>
        /// Gets the value indicating the size of the stream in bytes.
        /// </summary>
        /// <value>The size of the stream in bytes.</value>
        uint? Size { get; }

        /// <summary>
        /// Gets the value indicating the name of the stream header.
        /// </summary>
        /// <value>The name of the stream header.</value>
        string Name { get; }
    }
}