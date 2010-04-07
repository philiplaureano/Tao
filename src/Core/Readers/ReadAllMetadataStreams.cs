using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao.Core.Readers
{
    /// <summary>
    /// Represents a class that reads all .NET metadata streams from the given stream.
    /// </summary>
    public class ReadAllMetadataStreams : IFunction<Stream, IDictionary<string, Stream>>
    {
        private readonly IFunction<Stream, IEnumerable<ITuple<int, int, string>>> _readMetadataStreamHeaders;
        private readonly IFunction<ITuple<int, Stream>, Stream> _readMetadataStreamByIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadAllMetadataStreams"/> class.
        /// </summary>
        public ReadAllMetadataStreams(IFunction<Stream, IEnumerable<ITuple<int, int, string>>> readMetadataStreamHeaders, IFunction<ITuple<int, Stream>, Stream> readMetadataStreamByIndex)
        {
            _readMetadataStreamHeaders = readMetadataStreamHeaders;
            _readMetadataStreamByIndex = readMetadataStreamByIndex;
        }

        /// <summary>
        /// Reads all .NET metadata streams from the given stream.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <returns>A dictionary containing all the metadata streams within a .NET portable executable, indexed by stream name.</returns>
        public IDictionary<string, Stream> Execute(Stream input)
        {
            var headers = _readMetadataStreamHeaders.Execute(input);
            var headerList = new List<ITuple<int, int, string>>(headers);
            var results = new Dictionary<string, Stream>();

            var headerCount = headerList.Count;
            for(var i = 0; i < headerCount; i++)
            {
                var header = headerList[i];
                var name = header.Item3;
                results[name] = _readMetadataStreamByIndex.Execute(i, input);
            }

            return results;
        }
    }
}
