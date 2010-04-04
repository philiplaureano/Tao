using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao.Core.Seekers
{
    /// <summary>
    /// Represents a stream seeker class that sets the stream position to point to the beginning of the list of data directory headers in a portable executable file.
    /// </summary>
    public class DataDirectoriesSeeker : IStreamSeeker
    {
        /// <summary>
        /// Sets the stream position to point to the beginning of the list of data directory headers in a portable executable file.
        /// </summary>
        /// <param name="stream">The target stream.</param>
        public void Seek(Stream stream)
        {
            stream.Seek(0xF8, SeekOrigin.Begin);
        }
    }
}
