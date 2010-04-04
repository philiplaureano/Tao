using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao.Core.Factories
{
    /// <summary>
    /// Represents a class that extracts the Data Directories from a given portable executable stream.
    /// </summary>
    public class DataDirectoryStreamFactory : IConversion<Stream, Stream>
    {
        private IConversion<Stream, int> _dataDirectoryCounter;
        private ISubStreamReader _inMemorySubStreamReader;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        /// <param name="dataDirectoryCounter">The object that will be responsible for determining the available number of data directories from a given portable executable stream.</param>
        /// <param name="inMemorySubStreamReader">The <see cref="ISubStreamReader"/> that will be used to read the block of data directories.</param>
        public DataDirectoryStreamFactory(IConversion<Stream, int> dataDirectoryCounter, ISubStreamReader inMemorySubStreamReader)
        {
            _dataDirectoryCounter = dataDirectoryCounter;
            _inMemorySubStreamReader = inMemorySubStreamReader;
        }

        /// <summary>
        /// Extracts the Data Directories from a given portable executable stream.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <returns>A substream containing the data directories of the given portable executable stream.</returns>
        public Stream Convert(Stream input)
        {
            var numberOfDirectories = _dataDirectoryCounter.Convert(input);
            const int dataDirectorySize = 8;

            var bytesToRead = numberOfDirectories*dataDirectorySize;

            input.Seek(0, SeekOrigin.Begin);
            return _inMemorySubStreamReader.Read(bytesToRead, input);
        }
    }
}
