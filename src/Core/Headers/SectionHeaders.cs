using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tao.Core
{
    /// <summary>
    /// Represents a collection of section headers.
    /// </summary>
    public class SectionHeaders : List<ISectionHeader>, IReader
    {
        private readonly ICOFFHeader _coffHeader;
        private readonly IHeaderReader<ISectionHeader> _sectionHeaderReader;

        /// <summary>
        /// Initializes a new instance of the <see cref="SectionHeaders"/> class.
        /// </summary>
        public SectionHeaders()
            : this(new COFFHeader(), new SectionHeaderReader())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SectionHeaders"/> class.
        /// </summary>
        /// <param name="coffHeader">The coff header that will be used to determine the number of sections and the position of the section headers.</param>
        /// <param name="reader">The section header reader.</param>
        public SectionHeaders(ICOFFHeader coffHeader, IHeaderReader<ISectionHeader> reader)
        {
            if (coffHeader == null)
                throw new ArgumentNullException("coffHeader");

            if (reader == null)
                throw new ArgumentNullException("reader");

            _coffHeader = coffHeader;
            _sectionHeaderReader = reader;
        }

        /// <summary>
        /// Reads the section headers from the given binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public void ReadFrom(IBinaryReader reader)
        {
            // Read the other headers
            _coffHeader.ReadFrom(reader);

            // Skip the optional header
            var optionalHeaderSize = _coffHeader.OptionalHeaderSize;
            var bytesToSkip = optionalHeaderSize != null ? Convert.ToInt32(optionalHeaderSize) : 0;

            if (bytesToSkip > 0)
                reader.Seek(bytesToSkip, SeekOrigin.Current);

            var headerCount = _coffHeader.NumberOfSections;
            var numberOfSections = headerCount != null ? Convert.ToInt32(headerCount.Value) : 0;
            var items = _sectionHeaderReader.ReadFrom(numberOfSections, reader);

            AddRange(items);
        }
    }
}
