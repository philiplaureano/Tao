using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Model
{
    /// <summary>
    /// Represents a type definition.
    /// </summary>
    public class TypeDef
    {
        /// <summary>
        /// Gets a value indicating the Name of the current type.
        /// </summary>
        /// <value>The Name of the current type.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets a value indicating the Namespace of the current type.
        /// </summary>
        /// <value>The Name of the current type.</value>
        public string Namespace { get; set; }

        /// <summary>
        /// Gets a value indicating the Flags of the current type.
        /// </summary>
        /// <value>The Flags of the current type.</value>
        public TypeAttributes? Flags { get; set; }   
    }
}
