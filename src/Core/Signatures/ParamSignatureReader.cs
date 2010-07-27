using System;
using System.Collections.Generic;
using System.Text;
using Tao.Interfaces;
using Tao.Model;

namespace Tao.Signatures
{
    /// <summary>
    /// Represents a type that can read <see cref="ParamSignature"/> instances into memory.
    /// </summary>
    public class ParamSignatureReader : IFunction<IEnumerable<byte>, ParamSignature>
    {
        private readonly IFunction<IEnumerable<byte>, TypeSignature> _typeSignatureReader;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParamSignatureReader"/> class.
        /// </summary>
        /// <param name="typeSignatureReader">The type signature reader.</param>
        public ParamSignatureReader(IFunction<IEnumerable<byte>, TypeSignature> typeSignatureReader)
        {
            _typeSignatureReader = typeSignatureReader;
        }

        /// <summary>
        /// Reads a <see cref="ParamSignature"/> from the given byte stream.
        /// </summary>
        /// <param name="input">The input bytes.</param>
        /// <returns>A <see cref="ParamSignature"/> instance.</returns>
        public ParamSignature Execute(IEnumerable<byte> input)
        {
            var byteQueue = new Queue<byte>(input);
            var nextElement = (ElementType)byteQueue.Peek();
            var inputBytes = input;
            

            var result = new ParamSignature();
            if (nextElement == ElementType.ByRef)
            {
                byteQueue.Dequeue();
                inputBytes = byteQueue;
                result.IsByRef = true;
            }

            var typeSignature = _typeSignatureReader.Execute(inputBytes);
            result.Type = typeSignature;

            return result;
        }
    }
}
