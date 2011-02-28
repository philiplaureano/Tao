using System.IO;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a class that reads an <see cref="uint"/> from a given stream.
    /// </summary>
    public class ReadUInt32 : IFunction<Stream, uint>
    {
        /// <summary>
        /// Reads an <see cref="uint"/> from the given stream.
        /// </summary>
        /// <param name="input">the input stream.</param>
        /// <returns>Returns an <see cref="uint"/>.</returns>
        public uint Execute(Stream input)
        {
            var reader = new BinaryReader(input);
            var result = reader.ReadUInt32();

            return result;
        }
    }
}
