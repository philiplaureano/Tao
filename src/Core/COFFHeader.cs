using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Tao.Core
{
    /// <summary>
    /// Represents a class that reads the PR File header from a Portable Executable file.
    /// </summary>
    public class COFFHeader
    {       
        /// <summary>
        /// Parses the PR file header from the given input <paramref name="binaryReader"/>.
        /// </summary>
        /// <param name="binaryReader">The binary reader.</param>
        public void ReadFrom(IBinaryReader binaryReader)
        {
            ReadPESignature(binaryReader);

            // Read the target machine type
            MachineType = (ImageFileMachineType)binaryReader.ReadUInt16();

            // Read the section count
            NumberOfSections = binaryReader.ReadUInt16();

            // Read the time stamp
            TimeDateStamp = binaryReader.ReadInt32();

            // Read the pointer to the signature table
            PointerToSymbolTable = binaryReader.ReadInt32();

            // Read the number of symbols
            NumberOfSymbols = binaryReader.ReadInt32();

            // Read the optional header size
            OptionalHeaderSize = binaryReader.ReadUInt16();

            Characteristics = (ImageFileCharacteristics) binaryReader.ReadUInt16();
        }

        /// <summary>
        /// Reads the PE signature from the binary stream.
        /// </summary>
        /// <param name="binaryReader">The reader that contains the stream with the PE signature.</param>
        private void ReadPESignature(IBinaryReader binaryReader)
        {
            byte[] bytes = binaryReader.ReadBytes(4);

            var text = Encoding.ASCII.GetString(bytes);
            HasPortableExecutableSignature = text == "PE\0\0";
        }

        /// <summary>
        /// Gets the value indicating whether or not the previously read stream contains a PE signature.
        /// </summary>
        /// <value>A boolean flag that determines whether or not the previously read stream contains a PE signature.</value>
        public bool HasPortableExecutableSignature
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the size of the section table.
        /// </summary>
        /// <value>The size of the section table.</value>
        public int NumberOfSections
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the target <see cref="MachineType"/> for the given PE header.
        /// </summary>
        /// <value>The machine type of the target image.</value>
        public ImageFileMachineType MachineType
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the number of seconds since 00:00 January 1, 1970 when the file was created.
        /// </summary>
        /// <value>The number of seconds that have elapsed since 00:00 January 1, 1970.</value>
        public int TimeDateStamp
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the pointer to the symbol table.
        /// </summary>
        /// <value>The pointer to the symbol table.</value>
        public int PointerToSymbolTable
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the number of entries in the symbol table.
        /// </summary>
        /// <value>The number of entries in the symbol table.</value>
        public int NumberOfSymbols
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the size of the optional header.
        /// </summary>
        /// <value>The optional header size.</value>
        public int OptionalHeaderSize
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the value indicating the <see cref="ImageFileCharacteristics"/> of the COFF header.
        /// </summary>
        public ImageFileCharacteristics Characteristics
        {
            get;
            private set;
        }
    }
}
