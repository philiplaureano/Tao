using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Core
{
    public class SectionHeaderReader
    {
        public IList<SectionHeader> ReadSections(int sectionCount, IBinaryReader reader)
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
