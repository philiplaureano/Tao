using System.Collections.Generic;
using Tao.Interfaces;

namespace Tao.Schemas
{
    /// <summary>
    /// Represents the row schema for the EventMap table.
    /// </summary>
    public class EventMapRowSchema : BaseRowSchema
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventMapRowSchema"/> class.
        /// </summary>
        public EventMapRowSchema() : base(0, 0, 0, 0, 0, 0)
        {
        }

        /// <summary>
        /// Gets the value indicating the list of table indexes and the number of indexes that point to each table type.
        /// </summary>
        /// <value>The number of table indexes.</value>
        public override IEnumerable<ITuple<IEnumerable<TableId>, int>> Item7
        {
            get
            {
                yield return CreateTuple(1, TableId.TypeDef);
                yield return CreateTuple(1, TableId.Event);
            }
        }
    }
}