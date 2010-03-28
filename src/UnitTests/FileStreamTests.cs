using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Hiro.Containers;
using NUnit.Framework;
using Tao.Interfaces;

namespace Tao.UnitTests
{    
    [TestFixture]
    public class FileStreamTests : BaseStreamTests
    {
        [Test]
        public void ShouldBeAbleToOpenFileStream()
        {
            Stream stream = GetStream();
            Assert.IsNotNull(stream);
            Assert.IsTrue(stream.Length > 0);
        }
    }
}
