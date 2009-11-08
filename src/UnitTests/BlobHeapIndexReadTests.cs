using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;
using Tao.Core;

namespace Tao.UnitTests
{
    [TestFixture]
    public class BlobHeapIndexReadTests : HeapIndexReadTest
    {
        protected override byte GetHeapSizesForSingleWordReads()
        {
            return 0x7;
        }

        protected override byte GetHeapSizesForDoubleWordReads()
        {
            return 0xF;
        }

        protected override HeapIndex CreateHeapIndex(Mock<IMetadataStream> metadataStream)
        {
            return new BlobHeapIndex(metadataStream.Object);
        }
    }
}
