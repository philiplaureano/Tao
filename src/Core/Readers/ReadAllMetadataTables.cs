using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a class that reads all the metadata tables from the given stream.
    /// </summary>
    public class ReadAllMetadataTables : IFunction<Stream, IDictionary<TableId, ITuple<int, Stream>>>
    {
        private readonly IFunction<ITuple<Func<TableId, bool>, Stream>, IDictionary<TableId, ITuple<int, Stream>>> _readMetadataTables;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadAllMetadataTables"/> class.
        /// </summary>
        /// <param name="readMetadataTables">The metadata table reader.</param>
        public ReadAllMetadataTables(IFunction<ITuple<Func<TableId, bool>, Stream>, IDictionary<TableId, ITuple<int, Stream>>> readMetadataTables)
        {
            _readMetadataTables = readMetadataTables;
        }

        /// <summary>
        /// Reads all the metadata tables from the given stream.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <returns>A dictionary containing the <see cref="TableId"/> row count, and stream for each metadata table.</returns>
        public IDictionary<TableId, ITuple<int, Stream>> Execute(Stream input)
        {
            Func<TableId, bool> shouldReadTargetStream = currentTableId => true;

            return _readMetadataTables.Execute(shouldReadTargetStream, input);
        }
    }
}
