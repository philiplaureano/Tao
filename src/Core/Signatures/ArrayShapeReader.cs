using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Tao.Interfaces;
using Tao.Model;

namespace Tao.Signatures
{
    /// <summary>
    /// Represents a type that converts byte arrays into <see cref="ArrayShape"/> objects.
    /// </summary>
    public class ArrayShapeReader : IFunction<IEnumerable<byte>, ArrayShape>
    {
        private readonly IFunction<Stream, uint> _readCompressedInteger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArrayShapeReader"/> class.
        /// </summary>
        /// <param name="readCompressedInteger">The component that will be used to read compressed integers from the given stream.</param>
        public ArrayShapeReader(IFunction<Stream, uint> readCompressedInteger)
        {
            _readCompressedInteger = readCompressedInteger;
        }

        /// <summary>
        /// Converts a given <paramref name="input">byte array</paramref> into an array shape.
        /// </summary>
        /// <param name="input">The byte array containing the array shape data.</param>
        /// <returns>An <see cref="ArrayShape"/> object.</returns>
        public ArrayShape Execute(IEnumerable<byte> input)
        {
            var bytes = input.ToArray();

            var stream = new MemoryStream(bytes);
            var rank = _readCompressedInteger.Execute(stream);
            var numSizes = _readCompressedInteger.Execute(stream);
            var sizes = new List<uint>();
            for (var i = 0; i < numSizes; i++)
            {
                var size = _readCompressedInteger.Execute(stream);
                sizes.Add(size);
            }

            var loBounds = new List<uint>();
            var numLoBounds = _readCompressedInteger.Execute(stream);
            for (var i = 0; i < numLoBounds; i++)
            {
                var loBound = _readCompressedInteger.Execute(stream);
                loBounds.Add(loBound);
            }

            var shape = new ArrayShape(rank, numSizes, sizes, loBounds);

            return shape;
        }
    }
}
