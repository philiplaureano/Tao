using System.IO;
using NUnit.Framework;
using Tao.Interfaces;

namespace Tao.UnitTests
{
    [TestFixture]
    public class PESectionHeaderStreamTests : BaseStreamTests
    {
        [Test]
        public void ShouldBeAbleToReadFirstPESectionHeader()
        {
            var expectedStreamLength = 0x28;
            var expectedStreamPosition = 0x1a0;
            var sectionHeaderIndex = 0;

            TestPESectionHeaderStreamRead(sectionHeaderIndex, expectedStreamLength, expectedStreamPosition);
        }

        [Test]
        public void ShouldBeAbleToReadSecondPESectionHeader()
        {
            var expectedStreamLength = 0x28;
            var expectedStreamPosition = 0x1c8;
            var sectionHeaderIndex = 1;

            TestPESectionHeaderStreamRead(sectionHeaderIndex, expectedStreamLength, expectedStreamPosition);
        }

        private void TestPESectionHeaderStreamRead(int sectionHeaderIndex, int expectedStreamLength, int expectedStreamPosition)
        {
            var stream = GetStream();
            

            Assert.IsTrue(container.Contains(typeof(IFunction<ITuple<int, Stream>, Stream>), "InMemorySubStreamReader"));
            Assert.IsTrue(container.Contains(typeof(IFunction<Stream>), "DataDirectoriesEndSeeker"));
            Assert.IsTrue(container.Contains(typeof(IFunction<ITuple<int, Stream>, Stream>), "PESectionFactory"));
            var factory = (IFunction<ITuple<int, Stream>, Stream>)container.GetInstance(typeof(IFunction<ITuple<int, Stream>, Stream>), "PESectionFactory");
            Assert.IsNotNull(factory);
            
            var subStream = factory.Execute(sectionHeaderIndex, stream);            
            Assert.AreEqual(expectedStreamLength, subStream.Length);            
            Assert.AreEqual(expectedStreamPosition, stream.Position);
        }
    }
}
