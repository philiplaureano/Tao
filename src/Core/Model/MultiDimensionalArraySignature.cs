using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tao.Model
{
    /// <summary>
    /// Represents a multidimensional array type.
    /// </summary>
    public class MultiDimensionalArraySignature : TypeSignature, ITypeSpecification
    {
        /// <summary>
        /// Gets or sets the value indicating the <see cref="ArrayShape"/> of the current array.
        /// </summary>
        /// <value>The <see cref="ArrayShape"/> of the current array.</value>
        public ArrayShape Shape { get; set; }

        ///<summary>
        /// Gets or sets the value indicating the array type.
        ///</summary>
        /// <value>The type signature of a single array element.</value>
        public TypeSignature ArrayType { get; set; }
    }
}
