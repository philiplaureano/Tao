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
    public class DosHeaderStreamFactory : IFunction<Stream, Stream>
    {
        private readonly ISubStreamReader _reader;
        private readonly IStreamSeeker _seeker;

        /// <summary>
        /// Initializes a new instance of the <see cref="DosHeaderStreamFactory"/> class.
        /// </summary>
        /// <param name="reader">The substream reader.</param>
        /// <param name="dosHeaderSeeker">The <see cref="IStreamSeeker"/> instance that will be used to locate the position of the DOS header.</param>        
        public DosHeaderStreamFactory(ISubStreamReader reader, IStreamSeeker dosHeaderSeeker)
        {
            _reader = reader;
            _seeker = dosHeaderSeeker;
        }

        /// <summary>
        /// Creates a DOS header substream from the given <paramref name="input"/> stream.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <returns>A stream containing the DOS header data.</returns>
        public Stream Execute(Stream input)
        {
            _seeker.Seek(input);

            const int size = 0x3c;
            return _reader.Read(size, input);
        }
    }
}
