using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Core
{
    /// <summary>
    /// Represents a class that reads a set of <see cref="DataDirectory"/> objects from a given binary stream.
    /// </summary>
    public class DataDirectoryReader : IDataDirectoryReader
    {
        /// <summary>
        /// Reads the <paramref name="directoryCount">given number</paramref> of data directories from
        /// the binary <paramref name="reader"/>.
        /// </summary>
        /// <param name="directoryCount">The number of data directories to read.</param>
        /// <param name="reader">The binary reader.</param>
        /// <returns>The list of data directories that have been read from the reader.</returns>
        public IList<DataDirectory> ReadDirectories(int directoryCount, IBinaryReader reader)
        {
            var directories = new List<DataDirectory>();
            for (var i = 0; i < directoryCount; i++)
            {
                var directory = new DataDirectory();
                directory.ReadFrom(reader);

                directories.Add(directory);
            }

            return directories;
        }
    }
}
