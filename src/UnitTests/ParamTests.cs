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

            var param = (TypedParamSignature)reader.Execute(typeBytes);
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

            var param = (TypedParamSignature)reader.Execute(typeBytes);
            Assert.AreEqual(0, param.CustomMods.Count);
            Assert.IsFalse(param.IsByRef);
            Assert.IsNotNull(param.Type);

            TypeSignature typeSignature = param.Type;
            Assert.AreEqual(ElementType.Boolean, typeSignature.ElementType);
        }

        [Test]
        public void ShouldBeAbleToReadSimpleParamWithCustomMods()
        {
            var firstModBytes = GetCustomModBytes(ElementType.CMOD_REQD, 0x49);
            var secondModBytes = GetCustomModBytes(ElementType.CMOD_OPT, 0x49);

            var bytes = new List<byte>();
            bytes.AddRange(firstModBytes);
            bytes.AddRange(secondModBytes);
            bytes.Add(Convert.ToByte(ElementType.Boolean));

            var typeBytes = bytes.ToArray();
            var reader = container.GetInstance<IFunction<IEnumerable<byte>, ParamSignature>>();
            Assert.IsNotNull(reader);

            var param = (TypedParamSignature)reader.Execute(typeBytes);           
            Assert.IsFalse(param.IsByRef);
            Assert.IsNotNull(param.Type);

            TypeSignature typeSignature = param.Type;
            Assert.AreEqual(ElementType.Boolean, typeSignature.ElementType);

            var customMods = param.CustomMods;
            Assert.AreEqual(2, customMods.Count);
            var firstMod = customMods[0];

            Assert.AreEqual(firstMod.ElementType, ElementType.CMOD_REQD);
            Assert.AreEqual(firstMod.TableId, TableId.TypeRef);
            Assert.AreEqual(firstMod.RowIndex, 0x12);

            var secondMod = customMods[1];
            Assert.AreEqual(secondMod.ElementType, ElementType.CMOD_OPT);
            Assert.AreEqual(secondMod.TableId, TableId.TypeRef);
            Assert.AreEqual(secondMod.RowIndex, 0x12);
        }

        [Test]
        public void ShouldBeAbleToReadTypedByRef()
        {
            var typeBytes = new byte[] { Convert.ToByte(ElementType.TypedByRef) };
            var reader = container.GetInstance<IFunction<IEnumerable<byte>, ParamSignature>>();
            Assert.IsNotNull(reader);

            var param = reader.Execute(typeBytes);
            Assert.IsNotNull(param);
            Assert.IsInstanceOfType(typeof(TypedByRefParam), param);
            Assert.AreEqual(0, param.CustomMods.Count);
            Assert.IsFalse(param.IsByRef);
        }

        private byte[] GetCustomModBytes(ElementType elementType, byte codedToken)
        {
            return new byte[] { Convert.ToByte(elementType), Convert.ToByte(codedToken) };
        }
    }
}
