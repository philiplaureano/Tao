using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tao.Model
{
    [Flags]
    public enum FieldAttributes : ushort 
    {
        /// <summary>
        /// No description available.
        /// </summary>
        FieldAccessMask = 0x0007,

        /// <summary>
        /// Member not referenceable.
        /// </summary>
        CompilerControlled = 0x0000,

        /// <summary>
        /// Accessible only by the parent type.
        /// </summary>
        Private = 0x0001,

        /// <summary>
        /// Accessible by subtypes only in this Assembly.
        /// </summary>
        FamANDAssem = 0x002,

        /// <summary>
        /// Accessible by anyone in this assembly.
        /// </summary>
        Assembly = 0x0003,

        /// <summary>
        /// Accessible only by type and subtypes.
        /// </summary>
        Family = 0x0004,

        /// <summary>
        /// Accessible by subtypes anywhere plus anyone in an assembly.
        /// </summary>
        FamORAssem = 0x0005,

        /// <summary>
        /// Accessible by anyone who has visibility to this scope field contract attributes.
        /// </summary>
        Public = 0x0006,

        /// <summary>
        /// Defined on type, else per instance.
        /// </summary>
        Static = 0x0010,

        /// <summary>
        /// Field may only be initialized, not written to after init.
        /// </summary>
        InitOnly = 0x0020,

        /// <summary>
        /// Value is not a compile-time constant.
        /// </summary>
        Literal = 0x0040,

        /// <summary>
        /// Field does not have to be serialized when type is remoted.
        /// </summary>
        NotSerialized = 0x0080,

        /// <summary>
        /// Field is special.
        /// </summary>
        SpecialName = 0x0200,

        /// <summary>
        /// Implementation is forwarded through PInvoke.
        /// </summary>
        PInvokeImpl = 0x2000,

        /// <summary>
        /// CLI provides "special" behavior, depending upon the name of the field.
        /// </summary>
        RTSpecialName = 0x0400,

        /// <summary>
        /// Field has marshalling information.
        /// </summary>
        HasFieldMarshal = 0x1000
    }
}
