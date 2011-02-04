using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Tao.Interfaces;
using Tao.Model;

namespace Tao.Signatures
{
    /// <summary>
    /// Represents a type that can read <see cref="FieldSignature"/> instances from a given stream.
    /// </summary>
    public class FieldSignatureReader : IFunction<Stream, FieldSignature>
    {
        private readonly IFunction<byte, bool> _fieldSignatureConstantReader;
        private readonly IFunction<ITuple<Stream, ICollection<CustomMod>>, int> _readCustomMods;
        private IFunction<Stream, TypeSignature> _typeSignatureReader;

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldSignatureReader"/> class.
        /// </summary>
        public FieldSignatureReader(IFunction<byte, bool> fieldSignatureConstantReader, IFunction<ITuple<Stream, ICollection<CustomMod>>, int> readCustomMods, IFunction<Stream, TypeSignature> typeSignatureReader)
        {
            _fieldSignatureConstantReader = fieldSignatureConstantReader;
            _readCustomMods = readCustomMods;
            _typeSignatureReader = typeSignatureReader;
        }

        /// <summary>
        /// Reads a <see cref="FieldSignature"/> from the given <paramref name="input">input stream</paramref>.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <returns>A field signature.</returns>
        public FieldSignature Execute(Stream input)
        {
            // Search for the FIELD constant
            if (!_fieldSignatureConstantReader.Execute((byte)input.ReadByte()))
                throw new BadImageFormatException("Unable to read the field signature");

            var signature = new FieldSignature();
            _readCustomMods.Execute(input, signature.CustomMods);
            signature.FieldType = _typeSignatureReader.Execute(input);

            return signature;
        }
    }
}
