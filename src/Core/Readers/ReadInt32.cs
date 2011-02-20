using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a class that reads an <see cref="Int32"/> from a given stream.
    /// </summary>
    public class ReadInt32 : IFunction<Stream, int>
    {
        /// <summary>
        /// Reads an <see cref="Int32"/> from the given stream.
        /// </summary>
        /// <param name="input">the input stream.</param>
        /// <returns>Returns an <see cref="Int32"/>.</returns>
        public int Execute(Stream input)
        {
            var reader = new BinaryReader(input);
            var result = reader.ReadInt32();

            return result;
        }
    }
}
