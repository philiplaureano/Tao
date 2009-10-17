using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tao.Core
{
    /// <summary>
    /// Represents the default implementation of the <see cref="IBinaryReader"/> interface.
    /// </summary>
    public class BinaryReader : IBinaryReader
    {
        private readonly System.IO.BinaryReader _actualReader;

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryReader"/> class.
        /// </summary>
        /// <param name="stream">The input stream.</param>
        public BinaryReader(Stream stream)
        {
            _actualReader = new System.IO.BinaryReader(stream);
        }

        /// <summary>
        /// Reads count bytes from the current stream into a byte array and advances the current position by count bytes.
        /// </summary>
        /// <param name="count">The number of bytes to read</param>
        /// <returns>A byte array containing data read from the underlying stream. This might be less than the number of bytes requested if the end of the stream is reached.</returns>
        public byte[] ReadBytes(int count)
        {
            return _actualReader.ReadBytes(count);
        }

        /// <summary>
        /// Reads a 2-byte unsigned integer from the current stream using little-endian encoding and advances the position of the stream by two bytes.
        /// </summary>
        /// <returns>A 2-byte unsigned integer read from this stream.</returns>
        public ushort ReadUInt16()
        {
            return _actualReader.ReadUInt16();
        }

        /// <summary>
        /// Reads a 4-byte signed integer from the current stream and advances the position of the stream by four bytes.
        /// </summary>
        /// <returns>A 4-byte signed integer read from the current stream.</returns>
        public int ReadInt32()
        {
            return _actualReader.ReadInt32();
        }

        /// <summary>
        /// Reads the next byte from the current stream and advances the current position of the stream by one byte.
        /// </summary>
        /// <returns>The next byte read from the current stream.</returns>
        public byte ReadByte()
        {
            return _actualReader.ReadByte();
        }

        /// <summary>
        /// Reads a 4-byte unsigned integer from the current stream and advances the position of the stream by four bytes.
        /// </summary>
        /// <returns>A 4-byte unsigned integer read from this stream.</returns>
        public uint ReadUInt32()
        {
            return _actualReader.ReadUInt32();
        }
    }
}
