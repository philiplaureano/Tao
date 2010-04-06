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
    public class ReadMetadataStreamHeader : IFunction<ITuple<int, Stream>, ITuple<int, int, string>>
    {
        private readonly IFunction<Stream, string> _readVersionString;
        private readonly IFunction<Stream, string> _readNullTerminatedString;
        private readonly IFunction<ITuple<int, Stream>> _realignStreamPosition;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public ReadMetadataStreamHeader(IFunction<Stream, string> readVersionString, IFunction<Stream, string> readNullTerminatedString, IFunction<ITuple<int, Stream>> realignStreamPosition)
        {
            _readVersionString = readVersionString;
            _realignStreamPosition = realignStreamPosition;
            _readNullTerminatedString = readNullTerminatedString;
        }

        /// <summary>
        /// Reads a metadata stream header using a given index and stream.
        /// </summary>
        /// <param name="input">The index of the metadata stream header and the target stream.</param>
        /// <returns>A tuple that contains the metadata header stream.</returns>
        public ITuple<int, int, string> Execute(ITuple<int, Stream> input)
        {
            var stream = input.Item2;
            var version = _readVersionString.Execute(stream);

            // Seek the first byte of the first metadata header entry
            stream.Seek(5, SeekOrigin.Current);

            var offset = 0;
            var size = 0;
            var name = string.Empty;

            var reader = new BinaryReader(stream);
            var targetIndex = input.Item1;
            for (var i = 0; i <= targetIndex; i++)
            {                
                offset = reader.ReadInt32();                
                size = reader.ReadInt32();                
                name = _readNullTerminatedString.Execute(stream);

                // Realign the stream position to the nearest 4-byte boundary
                _realignStreamPosition.Execute(4, stream);
            }

            return Tuple.New(offset, size, name);
        }
    }
}
