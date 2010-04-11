using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a class that reads the RVA of the metadata header.
    /// </summary>
    public class ReadMetadataRva : IFunction<Stream, int>
    {
        private readonly IFunction<ITuple<int, Stream>, Stream> _readStreamFromDataDirectoryIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadMetadataRva"/> class.
        /// </summary>
        public ReadMetadataRva(IFunction<ITuple<int, Stream>, Stream> readStreamFromDataDirectoryIndex)
        {
            _readStreamFromDataDirectoryIndex = readStreamFromDataDirectoryIndex;
        }

        /// <summary>
        /// Reads the RVA of the metadata header.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <returns>The RVA of the metadata root header.</returns>
        public int Execute(Stream input)
        {
            var cliHeaderStream = _readStreamFromDataDirectoryIndex.Execute(0xE, input);
            cliHeaderStream.Seek(8, SeekOrigin.Begin);

            var reader = new BinaryReader(cliHeaderStream);
            var rva = reader.ReadInt32();

            return rva;
        }
    }
}