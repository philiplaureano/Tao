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
        [Test]
        public void ShouldReadFirstString()
        {
            AssertStringEquals(0, string.Empty);
        }

        [Test]
        public void ShouldReadModuleName()
        {
            AssertStringEquals(1, "<Module>");
        }

        [Test]
        public void ShouldReadProgramName()
        {
            AssertStringEquals(2, "skeleton.exe");
        }

        [Test]
        public void ShouldReadEntryPointMethodName()
        {
            AssertStringEquals(3, "main");
        }

        [Test]
        public void ShouldReadAssemblyName()
        {
            AssertStringEquals(4, "donothing");
        }

        private static void AssertStringEquals(int targetIndex, string expectedText)
        {
            var mockStrings = new Mock<IList<string>>();
            var heap = new Mock<IStringHeap>();
            heap.Expect(h => h.Strings).Returns(mockStrings.Object);
            mockStrings.Expect(ms => ms[targetIndex]).Returns(expectedText);

            var index = new StringHeapIndex((uint)targetIndex);
            var text = index.GetText(heap.Object);

            Assert.AreEqual(expectedText, text);

            heap.VerifyAll();
            mockStrings.VerifyAll();
        }
        
        protected override HeapIndex CreateHeapIndex(Mock<IMetadataStream> metadataStream)
        {
            return new StringHeapIndex(metadataStream.Object);
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
