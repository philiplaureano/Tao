using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tao.Model
{
    ///<summary>
    /// Represents a strongly-typed local variable
    ///</summary>
    public class TypedLocalVariable : LocalVariable
    {
        private readonly List<CustomMod> _customMods = new List<CustomMod>();

        /// <summary>
        /// Gets the value indicating the list of custom mods associated with the current local variable.
        /// </summary>
        /// <value>THe list of custom mods associated with the current local variable.</value>
        public List<CustomMod> CustomMods
        {
            get { return _customMods; }
        }

        /// <summary>
        /// Gets or sets the value indicating index of the current local variable.
        /// </summary>
        /// <value>The local variable index.</value>
        public uint Index { get; set; }

        /// <summary>
        /// Gets or sets the value indicating whether or not the local variable is pinned.
        /// </summary>
        /// <value>A boolean value that indicates whether or not the local variable can be moved around by the CLR's garbage collector.</value>
        public bool IsPinned { get; set; }

        /// <summary>
        /// Gets or sets the value indicating whether or not the variable is ByRef.
        /// </summary>
        /// <value>A boolean that indicates whether or not the variable is byref.</value>
        public bool IsByRef { get; set; }

        /// <summary>
        /// Gets or sets the value indicating the <see cref="ITypeSignature"/> that describes the variable type.
        /// </summary>
        /// <value>The value indicating the <see cref="ITypeSignature"/> that describes the variable type.</value>
        public ITypeSignature Type { get; set; }
    }
}
