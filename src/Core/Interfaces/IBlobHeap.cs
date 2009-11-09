using System.Collections.Generic;

namespace Tao.Core
{
    /// <summary>
    /// Represents the blob heap in a .NET assembly.
    /// </summary>
    public interface IBlobHeap 
    {
        /// <summary>
        /// Gets the value indicating the list of blobs within the current blob heap.
        /// </summary>
        /// <value>The list of blobs currently within the blob heap.</value>
        IList<byte[]> Blobs { get; }
    }
}