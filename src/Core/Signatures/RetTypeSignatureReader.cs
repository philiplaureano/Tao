using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
        /// <param name="modBytesRead">The number of bytes read.</param>
        /// <returns>A method signature element that describes the type that was read by the reader.</returns>
        protected override MethodSignatureElement ReadSignature(Stream inputStream, ElementType nextElement, IEnumerable<CustomMod> customMods, int modBytesRead)
        {
            if (nextElement != ElementType.Void)
                return base.ReadSignature(inputStream, nextElement, customMods, modBytesRead);

            var typeSignature = new TypeSignature { ElementType = ElementType.Void };
            var result = CreateTypedMethodElementSignature(false);
            result.Type = typeSignature;

            return result;

        }
        /// <summary>
        /// Reads a <see cref="MethodSignatureElement"/> from the given byte stream.
        /// </summary>
        /// <param name="input">The input bytes.</param>
        /// <returns>A <see cref="ITypedMethodSignatureElement"/> instance.</returns>
        public new ITypedMethodSignatureElement Execute(Stream input)
        {
            var result = (ITypedMethodSignatureElement)base.Execute(input);

            return result;
        }
    }
}
