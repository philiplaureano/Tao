using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Model
{
    /// <summary>
    /// Represents an assembly definition class.
    /// </summary>
    public class AssemblyDef
    {
        /// <summary>
        /// Gets a value indicating the MajorVersion of the current type.
        /// </summary>
        /// <value>The MajorVersion value.</value>
        public ushort MajorVersion { get; set; }

        /// <summary>
        /// Gets a value indicating the MinorVersion of the current type.
        /// </summary>
        /// <value>The MinorVersion value.</value>
        public ushort MinorVersion { get; set; }

        /// <summary>
        /// Gets a value indicating the BuildNumber of the current type.
        /// </summary>
        /// <value>The BuildNumber value.</value>
        public ushort BuildNumber { get; set; }

        /// <summary>
        /// Gets a value indicating the RevisionNumber of the current type.
        /// </summary>
        /// <value>The RevisionNumber value.</value>
        public ushort RevisionNumber { get; set; }

        /// <summary>
        /// Gets a value indicating the Name of the current type.
        /// </summary>
        /// <value>The Name value.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets a value indicating the Culture of the current type.
        /// </summary>
        /// <value>The Culture value.</value>
        public string Culture { get; set; }

        /// <summary>
        /// Gets a value indicating the HashAlgorithm of the current type.
        /// </summary>
        /// <value>The HashAlgorithm value.</value>
        public AssemblyHashAlgorithm HashAlgorithm { get; set; }

        /// <summary>
        /// Gets a value indicating the PublicKey of the current type.
        /// </summary>
        /// <value>The PublicKey value.</value>
        public byte[] PublicKey { get; set; }

        /// <summary>
        /// Gets a value indicating the Flags of the current type.
        /// </summary>
        /// <value>The AssemblyFlags value.</value>
        public AssemblyFlags Flags { get; set; }
    }
}
