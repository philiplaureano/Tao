namespace Tao.Model
{
    /// <summary>
    /// Represents a metadata type signature.
    /// </summary>
    public class TypeSignature : ITypeSignature
    {
        /// <summary>
        /// Gets or sets the value indicating the <see cref="ElementType"/>.
        /// </summary>
        /// <value>The element type.</value>
        public ElementType ElementType
        {
            get; set;
        }
    }
}
