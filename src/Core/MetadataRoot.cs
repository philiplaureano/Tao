﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Core
{
    /// <summary>
    /// Represents the root of the physical metadata.
    /// </summary>
    public class MetadataRoot : IHeader
    {
        /// <summary>
        /// Gets the value indicating the signature of the metadata root.
        /// </summary>
        /// <value>The metadata root signature.</value>
        public uint? Signature
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the major version of the metadata root
        /// </summary>
        /// <value>The major version of the metadata root.</value>
        public ushort? MajorVersion
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the minor version of the metadata root
        /// </summary>
        /// <value>The major version of the metadata root.</value>
        public ushort? MinorVersion
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the length of the version string in bytes
        /// </summary>
        /// <value>The length of the version string, in bytes.</value>
        public uint? VersionStringLength
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the version string.
        /// </summary>
        /// <value>The version string.</value>
        public string VersionString
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the number of streams that exist in the metadata.
        /// </summary>
        public ushort? NumberOfStreams
        {
            get;
            private set;
        }

        /// <summary>
        /// Reads the metadata root from the given reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public void ReadFrom(IBinaryReader reader)
        {
            Signature = reader.ReadUInt32();
            MajorVersion = reader.ReadUInt16();
            MinorVersion = reader.ReadUInt16();

            // Read the reserved data
            reader.ReadUInt32();

            VersionStringLength = reader.ReadUInt32();
            VersionString = GetVersionString(reader);

            // Read the reserved flags
            reader.ReadUInt16();

            NumberOfStreams = reader.ReadUInt16();
        }

        /// <summary>
        /// Reads the null-terminated version string from the given reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        /// <returns>The version string.</returns>
        private string GetVersionString(IBinaryReader reader)
        {
            var bytes = new List<byte>();
            for (uint i = 0; i < VersionStringLength.Value; i++)
            {
                var currentByte = reader.ReadByte();
                if (currentByte == 0)
                    continue;

                bytes.Add(currentByte);
            }

            return Encoding.UTF8.GetString(bytes.ToArray());
        }
    }
}