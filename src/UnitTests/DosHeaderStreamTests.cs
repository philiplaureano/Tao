using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Tao.UnitTests
{
    [TestFixture]
    public class DosHeaderStreamTests : BaseStreamTests
    {
        [Test]
        public void ShouldBeAbleToExtractDosStream()
        {
            var expectedEndPosition = 0x3C;
            var expectedHeaderSize = 0x3C;
            TestRead("DosHeaderStreamFactory", expectedEndPosition, expectedHeaderSize);
        }
    }
}
