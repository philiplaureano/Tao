using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;
using Tao.Core;

namespace Tao.UnitTests
{
    public abstract class HeapIndexReadTest
    {
        [Test]
        public void ShouldHaveSingleWordIndexSizeIfParticularHeapSizeBitIsZero()
        {
            // Set the target heap heapsize bit vector to zero
            var heapSizes = GetHeapSizesForSingleWordReads();

            var metadataStream = new Mock<IMetadataStream>();
            metadataStream.Expect(mdStream => mdStream.HeapSizes).Returns(heapSizes);

            var stringHeapIndex = CreateHeapIndex(metadataStream);
            Assert.AreEqual(2, stringHeapIndex.IndexSizeInBytes);

            metadataStream.VerifyAll();
        }

        [Test]
        public void ShouldReadSingleWordIfParticularHeapSizeBitIsZero()
        {
            var heapSizes = GetHeapSizesForSingleWordReads();
            var reader = new Mock<IBinaryReader>();
            reader.Expect(r => r.ReadUInt16()).Returns(0);

            TestIndexRead(reader, heapSizes, 2);
        }

        [Test]
        public void ShouldReadDoubleWordIfParticularHeapSizeBitIsZero()
        {
            var heapSizes = GetHeapSizesForDoubleWordReads();
            var reader = new Mock<IBinaryReader>();
            reader.Expect(r => r.ReadUInt32()).Returns(0);

            TestIndexRead(reader, heapSizes, 4);
        }

        private void TestIndexRead(Mock<IBinaryReader> reader, byte heapSizes, int expectedIndexSize)
        {
            var metadataStream = new Mock<IMetadataStream>();
            metadataStream.Expect(mdStream => mdStream.HeapSizes).Returns(heapSizes);

            HeapIndex heapIndex = CreateHeapIndex(metadataStream);
            Assert.AreEqual(expectedIndexSize, heapIndex.IndexSizeInBytes);

            heapIndex.ReadFrom(reader.Object);

            reader.VerifyAll();
        }

        protected abstract byte GetHeapSizesForSingleWordReads();
        protected abstract byte GetHeapSizesForDoubleWordReads();
        protected abstract HeapIndex CreateHeapIndex(Mock<IMetadataStream> metadataStream);
        
    }
}
