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
            const int baseValue = 0x80;
            const byte sizeBitShift = 14;
            const uint sizeMask = 0x10b << sizeBitShift;

            var reader = new BinaryReader(stream);
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
