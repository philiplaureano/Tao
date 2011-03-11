using System.IO;
using Tao.Interfaces;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a class that reads a metadata heap index value.
    /// </summary>
    public class ReadHeapIndexValue : IFunction<ITuple<int, BinaryReader>, uint>
    {
        /// <summary>
        /// Reads a metadata heap index value.
        /// </summary>
        /// <param name="input">The index size and the target binary reader.</param>
        /// <returns>The index value.</returns>
        public uint Execute(ITuple<int, BinaryReader> input)
        {
            var indexSize = input.Item1;
            var reader = input.Item2;
            return indexSize == 2 ? reader.ReadUInt16() : reader.ReadUInt32();
        }
    }
}
