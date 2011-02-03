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
        public TypeSignatureReader(IFunction<byte, ITuple<TableId, uint>> typeDefOrRefEncodedReader,
            IFunction<ITuple<Stream, ICollection<CustomMod>>, int> readCustomMods, IFunction<Stream, ArrayShape> arrayShapeReader)
        {
            if (typeDefOrRefEncodedReader == null)
                throw new ArgumentNullException("typeDefOrRefEncodedReader");

            _typeDefOrRefEncodedReader = typeDefOrRefEncodedReader;
            //_methodDefSignatureReader = methodDefSignatureReader;
            //_methodRefSignatureReader = methodRefSignatureReader;
            _readCustomMods = readCustomMods;
            _arrayShapeReader = arrayShapeReader;

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
            {
                var createSignature = _primitiveEntries[elementType];
                var signature = createSignature(input);
                signature.ElementType = elementType;
                return signature;
            }

            if (elementType == ElementType.Array)
            {
                var arrayType = Execute(input);
                var shape = _arrayShapeReader.Execute(input);
                var signature = new MultiDimensionalArraySignature { ArrayType = arrayType, Shape = shape };

                return signature;
            }

            if (elementType == ElementType.Class)
            {
                var signature = CreateTypeDefOrRefEncodedSignature(input);
                signature.ElementType = elementType;

                return signature;
            }

            if (elementType == ElementType.FnPtr)
            {
                var flags = (MethodSignatureFlags)input.PeekByte();
                var isVarArg = (flags & MethodSignatureFlags.VarArg) != 0;

                // Use the MethodRefSignatureStreamReader for VARARG methods
                var streamReader = isVarArg ? _methodRefSignatureStreamReader : _methodDefSignatureStreamReader;
                var targetMethod = streamReader.ReadSignature(input);

                var functionPointer = new FunctionPointerSignature
                                          {
                                              ElementType = ElementType.FnPtr,
                                              TargetMethod = targetMethod
                                          };

                return functionPointer;
            }

            if (elementType == ElementType.GenericInst)
            {
                // TODO: Should the generic element type be passed to the GenericTypeInstance signature?
                var genericElementType = (ElementType)input.ReadByte();

                var signature = new GenericTypeInstance();
                signature.ElementType = elementType;
                signature.GenericTypeDefinition = CreateTypeDefOrRefEncodedSignature(input);

                // TODO: Read the argument count as a compressed integer rather than a byte
                var argumentCount = (int)input.ReadByte();
                for (var i = 0; i < argumentCount; i++)
                {
                    var typeSignature = Execute(input);
                    signature.TypeParameters.Add(typeSignature);
                }

                return signature;
            }

            if (elementType == ElementType.Mvar)
            {
                // TODO: Read the argument index as a compressed integer rather than a byte
                var argumentIndex = (uint)input.ReadByte();

                var signature = new MvarSignature
                                    {
                                        ArgumentIndex = argumentIndex,
                                        ElementType = elementType
                                    };
                return signature;
            }

            if (elementType == ElementType.Object)
                return new TypeSignature() { ElementType = ElementType.Object };

            if (elementType == ElementType.Ptr)
            {
                var signature = CreatePointerSignature(input);
                signature.ElementType = elementType;

                return signature;
            }

            if (elementType == ElementType.String)
                return new TypeSignature() { ElementType = ElementType.String };

            if (elementType == ElementType.SzArray)
            {
                var signature = new SzArraySignature { ElementType = elementType };

                _readCustomMods.Execute(input, signature.CustomMods);
                signature.ArrayElementType = Execute(input);

                return signature;
            }

            if (elementType == ElementType.ValueType)
            {
                var signature = CreateTypeDefOrRefEncodedSignature(input);
                signature.ElementType = elementType;
                return signature;
            }

            if (elementType == ElementType.Var)
            {
                // TODO: Read the argument index as a compressed integer rather than a byte
                var argumentIndex = (uint)input.ReadByte();

                var signature = new VarSignature
                {
                    ArgumentIndex = argumentIndex,
                    ElementType = elementType
                };

                return signature;
            }

            throw new NotSupportedException(string.Format("Element type not supported: {0}", elementType));
        }

        private TypeSignature CreatePointerSignature(Stream inputStream)
        {
            if (inputStream.Length == 0)
                throw new ArgumentException("Unexpected end of byte stream", "inputStream");

            var mods = new List<CustomMod>();
            _readCustomMods.Execute(inputStream, mods);

            PointerSignature result = null;

            var nextElementType = (ElementType)inputStream.PeekByte();
            if (nextElementType != ElementType.Void)
            {
                result = CreateTypePointerSignature(inputStream);
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

        private PointerSignature CreateTypePointerSignature(Stream inputStream)
        {
            PointerSignature result;
            var typePointerSignature = new TypePointerSignature();

            var attachedSignature = Execute(inputStream);
            typePointerSignature.TypeSignature = attachedSignature;
            result = typePointerSignature;

            return result;
        }

        private TypeDefOrRefEncodedSignature CreateTypeDefOrRefEncodedSignature(Stream inputStream)
        {
            var nextByte = (byte)inputStream.ReadByte();
            var decodedToken = _typeDefOrRefEncodedReader.Execute(nextByte);
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
