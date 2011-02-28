using System.IO;
using Tao.Interfaces;

namespace Tao.Factories
{
    /// <summary>
    /// Represents a class that can read PE Section header substreams.
    /// </summary>
    public class PESectionFactory : IFunction<ITuple<int, Stream>, Stream>
    {
        private readonly IFunction<Stream> _dataDirectoriesEndSeeker;
        private readonly IFunction<ITuple<int, Stream>, Stream> _inMemorySubStreamReader;

        /// <summary>
        /// Initializes a new instance of the <see cref="PESectionFactory"/> class.
        /// </summary>
        public PESectionFactory(IFunction<ITuple<int, Stream>, Stream> inMemorySubStreamReader, IFunction<Stream> dataDirectoriesEndSeeker)
        {
            _dataDirectoriesEndSeeker = dataDirectoriesEndSeeker;
            _inMemorySubStreamReader = inMemorySubStreamReader;
        }

        /// <summary>
        /// Reads a PE Section header substream.
        /// </summary>
        /// <param name="input">The target stream.</param>
        /// <returns>A substream that contains the PE section header.</returns>
        public Stream Execute(ITuple<int, Stream> input)
        {
            var stream = input.Item2;
            var index = input.Item1;

            const int headerSize = 0x28;
            var offset = headerSize * index;

            _dataDirectoriesEndSeeker.Execute(input.Item2);
            stream.Seek(offset, SeekOrigin.Current);

            return _inMemorySubStreamReader.Execute(headerSize, stream);
        }
    }
}
