using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Tao
{
    /// <summary>
    /// Represents a set of extension methods that add functionality to an existing <see cref="Stream"/> instance.
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// Converts the given <paramref name="input">input stream</paramref> into a byte array, regardless of its current stream position.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <returns>A byte array containing the contents of the given stream.</returns>
        public static byte[] ToArray(this Stream input)
        {
            var startPosition = input.Position;

            // Read the bytes from the beginning of the stream
            input.Seek(0, SeekOrigin.Begin);
            var results = input.ReadToEnd(false);

            // Restore the original stream position
            input.Seek(startPosition, SeekOrigin.Begin);

            return results;
        }

        /// <summary>
        /// Reads the remaining contents of the <paramref name="input">input stream</paramref> into a byte array.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <param name="resetStreamPositionAfterRead">Determines whether or not the stream position should be reset to its original position after the read operation succeeds.</param>
        /// <returns>A byte array containing the remaining contents of the given stream.</returns>
        public static byte[] ReadToEnd(this Stream input, bool resetStreamPositionAfterRead)
        {
            var startPosition = input.Position;
            var buffer = new byte[16 * 1024];
            using (var memoryStream = new MemoryStream())
            {
                var bytesRead = input.Read(buffer, 0, buffer.Length);

                while (bytesRead > 0)
                {
                    memoryStream.Write(buffer, 0, bytesRead);
                    bytesRead = input.Read(buffer, 0, buffer.Length);
                }

                if (resetStreamPositionAfterRead)
                    input.Seek(startPosition, SeekOrigin.Begin);

                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Reads a single byte from the <paramref name="input">input stream</paramref> without advancing the stream pointer.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <returns>The next byte in the stream.</returns>
        public static int PeekByte(this Stream input)
        {
            var startPosition = input.Position;
            var result = input.ReadByte();
            input.Seek(startPosition, SeekOrigin.Begin);

            return result;
        }
    }
}
