using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao.Core.Seekers
{
    /// <summary>
    /// Represents a class that realigns the stream position according to a particular byte boundary
    /// </summary>
    public class RealignStreamPosition : IFunction<ITuple<int, Stream>>
    {
        public void Execute(ITuple<int, Stream> input)
        {
            var stream = input.Item2;
            var byteAlignment = input.Item1;

            // Move the reader position beyond
            // the padded bytes of the byte alignment
            var currentPosition = stream.Position;
            var paddedBytes = currentPosition % byteAlignment;

            if (paddedBytes <= 0)
                return;

            var nextPosition = currentPosition - paddedBytes + byteAlignment;
            stream.Seek(nextPosition, SeekOrigin.Begin);
        }
    }
}
