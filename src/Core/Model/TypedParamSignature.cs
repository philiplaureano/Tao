using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Model
{
    /// <summary>
    /// Represents a <see cref="ParamSignature"/> that has an associated type.
    /// </summary>
    public class TypedParamSignature : ParamSignature, ITypedMethodSignatureElement
    {
        private bool _isByRef;

        /// <summary>
        /// Gets or sets the value indicating the <see cref="TypeSignature"/> of the current parameter.
        /// </summary>
        /// <value>The type signature of the current parameter.</value>
        public TypeSignature Type
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the value indicating whether nor not the current type signature is ByRef.
        /// </summary>
        /// <value>Determines whether or not the type is ByRef.</value>
        public override bool IsByRef
        {
            get { return _isByRef; }
        }

        /// <summary>
        /// Sets the value indicating whether or not the current signature is ByRef.
        /// </summary>
        /// <param name="isByRef">Determines whether or not the current signature is ByRef.</param>
        public void SetIsByRef(bool isByRef)
        {
            _isByRef = isByRef;
        }
    }
}
