using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Core
{
    /// <summary>
    /// Represents the Optional Header of a Portable Executable file.
    /// </summary>
    public class OptionalHeader : IOptionalHeader
    {
        private readonly IHeaderReader<IDataDirectory> _dataDirectoryReader;
        private readonly List<IDataDirectory> _dataDirectories = new List<IDataDirectory>();
        private readonly IHeader _coffHeader;

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionalHeader"/> class.
        /// </summary>
        public OptionalHeader()
            : this(new DataDirectoryReader())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionalHeader"/> class.
        /// </summary>
        /// <param name="dataDirectoryReader">The data directory reader that will be used to read the data directories from the image stream.</param>
        public OptionalHeader(IHeaderReader<IDataDirectory> dataDirectoryReader)
            : this(new COFFHeader(), dataDirectoryReader)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionalHeader"/> class.
        /// </summary>
        /// <param name="coffHeader">The coff header that will be used to determine the position of the optional header.</param>
        /// <param name="dataDirectoryReader">The data directory reader that will be used to read the data directories from the image stream.</param>
        public OptionalHeader(IHeader coffHeader, IHeaderReader<IDataDirectory> dataDirectoryReader)
        {
            _dataDirectoryReader = dataDirectoryReader;
            _coffHeader = coffHeader;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionalHeader"/> class.
        /// </summary>
        /// <param name="coffHeader">The coff header that will be used to determine the position of the optional header.</param>
        public OptionalHeader(IHeader coffHeader)
            : this(coffHeader, new DataDirectoryReader())
        {
        }

        #region Standard Fields
        /// <summary>
        /// Gets the value indicating the <see cref="PEFormat"/> of the PE file.
        /// </summary>
        /// <value>The enumeration that describes the PE file type.</value>
        public PEFormat? MagicNumber
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the major linker version.
        /// </summary>
        /// <value>The major linker version</value>
        public byte? MajorLinkerVersion
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the minor linker version.
        /// </summary>
        /// <value>The minor linker version.</value>
        public byte? MinorLinkerVersion
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the size of the code section, or the sum of all code sections if there are multiple sections.
        /// </summary>
        /// <value>The size of the code section.</value>
        public uint? SizeOfCode
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the size of the initialized data section, or the sum of all sections if there are multiple data sections.
        /// </summary>
        /// <value>The size of the data section.</value>
        public uint? SizeOfInitializedData
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the size of the uninitialized data section, or the sum of all such sections if there are multiple BSS sections.
        /// </summary>
        /// <value>The size of the uninitialized data.</value>
        public uint? SizeOfUninitializedData
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the address of the entry point relative to the image base when the executable file is loaded into memory.
        /// </summary>
        /// <value>The address of the program entry point.</value>
        public uint? AddressOfEntryPoint
        {
            get;
            private set;
        }

        /// <summary>
        /// The address, relative to the image base of the beginning of code section when loaded into memory.
        /// </summary>
        /// <value>The base of code address.</value>
        public uint? BaseOfCode
        {
            get;
            private set;
        }

        /// <summary>
        /// The address, relative to the image base, of the beginning of the data section, when loaded
        /// into memory.
        /// </summary>
        /// <value>The base of data address.</value>
        public uint? BaseOfData
        {
            get;
            private set;
        }
        #endregion

        #region Windows-Specific Fields
        /// <summary>
        /// Gets the value indicating the preferred address of the first byte of the image when loaded into memory.
        /// </summary>
        /// <value>The preferred address of the first byte once it's loaded into memory.</value>
        public uint? ImageBase
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the alignment (in bytes) of sections when loaded into memory. 
        /// </summary>
        /// <value>The section alignment, in bytes.</value>
        public uint? SectionAlignment
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the alignment factor (in bytes) used to align the raw data of sections in the image file.
        /// </summary>
        /// <value>The file alignment factor, expressed in bytes.</value>
        public uint? FileAlignment
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the major version number of the required OS.
        /// </summary>
        /// <value>The major version number of the required operating system.</value>
        public ushort? MajorOSVersion
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the minor version number of the required OS.
        /// </summary>
        /// <value>The minor version number of the required operating system.</value>
        public ushort? MinorOSVersion
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the major version number of the image.
        /// </summary>
        /// <value>The major version number of the target image.</value>
        public ushort? MajorImageVersion
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the minor version number of the image.
        /// </summary>
        /// <value>The minor version number of the target image.</value>
        public ushort? MinorImageVersion
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the major version number of the subsystem.
        /// </summary>
        /// <value>The major version number of the target subsystem.</value>
        public ushort? MajorSubsystemVersion
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the minor version number of the subsystem.
        /// </summary>
        /// <value>The minor version number of the target subsystem.</value>
        public ushort? MinorSubsystemVersion
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the Win32VersionValue.
        /// </summary>
        /// <value>This value is reserved and must be zero.</value>
        public uint? Win32VersionValue
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the size of the image.
        /// </summary>
        /// <value>The size of the image.</value>
        public uint? SizeOfImage
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the combined size of the MS-DOS stub, PE header, and section headers rounded up to a multiple of <see cref="FileAlignment"/>.
        /// </summary>
        /// <value>The combined size of all file headers.</value>
        public uint? SizeOfHeaders
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the image file checksum.
        /// </summary>
        /// <value>The image file checksum.</value>
        public uint? CheckSum
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the windows subsystem required to run the target image.
        /// </summary>
        /// <value>The windows subsystem required to run the target image.</value>
        public ImageSubsystem? Subsystem
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the <see cref="DLLCharacteristics"/> for the target image.
        /// </summary>
        /// <value>The <see cref="DllCharacteristics"/> of the target image.</value>
        public DllCharacteristics? DLLCharacteristics
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the size of the stack to reserve.
        /// </summary>
        /// <remarks>Only the Stack Commit Size is committed; the rest is made available one page at a time, until reserve size is reached.</remarks>
        public uint? SizeOfStackReserve
        {
            get;
            private set;
        }


        /// <summary>
        /// Gets the value indicating the size of local heap space to reserve.
        /// </summary>
        /// <value>The size of the local heap to reserve.</value>
        public uint? SizeOfHeapReserve
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the size of local heap space to commit.
        /// </summary>
        /// <value>The size of local heap space to commit.</value>
        public uint? SizeOfHeapCommit
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the size of stack to commit.
        /// </summary>
        /// <value>The stack size to commit.</value>
        public uint? SizeOfStackCommit
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the loader flags for the current image.
        /// </summary>
        /// <value>Reserved. This value must be zero.</value>
        public uint? LoaderFlags
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the number of data directories that the image contains.
        /// </summary>
        /// <value>The number of data directories.</value>
        public uint? NumberOfDirectories
        {
            get;
            private set;
        }

        #endregion

        /// <summary>
        /// Gets the value indicating the data directories that currently reside within the image.
        /// </summary>
        /// <value>The list of data directories.</value>
        public IEnumerable<IDataDirectory> DataDirectories
        {
            get
            {
                return _dataDirectories;
            }
        }

        /// <summary>
        /// Reads the data from the given <paramref name="reader"/>.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public void ReadFrom(IBinaryReader reader)
        {
            if (_coffHeader != null)
                _coffHeader.ReadFrom(reader);

            ReadStandardFields(reader);
            ReadWindowSpecificFields(reader);
        }

        /// <summary>
        /// Reads the Windows-specific fields from the Optional Header in the portable executable file.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        private void ReadWindowSpecificFields(IBinaryReader reader)
        {
            ImageBase = reader.ReadUInt32();
            SectionAlignment = reader.ReadUInt32();
            FileAlignment = reader.ReadUInt32();

            MajorOSVersion = reader.ReadUInt16();
            MinorOSVersion = reader.ReadUInt16();

            MajorImageVersion = reader.ReadUInt16();
            MinorImageVersion = reader.ReadUInt16();

            MajorSubsystemVersion = reader.ReadUInt16();
            MinorSubsystemVersion = reader.ReadUInt16();

            Win32VersionValue = reader.ReadUInt32();
            SizeOfImage = reader.ReadUInt32();
            SizeOfHeaders = reader.ReadUInt32();

            CheckSum = reader.ReadUInt32();
            Subsystem = (ImageSubsystem)reader.ReadUInt16();
            DLLCharacteristics = (DllCharacteristics)reader.ReadUInt16();

            SizeOfStackReserve = reader.ReadUInt32();
            SizeOfStackCommit = reader.ReadUInt32();

            SizeOfHeapReserve = reader.ReadUInt32();
            SizeOfHeapCommit = reader.ReadUInt32();

            LoaderFlags = reader.ReadUInt32();
            NumberOfDirectories = reader.ReadUInt32();

            if (_dataDirectoryReader == null)
                return;

            ReadDataDirectories(reader);
        }

        private void ReadDataDirectories(IBinaryReader reader)
        {
            _dataDirectories.Clear();
            var directoryCount = Convert.ToInt32(NumberOfDirectories);
            var directories = _dataDirectoryReader.ReadFrom(directoryCount, reader);

            _dataDirectories.AddRange(directories);
        }

        /// <summary>
        /// Reads the standard fields from the Optional Header in the portable executable file.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        private void ReadStandardFields(IBinaryReader reader)
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

            if (MagicNumber == PEFormat.PE32)
                return;

            throw new NotSupportedException("Unsupported image type; Tao only supports 32-bit images.");
        }
    }
}
