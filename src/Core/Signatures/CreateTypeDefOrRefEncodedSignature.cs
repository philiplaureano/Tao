using System.IO;
using Tao.Interfaces;
using Tao.Model;

namespace Tao.Signatures
{
    /// <summary>
    /// Represents a type that can read TypeDefOrRefEncoded <see cref="TypeSignature"/> instances into memory.
    /// </summary>
    public class CreateTypeDefOrRefEncodedSignature : IFunction<Stream, TypeDefOrRefEncodedSignature>
    {
        private readonly IFunction<byte, ITuple<TableId, uint>> _typeDefOrRefEncodedReader;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTypeDefOrRefEncodedSignature"/> class.
        /// </summary>
        public CreateTypeDefOrRefEncodedSignature(IFunction<byte, ITuple<TableId, uint>> typeDefOrRefEncodedReader)
        {
            _typeDefOrRefEncodedReader = typeDefOrRefEncodedReader;
        }

        /// <summary>
        /// Reads a <see cref="TypeDefOrRefEncodedSignature"/> into memory.
        /// </summary>
        /// <param name="input">The input stream that contains the signature.</param>
        /// <returns>A <see cref="TypeDefOrRefEncodedSignature"/> object instance.</returns>
        public TypeDefOrRefEncodedSignature Execute(Stream input)
        {
            var nextByte = (byte)input.ReadByte();
            var decodedToken = _typeDefOrRefEncodedReader.Execute(nextByte);
            var encodedSignature = new TypeDefOrRefEncodedSignature
            {
                TableId = decodedToken.Item1,
                TableIndex = decodedToken.Item2
            };

            return encodedSignature;
        }
    }
}
