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
    /// Represents a class that reads <see cref="IMethodSignatureElement"/> instances into memory.
    /// </summary>
    /// <typeparam name="TSignatureElement">The base method signature element type.</typeparam>
    /// <typeparam name="TTypedByRefSignatureElement">The TypedByRef method signature element type.</typeparam>
    /// <typeparam name="TTypedSignatureElement">The typed method signature element type.</typeparam>
    public abstract class MethodSignatureElementReader<TSignatureElement, TTypedByRefSignatureElement, TTypedSignatureElement> : IFunction<Stream, TSignatureElement>
        where TSignatureElement : class, IMethodSignatureElement
        where TTypedByRefSignatureElement : class, ITypedByRefMethodSignatureElement, TSignatureElement
        where TTypedSignatureElement : class, ITypedMethodSignatureElement, TSignatureElement
    {
        private readonly IFunction<Stream, TypeSignature> _typeSignatureReader;
        private readonly IFunction<ITuple<Stream, ICollection<CustomMod>>, int> _readCustomMods;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParamSignatureReader"/> class.
        /// </summary>
        /// <param name="typeSignatureReader">The type signature reader.</param>
        /// <param name="readCustomMods">The <see cref="CustomMod"/> reader.</param>
        protected MethodSignatureElementReader(IFunction<Stream, TypeSignature> typeSignatureReader,
            IFunction<ITuple<Stream, ICollection<CustomMod>>, int> readCustomMods)
        {
            _typeSignatureReader = typeSignatureReader;
            _readCustomMods = readCustomMods;
        }

        /// <summary>
        /// Reads a <see cref="MethodSignatureElement"/> from the given byte stream.
        /// </summary>
        /// <param name="input">The input bytes that contains the given <see cref="MethodSignatureElement"/>.</param>
        /// <returns>A <see cref="IMethodSignatureElement"/> instance.</returns>
        public TSignatureElement Execute(Stream input)
        {
            var nextElement = (ElementType)input.PeekByte();
            var startPosition = input.Position;

            var customMods = new List<CustomMod>();
            int modBytesRead = 0;
            if (nextElement == ElementType.CMOD_OPT || nextElement == ElementType.CMOD_REQD)
            {
                modBytesRead += _readCustomMods.Execute(input, customMods);

                nextElement = (ElementType)input.ReadByte();
            }

            // Point to the end of the custom mod bytes
            var nextPosition = startPosition + modBytesRead;
            input.Seek(nextPosition, SeekOrigin.Begin);

            var results = ReadSignature(input, nextElement, customMods);

            return results;
        }

        /// <summary>
        /// Reads the method signature element.
        /// </summary>
        /// <param name="inputStream">The remaining bytes that will be read by the reader.</param>
        /// <param name="nextElement">The next element type.</param>
        /// <param name="customMods">The list of custom mods read from the given byte array.</param>
        /// <returns>A method signature element that describes the type that was read by the reader.</returns>
        protected virtual TSignatureElement ReadSignature(Stream inputStream, ElementType nextElement, IEnumerable<CustomMod> customMods)
        {
            TTypedSignatureElement result = null;
            var isByRef = nextElement == ElementType.ByRef;
            if (isByRef)
            {
                result = CreateTypedMethodElementSignature(true);
                inputStream.ReadByte();
            }

            if (nextElement == ElementType.TypedByRef)
            {
                var byRefParamSignature = CreateByRefSignature();
                foreach (var mod in customMods)
                {
                    byRefParamSignature.CustomMods.Add(mod);
                }

                return byRefParamSignature;
            }

            var typedParamSignature = result ?? CreateTypedMethodElementSignature(isByRef);
            var typeSignature = ReadTypeSignature(inputStream);
            typedParamSignature.Type = typeSignature;

            foreach (var mod in customMods)
            {
                typedParamSignature.CustomMods.Add(mod);
            }

            return typedParamSignature;
        }

        /// <summary>
        /// Reads the type signature from the given <paramref name="inputStream"/>.
        /// </summary>
        /// <param name="inputStream">The input stream that contains the type signature.</param>
        /// <returns>A <see cref="TypeSignature"/> instance.</returns>
        protected virtual TypeSignature ReadTypeSignature(Stream inputStream)
        {
            return _typeSignatureReader.Execute(inputStream);
        }

        /// <summary>
        /// Creates a TypedByRef method signature element.
        /// </summary>
        /// <returns>A method signature element.</returns>
        protected abstract TTypedByRefSignatureElement CreateByRefSignature();

        /// <summary>
        /// Create a typed method signature element.
        /// </summary>
        /// <param name="isByRef">Determines whether or not the element is passed by reference.</param>
        /// <returns>A typed method signature element.</returns>
        protected abstract TTypedSignatureElement CreateTypedMethodElementSignature(bool isByRef);
    }
}
