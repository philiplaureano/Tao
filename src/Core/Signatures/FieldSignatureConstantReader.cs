using System;
using System.Collections.Generic;
using System.Text;
using Tao.Interfaces;

namespace Tao.Signatures
{
    /// <summary>
    /// Represents a class that recognizes the field constant in a FieldSig metadata signature.
    /// </summary>
    public class FieldSignatureConstantReader : IFunction<byte, bool>
    {
        /// <summary>
        /// Determines whether or not the given byte <paramref name="input"/> matches
        /// the field constant in a metadata signature.
        /// </summary>
        /// <param name="input">The byte input.</param>
        /// <returns>Returns <c>true</c> if the signature is a match; otherwise, it will return <c>false</c>.</returns>
        public bool Execute(byte input)
        {
            return input == 0x6;
        }
    }
}
