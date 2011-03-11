using System;
using System.IO;
using Tao.Interfaces;

namespace Tao.Signatures
{
    /// <summary>
    /// Represents a class that reads a CustomMod signature element.
    /// </summary>
    public class CustomModReader : IFunction<Stream, ITuple<ElementType, TableId, uint>>
    {
        private readonly IFunction<byte, ITuple<TableId, uint>> _typeDefOrRefEncodedReader;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomModReader"/> class.
        /// </summary>
        /// <param name="typeDefOrRefEncodedReader">The TypeDefOrRefEncoded signature reader.</param>
        public CustomModReader(IFunction<byte, ITuple<TableId, uint>> typeDefOrRefEncodedReader)
        {
            _typeDefOrRefEncodedReader = typeDefOrRefEncodedReader;
        }

        /// <summary>
        /// Reads a CustomMod signature element.
        /// </summary>
        /// <param name="input">The bytes that represent the CustomMod.</param>
        /// <returns>A CustomMod descriptor.</returns>
        public ITuple<ElementType, TableId, uint> Execute(Stream input)
        {
            if (input.Length != 2)
                throw new ArgumentException("Invalid number of bytes", "input");

            var firstByte = (byte)input.ReadByte();
            var elementType = (ElementType) firstByte;

            var secondByte = (byte) input.ReadByte();
            var encodedTypeDefOrRef = _typeDefOrRefEncodedReader.Execute(secondByte);

            return Tuple.New(elementType, encodedTypeDefOrRef.Item1, encodedTypeDefOrRef.Item2);
        }
    }
}
