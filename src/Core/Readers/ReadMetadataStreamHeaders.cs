using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao.Core.Readers
{
    /// <summary>
    /// Represents a class that reads a metadata stream header using a given index.
    /// </summary>
    public class ReadMetadataStreamHeaders : IFunction<Stream, IEnumerable<ITuple<int, int, string>>>
    {
        private readonly IFunction<Stream, string> _readVersionString;
        private readonly IFunction<Stream, string> _readNullTerminatedString;
        private readonly IFunction<ITuple<int, Stream>> _realignStreamPosition;
        private readonly IFunction<Stream, int> _readMetadataStreamCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadMetadataStreamHeaders"/> class.
        /// </summary>
        public ReadMetadataStreamHeaders(IFunction<Stream, string> readVersionString, IFunction<Stream, string> readNullTerminatedString, IFunction<ITuple<int, Stream>> realignStreamPosition, IFunction<Stream, int> readMetadataStreamCount)
        {
            _readVersionString = readVersionString;
            _readMetadataStreamCount = readMetadataStreamCount;
            _realignStreamPosition = realignStreamPosition;
            _readNullTerminatedString = readNullTerminatedString;
        }

        /// <summary>
        /// Reads a metadata stream headers using a given stream.
        /// </summary>
        /// <param name="input">The index of the metadata stream header and the target stream.</param>
        /// <returns>A set of tuples that contain the metadata header stream.</returns>
        public IEnumerable<ITuple<int, int, string>> Execute(Stream stream)
        {
            _readVersionString.Execute(stream);

            // Seek the first byte of the first metadata header entry
            stream.Seek(5, SeekOrigin.Current);
            
            var reader = new BinaryReader(stream);
            var streamCount = _readMetadataStreamCount.Execute(stream);
            for (var i = 0; i < streamCount; i++)
            {                
                var offset = reader.ReadInt32();                
                var size = reader.ReadInt32();                
                var name = _readNullTerminatedString.Execute(stream);

                // Realign the stream position to the nearest 4-byte boundary
                _realignStreamPosition.Execute(4, stream);
                yield return Tuple.New(offset, size, name);
            }
        }
    }
}
