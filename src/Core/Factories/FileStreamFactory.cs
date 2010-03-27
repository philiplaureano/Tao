using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao.Core.Factories
{
    /// <summary>
    /// Represents a factory type that can read the contents of a file and dump it into a memory stream.
    /// </summary>
    public class FileStreamFactory : IFactory<string, Stream>
    {
        /// <summary>
        /// Reads the contents of a file and dump it into a memory stream.
        /// </summary>
        /// <param name="input">The input file name.</param>
        /// <returns>A stream that contains the target file.</returns>
        public Stream Create(string input)
        {
            MemoryStream result = null;
            using (var fileStream = new FileStream(input, FileMode.Open))
            {
                var streamLength = Convert.ToInt32(fileStream.Length);
                var buffer = new byte[streamLength];
                fileStream.Read(buffer, 0, streamLength);
                result = new MemoryStream(buffer);
            }

            return result;
        }
    }
}