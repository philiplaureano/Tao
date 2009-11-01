namespace Tao.Core
{
    /// <summary>
    /// Represents a single row in the Module table.
    /// </summary>
    public interface IModuleTableRow : IReader
    {
        /// <summary>
        /// Gets the value indicating the generation of the module.
        /// </summary>
        /// <value>The generation of the target module.</value>
        ushort? Generation { get; }

        /// <summary>
        /// Gets the value indicating the name of the target module.
        /// </summary>
        /// <value>The name of the target module.</value>
        IHeapIndex NameIndex { get; }

        /// <summary>
        /// Gets the value indicating the Mvid of the target module.
        /// </summary>
        /// <value>The module version ID.</value>
        IHeapIndex Mvid { get; }

        /// <summary>
        /// Gets the value indicating the EncId of the target module.
        /// </summary>
        /// <value>The EncId of the target module.</value>
        IHeapIndex EncId { get; }

        /// <summary>
        /// Gets the value indicating the EncBaseId of the target module.
        /// </summary>
        /// <value>The EncBaseId of the target module.</value>
        IHeapIndex EncBaseId { get; }
    }
}