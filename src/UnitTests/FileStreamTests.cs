using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Hiro.Containers;
using NUnit.Framework;
using Tao.Interfaces;

namespace Tao.UnitTests
{
    [TestFixture]
    public class FileStreamTests
    {
        [Test]
        public void ShouldBeAbleToOpenFileStream()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var targetFile = Path.Combine(path, "skeleton.exe");

            var container = new MicroContainer();
            var serviceType = typeof(IFactory<string, Stream>);
            Assert.IsTrue(container.Contains(serviceType, null));
            var streamFactory = container.GetInstance(serviceType, null) as IFactory<string, Stream>;
            Assert.IsNotNull(streamFactory);

            var stream = streamFactory.Create(targetFile);
            Assert.IsNotNull(stream);
            Assert.IsTrue(stream.Length > 0);
        }
    }
}
