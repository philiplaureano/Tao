using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao.Core.Readers
{
    /// <summary>
    /// Represents a class that reads the version string from the metadata root header.
    /// </summary>
    public class ReadVersionString : IFunction<Stream, string>
    {
        private readonly IFunction<Stream> _seekMetadataRootPosition;
        private readonly IFunction<Stream, string> _readNullTerminatedString;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadVersionString"/> class.
        /// </summary>
        public ReadVersionString(IFunction<Stream> seekMetadataRootPosition, IFunction<Stream, string> readNullTerminatedString)
        {
            _seekMetadataRootPosition = seekMetadataRootPosition;
            _readNullTerminatedString = readNullTerminatedString;
        }

        /// <summary>
        /// Reads the version string from the metadata root header.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <returns>The version string.</returns>
        public string Execute(Stream input)
        {
            _seekMetadataRootPosition.Execute(input);
            input.Seek(0x10, SeekOrigin.Current);

            return _readNullTerminatedString.Execute(input);
        }
    }
}
