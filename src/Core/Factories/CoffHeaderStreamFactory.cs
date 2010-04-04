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
    public class CoffHeaderStreamFactory : IFunction<Stream, Stream>
    {
        private readonly IFunction<ITuple<int, Stream>, Stream> _reader;
        private readonly IFunction<Stream> _seeker;

        /// <summary>
        /// Initializes a new instance of the <see cref="CoffHeaderStreamFactory"/> class.
        /// </summary>
        /// <param name="reader">The substream reader.</param>
        /// <param name="coffHeaderSeeker">The <see cref="IFunction{Stream}"/> that will be used to locate the Coff header position.</param>
        public CoffHeaderStreamFactory(IFunction<ITuple<int, Stream>, Stream> reader, IFunction<Stream> coffHeaderSeeker)
        {
            _reader = reader;
            _seeker = coffHeaderSeeker;
        }

        /// <summary>
        /// Creates a coff header substream from the given <paramref name="input"/> stream.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <returns>A stream containing the Coff header data.</returns>
        public Stream Execute(Stream input)
        {
            _seeker.Execute(input);

            const int size = 0x5c;
            return _reader.Execute(Tuple.New(size, input));
        }
    }
}
