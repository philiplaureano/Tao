using System.IO;
using Tao.Interfaces;

namespace Tao.Seekers
{
    /// <summary>
    /// Represents a class that can seek the absolute file position that corresponds to the RVA entry of the given data directory stored
    /// at the given index.
    /// </summary>
    public class SeekDataStreamFromDataDirectoryIndex : IFunction<ITuple<int,Stream>>
    {
        private readonly IFunction<ITuple<int, Stream>, ITuple<int, int>> _indexedDataDirectoryFactory;
        private readonly IFunction<ITuple<int, Stream>> _seekAbsoluteFilePositionFromRva;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeekDataStreamFromDataDirectoryIndex"/> class.
        /// </summary>
        public SeekDataStreamFromDataDirectoryIndex(IFunction<ITuple<int, Stream>, ITuple<int, int>> indexedDataDirectoryFactory, IFunction<ITuple<int, Stream>> seekAbsoluteFilePositionFromRva)
        {
            _indexedDataDirectoryFactory = indexedDataDirectoryFactory;
            _seekAbsoluteFilePositionFromRva = seekAbsoluteFilePositionFromRva;
        }

        public void Execute(ITuple<int, Stream> input)
        {
            // Find the target data directory entry
            var index = input.Item1;
            var stream = input.Item2;

            var dataDirectoryEntry = _indexedDataDirectoryFactory.Execute(index, stream);
            var rva = dataDirectoryEntry.Item1;

            _seekAbsoluteFilePositionFromRva.Execute(rva, stream);
        }
    }
}
