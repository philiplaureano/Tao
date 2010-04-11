using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a class that retrieves the given index for the given heap name.
    /// </summary>
    public class GetMetadataStreamIndexFromName : IFunction<ITuple<string, Stream>, int>
    {
        private readonly IFunction<Stream, IDictionary<string, int>> _readMetadataStreamHeaderNameToIndexMappings;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetMetadataStreamIndexFromName"/> class.
        /// </summary>
        public GetMetadataStreamIndexFromName(IFunction<Stream, IDictionary<string, int>> readMetadataStreamHeaderNameToIndexMappings)
        {
            _readMetadataStreamHeaderNameToIndexMappings = readMetadataStreamHeaderNameToIndexMappings;
        }

        /// <summary>
        /// Retrieves the given index for the given heap name.
        /// </summary>
        /// <param name="input">The heap name.</param>
        /// <returns>The corresponding index.</returns>
        public int Execute(ITuple<string, Stream> input)
        {
            var targetHeaderName = input.Item1;
            var stream = input.Item2;
            var headerMap = _readMetadataStreamHeaderNameToIndexMappings.Execute(stream);
            if (headerMap.Count == 0 || !headerMap.ContainsKey(targetHeaderName))
                throw new InvalidOperationException(string.Format("CLR Metadata Stream '{0}' Not Found", targetHeaderName));

            return headerMap[targetHeaderName];
        }
    }
}
