using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tao.Model
{
    /// <summary>
    /// Represents an enum that describes the traits of a given method.
    /// </summary>
    public enum MethodAttributes : ushort 
    {
        /// <summary>
        /// The mask used to determine a method's visibility.
        /// </summary>
        MemberAccessMask = 0x0007,

        /// <summary>
        /// Member not referenceable
        /// </summary>
        CompilerControlled = 0x0000,

        /// <summary>
        /// Accessible only by the parent type.
        /// </summary>
        Private = 0x0001,

        /// <summary>
        /// Accessible by sub-types only in htis Assembly
        /// </summary>
        FamANDAssem = 0x0002,

        /// <summary>
        /// Accessible by anyone in the Assembly
        /// </summary>        
        Assem = 0x0003,

        /// <summary>
        /// Accessible only by type and sub-types
        /// </summary>
        Family = 0x0004,

        /// <summary>
        /// Accessible by sub-types anywhere, plus anyone in the assembly.
        /// </summary>
        FamORAssem = 0x0005,

        /// <summary>
        /// Accessible by anyone who has visibility to this scope.
        /// </summary>
        Public = 0x0006,

        /// <summary>
        /// Defined on type, else per instance
        /// </summary>
        Static = 0x0010,

        /// <summary>
        /// Indicates that the given method cannot be overridden.
        /// </summary>
        Final = 0x0020,

        /// <summary>
        /// Method is virtual.
        /// </summary>
        Virtual = 0x0040,

        /// <summary>
        /// Method hides by name and signature, else just by name.
        /// </summary>
        HideBySig = 0x0080,

        /// <summary>
        /// Use this mask to retrieve vtable attributes.
        /// </summary>
        VtableLayoutMask = 0x0100,

        /// <summary>
        /// Method reuses existing slot in vtable.
        /// </summary>
        ReuseSlot = 0x0000,

        /// <summary>
        /// Method always gets a new slot in the vtable.
        /// </summary>
        NewSlot = 0x0100,

        /// <summary>
        /// Method can only be overridden if also accessible.
        /// </summary>
        Strict = 0x0200,

        /// <summary>
        /// Method does not provide an implementation.
        /// </summary>
        Abstract = 0x0400,

        /// <summary>
        /// Method is special
        /// </summary>
        SpecialName = 0x0800,

        /// <summary>
        /// Implementation is forwarded through PInvoke
        /// </summary>
        PInvokeImpl = 0x2000,

        /// <summary>
        /// Reserved: shall be zero for conforming implementations.
        /// </summary>
        UnamangedExport = 0x0008,

        /// <summary>
        /// CLI provies 'special' behavior, depending upon the name of the method.
        /// </summary>
        RTSpecialName = 0x1000,

        /// <summary>
        /// Method has security associated with it.
        /// </summary>
        HasSecurity = 0x4000,

        /// <summary>
        /// Method calls another method containing security code.
        /// </summary>
        RequireSecObject = 0x8000
    }
}
