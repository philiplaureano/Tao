using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Core
{
    /// <summary>
    /// Represents the IAT in a portable executable file.
    /// </summary>
    public class ImportAddressTable
    {
        /// <summary>
        /// Gets the value indicating the relative virtual address that points to the Hint/Name Table.
        /// </summary>
        /// <value>The RVA of the Hint/Name table.</value>
        public uint? HintNameTableRVA
        {
            get; private set;
        }

        /// <summary>
        /// Reads the IAT from the given <paramref name="reader"/>.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public void ReadFrom(IBinaryReader reader)
        {
            HintNameTableRVA = reader.ReadUInt32();
        }
    }
}
