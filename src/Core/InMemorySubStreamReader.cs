using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao.Core
{
    /// <summary>
    /// Represents a type that can extract streams from other streams and returns an in-memory stream.
    /// </summary>
    public class InMemorySubStreamReader : ISubStreamReader
    {
        /// <summary>
        /// Reads a substream from a given <paramref name="input"/> stream.
        /// </summary>
        /// <param name="size">The size of the substream to be read.</param>
        /// <param name="input">The input stream.</param>
        /// <returns>A substream containing the data that was read.</returns>
        public Stream Read(int size, Stream input)
        {
            var data = new byte[size];
            input.Read(data, 0, size);

            return new MemoryStream(data);
        }
    }
}
