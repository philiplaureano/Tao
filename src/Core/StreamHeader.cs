using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tao.Core
{
    /// <summary>
    /// Represents a single metadata data stream header.
    /// </summary>
    public class StreamHeader : IHeader
    {
        /// <summary>
        /// Gets the value indicating the memory offset from the start of the metadata root.
        /// </summary>
        /// <value>The memory offset from the start of the metadata root.</value>
        public uint? Offset
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the size of the stream in bytes.
        /// </summary>
        /// <value>The size of the stream in bytes.</value>
        public uint? Size
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the name of the stream header.
        /// </summary>
        /// <value>The name of the stream header.</value>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Reads the stream header name from the binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public void ReadFrom(IBinaryReader reader)
        {
            Offset = reader.ReadUInt32();
            Size = reader.ReadUInt32();
            ReadName(reader);
        }

        /// <summary>
        /// Reads the name of the stream from the given binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        private void ReadName(IBinaryReader reader)
        {

            var name = string.Empty;
            byte currentByte = reader.ReadByte();
            var encoding = Encoding.UTF8;
            while (currentByte != 0)
            {
                name += encoding.GetString(new byte[] { currentByte });
                currentByte = reader.ReadByte();
            }

            Name = name;

            var byteAlignment = 4;

            Realign(reader, byteAlignment);
        }

        /// <summary>
        /// Realigns the reader position according to the specified byte alignment.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        /// <param name="byteAlignment">The byte alignment.</param>
        private static void Realign(IBinaryReader reader, int byteAlignment)
        {
            // Move the reader position beyond
            // the padded bytes of the byte alignment
            var currentPosition = reader.GetPosition();
            var paddedBytes = currentPosition % byteAlignment;

            if (paddedBytes <= 0)
                return;

            var nextPosition = currentPosition - paddedBytes + byteAlignment;
            reader.Seek(nextPosition, SeekOrigin.Begin);
        }
    }
}
