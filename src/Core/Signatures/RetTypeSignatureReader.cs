using System;
using System.Collections.Generic;
using System.Text;
using Tao.Interfaces;
using Tao.Model;

namespace Tao.Signatures
{
    /// <summary>
    /// Represents a type that can read RetType method signature elements into memory.
    /// </summary>
    public class RetTypeSignatureReader : ParamSignatureReader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RetTypeSignatureReader"/> class.
        /// </summary>
        /// <param name="typeSignatureReader">The type signature reader.</param>
        /// <param name="readCustomMods">The <see cref="CustomMod"/> reader.</param>
        public RetTypeSignatureReader(IFunction<IEnumerable<byte>, TypeSignature> typeSignatureReader, IFunction<ITuple<Queue<byte>, ICollection<CustomMod>>, int> readCustomMods) : 
            base(typeSignatureReader, readCustomMods)
        {
        }

        /// <summary>
        /// Reads the RetType method signature element.
        /// </summary>
        /// <param name="byteQueue">The list of remaining bytes that will be read by the reader.</param>
        /// <param name="nextElement">The next element type.</param>
        /// <param name="customMods">The list of custom mods read from the given byte array.</param>
        /// <param name="modBytesRead">The number of bytes read.</param>
        /// <param name="inputBytes">The original input bytes for the given signature.</param>
        /// <returns>A method signature element that describes the type that was read by the reader.</returns>
        protected override MethodSignatureElement ReadSignature(Queue<byte> byteQueue, ElementType nextElement, IEnumerable<CustomMod> customMods, int modBytesRead, IEnumerable<byte> inputBytes)
        {
            if (nextElement != ElementType.Void)
                return base.ReadSignature(byteQueue, nextElement, customMods, modBytesRead, inputBytes);

            var typeSignature = new TypeSignature {ElementType = ElementType.Void};
            var result = CreateTypedMethodElementSignature(false);
            result.Type = typeSignature;

            return result;
        }
    }
}
