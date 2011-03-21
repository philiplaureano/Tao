using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tao.Interfaces;
using Tao.Model;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a type that reads <see cref="TypeDefRow"/> instances from the metadata tables.
    /// </summary>
    public class ReadTypeDefs : IFunction<Stream, IEnumerable<TypeDefRow>>
    {
        private readonly IFunction<ITuple<Func<TableId, bool>, Stream>, IDictionary<TableId, ITuple<int, Stream>>> _readMetadataTables;
        private readonly IFunction<ITuple<TableId, Stream>, int> _getTableRowSize;                
        private readonly IFunction<ITuple<Stream, BinaryReader>, uint> _readStringIndex;

        private readonly IFunction<ITuple<IDictionary<TableId, ITuple<int, Stream>>, BinaryReader, TableId[], CodedTokenType>,
                    ITuple<TableId, int>> _readCodedToken;

        private readonly IFunction<ITuple<IDictionary<TableId, ITuple<int, Stream>>, BinaryReader, TableId[]>, int> _readToken;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadTypeDefs"/> class.
        /// </summary>
        public ReadTypeDefs(IFunction<ITuple<TableId, Stream>, int> getTableRowSize,
            IFunction<ITuple<Func<TableId, bool>, Stream>,
            IDictionary<TableId, ITuple<int, Stream>>> readMetadataTables, 
            IFunction<ITuple<IDictionary<TableId, ITuple<int, Stream>>, BinaryReader, TableId[], CodedTokenType>, 
            ITuple<TableId, int>> readCodedToken, IFunction<ITuple<IDictionary<TableId, ITuple<int, Stream>>, BinaryReader, TableId[]>, int> readToken, 
            IFunction<ITuple<Stream, BinaryReader>, uint> readStringIndex)
        {

            _readToken = readToken;
            _readStringIndex = readStringIndex;
            _readCodedToken = readCodedToken;

            _readMetadataTables = readMetadataTables;
            _getTableRowSize = getTableRowSize;
        }

        /// <summary>
        /// Reads <see cref="TypeDefRow"/> instances from the metadata tables.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <returns>A list of <see cref="TypeDefRow"/>instances. </returns>
        public IEnumerable<TypeDefRow> Execute(Stream input)
        {
            IEnumerable<TableId> targetTableIds = new[] { TableId.Field, TableId.MethodDef, TableId.TypeDef };
            Func<TableId, bool> shouldReadGivenTables = currentTableId => targetTableIds.Contains(currentTableId);

            var tables = _readMetadataTables.Execute(shouldReadGivenTables, input);
            var targetTable = tables[TableId.TypeDef];

            var rowCount = targetTable.Item1;
            var rowSize = _getTableRowSize.Execute(TableId.TypeDef, input);
            var tableStream = targetTable.Item2;

            var reader = new BinaryReader(tableStream);
            for (var i = 0; i < rowCount; i++)
            {
                var offset = rowSize * i;
                tableStream.Seek(offset, SeekOrigin.Begin);

                var typeDef = Read(i, input, reader, tables);

                yield return typeDef;
            }
        }

        /// <summary>
        /// Reads a <see cref="TypeDefRow"/> from the current stream.
        /// </summary>
        /// <param name="currentRow">The current row index.</param>
        /// <param name="input">The input stream.</param>
        /// <param name="reader">The stream reader.</param>
        /// <param name="tables">The collection of metadata tables.</param>
        /// <returns>A <see cref="TypeDefRow"/> that represents the current stream.</returns>
        private TypeDefRow Read(int currentRow, Stream input, BinaryReader reader, IDictionary<TableId, ITuple<int, Stream>> tables)
        {
            // Extract the TypeDef table stream from the portable executable image
            var typeDefRow = new TypeDefRow();
            var typeDefTable = tables[TableId.TypeDef];
            var rowCount = typeDefTable.Item1;

            typeDefRow.Flags = (TypeAttributes)reader.ReadUInt32();

            typeDefRow.Name = _readStringIndex.Execute(input, reader);
            typeDefRow.Namespace = _readStringIndex.Execute(input, reader);

            // Read the extends column
            var tableIds = new[] { TableId.TypeDef, TableId.TypeRef, TableId.TypeSpec };
            const CodedTokenType codedTokenType = CodedTokenType.TypeDefOrTypeRef;

            var codedToken = _readCodedToken.Execute(tables, reader, tableIds, codedTokenType);

            var extends = codedToken;
            if (currentRow >= rowCount)
                return typeDefRow;

            typeDefRow.Extends = extends;

            typeDefRow.FieldList = (uint)_readToken.Execute(tables, reader, new[] { TableId.TypeDef });
            typeDefRow.MethodList = (uint)_readToken.Execute(tables, reader, new[] { TableId.MethodDef });

            return typeDefRow;
        }        
    }
}
