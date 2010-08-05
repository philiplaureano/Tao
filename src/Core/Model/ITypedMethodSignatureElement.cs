namespace Tao.Model
{
    /// <summary>
    /// Represents a return type or parameter type with a specific <see cref="TypeSignature"/>.
    /// </summary>
    public interface ITypedMethodSignatureElement : IMethodSignatureElement
    {
        /// <summary>
        /// Gets or sets the value indicating the <see cref="TypeSignature"/> of the current parameter.
        /// </summary>
        /// <value>The type signature of the current parameter.</value>
        TypeSignature Type { get; set; }
    }
}