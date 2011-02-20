using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a class that reads an <see cref="double"/> from a given stream.
    /// </summary>
    public class ReadDouble : IFunction<Stream, double>
    {
        /// <summary>
        /// Reads a <see cref="double"/> from the given stream.
        /// </summary>
        /// <param name="input">the input stream.</param>
        /// <returns>Returns an <see cref="double"/>.</returns>
        public double Execute(Stream input)
        {
            var reader = new BinaryReader(input);
            var result = reader.ReadDouble();

            return result;
        }
    }
}
