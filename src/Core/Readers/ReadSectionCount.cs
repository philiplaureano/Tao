using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a class that reads the number of sections from a COFF file header.
    /// </summary>
    public class ReadSectionCount : IFunction<Stream, int>
    {
        /// <summary>
        /// Reads the number of sections from a COFF file header.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <returns>Returns the number of sections indicated in the COFF file header.</returns>
        public int Execute(Stream input)
        {
            input.Seek(0x84, SeekOrigin.Begin);

            // Skip the Machine field
            input.Seek(2, SeekOrigin.Current);

            var reader = new BinaryReader(input);
            var sectionCount = reader.ReadInt16();

            return sectionCount;
        }
    }
}
