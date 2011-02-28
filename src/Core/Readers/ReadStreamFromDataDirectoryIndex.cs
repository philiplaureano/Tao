using System.IO;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a class that can read a stream using a given data directory header index.
    /// </summary>
    public class ReadStreamFromDataDirectoryIndex : IFunction<ITuple<int, Stream>, Stream>
    {
        private readonly IFunction<ITuple<int, Stream>, ITuple<int, int>> _indexedDataDirectoryFactory;
        private readonly IFunction<ITuple<int, Stream>> _seekAbsoluteFilePositionFromRva;
        private readonly IFunction<ITuple<int, Stream>, Stream> _inMemorySubStreamReader;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadStreamFromDataDirectoryIndex"/> class.
        /// </summary>
        public ReadStreamFromDataDirectoryIndex(IFunction<ITuple<int, Stream>, ITuple<int, int>> indexedDataDirectoryFactory, IFunction<ITuple<int, Stream>> seekAbsoluteFilePositionFromRva, IFunction<ITuple<int, Stream>, Stream> inMemorySubStreamReader)
        {
            _indexedDataDirectoryFactory = indexedDataDirectoryFactory;
            _seekAbsoluteFilePositionFromRva = seekAbsoluteFilePositionFromRva;
            _inMemorySubStreamReader = inMemorySubStreamReader;
        }

        /// <summary>
        /// Reads a stream using a given data directory header index.
        /// </summary>
        /// <param name="input">The index and the input stream.</param>
        /// <returns>The data from the given data directory header index.</returns>
        public Stream Execute(ITuple<int, Stream> input)
        {
            var dataDirectoryEntry = _indexedDataDirectoryFactory.Execute(input);

            var rva = dataDirectoryEntry.Item1;
            var size = dataDirectoryEntry.Item2;

            var stream = input.Item2;

            // Seek the first byte of the target data
            _seekAbsoluteFilePositionFromRva.Execute(rva, stream);

            return _inMemorySubStreamReader.Execute(size, stream);
        }
    }
}