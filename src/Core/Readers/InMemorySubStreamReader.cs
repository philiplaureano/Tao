using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a type that can extract streams from other streams and returns an in-memory stream.
    /// </summary>
    public class InMemorySubStreamReader : IFunction<ITuple<int, Stream>, Stream>
    {
        /// <summary>
        /// Reads a substream from a given <paramref name="input"/> stream.
        /// </summary>
        /// <param name="input">The tuple that represents the size of the substream to be read and the input stream itself.</param>
        /// <returns>A substream containing the data that was read.</returns>
        public Stream Execute(ITuple<int, Stream> input)
        {
            var size = input.Item1;
            var stream = input.Item2;

            var data = new byte[size];
            stream.Read(data, 0, size);

            return new MemoryStream(data);
        }
    }
}