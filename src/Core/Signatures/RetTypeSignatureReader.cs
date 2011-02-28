using System.Collections.Generic;
using System.IO;
using Tao.Interfaces;
using Tao.Model;

namespace Tao.Signatures
{
    /// <summary>
    /// Represents a type that can read RetType method signature elements into memory.
    /// </summary>
    public class RetTypeSignatureReader : ParamSignatureReader, IRetTypeSignatureReader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RetTypeSignatureReader"/> class.
        /// </summary>
        /// <param name="typeSignatureReader">The type signature reader.</param>
        /// <param name="readCustomMods">The <see cref="CustomMod"/> reader.</param>
        public RetTypeSignatureReader(IFunction<Stream, TypeSignature> typeSignatureReader, IFunction<ITuple<Stream, ICollection<CustomMod>>, int> readCustomMods) :
            base(typeSignatureReader, readCustomMods)
        {
        }

        /// <summary>
        /// Reads the RetType method signature element.
        /// </summary>
        /// <param name="inputStream">The list of remaining bytes that will be read by the reader.</param>
        /// <param name="nextElement">The next element type.</param>
        /// <param name="customMods">The list of custom mods read from the given byte array.</param>
        /// <returns>A method signature element that describes the type that was read by the reader.</returns>
        protected override MethodSignatureElement ReadSignature(Stream inputStream, ElementType nextElement, IEnumerable<CustomMod> customMods)
        {
            if (nextElement != ElementType.Void)
                return base.ReadSignature(inputStream, nextElement, customMods);

            var typeSignature = new TypeSignature { ElementType = nextElement };
            var result = CreateTypedMethodElementSignature(false);
            result.Type = typeSignature;

            // Move the stream pointer past the void element
            inputStream.ReadByte();

            return result;

        }       
    }
}
