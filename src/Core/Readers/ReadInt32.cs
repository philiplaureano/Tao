using System.IO;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a class that reads an <see cref="int"/> from a given stream.
    /// </summary>
    public class ReadInt32 : IFunction<Stream, int>
    {
        /// <summary>
        /// Reads an <see cref="int"/> from the given stream.
        /// </summary>
        /// <param name="input">the input stream.</param>
        /// <returns>Returns an <see cref="int"/>.</returns>
        public int Execute(Stream input)
        {
            var reader = new BinaryReader(input);
            var result = reader.ReadInt32();

            return result;
        }
    }
}
