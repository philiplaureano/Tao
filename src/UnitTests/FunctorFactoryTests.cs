using System;
using System.Collections.Generic;
using System.Text;
using Hiro;
using Hiro.Containers;
using Moq;
using NUnit.Framework;
using Tao.Interfaces;

namespace Tao.UnitTests
{
    [TestFixture]
    public class FunctorFactoryTests
    {
        [Test]
        public void ShouldCallFunctorInstanceWhenConvertedToFactory()
        {
            var dummyList = new List<int>();

            var targetlist = dummyList;
            Func<int> functor = () =>
            {
                targetlist.Add(42);
                return 12345;
            };

            var container = new MicroContainer();
            var converter = container.GetInstance<IFunctorFactoryConverter>();
            Assert.IsNotNull(converter);

            var convertedFactory = converter.ConvertFrom(functor);
            var result = convertedFactory.Create();
            Assert.AreEqual(12345, result);
            Assert.IsTrue(targetlist.Contains(42));
        }
    }
}
