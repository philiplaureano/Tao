using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tao.Interfaces
{
    /// <summary>
    /// Represents a type that can extract streams from other streams.
    /// </summary>
    public interface ISubStreamReader
    {
        /// <summary>
        /// Reads a substream from a given <paramref name="input"/> stream.
        /// </summary>
        /// <param name="size">The size of the substream to be read.</param>
        /// <param name="input">The input stream.</param>
        /// <returns>A substream containing the data that was read.</returns>
        Stream Read(int size, Stream input);
    }
}
