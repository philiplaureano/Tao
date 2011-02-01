using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tao.Model
{
    /// <summary>
    /// Represents a MethodRefSig signature.
    /// </summary>
    public class MethodRefSig : MethodSignature, IMethodRefSignature
    {
        private readonly List<ITypedMethodSignatureElement> _additionalParameters =
            new List<ITypedMethodSignatureElement>();

        /// <summary>
        /// Gets the value indicating the list of additional parameters used at the call site.
        /// </summary>
        /// <value>The list of additional parameters used at the call site.</value>
        public IList<ITypedMethodSignatureElement> AdditionalParameters
        {
            get
            {
                return _additionalParameters;
            }
        }
    }
}
