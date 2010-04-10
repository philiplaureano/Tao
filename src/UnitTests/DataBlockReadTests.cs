using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tao.Interfaces;

namespace Tao.UnitTests
{
    [TestFixture]
    public class DataBlockReadTests : BaseStreamTests
    {
        [Test]
        public void ShouldBeAbleToResolveFileAbsolutePositionFromRva()
        {
            var stream = GetStream();
            

            var resolver = (IFunction<ITuple<int, Stream>, int>) container.GetInstance(typeof (IFunction<ITuple<int, Stream>, int>), "ResolveAbsoluteFilePositionFromRva");
            Assert.IsNotNull(resolver);
            
            // Use the CLR header RVA
            var rva = 0x2008;
            var expectedPosition = 0x208;
            var position = resolver.Execute(rva, stream);

            Assert.AreEqual(expectedPosition, position);
        }

        [Test]
        public void ShouldBeAbleToSeekAbsoluteFilePositionFromRva()
        {
            var stream = GetStream();
            

            // Use the CLR header RVA
            var rva = 0x2008;
            var expectedPosition = 0x208;

            var seeker = (IFunction<ITuple<int, Stream>>)container.GetInstance(typeof(IFunction<ITuple<int, Stream>>), "SeekAbsoluteFilePositionFromRva");
            Assert.IsNotNull(seeker);
            seeker.Execute(rva, stream);

            var position = stream.Position;
            Assert.AreEqual(expectedPosition, position);
        }

        [Test]
        public void ShouldBeAbleToReadDataBlockUsingDataDirectoryIndex()
        {
            var stream = GetStream();
            

            // Read the CLR header
            var reader = (IFunction<ITuple<int, Stream>, Stream>)container.GetInstance(typeof(IFunction<ITuple<int, Stream>, Stream>), "ReadStreamFromDataDirectoryIndex");
            Assert.IsNotNull(reader);

            var result = reader.Execute(14, stream);
            Assert.AreEqual(0x48, result.Length);
        }

        [Test]
        public void ShouldBeAbleToReadDataUsingGivenRvaAndSize()
        {
            var stream = GetStream();
            

            // Use the CLR header RVA
            var rva = 0x2008;
            var size = 0x48;
            var reader = container.GetInstance(typeof(IFunction<ITuple<int, int, Stream>, Stream>), "ReadStreamFromRvaAndSize") as IFunction<ITuple<int, int, Stream>, Stream>;
            Assert.IsNotNull(reader);

            var result = reader.Execute(rva, size, stream);
            Assert.IsNotNull(result);
            Assert.AreEqual(0x48, result.Length);

            var binaryReader = new BinaryReader(result);
            var firstValue = binaryReader.ReadInt32();
            var secondValue = binaryReader.ReadInt16();

            Assert.AreEqual(0x48, firstValue);
            Assert.AreEqual(2, secondValue);
        }
    }
}
