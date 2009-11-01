using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Core
{
    /// <summary>
    /// Represents an index that points to the #Strings heap.
    /// </summary>
    public class StringHeapIndex : HeapIndex
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringHeapIndex"/> class.
        /// </summary>
        /// <param name="metadataStream">The metadata stream.</param>
       public StringHeapIndex(IMetadataStream metadataStream) : base(0, metadataStream)
       {           
       }
    }
}
