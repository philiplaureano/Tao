using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a class that reads an <see cref="bool"/> from a given stream.
    /// </summary>
    public class ReadBoolean : IFunction<Stream, bool>
    {
        /// <summary>
        /// Reads an <see cref="bool"/> from the given stream.
        /// </summary>
        /// <param name="input">the input stream.</param>
        /// <returns>Returns an <see cref="bool"/>.</returns>
        public bool Execute(Stream input)
        {
            var reader = new BinaryReader(input);
            var result = reader.ReadBoolean();

            return result;
        }
    }
}
