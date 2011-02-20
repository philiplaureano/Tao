using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a class that reads an <see cref="byte"/> from a given stream.
    /// </summary>
    public class ReadByte : IFunction<Stream, byte>
    {
        /// <summary>
        /// Reads a <see cref="byte"/> from the given stream.
        /// </summary>
        /// <param name="input">the input stream.</param>
        /// <returns>Returns an <see cref="byte"/> value.</returns>
        public byte Execute(Stream input)
        {
            var reader = new BinaryReader(input);
            var result = reader.ReadByte();

            return result;
        }
    }
}
