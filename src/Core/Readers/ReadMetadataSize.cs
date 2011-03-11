using System.IO;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a class that reads the size of the metadata header.
    /// </summary>
    public class ReadMetadataSize : IFunction<Stream, int>
    {
        private readonly IFunction<ITuple<int, Stream>, Stream> _readStreamFromDataDirectoryIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadMetadataSize"/> class.
        /// </summary>
        public ReadMetadataSize(IFunction<ITuple<int, Stream>, Stream> readStreamFromDataDirectoryIndex)
        {
            _readStreamFromDataDirectoryIndex = readStreamFromDataDirectoryIndex;
        }

        /// <summary>
        /// Reads the size of the metadata header.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <returns>The size of the metadata root header.</returns>
        public int Execute(Stream input)
        {
            var cliHeaderStream = _readStreamFromDataDirectoryIndex.Execute(0xE, input);
            cliHeaderStream.Seek(12, SeekOrigin.Begin);

            var reader = new BinaryReader(cliHeaderStream);
            var size = reader.ReadInt32();

            return size;
        }
    }
}