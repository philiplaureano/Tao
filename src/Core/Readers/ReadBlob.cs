using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a class that reads a blob using the given offset into the #Blob heap.
    /// </summary>
    public class ReadBlob : IFunction<ITuple<uint, Stream>, Stream>, IFunction<ITuple<uint, uint, Stream>, Stream>, IFunction<ITuple<uint, Stream>, byte[]>
    {
        private readonly IFunction<ITuple<string, Stream>, Stream> _readMetadataStreamByName;
        private readonly IFunction<ITuple<int, Stream>, Stream> _inMemorySubStreamReader;
        private readonly IFunction<Stream, uint> _readCompressedInteger;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public ReadBlob(IFunction<ITuple<string, Stream>, Stream> readMetadataStreamByName, IFunction<Stream, uint> readCompressedInteger, IFunction<ITuple<int, Stream>, Stream> inMemorySubStreamReader)
        {
            _readMetadataStreamByName = readMetadataStreamByName;
            _inMemorySubStreamReader = inMemorySubStreamReader;
            _readCompressedInteger = readCompressedInteger;
        }

        /// <summary>
        /// Reads a blob using the given offset into the #Blob heap.
        /// </summary>
        /// <param name="input">The target offset and input stream.</param>
        /// <returns>The stream containing the target blob.</returns>
        public Stream Execute(ITuple<uint, Stream> input)
        {
            var offset = input.Item1;
            var stream = input.Item2;

            var blobHeap = _readMetadataStreamByName.Execute("#Blob", stream);
            blobHeap.Seek(offset, SeekOrigin.Begin);

            var blobSize = Convert.ToInt32(_readCompressedInteger.Execute(blobHeap));
            return _inMemorySubStreamReader.Execute(blobSize, blobHeap);
        }

        /// <summary>
        /// Reads a blob using the given offset into the #Blob heap.
        /// </summary>
        /// <param name="input">The target offset and input stream.</param>
        /// <returns>The stream containing the target blob.</returns>
        byte[] IFunction<ITuple<uint, Stream>, byte[]>.Execute(ITuple<uint, Stream> input)
        {
            var index = input.Item1;
            if (index == 0)
                return null;

            var blobStream = this.Execute(input);

            var length = Convert.ToInt32(blobStream.Length);
            var reader = new BinaryReader(blobStream);
            return reader.ReadBytes(length);
        }

        /// <summary>
        /// Reads a blob using the given offset into the #Blob heap.
        /// </summary>
        /// <param name="input">The target offset, blob size, and the input stream.</param>
        /// <returns>The stream containing the target blob.</returns>
        public Stream Execute(ITuple<uint, uint, Stream> input)
        {
            var offset = input.Item1;
            var blobSize = Convert.ToInt32(input.Item2);
            var stream = input.Item3;
            var blobHeap = _readMetadataStreamByName.Execute("#Blob", stream);

            blobHeap.Seek(offset, SeekOrigin.Begin);
            return _inMemorySubStreamReader.Execute(blobSize, blobHeap);
        }
    }
}
