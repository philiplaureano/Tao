using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Core
{
    /// <summary>
    /// Represents the stream headers in a .NET portable executable.
    /// </summary>
    public class StreamHeaders : List<IStreamHeader>, IStreamHeaders
    {
        private readonly IMetadataRoot _root;
        private readonly IHeaderReader<IStreamHeader> _streamHeaderReader;

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamHeaders"/> class.
        /// </summary>
        public StreamHeaders() : this(new MetadataRoot())
        {            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamHeaders"/> class.
        /// </summary>
        /// <param name="root">The metadata root that determines the number of stream headers that exist within a PE file.</param>
        public StreamHeaders(IMetadataRoot root) : this(root, new StreamHeaderReader())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamHeaders"/> class.
        /// </summary>
        /// <param name="root">The metadata root that determines the number of stream headers that exist within a PE file.</param>
        /// <param name="streamHeaderReader">The stream header reader that will read the stream headers.</param>
        public StreamHeaders(IMetadataRoot root, IHeaderReader<IStreamHeader> streamHeaderReader)
        {
            if (root == null)
                throw new ArgumentNullException("root");

            if (streamHeaderReader == null)
                throw new ArgumentNullException("streamHeaderReader");

            _root = root;
            _streamHeaderReader = streamHeaderReader;
        }

        /// <summary>
        /// Reads the stream headers from the given binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public void ReadFrom(IBinaryReader reader)
        {
            _root.ReadFrom(reader);

            var streamCount = _root.NumberOfStreams;
            int headerCount = streamCount != null ? Convert.ToInt32(streamCount.Value) : 0;
            var headers = _streamHeaderReader.ReadFrom(headerCount, reader);

            AddRange(headers);
        }
    }
}
