using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;
using Tao.Model;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a reader that can read multiple <see cref="CustomMod"/> instances into memory.
    /// </summary>
    public class ReadCustomMods : IFunction<ITuple<Stream, ICollection<CustomMod>>, int>
    {
        private readonly IFunction<Stream, ITuple<ElementType, TableId, uint>> _customModReader;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadCustomMods"/> class.
        /// </summary>
        /// <param name="customModReader">The custom mod reader.</param>
        public ReadCustomMods(IFunction<Stream, ITuple<ElementType, TableId, uint>> customModReader)
        {
            _customModReader = customModReader;
        }

        public int Execute(ITuple<Stream, ICollection<CustomMod>> input)
        {
            var stream = input.Item1;

            // Keep track of the starting position
            // so that we can reset the stream position once
            // we're done reading the CustomMods
            var startPosition = stream.Position;

            var bytes = stream.ReadToEnd(false);

            var byteQueue = new Queue<byte>(bytes);
            var mods = input.Item2;
            var nextByte = (ElementType)byteQueue.Peek();
            var bytesRead = 0;
            while ((nextByte == ElementType.CMOD_OPT
                    || nextByte == ElementType.CMOD_REQD) && byteQueue.Count >= 2)
            {
                var currentBytes = new List<byte> { byteQueue.Dequeue(), byteQueue.Dequeue() };
                var currentStream = new MemoryStream(currentBytes.ToArray());

                bytesRead += 2;
                var mod = _customModReader.Execute(currentStream);
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

            // Reset the stream pointer to point to the last known read position
            var endPosition = startPosition + bytesRead;
            stream.Seek(endPosition,SeekOrigin.Begin);

            return bytesRead;
        }
    }
}
