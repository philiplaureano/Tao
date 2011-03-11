using System.IO;
using NUnit.Framework;

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
