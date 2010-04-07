using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao.Core.Readers
{
    /// <summary>
    /// Represents a class that reads the number of table rows from a given metadata stream.
    /// </summary>
    public class ReadRowCountsFromMetadataStream : IFunction<ITuple<Stream, IDictionary<TableId, int>>>
    {
        /// <summary>
        /// Reads the number of table rows from a given metadata stream.
        /// </summary>
        /// <param name="input">The input stream and the resultset.</param>
        public void Execute(ITuple<Stream, IDictionary<TableId, int>> input)
        {
            var metadataStream = input.Item1;
            var results = input.Item2;

            metadataStream.Seek(0x18, SeekOrigin.Begin);

            using (var reader = new BinaryReader(metadataStream))
            {
                var keys = new List<TableId>(results.Keys);
                foreach (var tableId in keys)
                {
                    results[tableId] = reader.ReadInt32();
                }
            }
        }
    }
}
