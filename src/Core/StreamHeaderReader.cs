using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Core
{
    /// <summary>
    /// Represents a class that reads a set of stream headers from a binary reader.
    /// </summary>
    public class StreamHeaderReader : IHeaderReader<IStreamHeader>
    {
        /// <summary>
        /// Reads the specified number of headers from the given binary reader.
        /// </summary>
        /// <param name="headerCount">The number of stream headers to read.</param>
        /// <param name="reader">The binary reader.</param>
        /// <returns>The list of stream headers that were read from the binary reader.</returns>
        public IEnumerable<IStreamHeader> ReadFrom(int headerCount, IBinaryReader reader)
        {
            var results = new List<IStreamHeader>();
            for (var i = 0; i < headerCount; i++)
            {
                var header = new StreamHeader();
                header.ReadFrom(reader);

                results.Add(header);
            }

            return results;
        }
    }
}
