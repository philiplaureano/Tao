using System;
using System.IO;

namespace Tao.Core
{
    /// <summary>
    /// Represents a metadata heap in a .NET assembly.
    /// </summary>
    public abstract class Heap : IReader
    {
        private readonly ICLIHeader _cliHeader;
        private readonly IStreamHeaders _streamHeaders;
        private readonly IOptionalHeader _optionalHeader;

        /// <summary>
        /// Initializes a new instance of the <see cref="Heap"/> class.
        /// </summary>
        protected Heap()
            : this(new OptionalHeader(), new CLIHeader(), new StreamHeaders())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Heap"/> class.
        /// </summary>
        /// <param name="optionalHeader">The optional header that will determine the file position of the heap.</param>
        /// <param name="cliHeader">The CLI header that determines the position of the metadata root.</param>
        /// <param name="streamHeaders">The collection of metadata stream headers that determine the position of the heap itself.</param>
        protected Heap(IOptionalHeader optionalHeader, ICLIHeader cliHeader, IStreamHeaders streamHeaders)
        {
            _streamHeaders = streamHeaders;
            _cliHeader = cliHeader;
            _optionalHeader = optionalHeader;
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
            
            IStreamHeader streamHeader = GetStreamHeader();

            SeekHeapStartingPosition(reader, streamHeader);

            ReadFrom(reader, streamHeader);
        }

        /// <summary>
        /// Reads the stream header from the given binary reader.
        /// </summary>
        /// <param name="reader">The binary reader that contains the stream to be read.</param>
        /// <param name="streamHeader">The stream header that describes the target stream.</param>
        protected abstract void ReadFrom(IBinaryReader reader, IStreamHeader streamHeader);

        /// <summary>
        /// Determines the name of the metadata stream to be read from a portable executable file.
        /// </summary>
        /// <returns>The name of the metadata stream.</returns>
        protected abstract string GetStreamName();        

        /// <summary>
        /// Gets the stream header of the #String heap.
        /// </summary>
        /// <returns>The #String stream header.</returns>
        private IStreamHeader GetStreamHeader()
        {
            var streamName = GetStreamName();

            IStreamHeader stringStreamHeader = null;
            foreach (var streamHeader in _streamHeaders)
            {
                if (streamHeader.Name != streamName)
                    continue;

                stringStreamHeader = streamHeader;
                break;
            }

            if (stringStreamHeader == null)
            {
                var errorMessage = string.Format("Unable to find the {0} stream header", streamName);
                throw new InvalidOperationException(errorMessage);
            }

            return stringStreamHeader;
        }

        /// <summary>
        /// Locates the starting position of the #String stream.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        /// <param name="stringStreamHeader">The string stream header.</param>
        private void SeekHeapStartingPosition(IBinaryReader reader, IStreamHeader stringStreamHeader)
        {
            var metadataRva = (uint)_cliHeader.MetadataRva;
            var rootFileOffset = _optionalHeader.GetFileOffset(metadataRva);
            var stringStreamOffset = (uint)stringStreamHeader.Offset;

            var stringStreamFileOffset = rootFileOffset + stringStreamOffset;

            reader.Seek(stringStreamFileOffset, SeekOrigin.Begin);
        }
    }
}