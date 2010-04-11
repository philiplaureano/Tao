using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Model
{
    /// <summary>
    /// Describes the algorithm used for hashing the assembly.
    /// </summary>
    public enum AssemblyHashAlgorithm : uint
    {
        /// <summary>
        /// This means that the assembly is currently using a hash algorithm.
        /// </summary>
        None = 0,
        /// <summary>
        /// Reserved. This means that the assembly is using an MD5 hashing algorithm.
        /// </summary>
        MD5 = 0x8003,

        /// <summary>
        /// This means that the assembly is using a SHA1 algorithm.
        /// </summary>
        SHA1 = 0x8004
    }
}
