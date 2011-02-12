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
    /// Represents a class that reads VAR <see cref="TypeSignature"/> instances into memory.
    /// </summary>
    public class CreateVarTypeSignature : IFunction<ITuple<Stream, ElementType>, TypeSignature>
    {
        private readonly IFunction<Stream, uint> _readCompressedInteger;
        
        ///<summary>
        /// Initializes a new instance of the <see cref="CreateVarTypeSignature"/> class.
        ///</summary>
        ///<param name="readCompressedInteger">The object that will read compressed integers into memory.</param>
        public CreateVarTypeSignature(IFunction<Stream, uint> readCompressedInteger)
        {
            _readCompressedInteger = readCompressedInteger;
        }

        public TypeSignature Execute(ITuple<Stream, ElementType> input)
        {
            var inputStream = input.Item1;
            var elementType = input.Item2;
            var argumentIndex = _readCompressedInteger.Execute(inputStream);

            var signature = new VarSignature
            {
                ArgumentIndex = argumentIndex,
                ElementType = elementType
            };

            return signature;
        }
    }
}
