using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Core
{
    /// <summary>
    /// Represents an exception that is thrown whenever a particular table schema cannot be found.
    /// </summary>
    internal class SchemaNotFoundException : Exception
    {
        private readonly TableId _tableId;

        /// <summary>
        /// Initializes a new instance of the <see cref="SchemaNotFoundException"/> class.
        /// </summary>
        /// <param name="tableId"></param>
        internal SchemaNotFoundException(TableId tableId)
        {
            _tableId = tableId;
        }

        public override string Message
        {
            get
            {
                var message = string.Format("Unable to find schema for metadata table '{0}'", _tableId);
                return message;
            }
        }
    }
}
