using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao.Core
{
    /// <summary>
    /// Represents a class that can read a substream using an RVA and a given size.
    /// </summary>
    public class ReadStreamFromRvaAndSize : IFunction<ITuple<int, int, Stream>, Stream>
    {
        private readonly IFunction<ITuple<int, Stream>> _seekAbsoluteFilePositionFromRva;
        private readonly IFunction<ITuple<int, Stream>, Stream> _inMemorySubStreamReader;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public ReadStreamFromRvaAndSize(IFunction<ITuple<int, Stream>> seekAbsoluteFilePositionFromRva, IFunction<ITuple<int, Stream>, Stream> inMemorySubStreamReader)
        {
            _seekAbsoluteFilePositionFromRva = seekAbsoluteFilePositionFromRva;
            _inMemorySubStreamReader = inMemorySubStreamReader;
        }

        /// <summary>
        /// Reads a substream using an RVA and a given size.
        /// </summary>
        /// <param name="input">The RVA, Size, and target stream.</param>
        /// <returns>A substream containing the requested data.</returns>
        public Stream Execute(ITuple<int, int, Stream> input)
        {
            var rva = input.Item1;
            var size = input.Item2;
            var stream = input.Item3;
            
            _seekAbsoluteFilePositionFromRva.Execute(rva, stream);
            return _inMemorySubStreamReader.Execute(size, stream);
        }
    }
}
