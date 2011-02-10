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
    /// Represents a type that reads <see cref="MethodSpec"/> object instances into memory from a given stream.
    /// </summary>
    public class MethodSpecReader : IFunction<Stream, MethodSpec>
    {
        private readonly IFunction<Stream, TypeSignature> _typeSignatureReader;
        private readonly IFunction<Stream, uint> _readCompressedInteger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodSpecReader"/> class.
        /// </summary>
        public MethodSpecReader(IFunction<Stream, TypeSignature> typeSignatureReader, IFunction<Stream, uint> readCompressedInteger)
        {
            _typeSignatureReader = typeSignatureReader;
            _readCompressedInteger = readCompressedInteger;
        }

        /// <summary>
        /// Reads <see cref="MethodSpec"/> object instances into memory from a given stream.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <returns>A <see cref="MethodSpec"/> instance.</returns>
        public MethodSpec Execute(Stream input)
        {
            var genericElementType = (ElementType)input.ReadByte();
            var argumentCount = _readCompressedInteger.Execute(input);

            var signature = new MethodSpec();
            signature.GenericTypeDefinition = _typeSignatureReader.Execute(input);
            
            for (var i = 0; i < argumentCount; i++)
            {
                var typeSignature = _typeSignatureReader.Execute(input);
                signature.TypeParameters.Add(typeSignature);
            }

            return signature;
        }
    }
}
