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
    public class RetTypeSignatureTests : BaseStreamTests
    {
        [Test]
        public void ShouldBeAbleToReadVoidRetType()
        {
            var typeBytes = new byte[] { Convert.ToByte(ElementType.Void) };
            var reader = container.GetInstance<IFunction<IEnumerable<byte>, MethodSignatureElement>>("RetTypeSignatureReader");
            Assert.IsNotNull(reader);

            var param = (TypedMethodSignatureElement)reader.Execute(typeBytes);
            Assert.IsNotNull(param);
            Assert.AreEqual(0, param.CustomMods.Count);
            Assert.IsFalse(param.IsByRef);
            Assert.IsNotNull(param.Type);

            TypeSignature typeSignature = param.Type;
            Assert.AreEqual(ElementType.Void, typeSignature.ElementType);
        }
    }
}
