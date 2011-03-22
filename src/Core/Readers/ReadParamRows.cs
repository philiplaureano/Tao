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
    /// Represents a class that reads <see cref="ParamRow"/> instances from a given stream.
    /// </summary>
    public class ReadParamRows : IFunction<Stream, IEnumerable<ParamRow>>
    {
        private readonly IFunction<Stream, ITuple<int, int, int>> _readMetadataHeapIndexSizes;
        private readonly IFunction<ITuple<int, BinaryReader>, uint> _readHeapIndexValue;
        private readonly IFunction<ITuple<TableId, Stream>, ITuple<int, Stream>> _readMetadataTable;

        public ReadParamRows(IFunction<Stream, ITuple<int, int, int>> readMetadataHeapIndexSizes, IFunction<ITuple<int, BinaryReader>, uint> readHeapIndexValue, IFunction<ITuple<TableId, Stream>, ITuple<int, Stream>> readMetadataTable)
        {
            _readMetadataHeapIndexSizes = readMetadataHeapIndexSizes;
            _readHeapIndexValue = readHeapIndexValue;
            _readMetadataTable = readMetadataTable;
        }

        /// <summary>
        /// Reads <see cref="ParamRow"/> instances from the given <paramref name="input"/>.
        /// </summary>
        /// <param name="input"The input stream.></param>
        /// <returns><A collection of <see cref="ParamRow"/> instances read from the current stream. </returns>
        public IEnumerable<ParamRow> Execute(Stream input)
        {
            var heapSizes = _readMetadataHeapIndexSizes.Execute(input);
            var paramTable = _readMetadataTable.Execute(TableId.Param, input);
            var rowCount = paramTable.Item1;

            var tableStream = paramTable.Item2;
            var reader = new BinaryReader(tableStream);
            var stringHeapIndexSize = heapSizes.Item1;
            for (var i = 0; i < rowCount; i++)
            {
                var flags = (ParamAttributes) reader.ReadUInt16();
                var sequence = reader.ReadUInt16();
                var name = (uint) _readHeapIndexValue.Execute(stringHeapIndexSize, reader);

                yield return new ParamRow()
                                 {
                                     Flags = flags,
                                     Sequence = sequence,
                                     Name = name
                                 };
            }
        }
    }
}
