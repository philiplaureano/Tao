using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Core
{
    /// <summary>
    /// Represents the Blob heap in a .NET assembly.
    /// </summary>
    public class BlobHeap : Heap, IBlobHeap
    {
        private readonly IList<byte[]> _blobs = new List<byte[]>();

        /// <summary>
        /// Initializes a new instance of the <see cref="BlobHeap"/> class.
        /// </summary>
        public BlobHeap()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlobHeap"/> class.
        /// </summary>
        /// <param name="optionalHeader">The optional header that will determine the file position of the heap.</param>
        /// <param name="cliHeader">The CLI header that determines the position of the metadata root.</param>
        /// <param name="streamHeaders">The collection of metadata stream headers that determine the position of the heap itself.</param>
        public BlobHeap(IOptionalHeader optionalHeader, ICLIHeader cliHeader, IStreamHeaders streamHeaders)
            : base(optionalHeader, cliHeader, streamHeaders)
        {

        }

        /// <summary>
        /// Gets the value indicating the list of blobs within the current blob heap.
        /// </summary>
        /// <value>The list of blobs currently within the blob heap.</value>
        public IList<byte[]> Blobs
        {
            get
            {
                return _blobs;
            }
        }

        /// <summary>
        /// Reads the stream header from the given binary reader.
        /// </summary>
        /// <param name="reader">The binary reader that contains the stream to be read.</param>
        /// <param name="streamHeader">The stream header that describes the target stream.</param>
        protected override void ReadFrom(IBinaryReader reader, IStreamHeader streamHeader)
        {
            var startingPosition = reader.GetPosition();


            var streamSize = streamHeader.Size;
            long bytesRead = 0;

            while (bytesRead < streamSize)
            {
                var currentBlobSize = GetBlobSize(reader);

                var blobData = reader.ReadBytes((int)currentBlobSize);
                _blobs.Add(blobData);

                bytesRead = reader.GetPosition() - startingPosition;
            }
        }

        /// <summary>
        /// Determines the name of the metadata stream to be read from a portable executable file.
        /// </summary>
        /// <returns>The name of the metadata stream.</returns>
        protected override string GetStreamName()
        {
            return "#Blob";
        }

        /// <summary>
        /// Determines the size of the current blob using the given binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        /// <returns>The size of the current blob, in bytes.</returns>
        private static uint GetBlobSize(IBinaryReader reader)
        {
            var leadingByte = reader.ReadByte();

            const byte signatureShift = 6;
            var signatureType = leadingByte >> signatureShift;

            // Determine the size of the current blob
            uint size = 0;
            if (signatureType == 0 || signatureType == 1)
            {
                var strippedValue = leadingByte << 2;
                size = (uint)strippedValue >> 2;

                return size;
            }

            if (signatureType == 0x10b)
            {
                // Strip the first two bits off the leading byte
                // to get the blob size
                size = GetBlobSizeFromWord(reader, leadingByte);
            }

            if (signatureType == 0x11b)
                size = GetSizeFromDoubleWord(reader, leadingByte);

            return size;
        }

        /// <summary>
        /// Reads the size of the blob stream from a 4-byte integer.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        /// <param name="leadingByte">The leading byte that contains the signature type.</param>
        /// <returns>The size of the blob stream, in bytes.</returns>
        private static uint GetSizeFromDoubleWord(IBinaryReader reader, byte leadingByte)
        {
            const int baseValue = 0x3fff;
            var remainingBytes = reader.ReadBytes(3);
            var lowDoubleWord = Convert.ToUInt32(remainingBytes);
            var hiDoubleWord = leadingByte << 24;

            // Use the first four bytes of the blob to determine
            // the size of the blob
            var combinedDoubleWord = hiDoubleWord + lowDoubleWord;

            const byte sizeBitShift = 29;
            const uint sizeMask = (uint)0x110b << sizeBitShift;

            return (uint)combinedDoubleWord ^ sizeMask + baseValue;
        }

        /// <summary>
        /// Reads the size of the blob stream from a 2-byte integer.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        /// <param name="leadingByte">The leading byte that contains the signature type.</param>
        /// <returns>The size of the blob stream, in bytes.</returns>
        private static uint GetBlobSizeFromWord(IBinaryReader reader, byte leadingByte)
        {
            const int baseValue = 0x80;
            const byte sizeBitShift = 14;
            const uint sizeMask = 0x10b << sizeBitShift;

            var remainingByte = reader.ReadByte();

            // Combine the leading byte and the remaining byte
            // into a single dword
            var leadingWord = leadingByte << 8;
            leadingWord |= remainingByte;

            var size = (uint)leadingWord ^ sizeMask + baseValue;
            return size;
        }
    }
}
