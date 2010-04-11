using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao.Factories
{
    /// <summary>
    /// Represents a class that can read the block that contains the PE Header.
    /// </summary>
    public class PEFileHeaderStreamFactory : IFunction<Stream, Stream>
    {
        private readonly IFunction<ITuple<int, Stream>, Stream> _inMemorySubStreamReader;
        private readonly IFunction<Stream> _peStreamSeeker;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        /// <param name="inMemorySubStreamReader">The substream reader that will be used to read the block of data directories.</param>
        /// <param name="peStreamSeeker">The <see cref="IFunction{Stream}"/> that will set the stream position to point to the first byte in the PE header stream.</param>
        public PEFileHeaderStreamFactory(IFunction<ITuple<int, Stream>, Stream> inMemorySubStreamReader, IFunction<Stream> peStreamSeeker)
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
            _peStreamSeeker.Execute(input);

            return _inMemorySubStreamReader.Execute(0x12, input);
        }
    }
}
