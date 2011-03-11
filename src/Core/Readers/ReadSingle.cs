using System.IO;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a class that reads an <see cref="float"/> from a given stream.
    /// </summary>
    public class ReadSingle : IFunction<Stream, float>
    {
        /// <summary>
        /// Reads a <see cref="float"/> from the given stream.
        /// </summary>
        /// <param name="input">the input stream.</param>
        /// <returns>Returns an <see cref="float"/>.</returns>
        public float Execute(Stream input)
        {
            var reader = new BinaryReader(input);
            var result = reader.ReadSingle();

            return result;
        }
    }
}
