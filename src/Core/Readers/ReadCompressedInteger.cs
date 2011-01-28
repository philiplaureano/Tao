using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// TODO: Add support for reading compressed signed integers
    /// <summary>
    /// Represents a type that returns the blob size for the current blob in the input stream.
    /// </summary>
    public class ReadCompressedInteger : IFunction<Stream, uint> 
    {
        private readonly IFunction<ITuple<Stream, byte>, uint> _getBlobSizeFromWord;
        private readonly IFunction<ITuple<Stream, byte>, uint> _getBlobSizeFromDoubleWord;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public ReadCompressedInteger(IFunction<ITuple<Stream, byte>, uint> getBlobSizeFromWord, IFunction<ITuple<Stream, byte>, uint> getBlobSizeFromDoubleWord)
        {
            _getBlobSizeFromWord = getBlobSizeFromWord;
            _getBlobSizeFromDoubleWord = getBlobSizeFromDoubleWord;
        }

        /// <summary>
        /// Returns the blob size for the current blob in the input stream.
        /// </summary>
        /// <param name="input">The input stream containing the target blob.</param>
        /// <returns>The size of the target blob.</returns>
        public uint Execute(Stream input)
        {
            var reader = new BinaryReader(input);
            var leadingByte = reader.ReadByte();

            const byte signatureShift = 6;
            var signatureType = leadingByte >> signatureShift;

            // Determine the size of the current blob
            uint size = 0;
            if (signatureType == 0 || signatureType == 1)
            {
                var strippedValue = leadingByte << 2;
                size = (uint)strippedValue >> 2;

                return size;
            }

            if (signatureType == 0x10b)
            {
                // Strip the first two bits off the leading byte
                // to get the blob size
                size = _getBlobSizeFromWord.Execute(input, leadingByte);
            }

            if (signatureType == 0x11b)
                size = _getBlobSizeFromDoubleWord.Execute(input, leadingByte);

            return size;
        }
    }
}
