using System.IO;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a class that reads an <see cref="short"/> from a given stream.
    /// </summary>
    public class ReadInt16 : IFunction<Stream, short>
    {
        /// <summary>
        /// Reads an <see cref="short"/> from the given stream.
        /// </summary>
        /// <param name="input">the input stream.</param>
        /// <returns>Returns an <see cref="short"/>.</returns>
        public short Execute(Stream input)
        {
            var reader = new BinaryReader(input);
            var result = reader.ReadInt16();

            return result;
        }
    }
}
