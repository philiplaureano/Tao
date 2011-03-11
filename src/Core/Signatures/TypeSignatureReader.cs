using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly IFunction<ITuple<Stream, ElementType>, TypeSignature> _createVarTypeSignature;
        private readonly IFunction<ITuple<Stream, ElementType>, TypeSignature> _createClassOrValueTypeSignature;

        private readonly IFunction<ITuple<IFunction<Stream, TypeSignature>, Stream, ElementType>, TypeSignature>
            _createPointerTypeSignature;

        private readonly IFunction<ITuple<IFunction<Stream, TypeSignature>, Stream, ElementType>, TypeSignature>
            _createSzArrayTypeSignature;

        private readonly IFunction<ITuple<Stream, IFunction<Stream, TypeSignature>, ElementType>, TypeSignature>
            _createGenericTypeSignature;

        private readonly IFunction<ITuple<Stream, ElementType>, TypeSignature> _createMVarTypeSignature;
        private readonly IFunction<ITuple<Stream, IMethodSignatureStreamReader<IMethodRefSignature>, IMethodSignatureStreamReader<IMethodSignature>>, TypeSignature>
            _createFunctionPointerSignature;

        private readonly IFunction<ITuple<Stream, IFunction<Stream, TypeSignature>>, TypeSignature>
            _createArrayTypeSignature;

        private readonly IFunction<ITuple<Stream, ElementType>, TypeSignature> _createPrimitiveTypeSignature;

        private IMethodSignatureStreamReader<IMethodRefSignature> _methodRefSignatureStreamReader;
        private IMethodSignatureStreamReader<IMethodSignature> _methodDefSignatureStreamReader;

        private static readonly HashSet<ElementType> PrimitiveTypes = new HashSet<ElementType>
                                                                          {
                                                                        ElementType.Boolean,
                                                                        ElementType.Char,
                                                                        ElementType.I1,
                                                                        ElementType.I2,
                                                                        ElementType.I4,
                                                                        ElementType.I8,
                                                                        ElementType.U1,
                                                                        ElementType.U2,
                                                                        ElementType.U4,
                                                                        ElementType.U8,
                                                                        ElementType.R4,
                                                                        ElementType.R8
                                                                    };

        private IMicroContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeSignatureReader"/> class.
        /// </summary>
        /// <param name="createVarTypeSignature">The object that reads VAR <see cref="TypeSignature"/> instances into memory.</param>
        /// <param name="createPointerTypeSignature">The object that reads pointer type signatures into memory.</param>
        /// <param name="createGenericTypeSignature">The object that creates generic <see cref="TypeSignature"/> instances from a given stream.</param>
        /// <param name="createClassOrValueTypeSignature">The object that creates class or value <see cref="TypeSignature"/> instances from the given stream. </param>
        /// <param name="createSzArrayTypeSignature">The instance that reads SZArray <see cref="TypeSignature"/> instances into memory.</param>
        /// <param name="createMVarTypeSignature">The instance that reads an MVar <see cref="TypeSignature"/> instance from the input stream.</param>
        /// <param name="createFunctionPointerSignature">The object that creates a function pointer <see cref="TypeSignature"/> instance from the given stream.</param>
        /// <param name="createArrayTypeSignature">The object that can create single-dimensional array type signature instances.</param>
        /// <param name="createPrimitiveTypeSignature">The object that creates primitive <see cref="TypeSignature"/> instances.</param>
        public TypeSignatureReader(IFunction<ITuple<Stream, ElementType>,
            TypeSignature> createVarTypeSignature,
            IFunction<ITuple<IFunction<Stream, TypeSignature>, Stream, ElementType>, TypeSignature> createPointerTypeSignature,
            IFunction<ITuple<Stream, IFunction<Stream, TypeSignature>, ElementType>, TypeSignature> createGenericTypeSignature,
            IFunction<ITuple<Stream, ElementType>, TypeSignature> createClassOrValueTypeSignature,
            IFunction<ITuple<IFunction<Stream, TypeSignature>, Stream, ElementType>, TypeSignature> createSzArrayTypeSignature,
            IFunction<ITuple<Stream, ElementType>, TypeSignature> createMVarTypeSignature,
            IFunction<ITuple<Stream, IMethodSignatureStreamReader<IMethodRefSignature>, IMethodSignatureStreamReader<IMethodSignature>>, TypeSignature> createFunctionPointerSignature,
            IFunction<ITuple<Stream, IFunction<Stream, TypeSignature>>, TypeSignature> createArrayTypeSignature,
            IFunction<ITuple<Stream, ElementType>, TypeSignature> createPrimitiveTypeSignature)
        {
            _createClassOrValueTypeSignature = createClassOrValueTypeSignature;
            _createPrimitiveTypeSignature = createPrimitiveTypeSignature;
            _createArrayTypeSignature = createArrayTypeSignature;
            _createFunctionPointerSignature = createFunctionPointerSignature;
            _createMVarTypeSignature = createMVarTypeSignature;
            _createSzArrayTypeSignature = createSzArrayTypeSignature;
            _createGenericTypeSignature = createGenericTypeSignature;
            _createPointerTypeSignature = createPointerTypeSignature;
            _createVarTypeSignature = createVarTypeSignature;
        }

        /// <summary>
        /// Reads the type signature from the given <paramref name="input"/> bytes.
        /// </summary>
        /// <param name="input">The bytes that represent the type signature.</param>
        /// <returns>A <see cref="TypeSignature"/> instance.</returns>
        public TypeSignature Execute(Stream input)
        {
            var elementType = (ElementType)input.ReadByte();

            if (PrimitiveTypes.Contains(elementType))
                return _createPrimitiveTypeSignature.Execute(input, elementType);

            if (elementType == ElementType.Array)
                return _createArrayTypeSignature.Execute(input, this);

            if (elementType == ElementType.Class)
                return _createClassOrValueTypeSignature.Execute(input, elementType);

            if (elementType == ElementType.FnPtr)
            {
                // HACK: Resolve the MethodDef/MethodRef stream readers at runtime as a workaround
                // for the recursive dependency errors
                if (_methodRefSignatureStreamReader == null)
                    _methodRefSignatureStreamReader = _container.GetInstance<IMethodSignatureStreamReader<IMethodRefSignature>>();

                if (_methodDefSignatureStreamReader == null)
                    _methodDefSignatureStreamReader = _container.GetInstance<IMethodSignatureStreamReader<IMethodSignature>>();

                return _createFunctionPointerSignature.Execute(input, _methodRefSignatureStreamReader, _methodDefSignatureStreamReader);
            }

            if (elementType == ElementType.GenericInst)
                return _createGenericTypeSignature.Execute(input, this, elementType);

            if (elementType == ElementType.Mvar)
                return _createMVarTypeSignature.Execute(input, elementType);

            if (elementType == ElementType.Object)
                return new TypeSignature { ElementType = ElementType.Object };

            if (elementType == ElementType.Ptr)
                return _createPointerTypeSignature.Execute(this, input, elementType);

            if (elementType == ElementType.String)
                return new TypeSignature { ElementType = ElementType.String };

            if (elementType == ElementType.SzArray)
                return _createSzArrayTypeSignature.Execute(this, input, elementType);

            if (elementType == ElementType.ValueType)
                return _createClassOrValueTypeSignature.Execute(input, elementType);

            if (elementType == ElementType.Var)
                return _createVarTypeSignature.Execute(input, elementType);

            throw new NotSupportedException(string.Format("Element type not supported: {0}", elementType));
        }

        /// <summary>
        /// Adds additional late-bound services to the current object instance.
        /// </summary>
        /// <param name="container">The container itself.</param>
        public void Initialize(IMicroContainer container)
        {
            _container = container;
        }
    }
}
