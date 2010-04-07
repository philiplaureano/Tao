using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao.Core
{
    /// <summary>
    /// Represents a class that maps a metadata stream header name to its index in the portable executable image stream..
    /// </summary>
    public class ReadMetadataStreamHeaderNameToIndexMappings : IFunction<Stream, IDictionary<string, int>>
    {
        private readonly IFunction<Stream, IEnumerable<ITuple<int, int, string>>> _readMetadataStreamHeaders;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadMetadataStreamHeaderNameToIndexMappings"/> class.
        /// </summary>
        public ReadMetadataStreamHeaderNameToIndexMappings(IFunction<Stream, IEnumerable<ITuple<int, int, string>>> readMetadataStreamHeaders)
        {
            _readMetadataStreamHeaders = readMetadataStreamHeaders;
        }

        /// <summary>
        /// Maps a metadata stream header name to its index in the portable executable image stream.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <returns>The name to index map.</returns>
        public IDictionary<string, int> Execute(Stream input)
        {
            var headers = _readMetadataStreamHeaders.Execute(input);
            var headerMap = new Dictionary<string, int>();

            var index = 0;
            foreach (var header in headers)
            {
                var name = header.Item3;
                headerMap[name] = index++;
            }

            return headerMap;
        }
    }
}
