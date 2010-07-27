using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tao.Interfaces;
using Tao.Model;
using Tao.Containers;
using Tao.Interfaces;

namespace Tao.UnitTests
{
    [TestFixture]
    public class ParamTests : BaseStreamTests
    {
        [Test]
        public void ShouldBeAbleToReadByRefSimpleParam()
        {
            var typeBytes = new byte[] { Convert.ToByte(ElementType.ByRef), Convert.ToByte(ElementType.Boolean) };
            var reader = container.GetInstance<IFunction<IEnumerable<byte>, ParamSignature>>();
            Assert.IsNotNull(reader);

            var param = reader.Execute(typeBytes);
            Assert.AreEqual(0, param.CustomMods.Count);
            Assert.IsTrue(param.IsByRef);
            Assert.IsNotNull(param.Type);

            TypeSignature typeSignature = param.Type;
            Assert.AreEqual(ElementType.Boolean, typeSignature.ElementType);
        }

        [Test]
        public void ShouldBeAbleToReadSimpleParam()
        {
            var typeBytes = new byte[] { Convert.ToByte(ElementType.Boolean) };
            var reader = container.GetInstance<IFunction<IEnumerable<byte>, ParamSignature>>();
            Assert.IsNotNull(reader);

            var param = reader.Execute(typeBytes);
            Assert.AreEqual(0, param.CustomMods.Count);
            Assert.IsFalse(param.IsByRef);
            Assert.IsNotNull(param.Type);

            TypeSignature typeSignature = param.Type;
            Assert.AreEqual(ElementType.Boolean, typeSignature.ElementType);
        }
    }
}
