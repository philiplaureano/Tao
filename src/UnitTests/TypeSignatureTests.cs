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
    public class TypeSignatureTests : BaseStreamTests
    {
        [Test]
        public void ShouldReadBooleanType()
        {
            var elementType = ElementType.Boolean;
            TestElementTypeRead(elementType);
        }

        [Test]
        public void ShouldReadCharType()
        {
            var elementType = ElementType.Char;
            TestElementTypeRead(elementType);
        }
        [Test]
        public void ShouldReadI1Type()
        {
            var elementType = ElementType.I1;
            TestElementTypeRead(elementType);
        }

        [Test]
        public void ShouldReadI2Type()
        {
            var elementType = ElementType.I2;
            TestElementTypeRead(elementType);
        }
        [Test]
        public void ShouldReadI4Type()
        {
            var elementType = ElementType.I4;
            TestElementTypeRead(elementType);
        }
        [Test]
        public void ShouldReadI8Type()
        {
            var elementType = ElementType.I8;
            TestElementTypeRead(elementType);
        }
        [Test]
        public void ShouldReadU1Type()
        {
            var elementType = ElementType.U1;
            TestElementTypeRead(elementType);
        }

        [Test]
        public void ShouldReadU4Type()
        {
            var elementType = ElementType.U4;
            TestElementTypeRead(elementType);
        }
        [Test]
        public void ShouldReadU8Type()
        {
            var elementType = ElementType.U8;
            TestElementTypeRead(elementType);
        }

        [Test]
        public void ShouldReadR4Type()
        {
            var elementType = ElementType.R4;
            TestElementTypeRead(elementType);
        }

        [Test]
        public void ShouldReadR8Type()
        {
            var elementType = ElementType.R8;
            TestElementTypeRead(elementType);
        }

        [Test]
        public void ShouldReadStringType()
        {
            var elementType = ElementType.String;
            TestElementTypeRead(elementType);
        }

        [Test]
        public void ShouldReadObjectType()
        {
            var elementType = ElementType.Object;
            TestElementTypeRead(elementType);
        }

        [Test]
        public void ShouldReadTypeDefOrRefEncodedSignature()
        {
            const byte token = 0x49;
            var expectedTableId = TableId.TypeRef;
            uint expectedIndex = 0x12;
            var elementType = ElementType.Class;

            var bytes = new byte[] { Convert.ToByte(elementType), token };
            var stream = new MemoryStream(bytes);

            var reader = container.GetInstance<IFunction<Stream, TypeSignature>>();
            Assert.IsNotNull(reader);

            var result = reader.Execute(stream);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(typeof(TypeDefOrRefEncodedSignature), result);

            var encodedSignature = (TypeDefOrRefEncodedSignature)result;
            Assert.AreEqual(elementType, encodedSignature.ElementType);
            Assert.AreEqual(expectedTableId, encodedSignature.TableId);
            Assert.AreEqual(expectedIndex, encodedSignature.TableIndex);
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

            var reader = container.GetInstance<IFunction<Stream, TypeSignature>>();
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
        public void ShouldReadTypePointerSignature()
        {
            var bytes = new byte[] { Convert.ToByte(ElementType.Ptr), Convert.ToByte(ElementType.I4) };
            var stream = new MemoryStream(bytes);

            var reader = container.GetInstance<IFunction<Stream, TypeSignature>>();
            Assert.IsNotNull(reader);

            var signature = reader.Execute(stream);

            Assert.IsNotNull(signature);
            Assert.IsInstanceOfType(typeof(TypePointerSignature), signature);

            var typePointer = (TypePointerSignature)signature;
            TypeSignature typeSignature = typePointer.TypeSignature;

            Assert.AreEqual(ElementType.I4, typeSignature.ElementType);
        }

        [Test]
        public void ShouldReadVoidPointerSignature()
        {
            var bytes = new byte[] { Convert.ToByte(ElementType.Ptr), Convert.ToByte(ElementType.Void) };

            var stream = new MemoryStream(bytes);
            var reader = container.GetInstance<IFunction<Stream, TypeSignature>>();
            Assert.IsNotNull(reader);

            var result = reader.Execute(stream);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(typeof(VoidPointerSignature), result);

            var signature = (VoidPointerSignature)result;
            Assert.AreEqual(signature.ElementType, ElementType.Ptr);
        }

        [Test]
        public void ShouldReadMethodDefFunctionPointer()
        {
            var flags = MethodSignatureFlags.HasThis | MethodSignatureFlags.Generic;
            byte genericParameters = 2;
            byte normalParameters = 2;
            byte returnType = Convert.ToByte(ElementType.Void);
            var parameterTypes = new byte[] { Convert.ToByte(ElementType.I4), Convert.ToByte(ElementType.Object) };

            // Create the FunctionPointerSignature
            var bytes = new List<byte> { Convert.ToByte(ElementType.FnPtr), Convert.ToByte(flags), genericParameters, normalParameters, returnType };
            bytes.AddRange(parameterTypes);

            var reader = container.GetInstance<IFunction<Stream, TypeSignature>>();
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

            var reader = container.GetInstance<IFunction<Stream, TypeSignature>>();
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

            var reader = container.GetInstance<IFunction<Stream, TypeSignature>>();
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
        public void ShouldBeAbleToReadMvar()
        {
            var reader = container.GetInstance<IFunction<Stream, TypeSignature>>();
            Assert.IsNotNull(reader);

            byte genericArgumentIndex = 0;
            var bytes = new byte[] { Convert.ToByte(ElementType.Mvar), genericArgumentIndex };
            var stream = new MemoryStream(bytes);

            var signature = reader.Execute(stream) as MvarSignature;
            Assert.IsNotNull(signature);
            Assert.AreEqual(signature.ElementType, ElementType.Mvar);
            Assert.AreEqual(signature.ArgumentIndex, genericArgumentIndex);            
        }

        private byte[] GetCustomModBytes(ElementType elementType, byte codedToken)
        {
            return new byte[] { Convert.ToByte(elementType), Convert.ToByte(codedToken) };
        }

        private void TestElementTypeRead(ElementType elementType)
        {
            var bytes = new byte[] { Convert.ToByte(elementType) };
            var stream = new MemoryStream(bytes);

            var reader = container.GetInstance<IFunction<Stream, TypeSignature>>();
            Assert.IsNotNull(reader);

            var result = reader.Execute(stream);

            Assert.AreEqual(elementType, result.ElementType);
        }
    }
}
