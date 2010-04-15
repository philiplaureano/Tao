using System;
using System.Collections.Generic;
using System.Text;
using Tao.Interfaces;

namespace Tao
{
    /// <summary>
    /// Represents a class that decodes coded tokens into a table tag and a row number.
    /// </summary>
    public class DecodeToken : IFunction<ITuple<CodedTokenType, int>, ITuple<byte, int>>, IFunction<ITuple<CodedTokenType, short>, ITuple<byte, int>>
    {
        private readonly IFunction<CodedTokenType, uint> _getNumberOfBitsToEncodeTag;

        /// <summary>
        /// Initializes a new instance of the <see cref="DecodeToken"/> class.
        /// </summary>
        public DecodeToken(IFunction<CodedTokenType, uint> getNumberOfBitsToEncodeTag)
        {
            _getNumberOfBitsToEncodeTag = getNumberOfBitsToEncodeTag;
        }

        /// <summary>
        /// Decodes coded tokens into a table tag and a row number.
        /// </summary>
        /// <param name="input">The coded token.</param>
        /// <returns>A table tag and a row number.</returns>
        public ITuple<byte, int> Execute(ITuple<CodedTokenType, int> input)
        {
            var tokenType = input.Item1;

            int bitsToEncodeTag = Convert.ToInt32(_getNumberOfBitsToEncodeTag.Execute(tokenType));
            var index = input.Item2;
            var row = index >> bitsToEncodeTag;
            var tag = index - (row << bitsToEncodeTag);

            return Tuple.New(Convert.ToByte(tag), row);
        }

        /// <summary>
        /// Decodes coded tokens into a table tag and a row number.
        /// </summary>
        /// <param name="input">The coded token.</param>
        /// <returns>A table tag and a row number.</returns>
        public ITuple<byte, int> Execute(ITuple<CodedTokenType, short> input)
        {
            var tokenType = input.Item1;

            int bitsToEncodeTag = Convert.ToInt32(_getNumberOfBitsToEncodeTag.Execute(tokenType));
            var index = input.Item2;
            var row = index >> bitsToEncodeTag;
            var tag = index - (row << bitsToEncodeTag);

            return Tuple.New(Convert.ToByte(tag), row);
        }
    }
}
