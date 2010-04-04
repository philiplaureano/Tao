using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tao.Interfaces
{
    /// <summary>
    /// Represents a type that can set the position of a given stream to a specific location.
    /// </summary>
    public interface IStreamSeeker
    {
        /// <summary>
        /// Seeks a specific position within a given stream.
        /// </summary>
        /// <param name="stream">The target stream.</param>
        void Seek(Stream stream);        
    }
}
