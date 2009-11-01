using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Core
{
    /// <summary>
    /// Represents an index that points to the #Strings heap.
    /// </summary>
    public class StringsHeapIndex : HeapIndex
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringsHeapIndex"/> class.
        /// </summary>
        /// <param name="metadataStream">The metadata stream.</param>
       public StringsHeapIndex(IMetadataStream metadataStream) : base(0, metadataStream)
       {           
       }
    }
}
