using System.IO;
using NUnit.Framework;
using Tao.Interfaces;

namespace Tao.UnitTests
{
    [TestFixture]
    public class PEFileHeaderStreamTests : BaseStreamTests
    {
        [Test]
        public void ShouldBeAbleToReadPEHeader()
        {
            TestRead("PEFileHeaderStreamFactory", 0x96, 0x12);
        }

        [Test]
        public void ShouldBeAbleToReadTheNumberOfPESections()
        {
            var stream = GetStream();
            

            var sectionCounter = (IFunction<Stream, int>)container.GetInstance(typeof(IFunction<Stream, int>), "PESectionCounter");
            Assert.IsNotNull(sectionCounter);

            var result = sectionCounter.Execute(stream);
            Assert.AreEqual(2, result);
        }
    }
}
