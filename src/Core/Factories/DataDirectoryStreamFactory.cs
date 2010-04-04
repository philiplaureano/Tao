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
    public class DataDirectoryStreamFactory : IFunction<Stream, Stream>
    {
        private readonly IFunction<Stream, int> _dataDirectoryCounter;        
        private readonly IFunction<Stream> _dataDirectoriesSeeker;
        private readonly IFunction<ITuple<int, Stream>, Stream> _inMemorySubStreamReader;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataDirectoryStreamFactory"/> class.
        /// </summary>
        /// <param name="dataDirectoryCounter">The object that will be responsible for determining the available number of data directories from a given portable executable stream.</param>
        /// <param name="inMemorySubStreamReader">The function that will be used to read the block of data directories.</param>
        /// <param name="dataDirectoriesSeeker">The <see cref="IFunction{Stream}"/> that will set the stream position to point to the first byte in the data directory headers.</param>
        public DataDirectoryStreamFactory(IFunction<Stream, int> dataDirectoryCounter, IFunction<ITuple<int, Stream>, Stream> inMemorySubStreamReader, IFunction<Stream> dataDirectoriesSeeker)
        {
            _dataDirectoryCounter = dataDirectoryCounter;
            _inMemorySubStreamReader = inMemorySubStreamReader;
            _dataDirectoriesSeeker = dataDirectoriesSeeker;
        }

        /// <summary>
        /// Extracts the Data Directories from a given portable executable stream.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <returns>A substream containing the data directories of the given portable executable stream.</returns>
        public Stream Execute(Stream input)
        {
            var numberOfDirectories = _dataDirectoryCounter.Execute(input);
            const int dataDirectorySize = 8;

            var bytesToRead = numberOfDirectories*dataDirectorySize;

            _dataDirectoriesSeeker.Execute(input);
            return _inMemorySubStreamReader.Execute(bytesToRead, input);
        }
    }
}
