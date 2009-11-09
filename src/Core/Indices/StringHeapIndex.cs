using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Core
{
    /// <summary>
    /// Represents an index that points to the #Strings heap.
    /// </summary>
    public class StringHeapIndex : HeapIndex, IStringHeapIndex
    {        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="StringHeapIndex"/> class.
        /// </summary>
        /// <param name="metadataStream">The metadata stream.</param>
        public StringHeapIndex(IMetadataStream metadataStream) : this(0, metadataStream)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringHeapIndex"/> class.
        /// </summary>
        /// <param name="index">The target index.</param>
        public StringHeapIndex(uint index)
            : this(index, new MetadataStream())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringHeapIndex"/> class.
        /// </summary>
        /// <param name="index">The target index.</param>
        /// <param name="metadataStream">The metadata stream.</param>
        public StringHeapIndex(uint index, IMetadataStream metadataStream)
            : base(0, metadataStream)
        {
            Value = index;
        }
        
        /// <summary>
        /// Reads the string associated with the current index <see cref="HeapIndex.Value"/>.
        /// </summary>
        /// <param name="stringHeap">The string heap that contains the target string.</param>
        /// <returns>The string associated with the given heap index.</returns>
        public string GetText(IStringHeap stringHeap)
        {
            if (stringHeap == null)
                throw new InvalidOperationException("Invalid String Heap");

            var index = (int)Value;
            return stringHeap.Strings[index];
        }
    }
}
