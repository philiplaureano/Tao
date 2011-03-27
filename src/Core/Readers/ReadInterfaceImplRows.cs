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
    /// Represents a class that reads <see cref="InterfaceImplRow"/> instances from a given stream.
    /// </summary>
    public class ReadInterfaceImplRows : IFunction<Stream, IEnumerable<InterfaceImplRow>>
    {
        private IFunction<Stream, IDictionary<TableId, int>> _readMetadataTableRowCounts;
        private readonly IFunction<ITuple<IDictionary<TableId, ITuple<int, Stream>>, BinaryReader, TableId[], CodedTokenType>,
                    ITuple<TableId, int>> _readCodedToken;
        private readonly IFunction<ITuple<Func<TableId, bool>, Stream>, IDictionary<TableId, ITuple<int, Stream>>>
            _readMetadataTables;
        
        public ReadInterfaceImplRows(IFunction<Stream, IDictionary<TableId, int>> readMetadataTableRowCounts, IFunction<ITuple<IDictionary<TableId, ITuple<int, Stream>>, BinaryReader, TableId[], CodedTokenType>, ITuple<TableId, int>> readCodedToken, IFunction<ITuple<Func<TableId, bool>, Stream>, IDictionary<TableId, ITuple<int, Stream>>> readMetadataTables)
        {
            _readMetadataTableRowCounts = readMetadataTableRowCounts;
            _readCodedToken = readCodedToken;
            _readMetadataTables = readMetadataTables;
        }

        /// <summary>
        /// Reads <see cref="InterfaceImplRow"/> instances from a given stream.
        /// </summary>
        /// <param name="input">The input stream that contains the target rows.</param>
        /// <returns>A collection <see cref="InterfaceImplRow"/> instances read from the input stream.</returns>
        public IEnumerable<InterfaceImplRow> Execute(Stream input)
        {
            var rowCounts = _readMetadataTableRowCounts.Execute(input);

            var targetTableIds = new[] { TableId.TypeDef, TableId.TypeRef, TableId.TypeSpec, TableId.InterfaceImpl };

            Func<TableId, bool> shouldReadGivenTables = currentTableId => targetTableIds.Contains(currentTableId);
            var tables = _readMetadataTables.Execute(shouldReadGivenTables, input);
            var typeDefRowCount = rowCounts[TableId.TypeDef];

            var rowCount = rowCounts[TableId.InterfaceImpl];

            var table = tables[TableId.InterfaceImpl];
            var tableStream = table.Item2;
            var reader = new BinaryReader(tableStream);
            for (var i = 0; i < rowCount; i++)
            {
                uint parentClass = 0;

                if (typeDefRowCount <= 64000)
                    parentClass = reader.ReadUInt16();
                else
                    parentClass = reader.ReadUInt32();

                var interfaceToken = _readCodedToken.Execute(tables, reader, new[] { TableId.TypeDef, TableId.TypeRef, TableId.TypeSpec },
                                                             CodedTokenType.TypeDefOrTypeRef);

                yield return new InterfaceImplRow()
                                 {
                                     Class = Tuple.New(TableId.TypeDef, parentClass),
                                     Interface = Tuple.New(interfaceToken.Item1, (uint) interfaceToken.Item2)
                                 };
            }
        }
    }
}
