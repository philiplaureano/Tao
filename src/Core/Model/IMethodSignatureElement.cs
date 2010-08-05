using System.Collections.Generic;

namespace Tao.Model
{
    /// <summary>
    /// Represents a method signature type that can act as the return type or the parameter type.
    /// </summary>
    public interface IMethodSignatureElement
    {
        /// <summary>
        /// Gets or sets the value indicating whether nor not the current type signature is ByRef.
        /// </summary>
        /// <value>Determines whether or not the type is ByRef.</value>
        bool IsByRef { get; }

        /// <summary>
        /// Gets the value indicating the list of <see cref="CustomMod"/> instances
        /// associated with the current parameter.
        /// </summary>
        /// <value>The list of <see cref="CustomMod"/> instances associated with the current param.</value>
        IList<CustomMod> CustomMods { get; }
    }
}