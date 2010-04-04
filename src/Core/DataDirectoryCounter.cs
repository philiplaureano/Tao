using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao.Core
{
    /// <summary>
    /// Represents a class that determines the number of data directories from a given portable executable file stream.
    /// </summary>
    public class DataDirectoryCounter : IFunction<Stream, int>
    {
        private readonly IStreamSeeker _dataDirectoryCountSeeker;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataDirectoryCounter"/> class.
        /// </summary>
        /// <param name="dataDirectoryCountSeeker">The seeker that will locate the position of the number of data directories.</param>
        public DataDirectoryCounter(IStreamSeeker dataDirectoryCountSeeker)
        {
            _dataDirectoryCountSeeker = dataDirectoryCountSeeker;
        }

        /// <summary>
        /// Determines the number of data directories from a given portable executable file stream.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <returns>The number of data directories that the <paramref name="input"/> stream contains.</returns>
        public int Execute(Stream input)
        {            
            var reader = new BinaryReader(input);
            _dataDirectoryCountSeeker.Seek(input);
            var result = reader.ReadInt32();       

            return result;
        }
    }
}
