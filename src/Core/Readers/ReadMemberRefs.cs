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
    /// Represents a class that reads <see cref="MemberRefRow"/> instances from a given stream.
    /// </summary>
    public class ReadMemberRefs : IFunction<Stream, IEnumerable<MemberRefRow>>
    {
        private IFunction<ITuple<IDictionary<TableId, ITuple<int, Stream>>, BinaryReader, TableId[], CodedTokenType>,
                    ITuple<TableId, int>> _readCodedToken;
        private readonly IFunction<ITuple<int, BinaryReader>, uint> _readHeapIndexValue;
        private IFunction<ITuple<TableId, Stream>, ITuple<int, Stream>> _readMetadataTable;
        private readonly IFunction<Stream, ITuple<int, int, int>> _readMetadataHeapIndexSizes;
        private readonly IFunction<ITuple<Func<TableId, bool>, Stream>, IDictionary<TableId, ITuple<int, Stream>>> _readMetadataTables;

        public ReadMemberRefs(IFunction<ITuple<IDictionary<TableId, ITuple<int, Stream>>, BinaryReader, TableId[], CodedTokenType>, ITuple<TableId, int>> readCodedToken, IFunction<ITuple<int, BinaryReader>, uint> readHeapIndexValue, IFunction<ITuple<TableId, Stream>, ITuple<int, Stream>> readMetadataTable, IFunction<Stream, ITuple<int, int, int>> readMetadataHeapIndexSizes, IFunction<ITuple<Func<TableId, bool>, Stream>, IDictionary<TableId, ITuple<int, Stream>>> readMetadataTables)
        {
            _readCodedToken = readCodedToken;
            _readHeapIndexValue = readHeapIndexValue;
            _readMetadataTable = readMetadataTable;
            _readMetadataHeapIndexSizes = readMetadataHeapIndexSizes;
            _readMetadataTables = readMetadataTables;
        }

        /// <summary>
        /// Reads <see cref="MemberRefRow"/> instances from a given stream.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <returns>The <see cref="MemberRefRow"/> instances read from the given stream. </returns>
        public IEnumerable<MemberRefRow> Execute(Stream input)
        {
            var table = _readMetadataTable.Execute(TableId.MemberRef, input);
            var rowCount = table.Item1;
            var tableStream = table.Item2;

            var indexSizes = _readMetadataHeapIndexSizes.Execute(input);
            var stringHeapIndexSize = indexSizes.Item1;
            var blobHeapIndexSize = indexSizes.Item3;

            var tableIds = new[] { TableId.TypeRef, TableId.ModuleRef, TableId.MethodDef, TableId.TypeSpec, TableId.TypeDef };
            Func<TableId, bool> shouldReadGivenTables = currentTableId => tableIds.Contains(currentTableId);
            var tables = _readMetadataTables.Execute(shouldReadGivenTables, input);

            var reader = new BinaryReader(tableStream);

            for (var i = 0; i < rowCount; i++)
            {
                var codedToken = _readCodedToken.Execute(tables, reader, tableIds, CodedTokenType.MemberRefParent);
                var name = _readHeapIndexValue.Execute(stringHeapIndexSize, reader);
                var signature = _readHeapIndexValue.Execute(blobHeapIndexSize, reader);

                yield return new MemberRefRow()
                                 {
                                     Class = Tuple.New(codedToken.Item1, (uint)codedToken.Item2),
                                     Name = name,
                                     Signature = signature
                                 };
            }
        }
    }
}
