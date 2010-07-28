using System;
using System.Collections.Generic;
using System.Text;
using Tao.Interfaces;
using Tao.Model;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a reader that can read multiple <see cref="CustomMod"/> instances into memory.
    /// </summary>
    public class ReadCustomMods : IFunction<ITuple<Queue<byte>, ICollection<CustomMod>>, int>
    {
        private readonly IFunction<IEnumerable<byte>, ITuple<ElementType, TableId, uint>> _customModReader;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadCustomMods"/> class.
        /// </summary>
        /// <param name="customModReader">The custom mod reader.</param>
        public ReadCustomMods(IFunction<IEnumerable<byte>, ITuple<ElementType, TableId, uint>> customModReader)
        {
            _customModReader = customModReader;
        }

        public int Execute(ITuple<Queue<byte>, ICollection<CustomMod>> input)
        {
            var byteQueue = input.Item1;
            var mods = input.Item2;
            var nextByte = (ElementType)byteQueue.Peek();
            var bytesRead = 0;
            while ((nextByte == ElementType.CMOD_OPT
                    || nextByte == ElementType.CMOD_REQD) && byteQueue.Count >= 2)
            {
                var currentBytes = new List<byte> { byteQueue.Dequeue(), byteQueue.Dequeue() };

                bytesRead += 2;
                var mod = _customModReader.Execute(currentBytes);
                var customMod = new CustomMod() { ElementType = mod.Item1, TableId = mod.Item2, RowIndex = mod.Item3 };

                mods.Add(customMod);
                //nextByte = byteQueue.Count >= 1 ? (ElementType)byteQueue.Dequeue() : default(ElementType);

                if (byteQueue.Count >= 1)
                {
                    var peekByte = (ElementType)byteQueue.Peek();                    

                    nextByte = peekByte;
                }
                else
                {
                    nextByte = default(ElementType);
                }
            }

            return bytesRead;
        }
    }
}
