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
    /// Represents a class that creates generic <see cref="TypeSignature"/> instances from a given stream.
    /// </summary>
    public class CreateGenericTypeSignature : IFunction<ITuple<Stream, IFunction<Stream, TypeSignature>, ElementType>, TypeSignature>
    {
        private readonly IFunction<Stream, uint> _readCompressedInteger;
        private readonly IFunction<Stream, TypeDefOrRefEncodedSignature> _createTypeDefOrRefEncodedSignature;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateGenericTypeSignature"/> class.
        /// </summary>
        public CreateGenericTypeSignature(IFunction<Stream, uint> readCompressedInteger, IFunction<Stream, TypeDefOrRefEncodedSignature> createTypeDefOrRefEncodedSignature)
        {
            _readCompressedInteger = readCompressedInteger;
            _createTypeDefOrRefEncodedSignature = createTypeDefOrRefEncodedSignature;
        }

        /// <summary>
        /// Creates a generic <see cref="TypeSignature"/> instance from the given <paramref name="input"/>.
        /// </summary>
        /// <param name="input">The input stream, the type signature reader, and the element type that describes the generic type.</param>
        /// <returns>A generic <see cref="TypeSignature"/> instance.</returns>
        public TypeSignature Execute(ITuple<Stream, IFunction<Stream, TypeSignature>, ElementType> input)
        {
            var inputStream = input.Item1;
            var typeSignatureReader = input.Item2;
            var elementType = input.Item3;

            // TODO: Should the generic element type be passed to the GenericTypeInstance signature?
            var genericElementType = (ElementType)inputStream.ReadByte();

            var signature = new GenericTypeInstance
                                {
                                    ElementType = elementType,
                                    GenericTypeDefinition = _createTypeDefOrRefEncodedSignature.Execute(inputStream)
                                };

            var argumentCount = _readCompressedInteger.Execute(inputStream);
            for (var i = 0; i < argumentCount; i++)
            {
                var typeSignature = typeSignatureReader.Execute(inputStream);
                signature.TypeParameters.Add(typeSignature);
            }

            return signature;
        }
    }
}
