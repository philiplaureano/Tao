using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tao.Core
{
    /// <summary>
    /// Represents the basic implementation of a PE header.
    /// </summary>
    public abstract class BaseHeader : IReader
    {
        /// <summary>
        /// Reads data from the given <paramref name="reader"/>.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public abstract void ReadFrom(IBinaryReader reader);

        /// <summary>
        /// Sets the binary reader position to point to the header at the given <paramref name="headerIndex"/>.
        /// </summary>
        /// <param name="optionalHeader">The Optional Header that contains the offset data.</param>
        /// <param name="reader">The target binary reader.</param>
        /// <param name="headerIndex">The index of the target header.</param>
        protected void SeekHeader(IOptionalHeader optionalHeader, IBinaryReader reader, int headerIndex)
        {
            var directories = new List<IDataDirectory>(optionalHeader.DataDirectories);
            var targetDirectory = directories[headerIndex];

            var rva = targetDirectory.VirtualAddress;
            var sectionAlignment = optionalHeader.SectionAlignment;
            var fileAlignment = optionalHeader.FileAlignment;

            var fileOffset = rva.Value - sectionAlignment.Value + fileAlignment.Value;
            reader.Seek(fileOffset, SeekOrigin.Begin);
        }
    }
}
