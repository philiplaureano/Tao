namespace Tao.Model
{
    /// <summary>
    /// Represents a method function pointer type signature.
    /// </summary>
    public class FunctionPointerSignature : TypeSignature, ITypeSpecification
    {
        /// <summary>
        /// Gets or sets the value indicating the method signature that this function pointer points to.
        /// </summary>
        /// <value>The target method signature.</value>
        public IMethodSignature TargetMethod { get; set; }
    }
}
