using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
        private readonly IFunction<Stream, ITuple<int, int, int>> _readMetadataHeapIndexSizes;
        private readonly IFunction<ITuple<uint, Stream>, string> _readStringFromStringHeap;
        private readonly IFunction<ITuple<TableId, Stream>, int> _getTableRowSize;
        private readonly IFunction<ITuple<int, BinaryReader>, uint> _readHeapIndexValue;
        private readonly IFunction<ITuple<CodedTokenType, int>, ITuple<TableId, int>> _getTableReferenceFromCodedToken;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadTypeDefs"/> class.
        /// </summary>
        public ReadTypeDefs(IFunction<Stream, ITuple<int, int, int>> readMetadataHeapIndexSizes,
            IFunction<ITuple<uint, Stream>, string> readStringFromStringHeap,
            IFunction<ITuple<TableId, Stream>, int> getTableRowSize,
            IFunction<ITuple<int, BinaryReader>, uint> readHeapIndexValue,
            IFunction<ITuple<CodedTokenType, int>, ITuple<TableId, int>> getTableReferenceFromCodedToken,
            IFunction<ITuple<Func<TableId, bool>, Stream>, IDictionary<TableId, ITuple<int, Stream>>> readMetadataTables)
        {
            _getTableReferenceFromCodedToken = getTableReferenceFromCodedToken;
            _readMetadataTables = readMetadataTables;
            _readMetadataHeapIndexSizes = readMetadataHeapIndexSizes;
            _readStringFromStringHeap = readStringFromStringHeap;
            _getTableRowSize = getTableRowSize;
            _readHeapIndexValue = readHeapIndexValue;
        }

        /// <summary>
        /// Reads <see cref="TypeDefRow"/> instances from the metadata tables.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <returns>A list of <see cref="TypeDefRow"/>instances. </returns>
        public IEnumerable<TypeDefRow> Execute(Stream input)
        {
            IEnumerable<TableId> targetTableIds = new TableId[] { TableId.Field, TableId.MethodDef, TableId.TypeDef };
            Func<TableId, bool> shouldReadGivenTables = currentTableId => targetTableIds.Contains(currentTableId);

            var tables = _readMetadataTables.Execute(shouldReadGivenTables, input);
            var targetTable = tables[TableId.TypeDef];

            var rowCount = targetTable.Item1;
            var rowSize = _getTableRowSize.Execute(TableId.TypeDef, input);
            var tableStream = targetTable.Item2;

            var indexSizes = _readMetadataHeapIndexSizes.Execute(input);
            var stringHeapIndexSize = indexSizes.Item1;

            var reader = new BinaryReader(tableStream);
            for (var i = 0; i < rowCount; i++)
            {
                var offset = rowSize * i;
                tableStream.Seek(offset, SeekOrigin.Begin);

                var typeDef = Read(stringHeapIndexSize, i, input, reader, tables);

                yield return typeDef;
            }
        }

        /// <summary>
        /// Reads a <see cref="TypeDefRow"/> from the current stream.
        /// </summary>
        /// <param name="stringHeapIndexSize">The current size of the string heap index.</param>
        /// <param name="currentRow">The current row index.</param>
        /// <param name="input">The input stream.</param>
        /// <param name="reader">The stream reader.</param>
        /// <param name="tables">The collection of metadata tables.</param>
        /// <returns>A <see cref="TypeDefRow"/> that represents the current stream.</returns>
        private TypeDefRow Read(int stringHeapIndexSize, int currentRow, Stream input, BinaryReader reader, IDictionary<TableId, ITuple<int, Stream>> tables)
        {
            var typeDefRow = new TypeDefRow();
            var typeDefTable = tables[TableId.TypeDef];
            var rowCount = typeDefTable.Item1;

            typeDefRow.Flags = (TypeAttributes)reader.ReadUInt32();
            var nameIndex = _readHeapIndexValue.Execute(stringHeapIndexSize, reader);
            var namespaceIndex = _readHeapIndexValue.Execute(stringHeapIndexSize, reader);

            typeDefRow.Name = _readStringFromStringHeap.Execute(nameIndex, input);
            typeDefRow.Namespace = _readStringFromStringHeap.Execute(namespaceIndex, input);

            var tableIds = new TableId[] { TableId.TypeDef, TableId.TypeRef, TableId.TypeSpec };
            int token = ReadToken(tables, reader, tableIds);

            var extends = _getTableReferenceFromCodedToken.Execute(CodedTokenType.TypeDefOrTypeRef, token);
            if (currentRow >= rowCount)
                return typeDefRow;

            typeDefRow.Extends = extends;

            typeDefRow.FieldList = (uint)ReadToken(tables, reader, TableId.TypeDef);
            typeDefRow.MethodList = (uint)ReadToken(tables, reader, TableId.TypeDef);

            return typeDefRow;
        }

        /// <summary>
        /// Reads a token from the given stream.
        /// </summary>
        /// <param name="tables">The collection of metadata tables.</param>
        /// <param name="reader">The stream reader.</param>
        /// <param name="tableIds">The list of <see cref="TableId"/> identifiers that determine which row will be scanned to determine the size of the table index.</param>
        /// <returns>A token value from the current stream.</returns>
        private int ReadToken(IDictionary<TableId, ITuple<int, Stream>> tables, BinaryReader reader, params TableId[] tableIds)
        {
            // Determine whether or not
            // the reader should use DWORD indicies
            // based on the maximum number of rows
            // from all of the given tables
            var shouldUseDwordIndices = false;
            foreach (var tableId in tableIds)
            {
                if (!tables.ContainsKey(tableId))
                    continue;

                var currentTable = tables[tableId];
                var currentRowCount = currentTable.Item1;
                if (currentRowCount < ushort.MaxValue)
                    continue;

                shouldUseDwordIndices = true;
                break;
            }

            return shouldUseDwordIndices ? reader.ReadInt32() : reader.ReadInt16();
        }
    }
}
