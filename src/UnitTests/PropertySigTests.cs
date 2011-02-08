using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tao.Containers;
using Tao.Interfaces;
using Tao.Model;

namespace Tao.UnitTests
{
    [TestFixture]
    public class PropertySigTests : BaseStreamTests
    {
        [Test]
        public void ShouldBeAbleToReadPropertySig()
        {
            const byte property = 0x8;
            const byte paramCount = 1;
            const byte type = (byte)ElementType.I4;
            const byte param = (byte)ElementType.String;

            var bytes = new byte[] { property, paramCount, type, param };
            var stream = new MemoryStream(bytes);

            var reader = container.GetInstance<IFunction<Stream, PropertySignature>>();
            Assert.IsNotNull(reader);

            var signature = reader.Execute(stream);
            Assert.IsNotNull(signature);
            Assert.IsFalse(signature.HasThis);
            Assert.AreEqual(0, signature.CustomMods.Count);
            Assert.AreEqual(signature.ParamCount, 1);
            Assert.AreEqual(signature.Type.ElementType, ElementType.I4);
            Assert.AreEqual(signature.Parameters[0].Type.ElementType, ElementType.String);
        }

        [Test]
        public void ShouldBeAbleToReadPropertySigWithHasThisSignature()
        {
            const byte hasThis = 0x20;
            const byte property = 0x28 | hasThis;
            const byte paramCount = 1;
            const byte type = (byte)ElementType.I4;
            const byte param = (byte)ElementType.String;

            var bytes = new byte[] { property, paramCount, type, param };
            var stream = new MemoryStream(bytes);

            var reader = container.GetInstance<IFunction<Stream, PropertySignature>>();
            Assert.IsNotNull(reader);

            var signature = reader.Execute(stream);
            Assert.IsNotNull(signature);
            Assert.IsTrue(signature.HasThis);
            Assert.AreEqual(0, signature.CustomMods.Count);
            Assert.AreEqual(signature.ParamCount, 1);
            Assert.AreEqual(signature.Type.ElementType, ElementType.I4);
            Assert.AreEqual(signature.Parameters[0].Type.ElementType, ElementType.String);
        }
    }
}
