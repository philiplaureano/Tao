using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Hiro.Containers;
using Tao.Containers;
using Tao.Interfaces;
using Tao.Model;

namespace Tao.Signatures
{
    /// <summary>
    /// Represents a type that reads type signatures.
    /// </summary>
    public class TypeSignatureReader : IFunction<Stream, TypeSignature>, IInitialize
    {
        private readonly IFunction<byte, ITuple<TableId, uint>> _typeDefOrRefEncodedReader;
        private readonly IFunction<ITuple<Stream, ICollection<CustomMod>>, int> _readCustomMods;
        private readonly IFunction<Stream, ArrayShape> _arrayShapeReader;
        private readonly IFunction<Stream, uint> _readCompressedInteger;

        private readonly Dictionary<ElementType, Func<Stream, TypeSignature>> _primitiveEntries =
            new Dictionary<ElementType, Func<Stream, TypeSignature>>();

        private IMethodSignatureStreamReader<IMethodRefSignature> _methodRefSignatureStreamReader;
        private IMethodSignatureStreamReader<IMethodSignature> _methodDefSignatureStreamReader;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeSignatureReader"/> class.
        /// </summary>
        /// <param name="typeDefOrRefEncodedReader">The reader that will read the embbedded type def or type ref token.</param>
        /// <param name="readCustomMods">The reader that will read the CustomMod signatures.</param>
        /// <param name="arrayShapeReader">The reader that will read the ArrayShape signatures.</param>
        /// <param name="readCompressedInteger">The reader that will read compressed signed integers.</param>
        public TypeSignatureReader(IFunction<byte, ITuple<TableId, uint>> typeDefOrRefEncodedReader,
            IFunction<ITuple<Stream, ICollection<CustomMod>>, int> readCustomMods, 
            IFunction<Stream, ArrayShape> arrayShapeReader, 
            IFunction<Stream, uint> readCompressedInteger)
        {
            if (typeDefOrRefEncodedReader == null)
                throw new ArgumentNullException("typeDefOrRefEncodedReader");

            _typeDefOrRefEncodedReader = typeDefOrRefEncodedReader;            
            _readCustomMods = readCustomMods;
            _arrayShapeReader = arrayShapeReader;
            _readCompressedInteger = readCompressedInteger;

            CreateEntries();
        }

        private void CreateEntries()
        {
            Func<Stream, TypeSignature> defaultCreator = bytes => new TypeSignature();
            _primitiveEntries[ElementType.Boolean] = defaultCreator;
            _primitiveEntries[ElementType.Char] = defaultCreator;
            _primitiveEntries[ElementType.I1] = defaultCreator;
            _primitiveEntries[ElementType.I2] = defaultCreator;
            _primitiveEntries[ElementType.I4] = defaultCreator;
            _primitiveEntries[ElementType.I8] = defaultCreator;
            _primitiveEntries[ElementType.U1] = defaultCreator;
            _primitiveEntries[ElementType.U4] = defaultCreator;
            _primitiveEntries[ElementType.U8] = defaultCreator;
            _primitiveEntries[ElementType.R4] = defaultCreator;
            _primitiveEntries[ElementType.R8] = defaultCreator;
        }

        /// <summary>
        /// Reads the type signature from the given <paramref name="input"/> bytes.
        /// </summary>
        /// <param name="input">The bytes that represent the type signature.</param>
        /// <returns>A <see cref="TypeSignature"/> instance.</returns>
        public TypeSignature Execute(Stream input)
        {
            var elementType = (ElementType)input.ReadByte();

            if (_primitiveEntries.ContainsKey(elementType))
                return CreatePrimitiveType(input, elementType, _primitiveEntries);

            if (elementType == ElementType.Array)
                return CreateArray(input, this, _arrayShapeReader);

            if (elementType == ElementType.Class)
                return CreateClass(input, elementType, this._typeDefOrRefEncodedReader);

            if (elementType == ElementType.FnPtr)
                return CreateFunctionPointerSignature(input, _methodDefSignatureStreamReader, _methodRefSignatureStreamReader);

            if (elementType == ElementType.GenericInst)
                return CreateGenericType(input, this, elementType, this._readCompressedInteger, this._typeDefOrRefEncodedReader);

            if (elementType == ElementType.Mvar)
                return CreateMVar(this, input, elementType, _readCompressedInteger);

            if (elementType == ElementType.Object)
                return new TypeSignature() { ElementType = ElementType.Object };

            if (elementType == ElementType.Ptr)
                return CreatePointerSignature(this, input, elementType, _readCustomMods);

            if (elementType == ElementType.String)
                return new TypeSignature() { ElementType = ElementType.String };

            if (elementType == ElementType.SzArray)
                return CreateSzArray(this, input, elementType, _readCustomMods);

            if (elementType == ElementType.ValueType)
                return CreateValueType(this, input, elementType);

            if (elementType == ElementType.Var)
                return CreateVar(input, elementType, _readCompressedInteger);

            throw new NotSupportedException(string.Format("Element type not supported: {0}", elementType));
        }

        private static TypeSignature CreateVar(Stream input, ElementType elementType, IFunction<Stream, uint> readCompressedInteger)
        {
            var argumentIndex = readCompressedInteger.Execute(input);

            var signature = new VarSignature
                                {
                                    ArgumentIndex = argumentIndex,
                                    ElementType = elementType
                                };

            return signature;
        }

        private static TypeSignature CreatePointerSignature(TypeSignatureReader typeSignatureReader, Stream input, ElementType elementType, IFunction<ITuple<Stream, ICollection<CustomMod>>, int> readCustomMods)
        {
            var signature = typeSignatureReader.CreatePointerSignature(input, readCustomMods);
            signature.ElementType = elementType;

            return signature;
        }

        private static TypeSignature CreateClass(Stream input, ElementType elementType, IFunction<byte, ITuple<TableId, uint>> typeDefOrRefEncodedReader)
        {
            var signature = CreateTypeDefOrRefEncodedSignature(input, typeDefOrRefEncodedReader);
            signature.ElementType = elementType;

            return signature;
        }

        private static TypeSignature CreateValueType(TypeSignatureReader typeSignatureReader, Stream input, ElementType elementType)
        {
            var signature = CreateTypeDefOrRefEncodedSignature(input, typeSignatureReader._typeDefOrRefEncodedReader);
            signature.ElementType = elementType;
            return signature;
        }

        private static TypeSignature CreateSzArray(IFunction<Stream, TypeSignature> typeSignatureReader, Stream input, ElementType elementType, IFunction<ITuple<Stream, ICollection<CustomMod>>, int> readCustomMods)
        {
            var signature = new SzArraySignature { ElementType = elementType };

            readCustomMods.Execute(input, signature.CustomMods);
            signature.ArrayElementType = typeSignatureReader.Execute(input);

            return signature;
        }

        private static TypeSignature CreateMVar(TypeSignatureReader typeSignatureReader, Stream input, ElementType elementType, IFunction<Stream, uint> readCompressedInteger)
        {
            var argumentIndex = readCompressedInteger.Execute(input);

            var signature = new MvarSignature
                                {
                                    ArgumentIndex = argumentIndex,
                                    ElementType = elementType
                                };
            return signature;
        }

        private static TypeSignature CreateGenericType(Stream input, IFunction<Stream, TypeSignature> typeSignatureReader, ElementType elementType, IFunction<Stream, uint> readCompressedInteger, IFunction<byte, ITuple<TableId, uint>> typeDefOrRefEncodedReader)
        {
            // TODO: Should the generic element type be passed to the GenericTypeInstance signature?
            var genericElementType = (ElementType)input.ReadByte();

            var signature = new GenericTypeInstance();
            signature.ElementType = elementType;
            signature.GenericTypeDefinition = CreateTypeDefOrRefEncodedSignature(input, typeDefOrRefEncodedReader);

            var argumentCount = readCompressedInteger.Execute(input);
            for (var i = 0; i < argumentCount; i++)
            {
                var typeSignature = typeSignatureReader.Execute(input);
                signature.TypeParameters.Add(typeSignature);
            }

            return signature;
        }

        private static TypeSignature CreateFunctionPointerSignature(Stream input, IMethodSignatureStreamReader<IMethodSignature> methodDefSignatureStreamReader, IMethodSignatureStreamReader<IMethodRefSignature> methodRefSignatureStreamReader)
        {
            var flags = (MethodSignatureFlags)input.PeekByte();
            var isVarArg = (flags & MethodSignatureFlags.VarArg) != 0;

            // Use the MethodRefSignatureStreamReader for VARARG methods
            var streamReader = isVarArg ? methodRefSignatureStreamReader : methodDefSignatureStreamReader;
            var targetMethod = streamReader.ReadSignature(input);

            var functionPointer = new FunctionPointerSignature
                                      {
                                          ElementType = ElementType.FnPtr,
                                          TargetMethod = targetMethod
                                      };

            return functionPointer;
        }

        private static TypeSignature CreateArray(Stream input, IFunction<Stream, TypeSignature> typeSignatureReader, IFunction<Stream, ArrayShape> arrayShapeReader)
        {
            var arrayType = typeSignatureReader.Execute(input);
            var shape = arrayShapeReader.Execute(input);
            var signature = new MultiDimensionalArraySignature { ArrayType = arrayType, Shape = shape };

            return signature;
        }

        private static TypeSignature CreatePrimitiveType(Stream input, ElementType elementType, Dictionary<ElementType, Func<Stream, TypeSignature>> primitiveEntries)
        {
            var createSignature = primitiveEntries[elementType];
            var signature = createSignature(input);
            signature.ElementType = elementType;
            return signature;
        }

        private TypeSignature CreatePointerSignature(Stream inputStream, IFunction<ITuple<Stream, ICollection<CustomMod>>, int> readCustomMods)
        {
            if (inputStream.Length == 0)
                throw new ArgumentException("Unexpected end of byte stream", "inputStream");

            var mods = new List<CustomMod>();
            readCustomMods.Execute(inputStream, mods);

            PointerSignature result = null;

            var nextElementType = (ElementType)inputStream.PeekByte();
            if (nextElementType != ElementType.Void)
            {
                result = CreateTypePointerSignature(this, inputStream);
            }
            else
            {
                result = new VoidPointerSignature();
            }

            foreach (var mod in mods)
            {
                result.CustomMods.Add(mod);
            }

            return result;
        }

        private static PointerSignature CreateTypePointerSignature(IFunction<Stream, TypeSignature> typeSignatureReader, Stream inputStream)
        {
            PointerSignature result;
            var typePointerSignature = new TypePointerSignature();

            var attachedSignature = typeSignatureReader.Execute(inputStream);
            typePointerSignature.TypeSignature = attachedSignature;
            result = typePointerSignature;

            return result;
        }

        private static TypeDefOrRefEncodedSignature CreateTypeDefOrRefEncodedSignature(Stream inputStream, IFunction<byte, ITuple<TableId, uint>> typeDefOrRefEncodedReader)
        {
            var nextByte = (byte)inputStream.ReadByte();
            var decodedToken = typeDefOrRefEncodedReader.Execute(nextByte);
            var encodedSignature = new TypeDefOrRefEncodedSignature
                                       {
                                           TableId = decodedToken.Item1,
                                           TableIndex = decodedToken.Item2
                                       };

            return encodedSignature;
        }

        public void Initialize(IMicroContainer container)
        {
            // HACK: Resolve the MethodDef/MethodRef stream readers at runtime as a workaround
            // for the recursive dependency errors
            _methodRefSignatureStreamReader = container.GetInstance<IMethodSignatureStreamReader<IMethodRefSignature>>();
            _methodDefSignatureStreamReader = container.GetInstance<IMethodSignatureStreamReader<IMethodSignature>>();
        }
    }
}
