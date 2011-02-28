using System.IO;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a type that reads a signed byte from a given stream.
    /// </summary>
    public class ReadSByte : IFunction<Stream, sbyte>
    {
        /// <summary>
        /// Reads a signed byte from the given <paramref name="input"/> stream.
        /// </summary>
        /// <param name="input">The input stream that contains the signed byte.</param>
        /// <returns>A signed byte.</returns>
        public sbyte Execute(Stream input)
        {
            var reader = new BinaryReader(input);
            return reader.ReadSByte();
        }
    }
}
