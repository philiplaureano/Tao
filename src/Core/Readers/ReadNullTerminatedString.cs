using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao.Core.Readers
{
    /// <summary>
    /// Represents a class that reads a null-terminated string from the current stream position.
    /// </summary>
    public class ReadNullTerminatedString : IFunction<Stream, string>
    {
        /// <summary>
        /// Reads a null terminated string from the current stream position.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <returns>The null-terminated string that was read from the stream.</returns>
        public string Execute(Stream input)
        {
            var reader = new BinaryReader(input);
            var result = string.Empty;

            var currentByte = reader.ReadByte();
            var encoding = Encoding.UTF8;
            while (currentByte != 0)
            {
                result += encoding.GetString(new[] { currentByte });
                currentByte = reader.ReadByte();
            }

            return result;
        }
    }
}
