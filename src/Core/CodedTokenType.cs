using System;
using System.Collections.Generic;
using System.Text;

namespace Tao
{
    public enum CodedTokenType
    {
        TypeDefOrTypeRef,
        HasConstant,
        HasCustomAttribute,
        HasFieldMarshall,
        HasDeclSecurity,
        MemberRefParent,
        HasSemantics,
        MethodDefOrRef,
        MemberForwarded,
        Implementation,
        CustomAttributeType,
        ResolutionScope
    }
}
