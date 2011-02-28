using System.IO;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a type that reads the file alignment from a coff header stream.
    /// </summary>
    public class ReadSectionAlignment : IFunction<Stream, int>
    {
        private readonly IFunction<Stream> _coffHeaderSeeker;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadFileAlignment"/> class.
        /// </summary>
        public ReadSectionAlignment(IFunction<Stream> coffHeaderSeeker)
        {
            _coffHeaderSeeker = coffHeaderSeeker;
        }

        /// <summary>
        /// Reads the file alignment from a coff header stream.
        /// </summary>
        /// <param name="input">the input stream.</param>
        /// <returns>The file alignment.</returns>
        public int Execute(Stream input)
        {
            _coffHeaderSeeker.Execute(input);
            input.Seek(0x20, SeekOrigin.Current);

            var reader = new BinaryReader(input);
            return reader.ReadInt32();
        }
    }
}