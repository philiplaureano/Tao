using System.Collections.Generic;

namespace Tao.Model
{
    /// <summary>
    /// Represents a PropertySig signature type.
    /// </summary>
    public class PropertySignature
    {
        private readonly IList<ITypedMethodSignatureElement> _parameters = new List<ITypedMethodSignatureElement>();
        private readonly ICollection<CustomMod> _customMods = new List<CustomMod>();
        /// <summary>
        /// Gets or sets value that indicates whether or not the property has a 'this' pointer as part of its signature.
        /// </summary>
        /// <value>Determines whether or not the property has a 'this' pointer.</value>
        public bool HasThis { get; set; }

        /// <summary>
        /// Gets or sets the value that indicates the number of parameters in the current property signature.
        /// </summary>
        /// <value>The number of parameters in the current property signature.</value>
        public uint ParamCount { get; set; }

        /// <summary>
        /// Gets or sets the value indicating the <see cref="TypeSignature"/> that describes the return type of the 
        /// property getter method.
        /// </summary>
        /// <value>The return type of the property getter method.</value>
        public TypeSignature Type { get; set; }

        /// <summary>
        /// Gets the value indicating the list of parameters for the current property
        /// </summary>
        /// <value>The list of parameters for the current property.</value>
        public IList<ITypedMethodSignatureElement> Parameters
        {
            get { return _parameters; }
        }

        ///<summary>
        /// Gets the value indicating the list of custom mods assigned to the current property signature.
        ///</summary>
        /// <value>The list of custom mods assigned to the current property signature.</value>
        public ICollection<CustomMod> CustomMods
        {
            get { return _customMods; }
        }
    }
}
