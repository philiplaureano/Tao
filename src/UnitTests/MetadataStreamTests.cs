using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            var reader = container.GetInstance<IFunction<Stream, IEnumerable<ITuple<int, int, string>>>>(
                    "ReadMetadataStreamHeaders");

            Assert.IsNotNull(reader);

            var results = new List<ITuple<int, int, string>>(reader.Execute(stream));
            Assert.AreEqual(5, results.Count);

            // Read the first header            
            var result = results[0];
            Assert.AreEqual(0x6c, result.Item1);
            Assert.AreEqual(0x68, result.Item2);
            Assert.AreEqual("#~", result.Item3);

            // Read the last header
            result = results[4];
            Assert.AreEqual(0x114, result.Item1);
            Assert.AreEqual(0x08, result.Item2);
            Assert.AreEqual("#Blob", result.Item3);
        }
    }
}
