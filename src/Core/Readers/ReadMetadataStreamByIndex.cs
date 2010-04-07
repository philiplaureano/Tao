using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao.Core.Readers
{
    /// <summary>
    /// Represents a class that reads a metadata stream at the given index.
    /// </summary>
    public class ReadMetadataStreamByIndex : IFunction<ITuple<int, Stream>, Stream>
    {
        private readonly IFunction<Stream, IEnumerable<ITuple<int, int, string>>> _readMetadataStreamHeaders;
        private readonly IFunction<ITuple<int, Stream>, Stream> _inMemorySubStreamReader;
        private readonly IFunction<Stream> _seekMetadataRootPosition;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadMetadataStreamByIndex"/> class.
        /// </summary>
        public ReadMetadataStreamByIndex(IFunction<Stream, IEnumerable<ITuple<int, int, string>>> readMetadataStreamHeaders, IFunction<Stream> seekMetadataRootPosition, IFunction<ITuple<int, Stream>, Stream> inMemorySubStreamReader)
        {
            _readMetadataStreamHeaders = readMetadataStreamHeaders;
            _seekMetadataRootPosition = seekMetadataRootPosition;
            _inMemorySubStreamReader = inMemorySubStreamReader;
        }

        /// <summary>
        /// Reads a metadata stream at the given index.
        /// </summary>
        /// <param name="input">The index and the input stream.</param>
        /// <returns>The metadata stream.</returns>
        public Stream Execute(ITuple<int, Stream> input)
        {
            var stream = input.Item2;
            var headers = _readMetadataStreamHeaders.Execute(stream);

            var index = input.Item1;
            var headerList = new List<ITuple<int, int, string>>(headers);
            var targetHeader = headerList[index];

            // Find the root position
            _seekMetadataRootPosition.Execute(stream);

            var offset = targetHeader.Item1;
            var size = targetHeader.Item2;

            stream.Seek(offset, SeekOrigin.Current);

            return _inMemorySubStreamReader.Execute(size, stream);
        }
    }
}
