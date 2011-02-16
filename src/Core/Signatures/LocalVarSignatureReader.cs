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
    /// Represents a class that reads <see cref="LocalVarSignature"/> instances from a given stream.
    /// </summary>
    public class LocalVarSignatureReader : IFunction<Stream, LocalVarSignature>
    {
        private readonly IFunction<Stream, uint> _readCompressedInteger;
        private readonly IFunction<ITuple<Stream, ICollection<CustomMod>>, int> _readCustomMods;
        private readonly IFunction<Stream, TypeSignature> _typeSignatureReader;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalVarSignatureReader"/> class.
        /// </summary>
        /// <param name="readCompressedInteger">The compressed integer reader.</param>
        /// <param name="readCustomMods">The custom mod reader.</param>
        /// <param name="typeSignatureReader">The type signature reader.</param>
        public LocalVarSignatureReader(IFunction<Stream, uint> readCompressedInteger, IFunction<ITuple<Stream, ICollection<CustomMod>>, int> readCustomMods, IFunction<Stream, TypeSignature> typeSignatureReader)
        {
            _readCompressedInteger = readCompressedInteger;
            _readCustomMods = readCustomMods;
            _typeSignatureReader = typeSignatureReader;
        }

        /// <summary>
        /// Reads a <see cref="LocalVarSignature"/> instance from the given stream.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <returns>A <see cref="LocalVarSignature"/> the describes the local variables in a given method body.</returns>
        public LocalVarSignature Execute(Stream input)
        {
            // Read the LOCAL_SIG constant
            var localVarSig = (byte)input.ReadByte();
            if (localVarSig != 0x7)
                throw new BadImageFormatException("Invalid LocalVarSig signature; expected LOCAL_SIG constant");

            // Determine the number of local variables
            var count = _readCompressedInteger.Execute(input);

            var signature = new LocalVarSignature();
            for (var i = 0; i < count; i++)
            {
                var isTypedByRef = ((byte)input.PeekByte()) == ((byte)ElementType.TypedByRef);
                if (isTypedByRef)
                {
                    // Ignore the element byte
                    input.Seek(1, SeekOrigin.Current);
                    signature.LocalVariables.Add(new TypedByRefVariable());
                    continue;
                }

                // Read the custom mods    
                var localVariable = new TypedLocalVariable();
                _readCustomMods.Execute(input, localVariable.CustomMods);

                var nextByte = (byte)input.ReadByte();
                var isPinned = nextByte == (byte)ElementType.Pinned;
                localVariable.IsPinned = isPinned;

                // Move the stream to the next position if the 
                // local variable is pinned
                nextByte = isPinned ? (byte)input.ReadByte() : nextByte;

                var isByRef = ((byte)nextByte) == (byte)ElementType.ByRef;
                localVariable.IsByRef = isByRef;
                localVariable.Type = _typeSignatureReader.Execute(input);

                // Read the variable type
                signature.LocalVariables.Add(localVariable);
            }

            return signature;
        }
    }
}
