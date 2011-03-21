using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a class that reads a string index from the current position in the metadata table stream.
    /// </summary>
    public class ReadStringIndex : IFunction<ITuple<Stream, BinaryReader>, uint>
    {
        private readonly IFunction<ITuple<int, BinaryReader>, uint> _readHeapIndexValue;
        private readonly IFunction<Stream, ITuple<int, int, int>> _readMetadataHeapIndexSizes;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadStringIndex"/> class.
        /// </summary>
        /// <param name="readHeapIndexValue">The heap index value reader.</param>
        /// <param name="readMetadataHeapIndexSizes">The heap index size reader.</param>
        public ReadStringIndex(IFunction<ITuple<int, BinaryReader>, uint> readHeapIndexValue, IFunction<Stream, ITuple<int, int, int>> readMetadataHeapIndexSizes)
        {
            _readHeapIndexValue = readHeapIndexValue;
            _readMetadataHeapIndexSizes = readMetadataHeapIndexSizes;
        }

        /// <summary>
        /// Reads a string index from the current position in the metadata table stream.
        /// </summary>
        /// <param name="input">The stream containing the portable executable image and the reader that contains the metadata table stream.</param>
        /// <returns>An index pointing to the stream heap.</returns>
        public uint Execute(ITuple<Stream, BinaryReader> input)
        {
            var stream = input.Item1;
            var reader = input.Item2;

            var indexSizes = _readMetadataHeapIndexSizes.Execute(stream);
            var stringHeapIndexSize = indexSizes.Item1;
            return _readHeapIndexValue.Execute(stringHeapIndexSize, reader);
        }
    }
}
