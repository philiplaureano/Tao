using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao.Core.Seekers
{
    /// <summary>
    /// Represents a class that can set the stream position to point to the first byte in the PE header stream.
    /// </summary>
    public class PEStreamSeeker : IFunction<Stream>
    {
        /// <summary>
        /// Set the stream position to point to the first byte in the PE header stream.
        /// </summary>
        /// <param name="stream">The target stream.</param>
        public void Execute(Stream stream)
        {
            stream.Seek(0x84, SeekOrigin.Begin);
        }
    }
}
