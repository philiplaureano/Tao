using System;

namespace Tao.Model
{
    /// <summary>
    /// Represents a module definition.
    /// </summary>
    public class ModuleDefRow
    {
        /// <summary>
        /// Gets a value indicating the string heap index that points to the name of the current type.
        /// </summary>
        /// <value>The Name of the current type.</value>
        public uint Name { get; set; }

        /// <summary>
        /// Gets a value indicating the Mvid of the current type.
        /// </summary>
        /// <value>The module Mvid value.</value>
        public uint Mvid { get; set; }  
    }
}
