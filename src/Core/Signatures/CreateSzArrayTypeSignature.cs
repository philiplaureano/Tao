using System.Collections.Generic;
using System.IO;
using Tao.Interfaces;
using Tao.Model;

namespace Tao.Signatures
{
    /// <summary>
    /// Represents a class that reads SZArray <see cref="TypeSignature"/> instances into memory.
    /// </summary>
    public class CreateSzArrayTypeSignature : IFunction<ITuple<IFunction<Stream, TypeSignature>, Stream, ElementType>, TypeSignature>
    {
        private readonly IFunction<ITuple<Stream, ICollection<CustomMod>>, int> _readCustomMods;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateSzArrayTypeSignature"/> class.
        /// </summary>
        /// <param name="readCustomMods">The custom mod reader.</param>
        public CreateSzArrayTypeSignature(IFunction<ITuple<Stream, ICollection<CustomMod>>, int> readCustomMods)
        {
            _readCustomMods = readCustomMods;
        }

        /// <summary>
        /// Reads an <see cref="TypeSignature">SZArray signature</see> instance into memory.
        /// </summary>
        /// <param name="input">The type signature reader, input stream, and element type that describe the type signature.</param>
        /// <returns>An <see cref="TypeSignature">SZArray signature.</see></returns>
        public TypeSignature Execute(ITuple<IFunction<Stream, TypeSignature>, Stream, ElementType> input)
        {            
            var typeSignatureReader = input.Item1;
            var inputStream = input.Item2;
            var elementType = input.Item3;
            var signature = new SzArraySignature { ElementType = elementType };

            _readCustomMods.Execute(inputStream, signature.CustomMods);
            signature.ArrayElementType = typeSignatureReader.Execute(inputStream);

            return signature;
        }
    }
}
