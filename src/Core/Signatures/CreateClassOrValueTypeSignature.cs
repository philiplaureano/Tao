using System.IO;
using Tao.Interfaces;
using Tao.Model;

namespace Tao.Signatures
{
    /// <summary>
    /// Represents a class that can create <see cref="TypeSignature">class or value type signatures</see> from a given stream.
    /// </summary>
    public class CreateClassOrValueTypeSignature : IFunction<ITuple<Stream, ElementType>, TypeSignature>
    {
        private readonly IFunction<Stream, TypeDefOrRefEncodedSignature> _createTypeDefOrRefEncodedSignature;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateClassOrValueTypeSignature"/> class.
        /// </summary>
        /// <param name="createTypeDefOrRefEncodedSignature"></param>
        public CreateClassOrValueTypeSignature(IFunction<Stream, TypeDefOrRefEncodedSignature> createTypeDefOrRefEncodedSignature)
        {
            _createTypeDefOrRefEncodedSignature = createTypeDefOrRefEncodedSignature;
        }

        /// <summary>
        /// Creates a class or value <see cref="TypeSignature"/> from the given <paramref name="input"/> stream.
        /// </summary>
        /// <param name="input">The input stream and element type that describes the class or value type signature.</param>
        /// <returns></returns>
        public TypeSignature Execute(ITuple<Stream, ElementType> input)
        {
            var inputStream = input.Item1;
            var elementType = input.Item2;
            var signature = _createTypeDefOrRefEncodedSignature.Execute(inputStream);
            signature.ElementType = elementType;

            return signature;
        }
    }
}
