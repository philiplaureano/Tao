using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tao.Interfaces;
using Tao.Containers;

namespace Tao.UnitTests
{
    [TestFixture]
    public class OptionalHeaderTests : BaseStreamTests
    {
        [Test]
        public void ShouldBeAbleToReadFileAlignment()
        {
            var stream = GetStream();
            var reader = (IFunction<Stream, int>)container.GetInstance(typeof(IFunction<Stream, int>), "ReadFileAlignment");
            Assert.IsNotNull(reader);

            var result = reader.Execute(stream);
            Assert.AreEqual(0x200, result);
        }

        [Test]
        public void ShouldBeAbleToReadSectionAlignment()
        {
            var stream = GetStream();
            var reader = (IFunction<Stream, int>)container.GetInstance(typeof(IFunction<Stream, int>), "ReadSectionAlignment");
            Assert.IsNotNull(reader);

            var result = reader.Execute(stream);
            Assert.AreEqual(0x2000, result);
        }       
    }
}
