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
        public void ShouldBeAbleToReadMetadataStreamHeaders()
        {
            var stream = GetStream();
            
            var reader = container.GetInstance<IFunction<Stream, IEnumerable<ITuple<int, int, string>>>>("ReadMetadataStreamHeaders");

            Assert.IsNotNull(reader);

            var results = new List<ITuple<int, int, string>>(reader.Execute(stream));
            Assert.AreEqual(5, results.Count);

            // Read the first header            
            var result = results[0];

            // Verify the offset from the start of the metadata root
            Assert.AreEqual(0x6c, result.Item1);

            // Verify the size
            Assert.AreEqual(0x68, result.Item2);
            Assert.AreEqual("#~", result.Item3);

            // Read the last header
            result = results[4];
            Assert.AreEqual(0x114, result.Item1);
            Assert.AreEqual(0x08, result.Item2);
            Assert.AreEqual("#Blob", result.Item3);
        }

        [Test]
        public void ShouldBeAbleToReadMetadataStreamUsingAGivenIndex()
        {
            var stream = GetStream();
            

            var reader = container.GetInstance<IFunction<ITuple<int, Stream>, Stream>>("ReadMetadataStreamByIndex");
            Assert.IsNotNull(reader);

            var result = reader.Execute(0, stream);
            Assert.AreEqual(0x68, result.Length);

            result = reader.Execute(4, stream);
            Assert.AreEqual(8, result.Length);
        }

        [Test]
        public void ShouldBeAbleToExtractAllMetadataStreams()
        {
            var stream = GetStream();
                        
            var extractor = container.GetInstance<IFunction<Stream, IDictionary<string, Stream>>>("ReadAllMetadataStreams");
            Assert.IsNotNull(extractor);
            
            var heaps = extractor.Execute(stream);
            Assert.IsNotNull(heaps);
            Assert.AreEqual(5, heaps.Count);

            Assert.IsTrue(heaps.ContainsKey("#~"));
            Assert.IsTrue(heaps.ContainsKey("#Blob"));
            Assert.IsTrue(heaps.ContainsKey("#Strings"));
            Assert.IsTrue(heaps.ContainsKey("#GUID"));
            Assert.IsTrue(heaps.ContainsKey("#US"));
        }
    }
}
