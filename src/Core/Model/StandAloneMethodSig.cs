using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tao.Model
{
    /// <summary>
    /// Represents a StandAloneMethodSig method signature type.
    /// </summary>
    public class StandAloneMethodSig : MethodRefSig, IStandAloneMethodSigSignature
    {
        /// <summary>
        /// Gets a value indicating whether or not the method signature uses a StdCall calling convention.
        /// </summary>
        /// <value>The value indicating whether or not the method signature uses a StdCall calling convention.</value>
        public bool IsStdCall { get; set; }

        /// <summary>
        /// Gets a value indicating whether or not the method signature uses a C calling convention.
        /// </summary>
        /// <value>The value indicating whether or not the method signature uses a C calling convention.</value>
        public bool IsUsingCCallingConvention { get; set; }

        /// <summary>
        /// Gets a value indicating whether or not the method signature uses a FastCall calling convention.
        /// </summary>
        /// <value>The value indicating whether or not the method signature uses a FastCall calling convention.</value>
        public bool IsFastCall { get; set; }

        /// <summary>
        /// Gets a value indicating whether or not the method signature uses a ThisCall calling convention.
        /// </summary>
        /// <value>The value indicating whether or not the method signature uses a ThisCall calling convention.</value>
        public bool IsThisCall { get; set; }
    }
}
