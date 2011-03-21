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
    /// Represents a class that reads <see cref="ClassLayoutRow"/> instances from the ClassLayout table.
    /// </summary>
    public class ReadClassLayouts : IFunction<Stream, IEnumerable<ClassLayoutRow>>
    {        
        private readonly IFunction<ITuple<Func<TableId, bool>, Stream>, IDictionary<TableId, ITuple<int, Stream>>> _readMetadataTables;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadClassLayouts"/> class.
        /// </summary>
        /// <param name="readMetadataTables">The metadata table reader.</param>
        public ReadClassLayouts(IFunction<ITuple<Func<TableId, bool>, Stream>, IDictionary<TableId, ITuple<int, Stream>>> readMetadataTables)
        {            
            _readMetadataTables = readMetadataTables;
        }

        /// <summary>
        /// Reads <see cref="ClassLayoutRow"/> instances from the ClassLayout table.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <returns>The list of class layouts read from the given stream.</returns>
        public IEnumerable<ClassLayoutRow> Execute(Stream input)
        {
            IEnumerable<TableId> targetTableIds = new[] { TableId.ClassLayout };
            Func<TableId, bool> shouldReadGivenTables = currentTableId => targetTableIds.Contains(currentTableId);

            var tables = _readMetadataTables.Execute(shouldReadGivenTables, input);
            var classLayoutTable = tables[TableId.ClassLayout];

            var stream = classLayoutTable.Item2;
            var reader = new BinaryReader(stream);

            var rowCount = classLayoutTable.Item1; ;
            for(var i = 0; i < rowCount;i++)
            {
                var packingSize = (ushort)reader.ReadInt16();
                var classSize = (uint)reader.ReadInt32();
                var shouldUseDwordIndices = rowCount > ushort.MaxValue;
                var parent = shouldUseDwordIndices ? reader.ReadUInt32() : reader.ReadUInt16();

                yield return new ClassLayoutRow()
                                 {
                                     ClassSize = classSize,
                                     PackingSize = packingSize,
                                     Parent = parent
                                 };
            }
        }
    }
}
