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
    public class StringHeapIndexReadTests : HeapIndexReadTest
    {
        protected override HeapIndex CreateHeapIndex(Mock<IMetadataStream> metadataStream)
        {
            return new StringsHeapIndex(metadataStream.Object);
        }

        protected override byte GetHeapSizesForSingleWordReads()
        {
            return 0xE;
        }

        protected override byte GetHeapSizesForDoubleWordReads()
        {
            return 0xF;
        }
    }
}
