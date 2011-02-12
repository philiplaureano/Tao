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
    /// Represents a class that can create MVar <see cref="TypeSignature"/> instances from a given stream.
    /// </summary>
    public class CreateMVarTypeSignature : IFunction<ITuple<Stream, ElementType>, TypeSignature>
    {
        private readonly IFunction<Stream, uint> _readCompressedInteger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateMVarTypeSignature"/> class.
        /// </summary>
        public CreateMVarTypeSignature(IFunction<Stream, uint> readCompressedInteger)
        {
            _readCompressedInteger = readCompressedInteger;
        }

        /// <summary>
        /// Reads a MVar <see cref="TypeSignature"/> instance from the <paramref name="input"/> stream.
        /// </summary>
        /// <param name="input">The input stream and element type that describes the MVar type signature.</param>
        /// <returns>An MVar <see cref="TypeSignature"/> instance.</returns>
        public TypeSignature Execute(ITuple<Stream, ElementType> input)
        {
            var inputStream = input.Item1;
            var elementType = input.Item2;

            var argumentIndex = _readCompressedInteger.Execute(inputStream);

            var signature = new MvarSignature
            {
                ArgumentIndex = argumentIndex,
                ElementType = elementType
            };

            return signature;
        }
    }
}
