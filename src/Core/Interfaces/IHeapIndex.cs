namespace Tao.Core
{
    /// <summary>
    /// Represents an index that points to a metadata heap.
    /// </summary>
    public interface IHeapIndex : IReader
    {
        /// <summary>
        /// Gets or sets the value indicating the size of each heap index, in bytes.
        /// </summary>
        /// <value>The size of each heap index, in bytes.</value>
        int IndexSizeInBytes { get; }

        /// <summary>
        /// Gets the value indicating the index value itself.
        /// </summary>
        /// <value>The index value itself.</value>
        uint? Value { get; }
    }
}