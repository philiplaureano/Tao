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
            var container = CreateContainer();

            var resolver = (IFunction<ITuple<int, Stream>, int>) container.GetInstance(typeof (IFunction<ITuple<int, Stream>, int>), "ResolveAbsoluteFilePositionFromRva");
            Assert.IsNotNull(resolver);
            
            // Use the CLR header RVA
            var rva = 0x2008;
            var expectedPosition = 0x208;
            var position = resolver.Execute(rva, stream);

            Assert.AreEqual(expectedPosition, position);
        }
    }
}
