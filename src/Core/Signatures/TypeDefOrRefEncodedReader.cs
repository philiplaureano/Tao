using System;
using System.Collections.Generic;
using Tao.Interfaces;

namespace Tao.Signatures
{
    /// <summary>
    /// Represents a type that can decode a TypeDefOrRefEncoded signature token.
    /// </summary>
    public class TypeDefOrRefEncodedReader : IFunction<byte, ITuple<TableId, uint>>
    {
        private readonly Dictionary<int, TableId> _tableMap = new Dictionary<int, TableId>();

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeDefOrRefEncodedReader"/> class.
        /// </summary>
        public TypeDefOrRefEncodedReader()
        {
            _tableMap[0] = TableId.TypeDef;
            _tableMap[1] = TableId.TypeRef;
            _tableMap[2] = TableId.TypeSpec;
        }

        /// <summary>
        /// Decodes a TypeDefOrRefEncoded signature token into its corresponding table and row index.
        /// </summary>
        /// <param name="input">The byte input.</param>
        /// <returns>The tuple containing the <see cref="TableId"/> and row index.</returns>
        public ITuple<TableId, uint> Execute(byte input)
        {
            var rowIndex = (input >> 2);

            var tableMask = (rowIndex << 2);
            var tableValue = input - tableMask;
            var tableId = _tableMap[tableValue];

            return Tuple.New(tableId, Convert.ToUInt32(rowIndex));
        }
    }
}
