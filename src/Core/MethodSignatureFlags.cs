using System;
using System.Collections.Generic;
using System.Text;

namespace Tao
{
    /// <summary>
    /// Defines the valid calling conventions for a method.
    /// </summary>
    [Flags]
    public enum MethodSignatureFlags : byte
    {
        /// <summary>
        /// This flag is set when method is instance or virtual.
        /// </summary>
        HasThis = 0x20,

        /// <summary>
        /// A flag that indicates whether or not the target object instance will also be an explicit parameter
        /// in the current set of method parameters.
        /// </summary>
        ExplicitThis = 0x40,

        /// <summary>
        /// A flag that indicates that the default calling convention should be used.
        /// </summary>
        Default= 0x0,

        /// <summary>
        /// A flag that indicates a variable argument calling convention.
        /// </summary>
        VarArg = 0x5,

        /// <summary>
        /// Indicates that the method is a generic method.
        /// </summary>
        Generic = 0x10,

        /// <summary>
        /// Used to encode "..." in the parameter list
        /// </summary>
        Sentinel = 0x41
    }
}
