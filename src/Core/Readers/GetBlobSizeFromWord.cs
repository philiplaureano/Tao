using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a class that reads the size of the blob stream from a 2-byte integer.
    /// </summary>
    public class GetBlobSizeFromWord : IFunction<ITuple<Stream, byte>, uint>
    {
        /// <summary>
        /// Reads the size of the blob stream from a 2-byte integer.
        /// </summary>
        /// <param name="input">The input stream and the leading byte.</param>
        /// <returns>The size of the current blob.</returns>
        public uint Execute(ITuple<Stream, byte> input)
        {
            var stream = input.Item1;
            var leadingByte = input.Item2;

            var mask = 0x0011111111111111b;
            var reader = new BinaryReader(stream);
            var word = reader.ReadInt16();
            uint value = (uint)(word & mask);

            return value;
        }
    }
}
