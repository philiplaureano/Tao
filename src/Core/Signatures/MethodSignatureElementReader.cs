using System;
using System.Collections.Generic;
using System.Text;
using Tao.Interfaces;
using Tao.Model;

namespace Tao.Signatures
{
    public abstract class MethodSignatureElementReader<TSignatureElement, TTypedByRefSignatureElement, TTypedSignatureElement> 
        where TSignatureElement : class, IMethodSignatureElement
        where TTypedByRefSignatureElement : class, ITypedByRefMethodSignatureElement, TSignatureElement
        where TTypedSignatureElement : class, ITypedMethodSignatureElement, TSignatureElement
    {
        private IFunction<IEnumerable<byte>, TypeSignature> _typeSignatureReader;
        private IFunction<ITuple<Queue<byte>, ICollection<CustomMod>>, int> _readCustomMods;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParamSignatureReader"/> class.
        /// </summary>
        /// <param name="typeSignatureReader">The type signature reader.</param>
        /// <param name="readCustomMods">The <see cref="CustomMod"/> reader.</param>
        protected MethodSignatureElementReader(IFunction<IEnumerable<byte>, TypeSignature> typeSignatureReader, IFunction<ITuple<Queue<byte>, ICollection<CustomMod>>, int> readCustomMods)
        {
            _typeSignatureReader = typeSignatureReader;
            _readCustomMods = readCustomMods;
        }

        /// <summary>
        /// Reads a <see cref="ParamSignature"/> from the given byte stream.
        /// </summary>
        /// <param name="input">The input bytes.</param>
        /// <returns>A <see cref="IMethodSignatureElement"/> instance.</returns>
        public TSignatureElement Execute(IEnumerable<byte> input)
        {
            var byteQueue = new Queue<byte>(input);
            var nextElement = (ElementType)byteQueue.Peek();
            var inputBytes = input;

            var customMods = new List<CustomMod>();
            int modBytesRead = 0;
            if (nextElement == ElementType.CMOD_OPT || nextElement == ElementType.CMOD_REQD)
            {
                modBytesRead = _readCustomMods.Execute(byteQueue, customMods);
                var items = new Queue<byte>(input);

                for (var i = 0; i < modBytesRead; i++)
                {
                    items.Dequeue();
                }

                byteQueue = items;
                nextElement = (ElementType)byteQueue.Peek();
            }

            return ReadSignature(byteQueue, nextElement, customMods, modBytesRead, inputBytes);
        }

        /// <summary>
        /// Reads the method signature element.
        /// </summary>
        /// <param name="byteQueue">The list of remaining bytes that will be read by the reader.</param>
        /// <param name="nextElement">The next element type.</param>
        /// <param name="customMods">The list of custom mods read from the given byte array.</param>
        /// <param name="modBytesRead">The number of bytes read.</param>
        /// <param name="inputBytes">The original input bytes for the given signature.</param>
        /// <returns>A method signature element that describes the type that was read by the reader.</returns>
        protected virtual TSignatureElement ReadSignature(Queue<byte> byteQueue, ElementType nextElement, IEnumerable<CustomMod> customMods, int modBytesRead, IEnumerable<byte> inputBytes)
        {
            TTypedSignatureElement result = null;
            var isByRef = nextElement == ElementType.ByRef;
            if (isByRef)
            {
                result = CreateTypedMethodElementSignature(isByRef);
                byteQueue.Dequeue();
                inputBytes = byteQueue;
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

            inputBytes = modBytesRead > 0 ? byteQueue : inputBytes;
            var typedParamSignature = result ?? CreateTypedMethodElementSignature(isByRef);
            var typeSignature = _typeSignatureReader.Execute(inputBytes);
            typedParamSignature.Type = typeSignature;

            foreach (var mod in customMods)
            {
                typedParamSignature.CustomMods.Add(mod);
            }

            return typedParamSignature;
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
