using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Model
{
    /// <summary>
    /// Represents the flags that describe a given assembly.
    /// </summary>
    public enum AssemblyFlags :  uint
    {
        /// <summary>
        /// The assembly refeerence holds the full (unhashed) public key.
        /// </summary>
        PublicKey = 0x0001,
       
        /// <summary>
        /// Reserved: both bits shall be zero.
        /// </summary>
        Reserved = 0x0030,

        /// <summary>
        /// The implementation of this assembly used at runtime is not expected to match the version seen at compile time.
        /// </summary>
        Retargetable = 0x0100,

        /// <summary>
        /// Reserved.
        /// </summary>
        EnableJITcompileTracking = 0x8000,

        /// <summary>
        /// Reserved.
        /// </summary>
        DisableJITcompileOptimizer = 0x4000
    }
}
