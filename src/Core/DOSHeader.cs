using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tao.Core
{
    /// <summary>
    /// Represents a rudimentary implementation of a DOS header.
    /// </summary>
    public class DOSHeader : IHeader
    {
        /// <summary>
        /// Reads the DOS header from the given binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public void ReadFrom(IBinaryReader reader)
        {
            // Skip the DOS header altogether
            reader.Seek(0x80, SeekOrigin.Begin);
        }
    }
}
