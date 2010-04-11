using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao
{
    /// <summary>
    /// Represents a class that converts a relative virtual address into an absolute file position.
    /// </summary>
    public class ResolveAbsoluteFilePositionFromRva : IFunction<ITuple<int, Stream>, int>
    {
        private readonly IFunction<Stream, int> _readSectionAlignment;
        private readonly IFunction<Stream, int> _readFileAlignment;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResolveAbsoluteFilePositionFromRva"/> class.
        /// </summary>
        public ResolveAbsoluteFilePositionFromRva(IFunction<Stream, int> readSectionAlignment, IFunction<Stream, int> readFileAlignment)
        {
            _readSectionAlignment = readSectionAlignment;
            _readFileAlignment = readFileAlignment;
        }

        /// <summary>
        /// Converts a relative virtual address into an absolute file position.
        /// </summary>
        /// <param name="input">The tuple that contains the RVA and the target stream.</param>
        /// <returns>The absolute file position of the RVA.</returns>
        public int Execute(ITuple<int, Stream> input)
        {
            var stream = input.Item2;
            var rva = input.Item1;
            var sectionAlignment = _readSectionAlignment.Execute(stream);
            var fileAlignment = _readFileAlignment.Execute(stream);

            var sectionId = rva / sectionAlignment;

            var baseAddress = fileAlignment * sectionId;
            var offset = rva%sectionAlignment;

            return baseAddress + offset;
        }
    }
}
