namespace Tao.Model
{
    /// <summary>
    /// Represents a CustomMod element signature.
    /// </summary>
    public class CustomMod
    {
        /// <summary>
        /// Gets or sets a value indicating the <see cref="ElementType"/> of the current <see cref="CustomMod"/> object.
        /// </summary>
        /// <value>The element type.</value>
        public ElementType ElementType
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets a value indicating the <see cref="TableId"/> referenced by the current <see cref="CustomMod"/> object.
        /// </summary>
        /// <value>The <see cref="TableId"/> of the current <see cref="CustomMod"/> object.</value>
        public TableId TableId
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets a value indicating the target row index.
        /// </summary>
        /// <value>The row index of the target table.</value>
        public uint RowIndex
        {
            get; set;
        }
    }
}
