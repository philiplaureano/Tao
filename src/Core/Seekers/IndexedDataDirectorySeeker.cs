using System.IO;
using Tao.Interfaces;

namespace Tao.Seekers
{
    /// <summary>
    /// Represents a class that can set the stream position to point to the any data directory from a given index.
    /// </summary>
    public class IndexedDataDirectorySeeker : IFunction<ITuple<int, Stream>>
    {
        private readonly IFunction<Stream> _dataDirectoriesSeeker;

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexedDataDirectorySeeker"/> class.
        /// </summary>
        public IndexedDataDirectorySeeker(IFunction<Stream> dataDirectoriesSeeker)
        {
            _dataDirectoriesSeeker = dataDirectoriesSeeker;
        }

        /// <summary>
        /// Sets the stream position to point to the any data directory from a given index.
        /// </summary>
        /// <param name="input">The tuple that contains the index and target stream.</param>
        public void Execute(ITuple<int, Stream> input)
        {
            var stream = input.Item2;

            // Seek the position of the first data directory entry
            _dataDirectoriesSeeker.Execute(input.Item2);

            const int headerSize = 8;
            var index = input.Item1;
            var offset = headerSize*index;

            stream.Seek(offset, SeekOrigin.Current);
        }
    }
}
