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
    public abstract class BaseStreamTests
    {
        protected Stream GetStream()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var targetFile = Path.Combine(path, "skeleton.exe");

            var container = new MicroContainer();
            var serviceType = typeof(IConversion<string, Stream>);
            Assert.IsTrue(container.Contains(serviceType, null));
            var streamFactory = container.GetInstance(serviceType, null) as IConversion<string, Stream>;
            Assert.IsNotNull(streamFactory);

            return streamFactory.Create(targetFile);
        }
    }

}
