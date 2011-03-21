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
    /// Represents a class that reads rows from the Field table.
    /// </summary>
    public class ReadFields : IFunction<Stream, IEnumerable<FieldRow>>
    {
        private readonly IFunction<ITuple<TableId, Stream>, ITuple<int, Stream>> _readMetadataTable;
        private readonly IFunction<ITuple<Stream, BinaryReader>, uint> _readStringIndex;
        private readonly IFunction<ITuple<int, BinaryReader>, uint> _readHeapIndexValue;
        private readonly IFunction<Stream, ITuple<int, int, int>> _readMetadataHeapIndexSizes;

        public ReadFields(IFunction<ITuple<TableId, Stream>, ITuple<int, Stream>> readMetadataTable, IFunction<ITuple<Stream, BinaryReader>, uint> readStringIndex, IFunction<ITuple<int, BinaryReader>, uint> readHeapIndexValue, IFunction<Stream, ITuple<int, int, int>> readMetadataHeapIndexSizes)
        {
            _readMetadataTable = readMetadataTable;
            _readStringIndex = readStringIndex;
            _readHeapIndexValue = readHeapIndexValue;
            _readMetadataHeapIndexSizes = readMetadataHeapIndexSizes;
        }

        /// <summary>
        /// Reads <see cref="FieldRow"/> instances from the field table.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <returns>A collection of <see cref="FieldRow"/> instances read from the given stream.</returns>
        public IEnumerable<FieldRow> Execute(Stream input)
        {
            var table = _readMetadataTable.Execute(TableId.Field, input);
            var rowCount = table.Item1;
            var tableStream = table.Item2;

            var heapSizes = _readMetadataHeapIndexSizes.Execute(input);
            var blobIndexSize = heapSizes.Item3;

            var reader = new BinaryReader(tableStream);
            for(var i = 0; i < rowCount;i++)
            {
                var flags = (FieldAttributes) reader.ReadUInt16();
                var nameIndex = _readStringIndex.Execute(input, reader);
                var signature = _readHeapIndexValue.Execute(blobIndexSize, reader);
                yield return new FieldRow() {Flags = flags, Name = nameIndex, Signature = signature};
            }
        }
    }
}
