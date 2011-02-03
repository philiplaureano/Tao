using System.Collections.Generic;

namespace Tao.Model
{
    /// <summary>
    /// Represents a StandAloneMethod method signature type.
    /// </summary>
    public interface IStandAloneMethodSignature : IMethodRefSignature
    {
        /// <summary>
        /// Gets a value indicating whether or not the method signature uses a StdCall calling convention.
        /// </summary>
        /// <value>The value indicating whether or not the method signature uses a StdCall calling convention.</value>
        bool IsStdCall { get; set; }

        /// <summary>
        /// Gets a value indicating whether or not the method signature uses a C calling convention.
        /// </summary>
        /// <value>The value indicating whether or not the method signature uses a C calling convention.</value>
        bool IsUsingCCallingConvention { get; set; }

        /// <summary>
        /// Gets a value indicating whether or not the method signature uses a FastCall calling convention.
        /// </summary>
        /// <value>The value indicating whether or not the method signature uses a FastCall calling convention.</value>
        bool IsFastCall { get; set; }

        /// <summary>
        /// Gets a value indicating whether or not the method signature uses a ThisCall calling convention.
        /// </summary>
        /// <value>The value indicating whether or not the method signature uses a ThisCall calling convention.</value>
        bool IsThisCall { get; set; }
    }
}