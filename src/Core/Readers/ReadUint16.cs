using System;
using System.IO;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a class that reads an <see cref="ushort"/> from a given stream.
    /// </summary>
    public class ReadUInt16 : IFunction<Stream, UInt16>
    {
        /// <summary>
        /// Reads an <see cref="UInt16"/> from the given stream.
        /// </summary>
        /// <param name="input">the input stream.</param>
        /// <returns>Returns an <see cref="UInt16"/>.</returns>
        public UInt16 Execute(Stream input)
        {
            var reader = new BinaryReader(input);
            var result = reader.ReadUInt16();

            return result;
        }
    }
}
