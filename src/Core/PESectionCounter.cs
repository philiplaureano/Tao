using System.IO;
using Tao.Interfaces;

namespace Tao
{
    /// <summary>
    /// Represents a type that can determine the number of PE sections in a portable executable file stream.
    /// </summary>
    public class PESectionCounter : IFunction<Stream, int>
    {
        /// <summary>
        /// Determines the number of PE sections in a portable executable file stream.
        /// </summary>
        /// <param name="input">The target stream.</param>
        /// <returns>The number of sections in the given file stream.</returns>
        public int Execute(Stream input)
        {
            input.Seek(0x86, SeekOrigin.Begin);

            var result = 0;
            using(var reader = new BinaryReader(input))
            {
                result = reader.ReadInt16();
            }

            return result;
        }
    }
}
