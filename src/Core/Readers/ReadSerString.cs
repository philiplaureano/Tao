using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a class that reads custom attribute string elements.
    /// </summary>
    public class ReadSerString : IFunction<Stream, string>
    {
        private readonly IFunction<Stream, uint> _readCompressedInteger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadSerString"/> class.
        /// </summary>
        /// <param name="readCompressedInteger">The compressed integer reader.</param>
        public ReadSerString(IFunction<Stream, uint> readCompressedInteger)
        {
            _readCompressedInteger = readCompressedInteger;
        }

        /// <summary>
        /// Reads a custom attribute string element from the given <paramref name="input">input stream</paramref>.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string Execute(Stream input)
        {
            var nextByte = (byte) input.ReadByte();
            if (nextByte == 0xFF)
                return null;

            if (nextByte == 0x00)
                return string.Empty;

            // Reset the stream position to 
            // point to the string length
            input.Seek(-1, SeekOrigin.Current);
            var length = (int)_readCompressedInteger.Execute(input);

            var reader = new BinaryReader(input);
            var bytes = reader.ReadBytes(length);
            var characters = Encoding.UTF8.GetChars(bytes);

            var result = new string(characters);
            return result;
        }
    }
}
