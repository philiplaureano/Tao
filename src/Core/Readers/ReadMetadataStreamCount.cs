using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao.Core.Readers
{
    /// <summary>
    /// Represents a type that determines the number of metadata streams within a given Portable Executable stream.
    /// </summary>
    public class ReadMetadataStreamCount : IFunction<Stream, int>
    {
        private readonly IFunction<Stream> _seekMetadataRootPosition;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public ReadMetadataStreamCount(IFunction<Stream> seekMetadataRootPosition)
        {
            _seekMetadataRootPosition = seekMetadataRootPosition;
        }

        /// <summary>
        /// Determines the number of metadata streams within a given Portable Executable stream.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <returns>The number of metadata streams in the current PE stream.</returns>
        public int Execute(Stream input)
        {
            _seekMetadataRootPosition.Execute(input);
            input.Seek(0x1e, SeekOrigin.Current);

            var reader = new BinaryReader(input);
            return reader.ReadInt16();
        }
    }
}