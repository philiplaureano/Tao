using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a type that reads a coded token from a given table stream regardless of the table index size.
    /// </summary>
    public class ReadCodedToken : IFunction<ITuple<IDictionary<TableId, ITuple<int, Stream>>, BinaryReader, TableId[], CodedTokenType>, ITuple<TableId, int>>
    {
        private readonly IFunction<ITuple<CodedTokenType, int>, ITuple<TableId, int>> _getTableReferenceFromCodedToken;
        private readonly IFunction<ITuple<IDictionary<TableId, ITuple<int, Stream>>, BinaryReader, TableId[]>, int> _readToken;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadCodedToken"/> class.
        /// </summary>
        /// <param name="getTableReferenceFromCodedToken">The coded token reader.</param>
        /// <param name="readToken">The simple token reader.</param>
        public ReadCodedToken(IFunction<ITuple<CodedTokenType, int>, ITuple<TableId, int>> getTableReferenceFromCodedToken, 
            IFunction<ITuple<IDictionary<TableId, ITuple<int, Stream>>, BinaryReader, TableId[]>, int> readToken)
        {
            _getTableReferenceFromCodedToken = getTableReferenceFromCodedToken;
            _readToken = readToken;
        }

        /// <summary>
        /// Reads a coded token from a given table stream regardless of the table index size.
        /// </summary>
        /// <param name="input">The dictionary containing the table streams, the target binary reader containing the target table stream, the list of <see cref="TableId"/> 
        /// values that indicate the tables that will be referenced, and the <see cref="CodedTokenType"/> that describes the token type.</param>
        /// <returns>A table reference containing the <see cref="TableId"/> and target table row.</returns>
        public ITuple<TableId, int> Execute(ITuple<IDictionary<TableId, ITuple<int, Stream>>, BinaryReader, TableId[], CodedTokenType> input)
        {
            var tables = input.Item1;
            var reader = input.Item2;
            var tableIds = input.Item3;
            var codedTokenType = input.Item4;

            var token = _readToken.Execute(tables, reader, tableIds);

            return _getTableReferenceFromCodedToken.Execute(codedTokenType, token);
        }        
    }
}
