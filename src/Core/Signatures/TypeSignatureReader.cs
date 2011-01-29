using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;
using Tao.Model;

namespace Tao.Signatures
{
    /// <summary>
    /// Represents a type that reads type signatures.
    /// </summary>
    public class TypeSignatureReader : IFunction<Stream, TypeSignature>
    {
        private readonly IFunction<byte, ITuple<TableId, uint>> _typeDefOrRefEncodedReader;
        private readonly IFunction<ITuple<Stream, ICollection<CustomMod>>, int> _readCustomMods;

        private readonly Dictionary<ElementType, Func<Stream, TypeSignature>> _entries =
            new Dictionary<ElementType, Func<Stream, TypeSignature>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeSignatureReader"/> class.
        /// </summary>
        /// <param name="typeDefOrRefEncodedReader">The reader that will read the embbedded type def or type ref token.</param>
        /// <param name="readCustomMods">The reader that will read the CustomMod signatures</param>
        public TypeSignatureReader(IFunction<byte, ITuple<TableId, uint>> typeDefOrRefEncodedReader, IFunction<ITuple<Stream, ICollection<CustomMod>>, int> readCustomMods)
        {
            if (typeDefOrRefEncodedReader == null)
                throw new ArgumentNullException("typeDefOrRefEncodedReader");

            _typeDefOrRefEncodedReader = typeDefOrRefEncodedReader;
            _readCustomMods = readCustomMods;

            CreateEntries();
        }

        private void CreateEntries()
        {
            Func<Stream, TypeSignature> defaultCreator = bytes => new TypeSignature();
            _entries[ElementType.Boolean] = defaultCreator;
            _entries[ElementType.Char] = defaultCreator;
            _entries[ElementType.I1] = defaultCreator;
            _entries[ElementType.I2] = defaultCreator;
            _entries[ElementType.I4] = defaultCreator;
            _entries[ElementType.I8] = defaultCreator;
            _entries[ElementType.U1] = defaultCreator;
            _entries[ElementType.U4] = defaultCreator;
            _entries[ElementType.U8] = defaultCreator;
            _entries[ElementType.R4] = defaultCreator;
            _entries[ElementType.R8] = defaultCreator;
            _entries[ElementType.Object] = defaultCreator;
            _entries[ElementType.String] = defaultCreator;

            _entries[ElementType.Class] = CreateTypeDefOrRefEncodedSignature;
            _entries[ElementType.ValueType] = CreateTypeDefOrRefEncodedSignature;
            _entries[ElementType.Ptr] = CreatePointerSignature;
        }

        /// <summary>
        /// Reads the type signature from the given <paramref name="input"/> bytes.
        /// </summary>
        /// <param name="input">The bytes that represent the type signature.</param>
        /// <returns>A <see cref="TypeSignature"/> instance.</returns>
        public TypeSignature Execute(Stream input)
        {
            var elementType = (ElementType)input.ReadByte();

            if (!_entries.ContainsKey(elementType))
                throw new NotSupportedException(string.Format("Element type not supported: {0}", elementType));

            var createSignature = _entries[elementType];
            var signature = createSignature(input);
            signature.ElementType = elementType;

            return signature;
        }

        private TypeSignature CreatePointerSignature(Stream inputStream)
        {
            if (inputStream.Length == 0)
                throw new ArgumentException("Unexpected end of byte stream", "inputStream");

            var mods = new List<CustomMod>();
            var bytesRead = _readCustomMods.Execute(inputStream, mods);

            PointerSignature result = null;

            var nextElementType = (ElementType)inputStream.PeekByte();
            if (nextElementType != ElementType.Void)
            {
                int targetIndex = bytesRead;
                result = CreateTypePointerSignature(inputStream);
            }
            else
            {
                result = new VoidPointerSignature();
            }

            foreach (var mod in mods)
            {
                result.CustomMods.Add(mod);
            }

            return result;
        }

        private PointerSignature CreateTypePointerSignature(Stream inputStream)
        {
            PointerSignature result;
            var typePointerSignature = new TypePointerSignature();

            var attachedSignature = Execute(inputStream);
            typePointerSignature.TypeSignature = attachedSignature;
            result = typePointerSignature;

            return result;
        }

        private TypeSignature CreateTypeDefOrRefEncodedSignature(Stream inputStream)
        {
            var nextByte = (byte)inputStream.ReadByte();
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
