namespace Tao.Model
{
    /// <summary>
    /// Represents a type pointer signature.
    /// </summary>
    public class TypePointerSignature : PointerSignature
    {
        /// <summary>
        /// Gets or sets the value indicating the pointer type.
        /// </summary>
        /// <value>The pointer type.</value>
        public TypeSignature TypeSignature { get; set; }
    }
}
