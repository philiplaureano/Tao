using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Core
{
    /// <summary>
    /// Represents an index into the blob heap.
    /// </summary>
    public class BlobHeapIndex : HeapIndex
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BlobHeapIndex"/> class.
        /// </summary>
        /// <param name="metadataStream">The metadata stream class.</param>
        public BlobHeapIndex(IMetadataStream metadataStream) : base(3, metadataStream)
        {            
        }
    }
}
