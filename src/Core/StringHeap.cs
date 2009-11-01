using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tao.Core
{
    /// <summary>
    /// Represents the #Strings heap in a .NET assembly.
    /// </summary>
    public class StringHeap : IStringHeap
    {
        private readonly List<string> _strings = new List<string>();
        private readonly ICLIHeader _cliHeader;
        private readonly IStreamHeaders _streamHeaders;
        private readonly IOptionalHeader _optionalHeader;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringHeap"/> class.
        /// </summary>
        public StringHeap()
            : this(new OptionalHeader(), new CLIHeader(), new StreamHeaders())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringHeap"/> class.
        /// </summary>
        /// <param name="optionalHeader">The optional header that will determine the file position of the string heap.</param>
        /// <param name="cliHeader">The CLI header that determines the position of the metadata root.</param>
        /// <param name="streamHeaders">The collection of metadata stream headers that determine the position of the string heap itself.</param>
        public StringHeap(IOptionalHeader optionalHeader, ICLIHeader cliHeader, IStreamHeaders streamHeaders)
        {
            _streamHeaders = streamHeaders;
            _cliHeader = cliHeader;
            _optionalHeader = optionalHeader;
        }

        /// <summary>
        /// Gets the value indicating the list of strings that currently reside within the string heap.
        /// </summary>
        /// <value>The strings that are currently stored inside the string heap.</value>
        public IList<string> Strings
        {
            get
            {
                return _strings;
            }
        }

        /// <summary>
        /// Reads the #String heap from the binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public void ReadFrom(IBinaryReader reader)
        {
            _optionalHeader.ReadFrom(reader);
            _cliHeader.ReadFrom(reader);
            _streamHeaders.ReadFrom(reader);
            
            IStreamHeader stringStreamHeader = GetStringStreamHeader();

            ReadStrings(reader, stringStreamHeader);
        }

        /// <summary>
        /// Gets the stream header of the #String heap.
        /// </summary>
        /// <returns>The #String stream header.</returns>
        private IStreamHeader GetStringStreamHeader()
        {
            IStreamHeader stringStreamHeader = null;
            foreach (var streamHeader in _streamHeaders)
            {
                if (streamHeader.Name != "#Strings")
                    continue;

                stringStreamHeader = streamHeader;
                break;
            }

            if (stringStreamHeader == null)
                throw new InvalidOperationException("Unable to find the #String stream header");

            return stringStreamHeader;
        }

        /// <summary>
        /// Reads the strings from the given binary reader and string stream reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        /// <param name="stringStreamHeader">The string stream header reader.</param>
        private void ReadStrings(IBinaryReader reader, IStreamHeader stringStreamHeader)
        {
            SeekStringHeapStartingPosition(reader, stringStreamHeader);

            var streamSize = (uint)stringStreamHeader.Size;
            var startingPosition = reader.GetPosition();
            long bytesRead = 0;
            while (bytesRead < streamSize)
            {
                var currentString = reader.ReadNullTerminatedString();
                Strings.Add(currentString);
                bytesRead = reader.GetPosition() - startingPosition;
            }
        }

        /// <summary>
        /// Locates the starting position of the #String stream.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        /// <param name="stringStreamHeader">The string stream header.</param>
        private void SeekStringHeapStartingPosition(IBinaryReader reader, IStreamHeader stringStreamHeader)
        {
            var metadataRva = (uint)_cliHeader.MetadataRva;
            var rootFileOffset = _optionalHeader.GetFileOffset(metadataRva);
            var stringStreamOffset = (uint)stringStreamHeader.Offset;

            var stringStreamFileOffset = rootFileOffset + stringStreamOffset;

            reader.Seek(stringStreamFileOffset, SeekOrigin.Begin);
        }
    }
}
