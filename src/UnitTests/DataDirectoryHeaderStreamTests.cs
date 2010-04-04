using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tao.Interfaces;

namespace Tao.UnitTests
{
    [TestFixture]
    public class DataDirectoryHeaderStreamTests : BaseStreamTests
    {
        [Test]
        public void ShouldHaveDataDirectoryCountingService()
        {
            var container = CreateContainer();
            Assert.IsTrue(container.Contains(typeof(IFunction<Stream, int>), "DataDirectoryCounter"));
        }

        [Test]
        public void ShouldBeAbleToReadTheNumberOfDataDirectories()
        {
            var stream = GetStream();
            var container = CreateContainer();
            var counter = (IFunction<Stream, int>)container.GetInstance(typeof(IFunction<Stream, int>), "DataDirectoryCounter");
            var result = counter.Execute(stream);

            Assert.AreEqual(0x10, result);
        }

        [Test]
        public void ShouldBeAbleToSeekEndOfDataDirectories()
        {
            var stream = GetStream();
            var container = CreateContainer();

            var seeker = (IFunction<Stream>)container.GetInstance(typeof(IFunction<Stream>), "DataDirectoriesEndSeeker");
            Assert.IsNotNull(seeker);
            seeker.Execute(stream);

            Assert.AreEqual(0x178, stream.Position);
        }

        [Test]
        public void ShouldBeAbleToReadDataDirectories()
        {
            TestRead("DataDirectoryStreamFactory", 0x178, 128);
        }
    }
}
