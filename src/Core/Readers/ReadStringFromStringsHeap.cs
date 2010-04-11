using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a class that reads a string from the #Strings heap using the given target offset.
    /// </summary>
    public class ReadStringFromStringsHeap : IFunction<ITuple<uint, Stream>, string>
    {
        private readonly IFunction<Stream, string> _readNullTerminatedString;
        private readonly IFunction<ITuple<string, Stream>, Stream> _readMetadataStreamByName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadStringFromStringsHeap"/> class.
        /// </summary>
        public ReadStringFromStringsHeap(IFunction<Stream, string> readNullTerminatedString, IFunction<ITuple<string, Stream>, Stream> readMetadataStreamByName)
        {
            _readNullTerminatedString = readNullTerminatedString;
            _readMetadataStreamByName = readMetadataStreamByName;
        }

        /// <summary>
        /// Reads a string from the #Strings heap using the given target offset.
        /// </summary>
        /// <param name="input">The target offset and the input stream.</param>
        /// <returns>The output string.</returns>
        public string Execute(ITuple<uint, Stream> input)
        {
            var targetOffset = input.Item1;
            var stream = input.Item2;
            var stringHeap = _readMetadataStreamByName.Execute("#Strings", stream);
            stringHeap.Seek(targetOffset, SeekOrigin.Begin);

            return _readNullTerminatedString.Execute(stringHeap);
        }
    }
}
