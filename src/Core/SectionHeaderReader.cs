using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Core
{
    /// <summary>
    /// Represents a class that reads multiple section headers from a given binary stream.
    /// </summary>
    public class SectionHeaderReader
    {
        /// <summary>
        /// Reads the section headers from a the binary reader.
        /// </summary>
        /// <param name="sectionCount">The number of sections to read.</param>
        /// <param name="reader">The binary reader.</param>
        /// <returns>A list of sections that have been read from the binary reader.</returns>
        public IList<SectionHeader> ReadFrom(int sectionCount, IBinaryReader reader)
        {
            var results = new List<SectionHeader>();
            for(var i = 0; i < sectionCount; i++)
            {
                var header = new SectionHeader();
                header.ReadFrom(reader);

                results.Add(header);
            }

            return results;
        }
    }
}
