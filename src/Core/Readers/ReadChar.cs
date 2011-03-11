using System.IO;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a class that reads an <see cref="char"/> from a given stream.
    /// </summary>
    public class ReadChar : IFunction<Stream, char>
    {
        /// <summary>
        /// Reads an <see cref="char"/> from the given stream.
        /// </summary>
        /// <param name="input">the input stream.</param>
        /// <returns>Returns an <see cref="char"/>.</returns>
        public char Execute(Stream input)
        {
            var reader = new BinaryReader(input);
            var result = reader.ReadChar();

            return result;
        }
    }
}
