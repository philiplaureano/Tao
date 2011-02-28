using System;
using System.IO;
using NUnit.Framework;
using Tao.Containers;
using Tao.Interfaces;
using Tao.Model;

namespace Tao.UnitTests
{
    [TestFixture]
    public class LocalVarSigTests : BaseStreamTests
    {
        [Test]
        public void ShouldReadByRefVariable()
        {
            byte localSig = 0x7;
            byte variableCount = 1;
            var byRef = (byte)ElementType.ByRef;
            var variableType = (byte)ElementType.I4;

            var bytes = new[] { localSig, variableCount, byRef, variableType };
            var stream = new MemoryStream(bytes);

            var reader = container.GetInstance<IFunction<Stream, LocalVarSignature>>();
            Assert.IsNotNull(reader);

            var signature = reader.Execute(stream);
            Assert.IsNotNull(signature);

            var localVariableCount = signature.LocalVariables.Count;
            Assert.AreEqual(1, localVariableCount);

            var variable = (TypedLocalVariable)signature.LocalVariables[0];
            Assert.IsNotNull(variable);
            Assert.IsFalse(variable.IsPinned);
            Assert.AreEqual(0, variable.CustomMods.Count);
            Assert.AreEqual(0, variable.Index);
            Assert.IsTrue(variable.IsByRef);

            ITypeSignature type = variable.Type;
            Assert.IsNotNull(type);

            var elementType = type.ElementType;
            Assert.AreEqual(ElementType.I4, elementType);
        }

        [Test]
        public void ShouldBeAbleToReadMultipleVariablesInLocalVarSigStream()
        {
            byte localSig = 0x7;
            byte variableCount = 2;
            var byRef = (byte)ElementType.ByRef;

            var bytes = new[] { localSig, variableCount, byRef, (byte)ElementType.I4, byRef, (byte)ElementType.I8 };
            var stream = new MemoryStream(bytes);

            var reader = container.GetInstance<IFunction<Stream, LocalVarSignature>>();
            Assert.IsNotNull(reader);

            var signature = reader.Execute(stream);
            Assert.IsNotNull(signature);

            var localVariableCount = signature.LocalVariables.Count;
            Assert.AreEqual(variableCount, localVariableCount);

            var variable = (TypedLocalVariable)signature.LocalVariables[0];
            Assert.IsNotNull(variable);
            Assert.IsFalse(variable.IsPinned);
            Assert.AreEqual(0, variable.CustomMods.Count);
            Assert.AreEqual(0, variable.Index);
            Assert.IsTrue(variable.IsByRef);

            ITypeSignature type = variable.Type;
            Assert.IsNotNull(type);

            var elementType = type.ElementType;
            Assert.AreEqual(ElementType.I4, elementType);

            variable = (TypedLocalVariable)signature.LocalVariables[1];
            Assert.IsNotNull(variable);
            Assert.IsFalse(variable.IsPinned);
            Assert.AreEqual(0, variable.CustomMods.Count);
            Assert.AreEqual(0, variable.Index);
            Assert.IsTrue(variable.IsByRef);

            type = variable.Type;
            Assert.IsNotNull(type);

            elementType = type.ElementType;
            Assert.AreEqual(ElementType.I8, elementType);
        }

        [Test]
        public void ShouldBeAbleToReadTypedByRefVariable()
        {
            byte localSig = 0x7;
            byte variableCount = 1;

            var bytes = new[] { localSig, variableCount, (byte)ElementType.TypedByRef };
            var stream = new MemoryStream(bytes);

            var reader = container.GetInstance<IFunction<Stream, LocalVarSignature>>();
            Assert.IsNotNull(reader);

            var signature = reader.Execute(stream);
            Assert.IsNotNull(signature);

            var localVariableCount = signature.LocalVariables.Count;
            Assert.AreEqual(variableCount, localVariableCount);

            var variable = signature.LocalVariables[0];
            Assert.IsNotNull(variable);
            Assert.IsInstanceOfType(typeof(TypedByRefVariable), variable);
        }

        [Test]
        [ExpectedException(typeof(BadImageFormatException))]
        public void ShouldThrowBadImageFormatExceptionIfNoLocalSigConstantIsFound()
        {
            byte badLocalSig = 0xFE;
            byte variableCount = 1;
            var byRef = (byte)ElementType.ByRef;
            var variableType = (byte)ElementType.I4;

            var bytes = new[] { badLocalSig, variableCount, byRef, variableType };
            var stream = new MemoryStream(bytes);

            var reader = container.GetInstance<IFunction<Stream, LocalVarSignature>>();
            Assert.IsNotNull(reader);

            var signature = reader.Execute(stream);
            Assert.IsNotNull(signature);
        }
    }
}
