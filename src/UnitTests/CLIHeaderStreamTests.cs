using System.IO;
using NUnit.Framework;
using Tao.Containers;
using Tao.Interfaces;

namespace Tao.UnitTests
{
    [TestFixture]
    public class CLIHeaderStreamTests : BaseStreamTests
    {
        [Test]
        public void ShouldBeAbleToReadMetadataRva()
        {
            var stream = GetStream();
            

            var reader = container.GetInstance<IFunction<Stream, int>>("ReadMetadataRva");
            var rva = reader.Execute(stream);
            Assert.AreEqual(0x2060, rva);
        }

        [Test]
        public void ShouldBeAbleToReadMetadataSize()
        {
            var stream = GetStream();
            

            var reader = container.GetInstance<IFunction<Stream, int>>("ReadMetadataSize");
            var size = reader.Execute(stream);
            Assert.AreEqual(0x11C, size);
        }
    }
}
