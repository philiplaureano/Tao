using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// <summary>
    /// Represens a class that reads the size of the blob stream from a 4-byte integer.
    /// </summary>
    public class GetBlobSizeFromDoubleWord : IFunction<ITuple<Stream, byte>, uint>
    {
        /// <summary>
        /// Reads the size of the blob stream from a 4-byte integer.
        /// </summary>
        /// <param name="input">The input stream and the leading byte.</param>
        /// <returns>The size of the current blob.</returns>
        public uint Execute(ITuple<Stream, byte> input)
        {
            var stream = input.Item1;
            var leadingByte = input.Item2;
            var reader = new BinaryReader(stream);

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
    }
}
