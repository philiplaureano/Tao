using System.Collections.Generic;

namespace Tao.Core
{
    /// <summary>
    /// Represents a type that can read the data directories from a given binary reader.
    /// </summary>
    public interface IDataDirectoryReader
    {
        /// <summary>
        /// Reads the <paramref name="directoryCount">given number</paramref> of data directories from
        /// the binary <paramref name="reader"/>.
        /// </summary>
        /// <param name="directoryCount">The number of data directories to read.</param>
        /// <param name="reader">The binary reader.</param>
        /// <returns>The list of data directories that have been read from the reader.</returns>
        IList<DataDirectory> ReadDirectories(int directoryCount, IBinaryReader reader);
    }
}