﻿using System.IO;
using Tao.Interfaces;

namespace Tao.Factories
{
    /// <summary>
    /// Represents a factory class that extracts the Coff header stream from an existing portable executable image.
    /// </summary>
    public class OptionalHeaderStreamFactory : IFunction<Stream, Stream>
    {
        private readonly IFunction<ITuple<int, Stream>, Stream> _reader;
        private readonly IFunction<Stream> _seeker;

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionalHeaderStreamFactory"/> class.
        /// </summary>
        /// <param name="reader">The substream reader.</param>
        /// <param name="optionalHeaderSeeker">The <see cref="IFunction{Stream}"/> that will be used to locate the Coff header position.</param>
        public OptionalHeaderStreamFactory(IFunction<ITuple<int, Stream>, Stream> reader, IFunction<Stream> optionalHeaderSeeker)
        {
            _reader = reader;
            _seeker = optionalHeaderSeeker;
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
            return _reader.Execute(size, input);
        }
    }
}