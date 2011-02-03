using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tao.Interfaces;
using Tao.Model;
using Tao.Containers;

namespace Tao.UnitTests
{
    [TestFixture]
    public class ArrayShapeTests : BaseStreamTests
    {
        [Test]
        public void ShouldBeAbleToReadArrayShapeBytes()
        {
            // Specify the number of array dimensions
            byte rank = 1;
            byte numSizes = 2;
            byte numLowBounds = 2;

            var sizes = new byte[] { 1, 2 };
            var loBounds = new byte[] { 0, 1 };

            var bytes = new List<byte> { rank, numSizes };
            bytes.AddRange(sizes);
            bytes.Add(numLowBounds);
            bytes.AddRange(loBounds);

            var reader = container.GetInstance<IFunction<Stream, ArrayShape>>("ArrayShapeReader");
            var shape = reader.Execute(new MemoryStream(bytes.ToArray()));

            var currentSizes = new List<uint>(shape.Sizes);
            var currentLoBounds = new List<uint>(shape.LoBounds);

            Assert.IsNotNull(shape);
            Assert.AreEqual(rank, shape.Rank);
            Assert.AreEqual(numSizes, shape.NumSizes);
            Assert.AreEqual(2, currentSizes.Count);
            Assert.AreEqual(sizes[0], currentSizes[0]);
            Assert.AreEqual(sizes[1], currentSizes[1]);
            Assert.AreEqual(numLowBounds, currentLoBounds.Count);
            Assert.AreEqual(loBounds[0], currentLoBounds[0]);
            Assert.AreEqual(loBounds[1], currentLoBounds[1]);
        }
    }
}
