using System.IO;
using NUnit.Framework;
using Tao.Containers;
using Tao.Interfaces;

namespace Tao.UnitTests
{
    [TestFixture]
    public class MetadataRootTests : BaseStreamTests
    {
        [Test]
        public void ShouldBeAbleToSeekMetadataRootPosition()
        {
            var stream = GetStream();
            

            var seeker = container.GetInstance<IFunction<Stream>>("SeekMetadataRootPosition");
            Assert.IsNotNull(seeker);

            seeker.Execute(stream);
            Assert.AreEqual(0x260, stream.Position);
        }

        [Test]
        public void ShouldBeAbleToReadMetadataStreamCount()
        {
            var stream = GetStream();
            

            var reader = container.GetInstance<IFunction<Stream, int>>("ReadMetadataStreamCount");
            Assert.IsNotNull(reader);

            var count = reader.Execute(stream);
            Assert.AreEqual(5, count);
        }

        [Test]
        public void ShouldBeAbleToReadVersionStringFromMetadataRoot()
        {
            var stream = GetStream();
            

            var reader = container.GetInstance<IFunction<Stream, string>>("ReadVersionString");
            Assert.IsNotNull(reader);

            var result = reader.Execute(stream);
            Assert.AreEqual("v2.0.50727", result);
        }
    }
}
