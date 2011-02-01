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
        Default = 0x0,

        /// <summary>
        /// A flag that indicates a variable argument calling convention.
        /// </summary>
        VarArg = 0x5,

        /// <summary>
        /// A flag that indicates that a method is using the StdCall calling convention.
        /// </summary>
        StdCall = 0x02,
        
        /// <summary>
        /// A flag that indicates that a method is using the C calling convention.
        /// </summary>
        C = 0x1,

        /// <summary>
        /// A flag that indicates that a method is using the ThisCall calling convention.
        /// </summary>
        ThisCall =0x2,

        /// <summary>
        /// A flag that indicates that a method is using the ThisCall calling convention.
        /// </summary>
        FastCall = 0x4,

        /// <summary>
        /// Indicates that the method is a generic method.
        /// </summary>
        Generic = 0x10
    }
}
