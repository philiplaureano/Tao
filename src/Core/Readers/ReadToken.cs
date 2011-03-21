using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a class that reads a simple metadata token.
    /// </summary>
    public class ReadToken : IFunction<ITuple<IDictionary<TableId, ITuple<int, Stream>>, BinaryReader, TableId[]>, int>
    {
        /// <summary>
        /// Reads a simple metadata token.
        /// </summary>
        /// <param name="input">The collection of metadata tables, the stream reader pointing to the table stream, and the list of <see cref="TableId"/> identifiers that determine which row will be scanned to determine the size of the table index.</param>
        /// <returns>A simple metadata token.</returns>
        public int Execute(ITuple<IDictionary<TableId, ITuple<int, Stream>>, BinaryReader, TableId[]> input)
        {
            var tables = input.Item1;
            var reader = input.Item2;
            var tableIds = input.Item3;

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
                if (currentRowCount < 64000)
                    continue;

                shouldUseDwordIndices = true;
                break;
            }

            return shouldUseDwordIndices ? reader.ReadInt32() : reader.ReadInt16();
        }
    }
}
