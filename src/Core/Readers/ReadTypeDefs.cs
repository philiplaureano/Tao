using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;
using Tao.Model;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a type that reads <see cref="TypeDef"/> instances from the metadata tables.
    /// </summary>
    public class ReadTypeDefs : IFunction<Stream, IEnumerable<TypeDef>>
    {
        private readonly IFunction<Stream, IDictionary<TableId, ITuple<int, Stream>>> _readAllMetadataTables;
        private readonly IFunction<Stream, ITuple<int, int, int>> _readMetadataHeapIndexSizes;
        private readonly IFunction<ITuple<uint, Stream>, string> _readStringFromStringHeap;
        private readonly IFunction<ITuple<TableId, Stream>, int> _getTableRowSize;
        private readonly IFunction<ITuple<int, BinaryReader>, uint> _readHeapIndexValue;
        private readonly IFunction<ITuple<CodedTokenType, int>, ITuple<TableId, int>> _getTableReferenceFromCodedToken;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadTypeDefs"/> class.
        /// </summary>
        public ReadTypeDefs(IFunction<Stream, IDictionary<TableId, ITuple<int, Stream>>> readAllMetadataTables, 
            IFunction<Stream, ITuple<int, int, int>> readMetadataHeapIndexSizes, 
            IFunction<ITuple<uint, Stream>, string> readStringFromStringHeap, 
            IFunction<ITuple<TableId, Stream>, int> getTableRowSize, 
            IFunction<ITuple<int, BinaryReader>, uint> readHeapIndexValue)
        {
            _readAllMetadataTables = readAllMetadataTables;
            _readMetadataHeapIndexSizes = readMetadataHeapIndexSizes;
            _readStringFromStringHeap = readStringFromStringHeap;
            _getTableRowSize = getTableRowSize;
            _readHeapIndexValue = readHeapIndexValue;
        }

        /// <summary>
        /// Reads <see cref="TypeDef"/> instances from the metadata tables.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <returns>A list of <see cref="TypeDef"/>instances. </returns>
        public IEnumerable<TypeDef> Execute(Stream input)
        {
            var tables = _readAllMetadataTables.Execute(input);
            var targetTable = tables[TableId.TypeDef];

            var rowCount = targetTable.Item1;
            var rowSize = _getTableRowSize.Execute(TableId.TypeDef, input);
            var tableStream = targetTable.Item2;

            var indexSizes = _readMetadataHeapIndexSizes.Execute(input);
            var stringHeapIndexSize = indexSizes.Item1;

            var reader = new BinaryReader(tableStream);
            for (var i = 0; i < rowCount; i++)
            {
                var offset = rowSize*i;
                tableStream.Seek(offset, SeekOrigin.Begin);
                var typeDef = new TypeDef();

                Read(stringHeapIndexSize, typeDef, input, reader, tables, rowSize, i);

                yield return typeDef;
            }
        }

        private void Read(int stringHeapIndexSize, TypeDef typeDef, Stream input, BinaryReader reader, IDictionary<TableId, ITuple<int, Stream>> tables, int rowSize, int currentRow)
        {
            var typeDefTable = tables[TableId.TypeDef];
            var rowCount = typeDefTable.Item1;

            typeDef.Flags = (TypeAttributes) reader.ReadUInt32();
            var nameIndex = _readHeapIndexValue.Execute(stringHeapIndexSize, reader);
            var namespaceIndex = _readHeapIndexValue.Execute(stringHeapIndexSize, reader);

            typeDef.Name = _readStringFromStringHeap.Execute(nameIndex, input);
            typeDef.Namespace = _readStringFromStringHeap.Execute(namespaceIndex, input);

            var tableIds = new TableId[] {TableId.TypeDef, TableId.TypeRef, TableId.TypeSpec};
            int token = ReadToken(tables, reader, tableIds);

            if (token == 0)
                return;

            var extends = _getTableReferenceFromCodedToken.Execute(CodedTokenType.TypeDefOrTypeRef, token);
            if (rowCount <= 1 || currentRow >= rowCount)
                return;

        }

        private int ReadToken(IDictionary<TableId, ITuple<int, Stream>> tables, BinaryReader reader, IEnumerable<TableId> tableIds)
        {
            var shouldUseDwordIndices = false;
            foreach(var tableId in tableIds)
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
