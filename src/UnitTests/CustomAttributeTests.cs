using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Tao.Containers;
using Tao.Interfaces;

namespace Tao.UnitTests
{
    [TestFixture]
    public class CustomAttributeTests : BaseStreamTests
    {        
        [Test]
        public void ShouldBeAbleToReadNullSerString()
        {
            var textBytes = new byte[] {0xFF};

            var bytes = new List<byte>();
            bytes.AddRange(textBytes);            

            var reader = container.GetInstance<IFunction<Stream, string>>("ReadSerString");
            Assert.IsNotNull(reader, "ReadSerString reader not found");

            var stream = new MemoryStream(bytes.ToArray());
            var result = reader.Execute(stream);
            Assert.IsNull(result);
        }

        [Test]
        public void ShouldBeAbleToReadEmptySerString()
        {
            var textBytes = new byte[] { 0x00 };

            var bytes = new List<byte>();
            bytes.AddRange(textBytes);

            var reader = container.GetInstance<IFunction<Stream, string>>("ReadSerString");
            Assert.IsNotNull(reader, "ReadSerString reader not found");

            var stream = new MemoryStream(bytes.ToArray());
            var result = reader.Execute(stream);
            Assert.AreEqual(string.Empty, result);
        }
    }
}
