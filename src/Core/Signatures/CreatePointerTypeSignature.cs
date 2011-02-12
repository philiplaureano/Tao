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
    /// Represents a class that can create pointer <see cref="TypeSignature"/> instances.
    /// </summary>
    public class CreatePointerTypeSignature : IFunction<ITuple<IFunction<Stream,TypeSignature>, Stream, ElementType>,TypeSignature>
    {
        private readonly IFunction<ITuple<Stream, ICollection<CustomMod>>,int>  _readCustomMods;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePointerTypeSignature"/> class.
        /// </summary>
        public CreatePointerTypeSignature(IFunction<ITuple<Stream, ICollection<CustomMod>>, int> readCustomMods)
        {
            _readCustomMods = readCustomMods;
        }

        /// <summary>
        /// Reads a pointer <see cref="TypeSignature"/> into memory.
        /// </summary>
        /// <param name="input">The type signature reader, input stream, and element type that describes the pointer signature.</param>
        /// <returns>A pointer <see cref="TypeSignature"/>.</returns>
        public TypeSignature Execute(ITuple<IFunction<Stream, TypeSignature>, Stream, ElementType> input)
        {
            var typeSignatureReader = input.Item1;
            var inputStream = input.Item2;
            var elementType = input.Item3;

            if (inputStream.Length == 0)
                throw new ArgumentException("Unexpected end of byte stream", "input");

            var mods = new List<CustomMod>();
            _readCustomMods.Execute(inputStream, mods);

            PointerSignature result = null;

            var nextElementType = (ElementType)inputStream.PeekByte();
            if (nextElementType != ElementType.Void)
            {
                var typePointerSignature = new TypePointerSignature();

                var attachedSignature = typeSignatureReader.Execute(inputStream);
                typePointerSignature.TypeSignature = attachedSignature;
                result = typePointerSignature;
            }
            else
            {
                result = new VoidPointerSignature();
            }

            foreach (var mod in mods)
            {
                result.CustomMods.Add(mod);
            }
            var signature = (TypeSignature)result;
            signature.ElementType = elementType;

            return signature;
        }
    }
}
