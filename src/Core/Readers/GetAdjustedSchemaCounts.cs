using System.Collections.Generic;
using System.IO;
using Tao.Interfaces;

namespace Tao
{
    /// <summary>
    /// Represents a class that adjusts the given schema according to the <see cref="TableId"/> and the metadata in the input stream.
    /// </summary>
    public class GetAdjustedSchemaCounts : IFunction<ITuple<TableId, ITuple<int, int, int, int, int, int, IEnumerable<ITuple<IEnumerable<TableId>, int>>>, Stream>, ITuple<int, int, int, int, int, int>>
    {
        private readonly IFunction<ITuple<ITuple<int, int, int, int, int, int, IEnumerable<ITuple<IEnumerable<TableId>, int>>>, Stream>,
                    ITuple<int, int>> _getAdditionalColumnCounts;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public GetAdjustedSchemaCounts(IFunction<ITuple<ITuple<int, int, int, int, int, int, IEnumerable<ITuple<IEnumerable<TableId>, int>>>, Stream>, ITuple<int, int>> getAdditionalColumnCounts)
        {
            _getAdditionalColumnCounts = getAdditionalColumnCounts;
        }

        /// <summary>
        /// Adjusts the given schema according to the <see cref="TableId"/> and the metadata in the input stream.
        /// </summary>
        /// <param name="input">The tuple containing the <see cref="TableId"/>, the input schema, and the target stream.</param>
        /// <returns>The adjusted column size count schema.</returns>
        public ITuple<int, int, int, int, int, int> Execute(ITuple<TableId, ITuple<int, int, int, int, int, int, IEnumerable<ITuple<IEnumerable<TableId>, int>>>, Stream> input)
        {
            var tableId = input.Item1;
            var schema = input.Item2;
            var stream = input.Item3;
            if (schema == null)
                throw new SchemaNotFoundException(tableId);

            var additionalCounts = _getAdditionalColumnCounts.Execute(schema, stream);            
            var additionalWordCount = additionalCounts.Item1;
            var additionalDwordColumns = additionalCounts.Item2;

            var stringHeapColumns = schema.Item4;
            var blobHeapColumns = schema.Item5;
            var guidHeapColumns = schema.Item6;

            return Tuple.New(schema.Item1, schema.Item2 + additionalWordCount, schema.Item3 + additionalDwordColumns, stringHeapColumns, blobHeapColumns,
                             guidHeapColumns);
        }
    }
}
