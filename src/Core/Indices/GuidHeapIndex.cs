using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Core
{
    /// <summary>
    /// Represents an index into the #GUID heap.
    /// </summary>
    public class GuidHeapIndex : HeapIndex
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GuidHeapIndex"/> class.
        /// </summary>
        /// <param name="metadataStream">The metadata stream.</param>
        public GuidHeapIndex(IMetadataStream metadataStream) : base(1, metadataStream)
        {            
        }
    }
}
