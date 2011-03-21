using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Hiro.Containers;
using Tao.Containers;
using Tao.Interfaces;
using Tao.Model;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a type that reads <see cref="TypeRefRow"/> instances from a given .NET portable executable stream.
    /// </summary>
    public class ReadTypeRefs : IFunction<Stream, IEnumerable<TypeRefRow>>
    {
        private readonly IFunction<ITuple<Func<TableId, bool>, Stream>, IDictionary<TableId, ITuple<int, Stream>>>
            _readMetadataTables;
        private readonly IFunction<ITuple<IDictionary<TableId, ITuple<int, Stream>>, BinaryReader, TableId[], CodedTokenType>,
                    ITuple<TableId, int>> _readCodedToken;        

        private readonly IFunction<ITuple<int, BinaryReader>, uint> _readHeapIndexValue;
        private readonly IFunction<Stream, ITuple<int, int, int>> _readMetadataHeapIndexSizes;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadTypeRefs"/> class.
        /// </summary>
        /// <param name="readMetadataTables">The metadata table stream reader.</param>
        /// <param name="readCodedToken">The coded token reader.</param>
        /// <param name="readHeapIndexValue">The heap index value reader.</param>
        /// <param name="readMetadataHeapIndexSizes">The heap index size reader.</param>
        public ReadTypeRefs(IFunction<ITuple<Func<TableId, bool>, Stream>, IDictionary<TableId, ITuple<int, Stream>>> readMetadataTables,
            IFunction<ITuple<IDictionary<TableId, ITuple<int, Stream>>, BinaryReader, TableId[], CodedTokenType>,
            ITuple<TableId, int>> readCodedToken, IFunction<ITuple<int, BinaryReader>, uint> readHeapIndexValue, 
            IFunction<Stream, ITuple<int, int, int>> readMetadataHeapIndexSizes)
        {
            _readMetadataTables = readMetadataTables;
            _readMetadataHeapIndexSizes = readMetadataHeapIndexSizes;
            _readHeapIndexValue = readHeapIndexValue;
            _readCodedToken = readCodedToken;
        }

        /// <summary>
        /// Reads <see cref="TypeRefRow"/> instances from a given .NET portable executable stream.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <returns>The set of <see cref="TypeRefRow"/> instances read from the given stream.</returns>
        public IEnumerable<TypeRefRow> Execute(Stream input)
        {
            var targetTableIds = new[] { TableId.Module, TableId.ModuleRef, TableId.AssemblyRef, TableId.TypeRef };

            Func<TableId, bool> shouldReadGivenTables = currentTableId => targetTableIds.Contains(currentTableId);
            var tables = _readMetadataTables.Execute(shouldReadGivenTables, input);

            var typeRefTable = tables[TableId.TypeRef];
            var rowCount = typeRefTable.Item1;
            var tableStream = typeRefTable.Item2;
            var reader = new BinaryReader(tableStream);

            var indexSizes = _readMetadataHeapIndexSizes.Execute(input);
            var stringHeapIndexSize = indexSizes.Item1;

            for (var i = 0; i < rowCount; i++)
            {
                var codedToken = _readCodedToken.Execute(tables, reader, targetTableIds, CodedTokenType.ResolutionScope);
                var resolutionScope = new TableIndex() { TableId = codedToken.Item1, Row = (uint)codedToken.Item2 };
                var name = _readHeapIndexValue.Execute(stringHeapIndexSize, reader);
                var nameSpace = _readHeapIndexValue.Execute(stringHeapIndexSize, reader);

                yield return new TypeRefRow() { Name = name, Namespace = nameSpace, ResolutionScope = resolutionScope };
            }
        }
    }
}
