using System.Collections.Generic;

namespace Tao.Model
{
    /// <summary>
    /// Represents a method signature type.
    /// </summary>
    public interface IMethodSignature
    {
        /// <summary>
        /// Gets or sets a value indicating whether or not the current method has a 'this' pointer.
        /// </summary>
        /// <value>The value indicating whether or not the current method requires a 'this' pointer.</value>
        bool HasThis { get; set; }

        /// <summary>
        /// Gets or sets the value indicating whether or not the current method is a generic method.
        /// </summary>
        /// <value>A value indicating whether or not the current method is a generic method.</value>
        bool IsGeneric { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the number of generic type parameters for the current method.
        /// </summary>
        /// <value>The number of generic type parameters for the current method.</value>
        uint GenericParameterCount { get; set; }

        /// <summary>
        /// Gets or sets the value indicating the number of parameters for the current method.
        /// </summary>
        /// <value>The number of parameters for the current method.</value>
        uint ParameterCount { get; set; }

        /// <summary>
        /// Gets or sets the value indicating return type for the current method.
        /// </summary>
        /// <value>The method return type.</value>
        ITypedMethodSignatureElement ReturnType { get; set; }

        /// <summary>
        /// Gets the value indicating the list of parameters for the current method.
        /// </summary>
        /// <value>The list of parameters for the current method.</value>
        IList<ITypedMethodSignatureElement> Parameters { get; }

        /// <summary>
        /// Gets or sets the value that indicates whether or not the target object instance will also be an explicit parameter
        /// in the current set of method parameters.
        /// </summary>
        /// <value>Determines whether or not the method will use the ExplicitThis calling convention.</value>
        bool HasExplicitThis { get; set; }

        /// <summary>
        /// Gets or sets the value indicating whether or not the current method supports variable arguments.
        /// </summary>
        /// <value>Determines whether or not the current method supports variable arguments.</value>
        bool IsVarArg { get; set; }
    }
}