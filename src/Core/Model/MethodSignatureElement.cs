using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Model
{
    /// <summary>
    /// Represents a Param signature type.
    /// </summary>
    public abstract class MethodSignatureElement : IMethodSignatureElement
    {
        private readonly List<CustomMod> _customMods = new List<CustomMod>();

        /// <summary>
        /// Gets or sets the value indicating whether nor not the current type signature is ByRef.
        /// </summary>
        /// <value>Determines whether or not the type is ByRef.</value>
        public abstract bool IsByRef { get; }

        /// <summary>
        /// Gets the value indicating the list of <see cref="CustomMod"/> instances
        /// associated with the current parameter.
        /// </summary>
        /// <value>The list of <see cref="CustomMod"/> instances associated with the current param.</value>
        public IList<CustomMod> CustomMods
        {
            get { return _customMods; }
        }
    }
}
