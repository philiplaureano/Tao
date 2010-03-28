using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao.Core.Factories
{
    /// <summary>
    /// Represents a factory class that extracts the Coff header stream from an existing portable executable image.
    /// </summary>
    public class CoffHeaderStreamFactory : IConversion<Stream, Stream>
    {
        /// <summary>
        /// Creates a coff header substream from the given <paramref name="input"/> stream.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <returns>A stream containing the Coff header data.</returns>
        public Stream Create(Stream input)
        {
            input.Seek(0x98, SeekOrigin.Begin);

            const int headerSize = 0x5c;
            var data = new byte[headerSize];
            input.Read(data, 0, headerSize);

            return new MemoryStream(data);
        }
    }
}
