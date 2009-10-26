using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Core
{
    /// <summary>
    /// Represents a structure that gives the address and size of a table or string used by Windows.
    /// </summary>
    public class DataDirectory : IDataDirectory
    {
        /// <summary>
        /// Gets the value indicating the RVA of the data directory.
        /// </summary>
        /// <value>The virtual address of the target data directory.</value>
        public uint? VirtualAddress
        {
            get; private set;
        }

        /// <summary>
        /// Gets the value indicating the size of the target data directory.
        /// </summary>
        /// <value>The size of the target data directory.</value>
        public uint? Size
        {
            get; private set;
        }

        /// <summary>
        /// Reads the directory from the target binary <paramref name="reader"/>
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public void ReadFrom(IBinaryReader reader)
        {
            VirtualAddress = reader.ReadUInt32();
            Size = reader.ReadUInt32();
        }
    }
}
