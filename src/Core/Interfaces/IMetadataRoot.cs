namespace Tao.Core
{
    /// <summary>
    /// Represents the CLI metadata root of a portable executable file.
    /// </summary>
    public interface IMetadataRoot : IHeader
    {
        /// <summary>
        /// Gets the value indicating the signature of the metadata root.
        /// </summary>
        /// <value>The metadata root signature.</value>
        uint? Signature { get; }

        /// <summary>
        /// Gets the value indicating the major version of the metadata root
        /// </summary>
        /// <value>The major version of the metadata root.</value>
        ushort? MajorVersion { get; }

        /// <summary>
        /// Gets the value indicating the minor version of the metadata root
        /// </summary>
        /// <value>The major version of the metadata root.</value>
        ushort? MinorVersion { get; }

        /// <summary>
        /// Gets the value indicating the length of the version string in bytes
        /// </summary>
        /// <value>The length of the version string, in bytes.</value>
        uint? VersionStringLength { get; }

        /// <summary>
        /// Gets the value indicating the version string.
        /// </summary>
        /// <value>The version string.</value>
        string VersionString { get; }

        /// <summary>
        /// Gets the value indicating the number of streams that exist in the metadata.
        /// </summary>
        ushort? NumberOfStreams { get; }
    }
}