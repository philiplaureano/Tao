using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Hiro;
using Hiro.Containers;
using Hiro.Loaders;
using NUnit.Framework;
using Tao.Core.Factories;
using Tao.Interfaces;

namespace Tao.UnitTests
{
    public abstract class BaseStreamTests
    {
        protected Stream GetStream()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var targetFile = Path.Combine(path, "skeleton.exe");

            var container = CreateContainer();
            var serviceType = typeof(IConversion<string, Stream>);
            Assert.IsTrue(container.Contains(serviceType, null));
            var streamFactory = container.GetInstance(serviceType, null) as IConversion<string, Stream>;
            Assert.IsNotNull(streamFactory);

            return streamFactory.Convert(targetFile);
        }

        protected void TestRead(string serviceName, int expectedEndPosition, int expectedHeaderSize)
        {
            var fileStream = GetStream();
            var container = CreateContainer();
            Assert.IsTrue(container.Contains(typeof(IConversion<Stream, Stream>), serviceName));

            var factory = container.GetInstance<IConversion<Stream, Stream>>(serviceName);
            Assert.IsNotNull(factory, "Service {0} not found", serviceName);

            var result = factory.Convert(fileStream);
            Assert.IsNotNull(result, "The stream cannot be null");
            Assert.AreEqual(expectedEndPosition, fileStream.Position);
            Assert.AreEqual(expectedHeaderSize, result.Length);
        }

        protected virtual IMicroContainer CreateContainer()
        {
            var loader = new DependencyMapLoader();

            var targetAssembly = Path.GetFileName(typeof(FileStreamFactory).Assembly.Location);
            var map = loader.LoadFromBaseDirectory(targetAssembly);

            return map.CreateContainer();
        }
    }
}
