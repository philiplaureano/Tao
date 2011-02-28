using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Tao.Containers;
using Tao.Interfaces;
using Tao.Model;

namespace Tao.UnitTests
{
    [TestFixture]
    public class TypeSpecTests : BaseStreamTests
    {
        [Test]
        [ExpectedException(typeof(NotSupportedException))]
        public void ShouldThrowNotSupportedExceptionOnInvalidElement()
        {
            var bytes = new[] {(byte) ElementType.I4};
            var stream = new MemoryStream(bytes);
            var reader = container.GetInstance<IFunction<Stream, ITypeSpecification>>();
            Assert.IsNotNull(reader);

            var result = reader.Execute(stream);            
        }

        [Test]
        public void ShouldReadVoidPointerSignature()
        {
            var bytes = new[] { Convert.ToByte(ElementType.Ptr), Convert.ToByte(ElementType.Void) };

            var stream = new MemoryStream(bytes);
            var reader = container.GetInstance<IFunction<Stream, ITypeSpecification>>();
            Assert.IsNotNull(reader);

            var result = reader.Execute(stream);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(typeof(VoidPointerSignature), result);

            var signature = (VoidPointerSignature)result;
            Assert.AreEqual(signature.ElementType, ElementType.Ptr);
        }

        [Test]
        public void ShouldReadVoidPointerSignatureWithCustomModSignaturesAttached()
        {
            var customModBytes = GetCustomModBytes(ElementType.CMOD_REQD, 0x49);

            // Add the PTR head element
            var byteList = new List<byte> { Convert.ToByte(ElementType.Ptr) };

            // Add the custom signatures
            byteList.AddRange(customModBytes);
            byteList.AddRange(customModBytes);
            byteList.Add(Convert.ToByte(ElementType.Void));

            var bytes = byteList.ToArray();
            var stream = new MemoryStream(bytes);

            var reader = container.GetInstance<IFunction<Stream, ITypeSpecification>>();
            Assert.IsNotNull(reader);

            var result = reader.Execute(stream);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(typeof(VoidPointerSignature), result);

            var signature = (VoidPointerSignature)result;
            Assert.AreEqual(signature.ElementType, ElementType.Ptr);

            var customMods = signature.CustomMods;
            Assert.IsTrue(customMods.Count == 2);

            var firstMod = customMods[0];

            Assert.AreEqual(firstMod.ElementType, ElementType.CMOD_REQD);
            Assert.AreEqual(firstMod.TableId, TableId.TypeRef);
            Assert.AreEqual(firstMod.RowIndex, 0x12);
        }

        [Test]
        public void ShouldReadMethodDefFunctionPointer()
        {
            var flags = MethodSignatureFlags.HasThis | MethodSignatureFlags.Generic;
            byte genericParameters = 2;
            byte normalParameters = 2;
            byte returnType = Convert.ToByte(ElementType.Void);
            var parameterTypes = new[] { Convert.ToByte(ElementType.I4), Convert.ToByte(ElementType.Object) };

            // Create the FunctionPointerSignature
            var bytes = new List<byte> { Convert.ToByte(ElementType.FnPtr), Convert.ToByte(flags), genericParameters, normalParameters, returnType };
            bytes.AddRange(parameterTypes);

            var reader = container.GetInstance<IFunction<Stream, ITypeSpecification>>();
            Assert.IsNotNull(reader);

            FunctionPointerSignature functionPointerSignature = reader.Execute(new MemoryStream(bytes.ToArray())) as FunctionPointerSignature;
            Assert.IsNotNull(functionPointerSignature);

            IMethodSignature targetMethod = functionPointerSignature.TargetMethod;
            Assert.IsNotNull(targetMethod);

            Assert.IsTrue(targetMethod.HasThis);
            Assert.IsNotNull(targetMethod.IsGeneric);
            Assert.AreEqual(targetMethod.GenericParameterCount, genericParameters);
            Assert.AreEqual(targetMethod.ParameterCount, normalParameters);
            Assert.AreEqual(targetMethod.ReturnType.Type.ElementType, (ElementType)returnType);
            Assert.AreEqual(targetMethod.Parameters[0].Type.ElementType, ElementType.I4);
            Assert.AreEqual(targetMethod.Parameters[1].Type.ElementType, ElementType.Object);
        }

        [Test]
        public void ShouldBeAbleToReadGenericTypeInstanceSignature()
        {
            var elementType = ElementType.GenericInst;
            var objectType = ElementType.Class;
            byte encodedToken = 0x9;
            byte genArgCount = 1;
            var firstGenericParameterType = ElementType.I4;

            var bytes = new List<byte>
                            {
                                Convert.ToByte(elementType),
                                Convert.ToByte(objectType),
                                encodedToken,
                                genArgCount,
                                Convert.ToByte(firstGenericParameterType)
                            };

            var reader = container.GetInstance<IFunction<Stream, ITypeSpecification>>();
            Assert.IsNotNull(reader);

            var stream = new MemoryStream(bytes.ToArray());
            var signature = reader.Execute(stream) as GenericTypeInstance;
            Assert.IsNotNull(signature);
            Assert.AreEqual(ElementType.GenericInst, signature.ElementType);

            // The TypeDefOrRefEncoded index should be pointing to the TypeRef table, row 2
            var genericType = signature.GenericTypeDefinition;
            Assert.IsNotNull(genericType);
            Assert.AreEqual(genericType.TableId, TableId.TypeRef);
            Assert.AreEqual(genericType.TableIndex, 2);

            Assert.AreEqual(1, signature.TypeParameters.Count);
            Assert.AreEqual(ElementType.I4, signature.TypeParameters[0].ElementType);
        }

        [Test]
        public void ShouldBeAbleToReadMultiDimensionalArraySignature()
        {
            var elementType = ElementType.Array;
            var currentType = ElementType.I4;
            byte dimensions = 3;
            byte numSizes = 0;
            byte lowerBounds = 0;

            var bytes = new[] 
            {
                Convert.ToByte(elementType), 
                Convert.ToByte(currentType),
                Convert.ToByte(dimensions),
                Convert.ToByte(numSizes),
                Convert.ToByte(lowerBounds)
            };

            var reader = container.GetInstance<IFunction<Stream, ITypeSpecification>>();
            Assert.IsNotNull(reader);

            var result = reader.Execute(new MemoryStream(bytes));
            Assert.IsNotNull(result);

            var arraySignature = result as MultiDimensionalArraySignature;
            Assert.IsNotNull(arraySignature);

            TypeSignature arrayType = arraySignature.ArrayType;
            Assert.IsNotNull(arrayType);
            Assert.AreEqual(arrayType.ElementType, ElementType.I4);

            ArrayShape shape = arraySignature.Shape;
            Assert.IsNotNull(shape);

            var currentSizes = new List<uint>(shape.Sizes);
            var currentLoBounds = new List<uint>(shape.LoBounds);

            Assert.AreEqual(dimensions, shape.Rank);
            Assert.AreEqual(numSizes, shape.NumSizes);
            Assert.AreEqual(0, currentSizes.Count);
            Assert.AreEqual(0, currentLoBounds.Count);
        }

        [Test]
        public void ShouldBeAbleToReadSZArray()
        {
            var elementType = ElementType.SzArray;
            var arrayElementType = ElementType.I4;

            var bytes = new[] { Convert.ToByte(elementType), Convert.ToByte(arrayElementType) };
            var stream = new MemoryStream(bytes);

            var reader = container.GetInstance<IFunction<Stream, TypeSignature>>();
            Assert.IsNotNull(reader);

            var signature = reader.Execute(stream) as SzArraySignature;
            Assert.IsNotNull(signature);
            Assert.AreEqual(signature.ElementType, ElementType.SzArray);

            var arrayElementTypeSignature = signature.ArrayElementType;
            Assert.IsNotNull(arrayElementTypeSignature);
            Assert.AreEqual(arrayElementTypeSignature.ElementType, ElementType.I4);

            Assert.AreEqual(0, signature.CustomMods.Count);
        }
        private byte[] GetCustomModBytes(ElementType elementType, byte codedToken)
        {
            return new[] { Convert.ToByte(elementType), Convert.ToByte(codedToken) };
        }

        private void TestElementTypeRead(ElementType elementType)
        {
            var bytes = new[] { Convert.ToByte(elementType) };
            var stream = new MemoryStream(bytes);

            var reader = container.GetInstance<IFunction<Stream, ITypeSpecification>>();
            Assert.IsNotNull(reader);

            var result = reader.Execute(stream);

            Assert.AreEqual(elementType, result.ElementType);
        }
    }
}
