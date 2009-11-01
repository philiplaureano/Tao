using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Core
{
    public class ModuleTable : IReader
    {
        public ushort? Generation
        {
            get; private set;
        }

        public void ReadFrom(IBinaryReader reader)
        {
            Generation = reader.ReadUInt16();
        }
    }
}
