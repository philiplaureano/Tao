using System;
using System.IO;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a class that reads a <see cref="Guid"/> from the #GUID heap.
    /// </summary>
    public class ReadGuidFromGuidHeap : IFunction<ITuple<uint, Stream>, Guid>
    {
        private readonly IFunction<ITuple<string, Stream>, Stream> _readMetadataStreamByName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadGuidFromGuidHeap"/> class.
        /// </summary>
        public ReadGuidFromGuidHeap(IFunction<ITuple<string, Stream>, Stream> readMetadataStreamByName)
        {
            _readMetadataStreamByName = readMetadataStreamByName;
        }

        /// <summary>
        /// Reads a <see cref="Guid"/> from the #GUID heap.
        /// </summary>
        /// <param name="input">The target offset and the input stream.</param>
        /// <returns>The target guid.</returns>
        public Guid Execute(ITuple<uint, Stream> input)
        {
            var index = input.Item1;            
            if (index == 0)
                return Guid.Empty;

            var stream = input.Item2;
            var guidHeap = _readMetadataStreamByName.Execute("#GUID", stream);

            var offset = index == 1 ? 0 : index*16;
            guidHeap.Seek(offset, SeekOrigin.Begin);

            var reader = new BinaryReader(guidHeap);
            var guidBytes = reader.ReadBytes(16);

            return new Guid(guidBytes);
        }
    }
}
