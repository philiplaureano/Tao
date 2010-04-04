using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao.Core.Seekers
{
    /// <summary>
    /// Represents a class that seek the position of the number of data directories within a PE stream.
    /// </summary>
    public class DataDirectoryCountSeeker : IFunction<Stream>
    {
        /// <summary>
        /// Represents a class that seek the position of the number of data directories within a PE stream.
        /// </summary>
        /// <param name="stream">The target stream.</param>
        public void Execute(Stream stream)
        {
            stream.Seek(0xF4, SeekOrigin.Begin);
        }
    }
}
