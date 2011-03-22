using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tao.Interfaces;
using Tao.Model;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a class that reads <see cref="MethodRow"/> instances from a given stream.
    /// </summary>
    public class ReadMethodRows : IFunction<Stream, IEnumerable<MethodRow>>
    {
        private readonly IFunction<Stream, ITuple<int, int, int>> _readMetadataHeapIndexSizes;
        private readonly IFunction<ITuple<int, BinaryReader>, uint> _readHeapIndexValue;
        private readonly IFunction<ITuple<Func<TableId, bool>, Stream>, IDictionary<TableId, ITuple<int, Stream>>> _readMetadataTables;
        private readonly IFunction<ITuple<IDictionary<TableId, ITuple<int, Stream>>, BinaryReader, TableId[]>, int> _readToken;

        //BMK TODO: Comment this method
        public ReadMethodRows(IFunction<Stream, ITuple<int, int, int>> readMetadataHeapIndexSizes, IFunction<ITuple<int, BinaryReader>, uint> readHeapIndexValue, IFunction<ITuple<Func<TableId, bool>, Stream>, IDictionary<TableId, ITuple<int, Stream>>> readMetadataTables, IFunction<ITuple<IDictionary<TableId, ITuple<int, Stream>>, BinaryReader, TableId[]>, int> readToken)
        {
            _readMetadataHeapIndexSizes = readMetadataHeapIndexSizes;
            _readHeapIndexValue = readHeapIndexValue;
            _readMetadataTables = readMetadataTables;
            _readToken = readToken;
        }

        /// <summary>
        /// Reads <see cref="MethodRow"/> instances from the given <paramref name="input"/> stream.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public IEnumerable<MethodRow> Execute(Stream input)
        {
            var heapSizes = _readMetadataHeapIndexSizes.Execute(input);
            var stringIndexSize = heapSizes.Item1;
            var blobIndexSize = heapSizes.Item3;

            IEnumerable<TableId> targetTableIds = new[] { TableId.MethodDef, TableId.Param };
            Func<TableId, bool> shouldReadGivenTables = currentTableId => targetTableIds.Contains(currentTableId);

            var tables = _readMetadataTables.Execute(shouldReadGivenTables, input);
            var targetTable = tables[TableId.MethodDef];

            var rowCount = targetTable.Item1;
            var tableStream = targetTable.Item2;
            var reader = new BinaryReader(tableStream);

            for (var i = 0; i < rowCount; i++)
            {
                var rva = reader.ReadUInt32();
                var implFlags = (MethodImplAttributes)reader.ReadUInt16();
                var flags = (MethodAttributes)reader.ReadUInt16();
                var name = _readHeapIndexValue.Execute(stringIndexSize, reader);
                var signature = _readHeapIndexValue.Execute(blobIndexSize, reader);
                var paramList = (uint) _readToken.Execute(tables, reader, new TableId[] { TableId.Param });

                yield return new MethodRow()
                {
                    Rva = rva,
                    ImplFlags = implFlags,
                    Flags = flags,
                    Name = name,
                    Signature = signature,
                    ParamList = paramList
                };
            }
        }
    }
}
