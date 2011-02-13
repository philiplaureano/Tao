namespace Tao.Model
{
    /// <summary>
    /// Represents a metadata type signature.
    /// </summary>
    public interface ITypeSignature
    {
        /// <summary>
        /// Gets or sets the value indicating the <see cref="ElementType"/>.
        /// </summary>
        /// <value>The element type.</value>
        ElementType ElementType { get; set; }
    }
}