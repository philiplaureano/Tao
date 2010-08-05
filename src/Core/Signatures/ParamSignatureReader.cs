using System;
using System.Collections.Generic;
using System.Text;
using Tao.Interfaces;
using Tao.Model;

namespace Tao.Signatures
{
    /// <summary>
    /// Represents a type that can read <see cref="MethodSignatureElement"/> instances into memory.
    /// </summary>
    public class ParamSignatureReader : MethodSignatureElementReader<MethodSignatureElement, TypedByRefMethodSignatureElement, TypedMethodSignatureElement>, 
        IFunction<IEnumerable<byte>, MethodSignatureElement>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParamSignatureReader"/> class.
        /// </summary>
        /// <param name="typeSignatureReader">The type signature reader.</param>
        /// <param name="readCustomMods">The <see cref="CustomMod"/> reader.</param>
        public ParamSignatureReader(IFunction<IEnumerable<byte>, TypeSignature> typeSignatureReader, IFunction<ITuple<Queue<byte>, ICollection<CustomMod>>, int> readCustomMods) : base(typeSignatureReader, readCustomMods)
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
    }
}
