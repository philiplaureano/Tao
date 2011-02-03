using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tao.Model
{
    /// <summary>
    /// Represents an SZArray type signature element.
    /// </summary>
    public class SzArraySignature : TypeSignature
    {
        private readonly List<CustomMod> _customMods = new List<CustomMod>();

        /// <summary>
        /// Gets or sets the value indicating the <see cref="TypeSignature">array element type.</see>
        /// </summary>
        /// <value>The type signature that describes a single array element.</value>
        public TypeSignature ArrayElementType { get; set; }

        /// <summary>
        /// Gets the value indicating the list of <see cref="CustomMod"/> instances associated with the current
        /// array signature.
        /// </summary>
        /// <value>The list of <see cref="CustomMod"/> instances associated with the current array signature.</value>
        public IList<CustomMod> CustomMods 
        { 
            get { return _customMods; }
        }
    }
}
