using System.IO;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a class that reads an <see cref="long"/> from a given stream.
    /// </summary>
    public class ReadInt64 : IFunction<Stream, long>
    {
        /// <summary>
        /// Reads a <see cref="long"/> from the given stream.
        /// </summary>
        /// <param name="input">the input stream.</param>
        /// <returns>Returns an <see cref="long"/>.</returns>
        public long Execute(Stream input)
        {
            var reader = new BinaryReader(input);
            var result = reader.ReadInt64();

            return result;
        }
    }
}
