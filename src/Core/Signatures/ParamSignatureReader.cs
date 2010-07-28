using System;
using System.Collections.Generic;
using System.Text;
using Tao.Interfaces;
using Tao.Model;

namespace Tao.Signatures
{
    /// <summary>
    /// Represents a type that can read <see cref="ParamSignature"/> instances into memory.
    /// </summary>
    public class ParamSignatureReader : IFunction<IEnumerable<byte>, ParamSignature>
    {
        private readonly IFunction<IEnumerable<byte>, TypeSignature> _typeSignatureReader;
        private readonly IFunction<ITuple<Queue<byte>, ICollection<CustomMod>>, int> _readCustomMods;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParamSignatureReader"/> class.
        /// </summary>
        /// <param name="typeSignatureReader">The type signature reader.</param>
        /// <param name="readCustomMods">The <see cref="CustomMod"/> reader.</param>
        public ParamSignatureReader(IFunction<IEnumerable<byte>, TypeSignature> typeSignatureReader, IFunction<ITuple<Queue<byte>, ICollection<CustomMod>>, int> readCustomMods)
        {
            _typeSignatureReader = typeSignatureReader;
            _readCustomMods = readCustomMods;
        }

        /// <summary>
        /// Reads a <see cref="ParamSignature"/> from the given byte stream.
        /// </summary>
        /// <param name="input">The input bytes.</param>
        /// <returns>A <see cref="ParamSignature"/> instance.</returns>
        public ParamSignature Execute(IEnumerable<byte> input)
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
                
                for(var i = 0; i < modBytesRead; i++)
                {
                    items.Dequeue();
                }
                   
                byteQueue = items;                
                nextElement = (ElementType)byteQueue.Peek();
            }

            TypedParamSignature result = null;                       
            var isByRef = nextElement == ElementType.ByRef;
            if (isByRef)
            {
                result = new TypedParamSignature(true);
                byteQueue.Dequeue();
                inputBytes = byteQueue;
            }

            if (nextElement == ElementType.TypedByRef)
            {
                var byRefParamSignature = new TypedByRefParamSignature();
                foreach(var mod in customMods)
                {
                    byRefParamSignature.CustomMods.Add(mod);
                }

                return byRefParamSignature;
            }

            inputBytes = modBytesRead > 0 ? byteQueue : inputBytes;
            var typedParamSignature = result ?? new TypedParamSignature(false);
            var typeSignature = _typeSignatureReader.Execute(inputBytes);
            typedParamSignature.Type = typeSignature;

            foreach (var mod in customMods)
            {
                typedParamSignature.CustomMods.Add(mod);
            }

            return typedParamSignature;
        }
    }
}
