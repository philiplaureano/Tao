using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;
using Tao.Model;

namespace Tao.Signatures
{
    /// <summary>
    /// Represents a type that can read <see cref="MethodSignatureElement"/> instances into memory.
    /// </summary>
    public class ParamSignatureReader : MethodSignatureElementReader<MethodSignatureElement, TypedByRefMethodSignatureElement, TypedMethodSignatureElement>,
        IParamSignatureReader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParamSignatureReader"/> class.
        /// </summary>
        /// <param name="typeSignatureReader">The type signature reader.</param>
        /// <param name="readCustomMods">The <see cref="CustomMod"/> reader.</param>
        public ParamSignatureReader(IFunction<Stream, TypeSignature> typeSignatureReader, IFunction<ITuple<Stream, ICollection<CustomMod>>, int> readCustomMods)
            : base(typeSignatureReader, readCustomMods)
        {
        }

        /// <summary>
        /// Creates a TypedByRef method signature element.
        /// </summary>
        /// <returns>A method signature element.</returns>
        protected override TypedByRefMethodSignatureElement CreateByRefSignature()
        {
            return new TypedByRefMethodSignatureElement();
        }

        /// <summary>
        /// Create a typed method signature element.
        /// </summary>
        /// <param name="isByRef">Determines whether or not the element is passed by reference.</param>
        /// <returns>A typed method signature element.</returns>
        protected override TypedMethodSignatureElement CreateTypedMethodElementSignature(bool isByRef)
        {
            var result = new TypedMethodSignatureElement();
            result.SetIsByRef(isByRef);

            return result;
        }

        /// <summary>
        /// Reads a <see cref="MethodSignatureElement"/> from the given byte stream.
        /// </summary>
        /// <param name="input">The input bytes that contains the given <see cref="MethodSignatureElement"/>.</param>
        /// <returns>A <see cref="IMethodSignatureElement"/> instance.</returns>
        public new ITypedMethodSignatureElement Execute(Stream input)
        {
            return (ITypedMethodSignatureElement) base.Execute(input);
        }
    }
}
