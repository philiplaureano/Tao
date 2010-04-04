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
            Assert.IsTrue(container.Contains(typeof(IConversion<Stream, int>), "DataDirectoryCounter"));
        }

        [Test]
        public void ShouldBeAbleToReadTheNumberOfDataDirectories()
        {
            var stream = GetStream();
            var container = CreateContainer();
            var counter = (IConversion<Stream, int>)container.GetInstance(typeof(IConversion<Stream, int>), "DataDirectoryCounter");
            var result = counter.Convert(stream);

            Assert.AreEqual(0x10, result);
        }

        [Test]
        public void ShouldBeAbleToSeekEndOfDataDirectories()
        {
            var stream = GetStream();
            var container = CreateContainer();

            var seeker = (IStreamSeeker)container.GetInstance(typeof(IStreamSeeker), "DataDirectoriesEndSeeker");
            Assert.IsNotNull(seeker);
            seeker.Seek(stream);

            Assert.AreEqual(0x178, stream.Position);
        }

        [Test]
        public void ShouldBeAbleToReadDataDirectories()
        {
            TestRead("DataDirectoryStreamFactory", 0x178, 128);
        }
    }
}
