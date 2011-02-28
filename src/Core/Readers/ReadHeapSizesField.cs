using System.IO;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a class that reads the heap sizes bit vector from the metadata stream.
    /// </summary>
    public class ReadHeapSizesField : IFunction<Stream, byte?>
    {
        private readonly IFunction<Stream, Stream> _readMetadataStream;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public ReadHeapSizesField(IFunction<Stream, Stream> readMetadataStream)
        {
            _readMetadataStream = readMetadataStream;
        }

        /// <summary>
        /// Reads the heap sizes bit vector from the metadata stream.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <returns>The bit vector that represents the heap sizes field.</returns>
        public byte? Execute(Stream input)
        {
            var metadataStream = _readMetadataStream.Execute(input);
            metadataStream.Seek(6, SeekOrigin.Begin);

            using (var reader = new BinaryReader(metadataStream))
            {
                return reader.ReadByte();
            }
        }
    }
}
