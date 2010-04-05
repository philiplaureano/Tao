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
            TestRead("CoffHeaderStreamFactory", expectedEndPosition, expectedHeaderSize);
        }

        [Test]
        public void ShouldBeAbleToReadFileAlignment()
        {
            var stream = GetStream();
            var container = CreateContainer();

            var reader = (IFunction<Stream,int>)container.GetInstance(typeof (IFunction<Stream, int>), "ReadFileAlignment");
            Assert.IsNotNull(reader);

            var result = reader.Execute(stream);
            Assert.AreEqual(0x200, result);
        }

        [Test]
        public void ShouldBeAbleToReadSectionAlignment()
        {
            var stream = GetStream();
            var container = CreateContainer();

            var reader = (IFunction<Stream, int>)container.GetInstance(typeof(IFunction<Stream, int>), "ReadSectionAlignment");
            Assert.IsNotNull(reader);

            var result = reader.Execute(stream);
            Assert.AreEqual(0x2000, result);
        }
    }
}
