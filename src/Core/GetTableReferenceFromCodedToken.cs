using Tao.Interfaces;

namespace Tao
{
    /// <summary>
    /// Represents a class that decodes a coded token into a corresponding <see cref="TableId"/> and table row number.
    /// </summary>
    public class GetTableReferenceFromCodedToken : IFunction<ITuple<CodedTokenType, int>, ITuple<TableId, int>>
    {
        private readonly IFunction<ITuple<CodedTokenType, int>, ITuple<byte, int>> _decodeToken;
        private readonly IFunction<ITuple<CodedTokenType, byte>, TableId?> _getTableId;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetTableReferenceFromCodedToken"/> class.
        /// </summary>
        public GetTableReferenceFromCodedToken(IFunction<ITuple<CodedTokenType, int>, ITuple<byte, int>> decodeToken, IFunction<ITuple<CodedTokenType, byte>, TableId?> getTableId)
        {
            _decodeToken = decodeToken;
            _getTableId = getTableId;
        }

        /// <summary>
        /// Decodes a coded token into a corresponding <see cref="TableId"/> and table row number.
        /// </summary>
        /// <param name="input">The coded token type and the coded token.</param>
        /// <returns>The <see cref="TableId"/> and table row number.</returns>
        public ITuple<TableId, int> Execute(ITuple<CodedTokenType, int> input)
        {
            var tokenType = input.Item1;
            var codedIndex = input.Item2;
            var decodedToken = _decodeToken.Execute(tokenType, codedIndex);

            var tag = decodedToken.Item1;
            var row = decodedToken.Item2;

            var tableId = _getTableId.Execute(tokenType, tag);

            return Tuple.New(tableId.Value, row);
        }
    }
}
