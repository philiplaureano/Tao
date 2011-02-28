using System;
using System.IO;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a class that reads an <see cref="ulong"/> from a given stream.
    /// </summary>
    public class ReadUInt64 : IFunction<Stream, UInt64>
    {
        /// <summary>
        /// Reads a <see cref="UInt64"/> from the given stream.
        /// </summary>
        /// <param name="input">the input stream.</param>
        /// <returns>Returns an <see cref="UInt64"/>.</returns>
        public UInt64 Execute(Stream input)
        {
            var reader = new BinaryReader(input);
            var result = reader.ReadUInt64();

            return result;
        }
    }
}
