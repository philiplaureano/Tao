using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Tao.Interfaces;
using Tao.Model;

namespace Tao.Signatures
{
    /// <summary>
    /// Represents a class that reads <see cref="ITypeSpecification"/> objects into memory.
    /// </summary>
    public class TypeSpecReader : IFunction<Stream, ITypeSpecification>
    {
        private static readonly HashSet<ElementType> SupportedElementTypes = new HashSet<ElementType>()
                                                                                  {
                                                                                     ElementType.Array,
                                                                                     ElementType.FnPtr,
                                                                                     ElementType.GenericInst,
                                                                                     ElementType.Ptr,
                                                                                     ElementType.SzArray,
                                                                                  };

        private readonly IFunction<Stream, TypeSignature> _typeSignatureReader;
        private readonly IMethodSignatureStreamReader<IMethodSignature> _methodDefSignatureStreamReader;
        private readonly IMethodSignatureStreamReader<IMethodRefSignature> _methodRefSignatureStreamReader;
        private readonly IFunction<ITuple<Stream, IMethodSignatureStreamReader<IMethodRefSignature>, IMethodSignatureStreamReader<IMethodSignature>>, TypeSignature> _createFunctionPointerSignature;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeSpecReader"/> class.
        /// </summary>
        /// <param name="typeSignatureReader">The type signature reader that will be used to instantiate each individual type specification.</param>
        /// <param name="methodDefSignatureStreamReader">The method definition signature stream reader.</param>
        /// <param name="methodRefSignatureStreamReader">The method reference signature stream reader.</param>
        /// <param name="createFunctionPointerSignature">The factory object that will create the function pointer signature.</param>
        public TypeSpecReader(IFunction<Stream, TypeSignature> typeSignatureReader,
            IMethodSignatureStreamReader<IMethodSignature> methodDefSignatureStreamReader,
            IMethodSignatureStreamReader<IMethodRefSignature> methodRefSignatureStreamReader,
            IFunction<ITuple<Stream, IMethodSignatureStreamReader<IMethodRefSignature>, IMethodSignatureStreamReader<IMethodSignature>>, TypeSignature> createFunctionPointerSignature)
        {
            _typeSignatureReader = typeSignatureReader;
            _methodRefSignatureStreamReader = methodRefSignatureStreamReader;
            _createFunctionPointerSignature = createFunctionPointerSignature;
            _methodDefSignatureStreamReader = methodDefSignatureStreamReader;
        }

        /// <summary>
        /// Reads a <see cref="ITypeSpecification"/> object into memory.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <returns>A type specification.</returns>
        public ITypeSpecification Execute(Stream input)
        {
            var elementType = (ElementType)input.PeekByte();
            if (!SupportedElementTypes.Contains(elementType))
                throw new NotSupportedException(string.Format("Element type not supported: {0}", elementType));

            if (elementType == ElementType.FnPtr)
            {
                // Skip the FNPTR byte
                input.Seek(1, SeekOrigin.Current);
                return (ITypeSpecification)_createFunctionPointerSignature.Execute(input, _methodRefSignatureStreamReader, _methodDefSignatureStreamReader);
            }
                
            return (ITypeSpecification)_typeSignatureReader.Execute(input);
        }
    }
}
