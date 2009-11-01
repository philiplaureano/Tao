using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Core
{
    /// <summary>
    /// Represents an index that points to a metadata heap in a .NET portable executable image.
    /// </summary>
    public class HeapIndex : IReader, IHeapIndex
    {
        private readonly IMetadataStream _metadataStream;
        private readonly byte _targetBit;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeapIndex"/> class.
        /// </summary>
        /// <param name="targetBit">The bit index of the <see cref="IMetadataStream.HeapSizes"/> bit vector that specifies whether or not a particular heap is using a double-worded index.</param>
        /// <param name="metadataStream">The metadata stream that determines the size of each heap index.</param>
        public HeapIndex(byte targetBit, IMetadataStream metadataStream)
        {
            if (metadataStream == null)
                throw new ArgumentNullException("metadataStream");

            _metadataStream = metadataStream;
            _targetBit = targetBit;
        }

        /// <summary>
        /// Gets or sets the value indicating the size of each heap index, in bytes.
        /// </summary>
        /// <value>The size of each heap index, in bytes.</value>
        public virtual int IndexSizeInBytes
        {
            get
            {
                var bitVector = _metadataStream.HeapSizes;
                var bitValue = (bitVector >> _targetBit) & 1;

                var useDwordIndices = Convert.ToBoolean(bitValue);

                return useDwordIndices ? 4 : 2;
            }
        }

        /// <summary>
        /// Gets or sets the value indicating the index value itself.
        /// </summary>
        /// <value>The index value itself.</value>
        public uint? Value
        {
            get;
            protected set;
        }

        /// <summary>
        /// Reads the heap index value from the given <paramref name="reader"/>.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public virtual void ReadFrom(IBinaryReader reader)
        {
            if (IndexSizeInBytes == 4)
            {
                Value = reader.ReadUInt32();
                return;
            }

            Value = reader.ReadUInt16();
        }
    }
}
