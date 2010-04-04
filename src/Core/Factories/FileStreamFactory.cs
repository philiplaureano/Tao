using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao.Core.Factories
{
    /// <summary>
    /// Represents a factory type that can read the contents of a file and dump it into a memory stream.
    /// </summary>
    public class FileStreamFactory : IFunction<string, Stream>
    {
        private readonly IFunction<ITuple<int, Stream>, Stream> _reader;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileStreamFactory"/> class.
        /// </summary>
        /// <param name="reader">The substream reader.</param>
        public FileStreamFactory(IFunction<ITuple<int, Stream>, Stream> reader)
        {
            _reader = reader;
        }

        /// <summary>
        /// Reads the contents of a file and dump it into a memory stream.
        /// </summary>
        /// <param name="input">The input file name.</param>
        /// <returns>A stream that contains the target file.</returns>
        public Stream Execute(string input)
        {
            Stream result = null;
            using (Stream fileStream = new FileStream(input, FileMode.Open))
            {
                var streamLength = Convert.ToInt32(fileStream.Length);                
                result = _reader.Execute(Tuple.New(streamLength, fileStream));
            }

            return result;
        }
    }
}