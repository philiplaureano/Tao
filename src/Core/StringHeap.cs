using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tao.Core
{
    /// <summary>
    /// Represents the #Strings heap in a .NET assembly.
    /// </summary>
    public class StringHeap : Heap, IStringHeap
    {
        private readonly List<string> _strings = new List<string>();
        private readonly Dictionary<long, string> _stringMap = new Dictionary<long, string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="StringHeap"/> class.
        /// </summary>
        public StringHeap()
            : base(new OptionalHeader(), new CLIHeader(), new StreamHeaders())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringHeap"/> class.
        /// </summary>
        /// <param name="optionalHeader">The optional header that will determine the file position of the string heap.</param>
        /// <param name="cliHeader">The CLI header that determines the position of the metadata root.</param>
        /// <param name="streamHeaders">The collection of metadata stream headers that determine the position of the string heap itself.</param>
        public StringHeap(IOptionalHeader optionalHeader, ICLIHeader cliHeader, IStreamHeaders streamHeaders)
            : base(optionalHeader, cliHeader, streamHeaders)
        {
        }

        /// <summary>
        /// Gets the value indicating the list of strings that currently reside within the string heap.
        /// </summary>
        /// <value>The strings that are currently stored inside the string heap.</value>
        public IList<string> Strings
        {
            get
            {
                return _strings;
            }
        }

        /// <summary>
        /// Reads the strings from the given binary reader and string stream reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        /// <param name="stringStreamHeader">The string stream header reader.</param>
        private void ReadStrings(IBinaryReader reader, IStreamHeader stringStreamHeader)
        {
            var streamSize = (uint)stringStreamHeader.Size;
            var startingPosition = reader.GetPosition();
            long bytesRead = 0;
            while (bytesRead < streamSize)
            {
                var currentString = reader.ReadNullTerminatedString();
                Strings.Add(currentString);
                
                var currentPosition = reader.GetPosition();
                
                // Map the current position to the current string                
                bytesRead = currentPosition - startingPosition;

                var stringPosition = bytesRead - currentString.Length;
                _stringMap[stringPosition] = currentString;
            }
        }

        /// <summary>
        /// Reads the stream header from the given binary reader.
        /// </summary>
        /// <param name="reader">The binary reader that contains the stream to be read.</param>
        /// <param name="streamHeader">The stream header that describes the target stream.</param>
        protected override void ReadFrom(IBinaryReader reader, IStreamHeader streamHeader)
        {
            ReadStrings(reader, streamHeader);
        }


        /// <summary>
        /// Determines the name of the metadata stream to be read from a portable executable file.
        /// </summary>
        /// <returns>The name of the metadata stream.</returns>
        protected override string GetStreamName()
        {
            return "#Strings";
        }

        /// <summary>
        /// Obtains the string located at the given offset into the string heap.
        /// </summary>
        /// <param name="offset">The target string heap offset.</param>
        /// <returns>A string located at the given offset.</returns>
        public string GetStringFromOffset(uint offset)
        {
            var keys = _stringMap.Keys;

            var values = new Queue<long>(keys);
            while(values.Count > 0)
            {
                var currentIndex = values.Peek();
                if (currentIndex > offset)
                    break;

                values.Dequeue();
            }

            var targetIndex = values.Peek();            
            return _stringMap[targetIndex];
        }
    }
}
