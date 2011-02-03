using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tao.Model
{
    /// <summary>
    /// Represents an MVAR type signature element.
    /// </summary>
    public class MvarSignature : TypeSignature
    {
        /// <summary>
        /// Gets the value indicating the argument index for the current generic method type argument.
        /// </summary>
        /// <value>The argument index for the current generic method type argument.</value>
        public uint ArgumentIndex { get; set; }
    }
}
