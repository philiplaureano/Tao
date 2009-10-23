namespace Tao.Core
{
    /// <summary>
    /// Represents the Import Address Table of a portable executable file.
    /// </summary>
    public interface IImportAddressTable : IHeader
    {
        /// <summary>
        /// Gets the value indicating the relative virtual address that points to the Hint/Name Table.
        /// </summary>
        /// <value>The RVA of the Hint/Name table.</value>
        uint? HintNameTableRVA { get; }
    }
}