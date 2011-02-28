using System.Collections.Generic;
using Tao.Interfaces;

namespace Tao
{
    /// <summary>
    /// Represents a class that determines the <see cref="TableId"/> of the given coded token type and token tag.
    /// </summary>
    public class GetTableId : IFunction<ITuple<CodedTokenType, byte>, TableId?>
    {
        private Dictionary<CodedTokenType, IDictionary<byte, TableId>> _entries =
            new Dictionary<CodedTokenType, IDictionary<byte, TableId>>();

        /// <summary>
        /// Initializes a new instance fo the <see cref="GetTableId"/> class.
        /// </summary>
        public GetTableId()
        {
            _entries[CodedTokenType.CustomAttributeType] = new Dictionary<byte, TableId>();
            _entries[CodedTokenType.HasConstant] = new Dictionary<byte, TableId>();
            _entries[CodedTokenType.HasCustomAttribute] = new Dictionary<byte, TableId>();
            _entries[CodedTokenType.HasDeclSecurity] = new Dictionary<byte, TableId>();
            _entries[CodedTokenType.HasFieldMarshall] = new Dictionary<byte, TableId>();
            _entries[CodedTokenType.HasSemantics] = new Dictionary<byte, TableId>();
            _entries[CodedTokenType.Implementation] = new Dictionary<byte, TableId>();
            _entries[CodedTokenType.MemberForwarded] = new Dictionary<byte, TableId>();
            _entries[CodedTokenType.MemberRefParent] = new Dictionary<byte, TableId>();
            _entries[CodedTokenType.MethodDefOrRef] = new Dictionary<byte, TableId>();
            _entries[CodedTokenType.ResolutionScope] = new Dictionary<byte, TableId>();
            _entries[CodedTokenType.TypeDefOrTypeRef] = new Dictionary<byte, TableId>();

            _entries[CodedTokenType.CustomAttributeType][2] = TableId.MethodDef;
            _entries[CodedTokenType.CustomAttributeType][3] = TableId.MemberRef;

            _entries[CodedTokenType.HasConstant][0] = TableId.Field;
            _entries[CodedTokenType.HasConstant][1] = TableId.Param;
            _entries[CodedTokenType.HasConstant][2] = TableId.Property;

            _entries[CodedTokenType.HasCustomAttribute][0] = TableId.MethodDef;
            _entries[CodedTokenType.HasCustomAttribute][1] = TableId.Field;
            _entries[CodedTokenType.HasCustomAttribute][2] = TableId.TypeRef;
            _entries[CodedTokenType.HasCustomAttribute][3] = TableId.TypeDef;
            _entries[CodedTokenType.HasCustomAttribute][4] = TableId.Param;
            _entries[CodedTokenType.HasCustomAttribute][5] = TableId.InterfaceImpl;
            _entries[CodedTokenType.HasCustomAttribute][6] = TableId.MemberRef;
            _entries[CodedTokenType.HasCustomAttribute][7] = TableId.Module;
            _entries[CodedTokenType.HasCustomAttribute][8] = TableId.DeclSecurity;
            _entries[CodedTokenType.HasCustomAttribute][9] = TableId.Property;
            _entries[CodedTokenType.HasCustomAttribute][10] = TableId.Event;
            _entries[CodedTokenType.HasCustomAttribute][11] = TableId.StandAloneSig;
            _entries[CodedTokenType.HasCustomAttribute][12] = TableId.ModuleRef;
            _entries[CodedTokenType.HasCustomAttribute][13] = TableId.TypeSpec;
            _entries[CodedTokenType.HasCustomAttribute][14] = TableId.Assembly;
            _entries[CodedTokenType.HasCustomAttribute][15] = TableId.AssemblyRef;
            _entries[CodedTokenType.HasCustomAttribute][16] = TableId.File;
            _entries[CodedTokenType.HasCustomAttribute][17] = TableId.ExportedType;
            _entries[CodedTokenType.HasCustomAttribute][18] = TableId.ManifestResource;

            _entries[CodedTokenType.HasDeclSecurity][0] = TableId.TypeDef;
            _entries[CodedTokenType.HasDeclSecurity][1] = TableId.MethodDef;
            _entries[CodedTokenType.HasDeclSecurity][2] = TableId.Assembly;

            _entries[CodedTokenType.HasFieldMarshall][0] = TableId.Field;
            _entries[CodedTokenType.HasFieldMarshall][1] = TableId.Param;

            _entries[CodedTokenType.HasSemantics][0] = TableId.Event;
            _entries[CodedTokenType.HasSemantics][1] = TableId.Property;

            _entries[CodedTokenType.Implementation][0] = TableId.File;
            _entries[CodedTokenType.Implementation][2] = TableId.AssemblyRef;
            _entries[CodedTokenType.Implementation][3] = TableId.ExportedType;

            _entries[CodedTokenType.MemberForwarded][0] = TableId.Field;
            _entries[CodedTokenType.MemberForwarded][1] = TableId.MethodDef;

            _entries[CodedTokenType.MemberRefParent][1] = TableId.TypeRef;
            _entries[CodedTokenType.MemberRefParent][2] = TableId.ModuleRef;
            _entries[CodedTokenType.MemberRefParent][3] = TableId.MethodDef;
            _entries[CodedTokenType.MemberRefParent][4] = TableId.TypeSpec;

            _entries[CodedTokenType.MethodDefOrRef][0] = TableId.MethodDef;
            _entries[CodedTokenType.MethodDefOrRef][1] = TableId.MemberRef;

            _entries[CodedTokenType.ResolutionScope][0] = TableId.Module;
            _entries[CodedTokenType.ResolutionScope][1] = TableId.ModuleRef;
            _entries[CodedTokenType.ResolutionScope][2] = TableId.AssemblyRef;
            _entries[CodedTokenType.ResolutionScope][3] = TableId.TypeRef;

            _entries[CodedTokenType.TypeDefOrTypeRef][0] = TableId.TypeDef;
            _entries[CodedTokenType.TypeDefOrTypeRef][1] = TableId.TypeRef;
            _entries[CodedTokenType.TypeDefOrTypeRef][2] = TableId.TypeSpec;
        }

        /// <summary>
        /// Determines the <see cref="TableId"/> of the given coded token type and token tag.
        /// </summary>
        /// <param name="input">The token type and the table tag.</param>
        /// <returns>The <see cref="TableId"/> that corresponds to the given table tag.</returns>
        public TableId? Execute(ITuple<CodedTokenType, byte> input)
        {
            var tokenType = input.Item1;
            var tag = input.Item2;

            if (!_entries.ContainsKey(tokenType))
                return null;

            var tagEntries = _entries[tokenType];
            if (!tagEntries.ContainsKey(tag))
                return null;

            return _entries[tokenType][tag];
        }
    }
}
