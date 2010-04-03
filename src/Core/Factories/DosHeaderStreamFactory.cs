using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao.Core.Factories
{
    /// <summary>
    /// Represents a factory class that extracts the DOS header stream from an existing portable executable image.
    /// </summary>
    public class DosHeaderStreamFactory : IConversion<Stream, Stream>
    {
        private readonly ISubStreamReader _reader;

        /// <summary>
        /// Initializes a new instance of the <see cref="DosHeaderStreamFactory"/> class.
        /// </summary>
        /// <param name="reader">The substream reader.</param>
        public DosHeaderStreamFactory(ISubStreamReader reader)
        {
            _reader = reader;
        }

        /// <summary>
        /// Creates a DOS header substream from the given <paramref name="input"/> stream.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <returns>A stream containing the DOS header data.</returns>
        public Stream Convert(Stream input)
        {
            input.Seek(0, SeekOrigin.Begin);

            const int size = 0x3c;
            return _reader.Read(size, input);
        }        
    }
}
