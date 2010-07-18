using System;
using System.Collections.Generic;
using System.Text;
using Tao.Interfaces;
using Tao.Model;

namespace Tao.Signatures
{
    /// <summary>
    /// Represents a type that reads type signatures.
    /// </summary>
    public class TypeSignatureReader : IFunction<IEnumerable<byte>, TypeSignature>
    {
        private readonly IFunction<byte, ITuple<TableId, uint>> _typeDefOrRefEncodedReader;
        private readonly IFunction<IEnumerable<byte>, ITuple<ElementType, TableId, uint>> _customModReader;

        private readonly Dictionary<ElementType, Func<IList<byte>, TypeSignature>> _entries =
            new Dictionary<ElementType, Func<IList<byte>, TypeSignature>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeSignatureReader"/> class.
        /// </summary>
        /// <param name="typeDefOrRefEncodedReader">The reader that will read the embbedded type def or type ref token.</param>
        /// <param name="customModReader">The reader that will read the CustomMod signature type</param>
        public TypeSignatureReader(IFunction<byte, ITuple<TableId, uint>> typeDefOrRefEncodedReader, IFunction<IEnumerable<byte>,
            ITuple<ElementType, TableId, uint>> customModReader)
        {
            if (typeDefOrRefEncodedReader == null)
                throw new ArgumentNullException("typeDefOrRefEncodedReader");

            _typeDefOrRefEncodedReader = typeDefOrRefEncodedReader;
            _customModReader = customModReader;

            CreateEntries();
        }

        private void CreateEntries()
        {
            Func<IList<byte>, TypeSignature> defaultCreator = bytes => new TypeSignature();
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
        public TypeSignature Execute(IEnumerable<byte> input)
        {
            var bytes = new List<byte>(input);
            var elementType = (ElementType)bytes[0];

            if (!_entries.ContainsKey(elementType))
                throw new NotSupportedException(string.Format("Element type not supported: {0}", elementType));

            var createSignature = _entries[elementType];
            var signature = createSignature(bytes);
            signature.ElementType = elementType;

            return signature;
        }

        private TypeSignature CreatePointerSignature(IList<byte> bytes)
        {
            if (bytes.Count == 0)
                throw new ArgumentException("Unexpected end of byte stream", "bytes");

            var mods = new List<CustomMod>();
            var byteQueue = new Queue<byte>(bytes);
            var elementType = (ElementType)byteQueue.Dequeue();
            var bytesRead = ReadCustomMods(byteQueue, mods);

            PointerSignature result = null;

            var targetIndex = bytesRead > 0 ? bytesRead : 0;
            targetIndex++;

            var nextElementType = (ElementType)bytes[targetIndex];
            if (nextElementType == ElementType.Void)
                result = new VoidPointerSignature();

            foreach (var mod in mods)
            {
                result.CustomMods.Add(mod);
            }

            return result;
        }

        private int ReadCustomMods(Queue<byte> byteQueue, ICollection<CustomMod> mods)
        {
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
                nextByte = byteQueue.Count >= 1 ? (ElementType)byteQueue.Dequeue() : default(ElementType);
            }
            return bytesRead;
        }

        private TypeSignature CreateTypeDefOrRefEncodedSignature(IList<byte> bytes)
        {
            var decodedToken = _typeDefOrRefEncodedReader.Execute(bytes[1]);
            var encodedSignature = new TypeDefOrRefEncodedSignature
                                       {
                                           TableId = decodedToken.Item1,
                                           TableIndex = decodedToken.Item2
                                       };

            return encodedSignature;
        }
    }
}
