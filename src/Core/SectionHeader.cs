using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Core
{
    /// <summary>
    /// Represents a class that describes the sections that reside within a given portable executable image file.
    /// </summary>
    public class SectionHeader : IHeader
    {
        /// <summary>
        /// Gets the value indicating the name of the current section.
        /// </summary>
        /// <value>The name of the current section.</value>
        public string SectionName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the total size of the section when loaded into memory.
        /// </summary>
        /// <value>The total size of the section when loaded into memory.</value>
        public uint? VirtualSize
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the address of the first byte of the section, when loaded into memory, relative to the image base.
        /// </summary>
        /// <value>The address of the first byte of the section.</value>
        public uint? VirtualAddress
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the size of the section or size of the initialized data on disk.
        /// </summary>
        /// <value>The section size or the size of the initialized data on disk.</value>
        public uint? SizeOfRawData
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the file pointer to the section's first page within the COFF file.
        /// </summary>
        /// <value>The file pointer to the section's first page within the COFF file.</value>
        public uint? PointerToRawData
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the file pointer to the beginning of relocation entries for the section.
        /// </summary>
        /// <value>The file pointer to the beginning of relocation entries for the section.</value>
        public uint? PointerToRelocations
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the file pointer to the beginning of line-number entries for the section.
        /// </summary>
        /// <value>The file pointer to the beginning of line-number entries for the section.</value>
        /// <remarks>This should be zero for an image as COFF debugging information is deprecated.</remarks>
        public uint? PointerToLineNumbers
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the number of relocation entries for the section.
        /// </summary>
        /// <value>The number of relocation entires for the section.</value>
        public ushort? NumberOfRelocations
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the number of line-number entires for the section.
        /// </summary>
        /// <value>The number of line-number entires for the section.</value>
        /// <value>This should be zero for an image as COFF debuggin information is deprecated.</value>
        public ushort? NumberOfLineNumbers
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the <see cref="SectionFlags"/> which describe the characteristics for the given section.
        /// </summary>
        /// <value>The section characteristics.</value>
        public SectionFlags Characteristics
        {
            get;
            private set;
        }

        /// <summary>
        /// Reads the section header from the given <paramref name="reader"/>.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public void ReadFrom(IBinaryReader reader)
        {
            ReadSectionName(reader);

            VirtualSize = reader.ReadUInt32();
            VirtualAddress = reader.ReadUInt32();
            SizeOfRawData = reader.ReadUInt32();
            PointerToRawData = reader.ReadUInt32();
            PointerToRelocations = reader.ReadUInt32();
            PointerToLineNumbers = reader.ReadUInt32();
            NumberOfRelocations = reader.ReadUInt16();
            NumberOfLineNumbers = reader.ReadUInt16();
            Characteristics = (SectionFlags)reader.ReadUInt32();
        }

        /// <summary>
        /// Reads the section name from the given <paramref name="reader"/>.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        private void ReadSectionName(IBinaryReader reader)
        {
            var nameBytes = reader.ReadBytes(8);
            var text = Encoding.UTF8.GetString(nameBytes);

            SectionName = null;
            foreach (var ch in text)
            {
                var currentCharacter = ch.ToString();

                if (currentCharacter == "\0")
                    break;

                SectionName += currentCharacter;
            }
        }
    }
}
