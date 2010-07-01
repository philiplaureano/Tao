using System;
using System.Collections.Generic;
using System.Text;
using Tao.Interfaces;

namespace Tao.Signatures
{
    /// <summary>
    /// Represents a class that reads a CustomMod signature element.
    /// </summary>
    public class CustomModReader : IFunction<IEnumerable<byte>, ITuple<ElementType, TableId, uint>>
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
        public ITuple<ElementType, TableId, uint> Execute(IEnumerable<byte> input)
        {
            var bytes = new List<byte>(input);
            if (bytes.Count != 2)
                throw new ArgumentException("Invalid number of bytes", "input");

            var firstByte = bytes[0];
            var elementType = (ElementType) firstByte;
            var encodedTypeDefOrRef = _typeDefOrRefEncodedReader.Execute(bytes[1]);

            return Tuple.New(elementType, encodedTypeDefOrRef.Item1, encodedTypeDefOrRef.Item2);
        }
    }
}
