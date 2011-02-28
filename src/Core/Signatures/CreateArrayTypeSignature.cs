using System.IO;
using Tao.Interfaces;
using Tao.Model;

namespace Tao.Signatures
{
    /// <summary>
    /// Represents a type that can create single-dimensional array type signatures.
    /// </summary>
    public class CreateArrayTypeSignature : IFunction<ITuple<Stream, IFunction<Stream, TypeSignature>>, TypeSignature>
    {
        private readonly IFunction<Stream, ArrayShape> _arrayShapeReader;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateArrayTypeSignature"/> class.
        /// </summary>
        /// <param name="arrayShapeReader">The <see cref="ArrayShape"/> reader.</param>
        public CreateArrayTypeSignature(IFunction<Stream, ArrayShape> arrayShapeReader)
        {
            _arrayShapeReader = arrayShapeReader;
        }

        /// <summary>
        /// Creates a single-dimensional array type signature.
        /// </summary>
        /// <param name="input">The input stream and the type signature reader.</param>
        /// <returns>A <see cref="TypeSignature"/> instance.</returns>
        public TypeSignature Execute(ITuple<Stream, IFunction<Stream, TypeSignature>> input)
        {
            var inputStream = input.Item1;
            var typeSignatureReader = input.Item2;
            var arrayType = typeSignatureReader.Execute(inputStream);

            var shape = _arrayShapeReader.Execute(inputStream);
            var signature = new MultiDimensionalArraySignature { ArrayType = arrayType, Shape = shape };

            return signature;
        }
    }
}
