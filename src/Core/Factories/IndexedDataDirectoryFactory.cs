using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;

namespace Tao.Core.Factories
{
    /// <summary>
    /// Represents a type that can read a single data directory entry using a given stream and a given index.
    /// </summary>
    public class IndexedDataDirectoryFactory : IFunction<ITuple<int, Stream>, ITuple<int, int>>
    {
        private readonly IFunction<ITuple<int, Stream>> _indexedDataDirectorySeeker;

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexedDataDirectoryFactory"/> class.
        /// </summary>
        /// <param name="indexedDataDirectorySeeker">The seeker that will be used to locate the target data directory entry.</param>
        public IndexedDataDirectoryFactory(IFunction<ITuple<int, Stream>> indexedDataDirectorySeeker)
        {
            _indexedDataDirectorySeeker = indexedDataDirectorySeeker;
        }

        /// <summary>
        /// Reads a single data directory entry using a given stream and a given index.
        /// </summary>
        /// <param name="input">The tuple containing the target index and target stream.</param>
        /// <returns>A tuple containing the RVA and the Size of the data directory entry, respectively.</returns>
        public ITuple<int, int> Execute(ITuple<int, Stream> input)
        {
            var index = input.Item1;
            var stream = input.Item2;

            _indexedDataDirectorySeeker.Execute(index, stream);
            using(var reader = new BinaryReader(stream))
            {
                var rva = reader.ReadInt32();
                var size = reader.ReadInt32();

                return Tuple.New(rva, size);
            }
        }
    }
}
