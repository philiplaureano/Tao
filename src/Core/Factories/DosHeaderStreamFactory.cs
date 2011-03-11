using System.IO;
using Tao.Interfaces;

namespace Tao.Factories
{
    /// <summary>
    /// Represents a factory class that extracts the DOS header stream from an existing portable executable image.
    /// </summary>
    public class DosHeaderStreamFactory : IFunction<Stream, Stream>
    {
        private readonly IFunction<ITuple<int, Stream>, Stream> _reader;
        private readonly IFunction<Stream> _seeker;

        /// <summary>
        /// Initializes a new instance of the <see cref="DosHeaderStreamFactory"/> class.
        /// </summary>
        /// <param name="reader">The substream reader.</param>
        /// <param name="dosHeaderSeeker">The <see cref="IFunction{Stream}"/> instance that will be used to locate the position of the DOS header.</param>        
        public DosHeaderStreamFactory(IFunction<ITuple<int, Stream>, Stream> reader, IFunction<Stream> dosHeaderSeeker)
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
            _seeker.Execute(input);

            const int size = 0x3c;
            return _reader.Execute(size, input);
        }
    }
}
