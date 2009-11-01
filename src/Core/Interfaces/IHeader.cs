using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Core
{
    /// <summary>
    /// Represents a type that can read from a binary stream.
    /// </summary>
    public interface IHeader
    {
        /// <summary>
        /// Reads data from the given <paramref name="reader"/>.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        void ReadFrom(IBinaryReader reader);
    }
}
