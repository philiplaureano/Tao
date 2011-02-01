using System.Collections.Generic;

namespace Tao.Model
{
    /// <summary>
    /// Represents a MethodRef signature.
    /// </summary>
    public interface IMethodRefSignature : IMethodSignature
    {
        /// <summary>
        /// Gets the value indicating the list of additional parameters used at the call site.
        /// </summary>
        /// <value>The list of additional parameters used at the call site.</value>
        IList<ITypedMethodSignatureElement> AdditionalParameters { get; }               
    }
}