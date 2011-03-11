using System.IO;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a class that reads a metadata stream using its given stream name.
    /// </summary>
    public class ReadMetadataStreamByName : IFunction<ITuple<string, Stream>, Stream>
    {
        private readonly IFunction<ITuple<int, Stream>, Stream> _readMetadataStreamByIndex;
        private readonly IFunction<ITuple<string, Stream>, int> _getMetadataStreamIndexFromName;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public ReadMetadataStreamByName(IFunction<ITuple<int, Stream>, Stream> readMetadataStreamByIndex, IFunction<ITuple<string, Stream>, int> getMetadataStreamIndexFromName)
        {
            _readMetadataStreamByIndex = readMetadataStreamByIndex;
            _getMetadataStreamIndexFromName = getMetadataStreamIndexFromName;
        }

        /// <summary>
        /// Reads a metadata stream using its given stream name.
        /// </summary>
        /// <param name="input">The tuple that contains the name of the metadata stream and the input stream.</param>
        /// <returns>The metadata stream that matches the given stream name.</returns>
        public Stream Execute(ITuple<string, Stream> input)
        {
            var targetHeaderName = input.Item1;
            var inputStream = input.Item2;
            var targetIndex = _getMetadataStreamIndexFromName.Execute(targetHeaderName, inputStream);

            return _readMetadataStreamByIndex.Execute(targetIndex, inputStream);
        }
    }
}
