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
            var fileStream = GetStream();
            var container = new MicroContainer();
            var factory = container.GetInstance<IConversion<Stream, Stream>>("CoffHeaderStreamFactory");
            Assert.IsNotNull(factory);
            var result = factory.Create(fileStream);
            Assert.IsNotNull(result);
            Assert.AreEqual(0x5C, result.Length);
        }
    }
}
