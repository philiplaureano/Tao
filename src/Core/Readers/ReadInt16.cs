using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a class that reads an <see cref="Int16"/> from a given stream.
    /// </summary>
    public class ReadInt16 : IFunction<Stream, short>
    {
        /// <summary>
        /// Reads an <see cref="Int16"/> from the given stream.
        /// </summary>
        /// <param name="input">the input stream.</param>
        /// <returns>Returns an <see cref="Int16"/>.</returns>
        public short Execute(Stream input)
        {
            var reader = new BinaryReader(input);
            var result = reader.ReadInt16();

            return result;
        }
    }
}
