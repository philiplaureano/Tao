using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a class that reads a string from the string heap, regardless of the string heap index size.
    /// </summary>
    public class ReadString : IFunction<ITuple<BinaryReader, Stream>, string>
    {
        private readonly IFunction<ITuple<uint, Stream>, string> _readStringFromStringHeap;
        private readonly IFunction<ITuple<Stream, BinaryReader>, uint> _readStringIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadString"/> class.
        /// </summary>
        /// <param name="readStringFromStringHeap">The string heap reader.</param>
        /// <param name="readStringIndex">The string index reader.</param>
        public ReadString(IFunction<ITuple<uint, Stream>, string> readStringFromStringHeap, IFunction<ITuple<Stream, BinaryReader>, uint> readStringIndex)
        {
            _readStringFromStringHeap = readStringFromStringHeap;
            _readStringIndex = readStringIndex;
        }

        /// <summary>
        /// Reads a string from the string heap, regardless of the string heap index size.
        /// </summary>
        /// <param name="input">The <see cref="BinaryReader"/> that contains the table stream and the stream that represents the portable executable stream that contains the string heap index size data.</param>
        /// <returns>A string read from the given table stream.</returns>
        public string Execute(ITuple<BinaryReader, Stream> input)
        {
            var reader = input.Item1;
            var stream = input.Item2;
            var index = _readStringIndex.Execute(stream, reader);

            return _readStringFromStringHeap.Execute(index, stream);
        }        
    }
}
