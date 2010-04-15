using System;
using System.Collections.Generic;
using System.Text;
using Tao.Interfaces;

namespace Tao
{
    /// <summary>
    /// Represents a class that determines the number of bits required to encode a coded token tag.
    /// </summary>
    public class GetNumberOfBitsToEncodeTag : IFunction<CodedTokenType, uint>
    {
        private readonly Dictionary<CodedTokenType, uint> _entries = new Dictionary<CodedTokenType, uint>();
        
        /// <summary>
        /// Initializes a new instance of the <see cref="GetNumberOfBitsToEncodeTag"/> class.
        /// </summary>
        public GetNumberOfBitsToEncodeTag()
        {
            _entries[CodedTokenType.CustomAttributeType] = 3;
            _entries[CodedTokenType.HasConstant] = 2;
            _entries[CodedTokenType.TypeDefOrTypeRef] = 2;
            _entries[CodedTokenType.HasCustomAttribute] = 5;
            _entries[CodedTokenType.HasDeclSecurity] = 2;
            _entries[CodedTokenType.MemberRefParent] = 3;
            _entries[CodedTokenType.HasSemantics] = 1;
            _entries[CodedTokenType.MethodDefOrRef] = 1;
            _entries[CodedTokenType.MemberForwarded] = 1;
            _entries[CodedTokenType.Implementation] = 2;
            _entries[CodedTokenType.ResolutionScope] = 2;
        }

        /// <summary>
        /// Determines the number of bits required to encode a coded token tag.
        /// </summary>
        /// <param name="input">The coded token type.</param>
        /// <returns>The number of bits required to encode the token tag.</returns>
        public uint Execute(CodedTokenType input)
        {
            return _entries[input];
        }
    }
}
