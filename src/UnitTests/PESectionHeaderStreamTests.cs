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
    public class PESectionHeaderStreamTests : BaseStreamTests
    {
        [Test]
        public void ShouldBeAbleToReadFirstPESectionHeader()
        {
            var stream = GetStream();
            var container = CreateContainer();

            Assert.IsTrue(container.Contains(typeof(ISubStreamReader), "InMemorySubStreamReader"));
            Assert.IsTrue(container.Contains(typeof(IStreamSeeker), "DataDirectoriesEndSeeker"));
            Assert.IsTrue(container.Contains(typeof(IFunction<ITuple<int, Stream>, Stream>), "PESectionFactory"));
            var factory = (IFunction<ITuple<int, Stream>, Stream>)container.GetInstance(typeof(IFunction<ITuple<int, Stream>, Stream>), "PESectionFactory");
            Assert.IsNotNull(factory);

            var subStream = factory.Execute(Tuple.New(0, stream));
            Assert.AreEqual(0x22, subStream.Length);
            Assert.AreEqual(0x19a, stream.Position);
        }
    }
}
