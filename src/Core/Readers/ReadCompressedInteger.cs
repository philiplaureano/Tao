using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// TODO: Add support for reading compressed signed integers
    /// <summary>
    /// Represents a type that returns the blob size for the current blob in the input stream.
    /// </summary>
    public class ReadCompressedInteger : IFunction<Stream, uint>
    {        
        /// <summary>
        /// Returns the blob size for the current blob in the input stream.
        /// </summary>
        /// <param name="input">The input stream containing the target blob.</param>
        /// <returns>The size of the target blob.</returns>
        public uint Execute(Stream input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            var leadingByte = (byte)input.ReadByte();
            if ((leadingByte & 0x80) == 0)
                return leadingByte;

            if ((leadingByte & 0xC0) == 0x80)
                return (uint)((leadingByte & 0x3F) << 8 | (byte)input.ReadByte());

            if ((leadingByte & 0xE0) != 0xC0)
                throw new NotSupportedException();

            return (uint)((leadingByte & 0x1F) << 24 | 
                        (byte)input.ReadByte() << 16 | 
                        (byte)input.ReadByte() << 8 | 
                        (byte)input.ReadByte());
        }      
    }
}
