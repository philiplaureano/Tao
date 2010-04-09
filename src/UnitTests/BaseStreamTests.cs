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
            var serviceType = typeof(IFunction<string, Stream>);
            Assert.IsTrue(container.Contains(serviceType, null));
            var streamFactory = container.GetInstance(serviceType, null) as IFunction<string, Stream>;
            Assert.IsNotNull(streamFactory);

            var result = streamFactory.Execute(targetFile);

            result.Seek(0, SeekOrigin.Begin);
            return result;
        }

        protected void TestRead(string serviceName, int expectedEndPosition, int expectedHeaderSize)
        {
            var fileStream = GetStream();
            var container = CreateContainer();
            Assert.IsTrue(container.Contains(typeof(IFunction<Stream, Stream>), serviceName));

            var factory = container.GetInstance<IFunction<Stream, Stream>>(serviceName);
            Assert.IsNotNull(factory, "Service {0} not found", serviceName);

            var result = factory.Execute(fileStream);
            Assert.IsNotNull(result, "The stream cannot be null");
            Assert.AreEqual(expectedEndPosition, fileStream.Position, "Incorrect file position!");
            Assert.AreEqual(expectedHeaderSize, result.Length, "Incorrect header length!");
        }

        protected virtual IMicroContainer CreateContainer()
        {
            var loader = new DependencyMapLoader();

            var targetAssembly = Path.GetFileName(typeof(FileStreamFactory).Assembly.Location);
            var map = CreateDependencyMap(loader, targetAssembly);

            return map.CreateContainer();
        }

        protected virtual DependencyMap CreateDependencyMap(DependencyMapLoader loader, string targetAssembly)
        {
            return loader.LoadFromBaseDirectory(targetAssembly);
        }
    }
}
