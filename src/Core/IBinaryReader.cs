using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tao.Core
{
    public interface IBinaryReader
    {
        byte[] ReadBytes(int p);
        UInt16 ReadUInt16();

        int ReadInt32();
    }
}
