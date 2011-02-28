using System;
using System.Collections.Generic;
using System.IO;
using Tao.Interfaces;
using Tao.Model;

namespace Tao.Signatures
{
    /// <summary>
    /// Represents a type that creates primitive <see cref="TypeSignature"/> instances.
    /// </summary>
    public class CreatePrimitiveTypeSignature : IFunction<ITuple<Stream, ElementType>, TypeSignature>
    {
        private readonly Dictionary<ElementType, Func<Stream, TypeSignature>> _primitiveEntries =
                new Dictionary<ElementType, Func<Stream, TypeSignature>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePrimitiveTypeSignature"/> class.
        /// </summary>
        public CreatePrimitiveTypeSignature()
        {
            Func<Stream, TypeSignature> defaultCreator = bytes => new TypeSignature();
            _primitiveEntries[ElementType.Boolean] = defaultCreator;
            _primitiveEntries[ElementType.Char] = defaultCreator;
            _primitiveEntries[ElementType.I1] = defaultCreator;
            _primitiveEntries[ElementType.I2] = defaultCreator;
            _primitiveEntries[ElementType.I4] = defaultCreator;
            _primitiveEntries[ElementType.I8] = defaultCreator;
            _primitiveEntries[ElementType.U1] = defaultCreator;
            _primitiveEntries[ElementType.U2] = defaultCreator;
            _primitiveEntries[ElementType.U4] = defaultCreator;
            _primitiveEntries[ElementType.U8] = defaultCreator;
            _primitiveEntries[ElementType.R4] = defaultCreator;
            _primitiveEntries[ElementType.R8] = defaultCreator;
        }

        public TypeSignature Execute(ITuple<Stream, ElementType> input)
        {
            var inputStream = input.Item1;
            var elementType = input.Item2;

            var createSignature = _primitiveEntries[elementType];
            var signature = createSignature(inputStream);
            signature.ElementType = elementType;
            return signature;
        }
    }
}
