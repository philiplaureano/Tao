using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Core
{
    /// <summary>
    /// Represents the Optional Header of a Portable Executable file.
    /// </summary>
    public class OptionalHeader
    {
        /// <summary>
        /// Reads the data from the given <paramref name="reader"/>.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public void ReadFrom(IBinaryReader reader)
        {
            ReadMagicNumber(reader);

            MajorLinkerVersion = reader.ReadByte();
            MinorLinkerVersion = reader.ReadByte();

            SizeOfCode = reader.ReadUInt32();
            SizeOfInitializedData = reader.ReadUInt32();
            SizeOfUninitializedData = reader.ReadUInt32();

            AddressOfEntryPoint = reader.ReadUInt32();
            BaseOfCode = reader.ReadUInt32();

            if (MagicNumber == PEFormat.PE32)
                BaseOfData = reader.ReadUInt32();
        }

        /// <summary>
        /// Reads the magic number from the optional header in a portable executable image.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        private void ReadMagicNumber(IBinaryReader reader)
        {
            // The magic number must be either a PE32 format or PE32+ format
            var inputValue = reader.ReadUInt16();
            var validValues = new[] { Convert.ToUInt16(PEFormat.PE32), Convert.ToUInt16(PEFormat.PE32Plus) };

            var items = new List<UInt16>(validValues);
            if (!items.Contains(inputValue))
                throw new NotSupportedException("Unrecognized Image Type");

            MagicNumber = (PEFormat)inputValue;
        }

        /// <summary>
        /// Gets the value indicating the <see cref="PEFormat"/> of the PE file.
        /// </summary>
        /// <value>The enumeration that describes the PE file type.</value>
        public PEFormat MagicNumber
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the major linker version.
        /// </summary>
        /// <value>The major linker version</value>
        public byte MajorLinkerVersion
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the minor linker version.
        /// </summary>
        /// <value>The minor linker version.</value>
        public byte MinorLinkerVersion
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the size of the code section, or the sum of all code sections if there are multiple sections.
        /// </summary>
        /// <value>The size of the code section.</value>
        public uint SizeOfCode
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the size of the initialized data section, or the sum of all sections if there are multiple data sections.
        /// </summary>
        /// <value>The size of the data section.</value>
        public uint SizeOfInitializedData
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the size of the uninitialized data section, or the sum of all such sections if there are multiple BSS sections.
        /// </summary>
        /// <value>The size of the uninitialized data.</value>
        public uint SizeOfUninitializedData
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the address of the entry point relative to the image base when the executable file is loaded into memory.
        /// </summary>
        /// <value>The address of the program entry point.</value>
        public uint AddressOfEntryPoint
        {
            get;
            private set;
        }

        /// <summary>
        /// The address, relative to the image base of the beginning of code section when loaded into memory.
        /// </summary>
        /// <value>The base of code address.</value>
        public uint BaseOfCode
        {
            get;
            private set;
        }

        /// <summary>
        /// The address, relative to the image base, of the beginning of the data section, when loaded
        /// into memory.
        /// </summary>
        /// <value>The base of data address.</value>
        public uint BaseOfData
        {
            get;
            private set;
        }
    }
}
