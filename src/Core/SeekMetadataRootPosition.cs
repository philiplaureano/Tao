using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao.Core
{
    /// <summary>
    /// Represents a class that sets the file pointer to point to the starting position of the metadata root header.
    /// </summary>
    public class SeekMetadataRootPosition : IFunction<Stream>
    {
        private readonly IFunction<Stream, int> _readMetadataRva;
        private readonly IFunction<ITuple<int, Stream>> _seekAbsoluteFilePositionFromRva;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeekMetadataRootPosition"/> class.
        /// </summary>
        public SeekMetadataRootPosition(IFunction<Stream, int> readMetadataRva, IFunction<ITuple<int, Stream>> seekAbsoluteFilePositionFromRva)
        {
            _readMetadataRva = readMetadataRva;
            _seekAbsoluteFilePositionFromRva = seekAbsoluteFilePositionFromRva;
        }

        /// <summary>
        /// Sets the file pointer to point to the starting position of the metadata root header.
        /// </summary>
        /// <param name="input">The input stream.</param>
        public void Execute(Stream input)
        {
            var rva = _readMetadataRva.Execute(input);
            _seekAbsoluteFilePositionFromRva.Execute(rva, input);
        }
    }
}
