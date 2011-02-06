using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a type that reads a compressed signed integer.
    /// </summary>
    public class ReadCompressedSignedInteger : IFunction<Stream, int>
    {
        private readonly IFunction<Stream, uint> _readCompressedInteger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadCompressedSignedInteger"/> class.
        /// </summary>
        public ReadCompressedSignedInteger(IFunction<Stream, uint> readCompressedInteger)
        {
            _readCompressedInteger = readCompressedInteger;
        }

        /// <summary>
        /// Reads a compressed signed integer.
        /// </summary>
        /// <param name="input">The input stream containing the compressed signed integer.</param>
        /// <returns>The integer value.</returns>
        public int Execute(Stream input)
        {
            var value = (int)_readCompressedInteger.Execute(input);

            if ((value & 1) == 0)
                return value >> 1;

            return -(value >> 1);
        }       
    }
}
