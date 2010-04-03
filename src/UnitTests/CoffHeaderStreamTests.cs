using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Hiro;
using Hiro.Containers;
using NUnit.Framework;
using Tao.Interfaces;

namespace Tao.UnitTests
{
    [TestFixture]
    public class CoffHeaderStreamTests : BaseStreamTests
    {
        [Test]
        public void ShouldBeAbleToExtractCoffHeaderStream()
        {
            var expectedEndPosition = 0xF4;
            var expectedHeaderSize = 0x5C;
            TestFactory("CoffHeaderStreamFactory", expectedEndPosition, expectedHeaderSize);
        }
    }
}
