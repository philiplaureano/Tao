using System;
using System.Collections.Generic;
using System.Text;
using Tao.Interfaces;

namespace Tao.Core.Schemas
{
    /// <summary>
    /// Represents the row schema for the Assembly table.
    /// </summary>
    public class AssemblyRowSchema : BaseRowSchema
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyRowSchema"/> class.
        /// </summary>
        public AssemblyRowSchema() : base(4, 2, 2, 1, 0)
        {
        }
    }
}
