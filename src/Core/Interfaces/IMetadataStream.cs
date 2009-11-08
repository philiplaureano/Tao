using System.Collections.Generic;

namespace Tao.Core
{
    /// <summary>
    /// Represents the metadata stream in a .NET portable executable image.
    /// </summary>
    public interface IMetadataStream : IReader
    {
        /// <summary>
        /// Gets the value indicating the major version of the table schemata
        /// </summary>
        /// <value>The major version of the table schemata.</value>
        byte? MajorVersion { get; }

        /// <summary>
        /// Gets the value indicating the minor version of the table schemata.
        /// </summary>
        byte? MinorVersion { get; }

        /// <summary>
        /// Gets the value indicating the bit vector for the heap sizes.
        /// </summary>
        /// <value>The bit vector for the heap sizes.</value>
        byte? HeapSizes { get; }

        /// <summary>
        /// Gets the value indicating the bit vector for the valid tables
        /// </summary>
        /// <value>The bit vector for the valid tables.</value>
        ulong? Valid { get; }

        /// <summary>
        /// Gets the value indicating the bit vector for the sorted tables.
        /// </summary>
        /// <value>The bit vector for the sorted tables.</value>
        ulong? Sorted { get; }

        /// <summary>
        /// Gets the value indicating the list of <see cref="TableId">metadata tables</see> that currently exist within the .NET executable image.
        /// </summary>
        /// <value>A list of TableIds that enumerate the existing tables in the current .NET executable</value>
        IEnumerable<TableId> ValidTables { get; }

        /// <summary>
        /// Determines whether or not the metadata table with the given <paramref name="tableId"/> exists within the current .NET executable image.
        /// </summary>
        /// <param name="tableId">The ID of the target metadata table.</param>
        /// <returns>Returns <c>true</c> if the table exists; otherwise, it will return <c>false</c>.</returns>
        bool IsValid(byte tableId);

        /// <summary>
        /// Gets the row counts for the given <paramref name="tableId"/>.
        /// </summary>
        /// <param name="tableId">The tableId.</param>
        /// <returns>The number of rows for the given table.</returns>
        uint GetRowCount(TableId tableId);
    }
}