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
    public class MetadataStreamTests : BaseStreamTests
    {       
        [Test]
        public void ShouldBeAbleToReadAbitrarymetadataStreamHeaderBasedOnAGivenIndex()
        {
            var stream = GetStream();
            var container = CreateContainer();

            var reader = container.GetInstance<IFunction<ITuple<int, Stream>, ITuple<int, int, string>>>(
                    "ReadMetadataStreamHeader");

            Assert.IsNotNull(reader);

            // Read the first header
            var result = reader.Execute(0, stream);
            Assert.AreEqual(0x6c, result.Item1);
            Assert.AreEqual(0x68, result.Item2);
            Assert.AreEqual("#~", result.Item3);

            // Read the last header
            result = reader.Execute(4, stream);
            Assert.AreEqual(0x114, result.Item1);
            Assert.AreEqual(0x08, result.Item2);
            Assert.AreEqual("#Blob", result.Item3);
        }
    }
}
