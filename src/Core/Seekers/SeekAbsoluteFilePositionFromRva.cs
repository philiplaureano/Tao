using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao.Core.Seekers
{
    /// <summary>
    /// Represents a type that sets the stream pointer to point to an absolute address derived from the given RVA.
    /// </summary>
    public class SeekAbsoluteFilePositionFromRva : IFunction<ITuple<int, Stream>>
    {
        private readonly IFunction<ITuple<int, Stream>, int> _resolveAbsoluteFilePositionFromRva;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeekAbsoluteFilePositionFromRva"/> class.
        /// </summary>
        public SeekAbsoluteFilePositionFromRva(IFunction<ITuple<int, Stream>, int> resolveAbsoluteFilePositionFromRva)
        {
            _resolveAbsoluteFilePositionFromRva = resolveAbsoluteFilePositionFromRva;
        }

        /// <summary>
        /// Sets the stream pointer to point to an absolute address derived from the given RVA.
        /// </summary>
        /// <param name="input">The input RVA and input stream.</param>
        public void Execute(ITuple<int, Stream> input)
        {
            var absolutePosition = _resolveAbsoluteFilePositionFromRva.Execute(input);
            var stream = input.Item2;
            stream.Seek(absolutePosition, SeekOrigin.Begin);
        }
    }
}
