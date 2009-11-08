using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tao.Core;

namespace Tao.UnitTests
{
    [TestFixture]
    public class BlobHeapReadTests : BaseHeaderReadTest
    {
        [Test]
        public void ShouldReadFirstBlobEntry()
        {
            Func<BlobHeap, bool> matchesExpectedValue =
                heap =>
                {
                    var blob = heap.Blobs[0];
                    return blob.Length == 0; 
                };

            AssertEquals(true, matchesExpectedValue);
        }

        protected override void SetStreamPosition(Stream stream)
        {
            stream.Seek(0x374, SeekOrigin.Begin);
        }
    }
}
