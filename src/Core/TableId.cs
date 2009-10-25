using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Core
{
    /// <summary>
    /// Represents the constants that uniquely identify a given metadata table in a .NET executable image.
    /// </summary>
    public enum TableId : byte
    {
        /// <summary>
        /// The assembly table.
        /// </summary>
        Assembly = 0x20,

        /// <summary>
        /// The AssemblyOS table.
        /// </summary>
        AssemblyOS = 0x22,

        /// <summary>
        /// The AssemblyProcessor table.
        /// </summary>
        AssemblyProcessor = 0x21,

        /// <summary>
        /// The AssemblyRef table.
        /// </summary>
        AssemblyRef = 0x23,

        /// <summary>
        /// The AssemblyRefOS table.
        /// </summary>
        AssemblyRefOS = 0x25,

        /// <summary>
        /// The AssemblyRefProcessor table.
        /// </summary>
        AssemblyRefProcessor = 0x24,

        /// <summary>
        /// The ClassLayout table.
        /// </summary>
        ClassLayout = 0x0f,

        /// <summary>
        /// The Constant table.
        /// </summary>
        Constant = 0x0b,

        /// <summary>
        /// The CustomAttribute table.
        /// </summary>
        CustomAttribute = 0x0c,

        /// <summary>
        /// The DeclSecurity table.
        /// </summary>
        DeclSecurity = 0x0e,

        /// <summary>
        /// The EventMap table.
        /// </summary>
        EventMap = 0x12,

        /// <summary>
        /// The Event table.
        /// </summary>
        Event = 0x14,

        /// <summary>
        /// The ExportedType table.
        /// </summary>
        ExportedType = 0x27,

        /// <summary>
        /// The Field table.
        /// </summary>
        Field = 0x04,

        /// <summary>
        /// The FieldLayout table.
        /// </summary>
        FieldLayout = 0x10,

        /// <summary>
        /// The FieldMarshal table.
        /// </summary>
        FieldMarshal = 0x0d,

        /// <summary>
        /// The FieldRVA table.
        /// </summary>
        FieldRVA = 0x1d,

        /// <summary>
        /// The File table.
        /// </summary>
        File = 0x26,

        /// <summary>
        /// The ImplMap table.
        /// </summary>
        ImplMap = 0x1c,

        /// <summary>
        /// The InterfaceImpl table.
        /// </summary>
        InterfaceImpl = 0x09,

        /// <summary>
        /// The ManifestResource table.
        /// </summary>
        ManifestResource = 0x28,

        /// <summary>
        /// The MemberRef table.
        /// </summary>
        MemberRef = 0x0a,

        /// <summary>
        /// The MethodDef table.
        /// </summary>
        MethodDef = 0x06,

        /// <summary>
        /// The MethodImpl table.
        /// </summary>
        MethodImpl = 0x19,

        /// <summary>
        /// The MethodSemantics table.
        /// </summary>
        MethodSemantics = 0x18,

        /// <summary>
        /// The Module table.
        /// </summary>
        Module = 0x00,

        /// <summary>
        /// The ModuleRef table.
        /// </summary>
        ModuleRef = 0x1A,

        /// <summary>
        /// The NestedClass table.
        /// </summary>
        NestedClass = 0x29,

        /// <summary>
        /// The Param table.
        /// </summary>
        Param = 0x08,

        /// <summary>
        /// The property table.
        /// </summary>
        Property = 0x17,

        /// <summary>
        /// The PropertyMap table.
        /// </summary>
        PropertyMap = 0x15,

        /// <summary>
        /// The StandAloneSig table.
        /// </summary>
        StandAloneSig = 0x11,

        /// <summary>
        /// The TypeDef table.
        /// </summary>
        TypeDef = 0x02,

        /// <summary>
        /// The TypeRef table.
        /// </summary>
        TypeRef = 0x01,

        /// <summary>
        /// The TypeSpec table.
        /// </summary>
        TypeSpec = 0x1b,
    }
}
