using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao.Core.Readers
{
    /// <summary>
    /// Represents a class that reads the metadata heap from the given stream.
    /// </summary>
    public class ReadMetadataStream : IFunction<Stream, Stream> 
    {
        private readonly IFunction<ITuple<int, Stream>, Stream> _readMetadataStreamByIndex;
        private readonly IFunction<ITuple<string, Stream>, int> _getMetadataStreamIndexFromName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadMetadataStream"/> class.
        /// </summary>
        public ReadMetadataStream(IFunction<ITuple<int, Stream>, Stream> readMetadataStreamByIndex, IFunction<ITuple<string, Stream>, int> getMetadataStreamIndexFromName)
        {
            _readMetadataStreamByIndex = readMetadataStreamByIndex;
            _getMetadataStreamIndexFromName = getMetadataStreamIndexFromName;
        }

        /// <summary>
        /// Reads the metadata heap from the given stream.
        /// </summary>
        /// <param name="input">The target stream.</param>
        /// <returns>The metadata stream.</returns>
        public Stream Execute(Stream input)
        {
            var targetHeaderName = "#~";
            var targetIndex = _getMetadataStreamIndexFromName.Execute(targetHeaderName, input);

            return _readMetadataStreamByIndex.Execute(targetIndex, input);
        }
    }
}
