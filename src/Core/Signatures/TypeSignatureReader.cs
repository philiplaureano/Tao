using System;
using System.Collections.Generic;
using System.Text;
using Tao.Interfaces;
using Tao.Model;

namespace Tao.Signatures
{
    /// <summary>
    /// Represents a type that reads type signatures.
    /// </summary>
    public class TypeSignatureReader : IFunction<IEnumerable<byte>, TypeSignature>
    {
        private readonly IFunction<byte, ITuple<TableId, uint>> _typeDefOrRefEncodedReader;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeSignatureReader"/> class.
        /// </summary>
        /// <param name="typeDefOrRefEncodedReader">The reader that will read the embbedded type def or type ref token.</param>
        public TypeSignatureReader(IFunction<byte, ITuple<TableId, uint>> typeDefOrRefEncodedReader)
        {
            _typeDefOrRefEncodedReader = typeDefOrRefEncodedReader;
        }

        public TypeSignature Execute(IEnumerable<byte> input)
        {
            var bytes = new List<byte>(input);
            var elementType = (ElementType) bytes[0];
            TypeSignature signature;
            if (elementType == ElementType.Class || elementType == ElementType.ValueType)
            {
                var decodedToken = _typeDefOrRefEncodedReader.Execute(bytes[1]);
                var encodedSignature = new TypeDefOrRefEncodedSignature
                                           {
                                               TableId = decodedToken.Item1,
                                               TableIndex = decodedToken.Item2
                                           };

                signature = encodedSignature;
            }
            else
            {
                signature = new TypeSignature();    
            }            
            
            signature.ElementType = elementType;

            
            return signature;
        }
    }
}
