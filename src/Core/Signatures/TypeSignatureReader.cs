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
        public TypeSignature Execute(IEnumerable<byte> input)
        {
            var bytes = new List<byte>(input);
            var elementType = (ElementType) bytes[0];
            var signature = new TypeSignature();
            signature.ElementType = elementType;

            return signature;
        }
    }
}
