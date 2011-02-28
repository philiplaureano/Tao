using System;
using System.IO;
using NUnit.Framework;
using Tao.Containers;
using Tao.Interfaces;
using Tao.Model;

namespace Tao.UnitTests
{
    [TestFixture]
    public class RetTypeSignatureTests : BaseStreamTests
    {
        [Test]
        public void ShouldBeAbleToReadVoidRetType()
        {
            var typeBytes = new[] { Convert.ToByte(ElementType.Void) };
            var stream = new MemoryStream(typeBytes);
            var reader = container.GetInstance<IFunction<Stream, MethodSignatureElement>>("RetTypeSignatureReader");
            Assert.IsNotNull(reader);

            var param = (TypedMethodSignatureElement)reader.Execute(stream);
            Assert.IsNotNull(param);
            Assert.AreEqual(0, param.CustomMods.Count);
            Assert.IsFalse(param.IsByRef);
            Assert.IsNotNull(param.Type);

            TypeSignature typeSignature = param.Type;
            Assert.AreEqual(ElementType.Void, typeSignature.ElementType);
        }
    }
}
