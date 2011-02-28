using System;
using System.Collections.Generic;
using System.IO;
using Tao.Interfaces;
using Tao.Model;

namespace Tao.Signatures
{
    /// <summary>
    /// Represents a type that can read <see cref="PropertySignature"/> instances from a given stream.
    /// </summary>
    public class PropertySignatureReader : IFunction<Stream, PropertySignature>
    {
        private readonly IFunction<Stream, uint> _readCompressedInteger;
        private readonly IFunction<ITuple<Stream, ICollection<CustomMod>>, int> _readCustomMods;
        private readonly IFunction<Stream, TypeSignature> _typeSignatureReader;
        private readonly IParamSignatureReader _paramSignatureReader;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertySignatureReader"/> class.
        /// </summary>
        public PropertySignatureReader(IFunction<Stream, uint> readCompressedInteger, IFunction<ITuple<Stream, ICollection<CustomMod>>, int> readCustomMods, IFunction<Stream, TypeSignature> typeSignatureReader, IParamSignatureReader paramSignatureReader)
        {
            _readCompressedInteger = readCompressedInteger;
            _readCustomMods = readCustomMods;
            _typeSignatureReader = typeSignatureReader;
            _paramSignatureReader = paramSignatureReader;
        }

        /// <summary>
        /// Reads a <see cref="PropertySignature"/> instance from a given stream.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <returns>A property signature.</returns>
        public PropertySignature Execute(Stream input)
        {
            var propertyByte = input.ReadByte();
            if (propertyByte != 0x8 && (propertyByte & 0x28) == 0)
                throw new BadImageFormatException("Unable to read prperty signature");

            // Read the parameter count            
            var parameterCount = _readCompressedInteger.Execute(input);
            var signature = new PropertySignature { ParamCount = parameterCount };

            const byte hasThis = 0x20;
            if ((propertyByte & 0x28) != 0 && (propertyByte & hasThis) != 0)
                signature.HasThis = true;

            // Read the custom mods
            _readCustomMods.Execute(input, signature.CustomMods);

            // Read the getter return type
            var type = _typeSignatureReader.Execute(input);
            signature.Type = type;

            for (var i = 0; i < parameterCount; i++)
            {
                var param = _paramSignatureReader.Execute(input);
                signature.Parameters.Add(param);
            }

            return signature;
        }
    }
}
