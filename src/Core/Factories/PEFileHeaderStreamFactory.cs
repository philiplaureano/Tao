using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao.Core.Factories
{
    /// <summary>
    /// Represents a class that can read the block that contains the PE Header.
    /// </summary>
    public class PEFileHeaderStreamFactory : IFunction<Stream, Stream>
    {
        private readonly ISubStreamReader _inMemorySubStreamReader;
        private readonly IStreamSeeker _peStreamSeeker;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        /// <param name="inMemorySubStreamReader">The <see cref="ISubStreamReader"/> that will be used to read the block of data directories.</param>
        /// <param name="peStreamSeeker">The <see cref="IStreamSeeker"/> that will set the stream position to point to the first byte in the PE header stream.</param>
        public PEFileHeaderStreamFactory(ISubStreamReader inMemorySubStreamReader, IStreamSeeker peStreamSeeker)
        {
            _inMemorySubStreamReader = inMemorySubStreamReader;
            _peStreamSeeker = peStreamSeeker;
        }

        /// <summary>
        /// Reads the block that contains the PE Header.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <returns>The PE Header substream.</returns>
        public Stream Execute(Stream input)
        {
            _peStreamSeeker.Seek(input);

            return _inMemorySubStreamReader.Read(0x12, input);
        }
    }
}
