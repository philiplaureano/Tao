using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Model
{
    /// <summary>
    /// Represents a type signature that has been encoded with a given tableId and table index.
    /// </summary>
    public class TypeDefOrRefEncodedSignature : TypeSignature
    {
        ///<summary>
        /// Gets the value indicating the <see cref="TableId"/> of the given type signature.
        ///</summary>
        /// <value>The tableId that represents the target metadata table that contains the given type.</value>
        public TableId TableId
        {
            get; set;
        }

        ///<summary>
        /// /// Gets the value indicating the metadata row of the given type signature.
        ///</summary>
        /// <value>The metadata row of the given type signature.</value>
        public uint TableIndex
        {
            get; set;
        }
    }
}
