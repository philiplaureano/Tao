using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Model
{
    /// <summary>
    /// Represents flags that describe a given <see cref="TypeDefRow"/> instance.
    /// </summary>
    public enum TypeAttributes : uint
    {
        /// <summary>
        /// Use this mask to retrieve visibility information.
        /// </summary>
        VisibilityMask = 0x00000007,

        /// <summary>
        /// Class has no public scope.
        /// </summary>
        NotPublic = 0x00000000,

        /// <summary>
        /// Class has public scope.
        /// </summary>
        Public = 0x00000001,

        /// <summary>
        /// Class is nested with no public visibility.
        /// </summary>
        NestedPublic = 0x00000002,

        /// <summary>
        /// Class is nested with private visibility.
        /// </summary>
        NestedPrivate = 0x00000003,

        /// <summary>
        /// Class is nested with family visibility.
        /// </summary>
        NestedFamily = 0x00000004,

        /// <summary>
        /// Class is nested with assembly visibility.
        /// </summary>
        NestedAssembly = 0x00000005,

        /// <summary>
        /// Class is nested with family and assembly visibility.
        /// </summary>
        NestedFamANDAssem = 0x00000006,

        /// <summary>
        /// Class is nested with family or assembly visibility.
        /// </summary>
        NestedFamORAssem = 0x000000007,

        /// <summary>
        /// Use this mask to retireve class lyaout information.
        /// </summary>
        LayoutMask = 0x00000018,

        /// <summary>
        /// Class fields are auto-laid out.
        /// </summary>
        AutoLayout = 0x00000000,

        /// <summary>
        /// Class fields are laid out sequentially.
        /// </summary>
        SequentialLayout = 0x00000008,

        /// <summary>
        /// Layout is supplied explicitly.
        /// </summary>
        ExplicitLayout = 0x00000010,

        /// <summary>
        /// Use this mask to retrieve class semantics information.
        /// </summary>
        ClassSemanticsMask = 0x00000020,

        /// <summary>
        /// Type is a class.
        /// </summary>
        Class = 0x00000000,

        /// <summary>
        /// Type is an interface.
        /// </summary>
        Interface = 0x00000020,

        /// <summary>
        /// Class is abstract.
        /// </summary>
        Abstract = 0x00000080,

        /// <summary>
        /// Class cannot be extended.
        /// </summary>
        Sealed = 0x00000100,

        /// <summary>
        /// Class name is special.
        /// </summary>
        SpecialName = 0x0000400,

        /// <summary>
        /// Class / Interface is imported.
        /// </summary>
        Import = 0x00001000,

        /// <summary>
        /// Class is serializable.
        /// </summary>
        Serializable = 0x00002000,

        /// <summary>
        /// Use this mask to retrieve string information for native interop.
        /// </summary>
        StringFormatMask = 0x00030000,

        /// <summary>
        /// LPSTR is interpreted as ANSI.
        /// </summary>
        AnsiClass = 0x00000000,

        /// <summary>
        /// LPSTR is interpreted as Unicode.
        /// </summary>
        UnicodeClass = 0x00010000,

        /// <summary>
        /// LPSTR is interpreted automatically.
        /// </summary>
        AutoClass = 0x00020000,

        /// <summary>
        /// Initialize the class before first static field access.
        /// </summary>
        BeforeFieldInit = 0x00100000,

        /// <summary>
        /// CLI provides "special" behavior, depending on the name of the Type.
        /// </summary>
        RTSpecialName = 0x00000800,

        /// <summary>
        /// Type has security associated with it.
        /// </summary>
        HasSecurity = 0x00040000
    }
}
