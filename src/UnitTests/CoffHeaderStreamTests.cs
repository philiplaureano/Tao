using System;
using System.IO;
using NUnit.Framework;
using Tao.Interfaces;
using Tao.Containers;

namespace Tao.UnitTests
{
    [TestFixture]
    public class CoffHeaderStreamTests : BaseStreamTests
    {
        [Test]
        public void ShouldBeAbleToReadNumberOfSections()
        {
            var stream = GetStream();
            var reader = container.GetInstance<IFunction<Stream, int>>("ReadSectionCount");
            Assert.IsNotNull(reader);

            var result = reader.Execute(stream);
            Assert.AreEqual(2, result);
        }
    }
}
