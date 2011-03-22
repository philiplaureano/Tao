using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tao.Model
{
    /// <summary>
    /// Represents various flags that describe a given parameter.
    /// </summary>
    public enum ParamAttributes : ushort 
    {
        /// <summary>
        /// Param is [In]
        /// </summary>
        In = 0x0001,

        /// <summary>
        /// Param is [Out]
        /// </summary>
        Out = 0x0002,

        /// <summary>
        /// Param is optional.
        /// </summary>
        Optional = 0x0010,

        /// <summary>
        /// Param has default value.
        /// </summary>
        HasDefault = 0x1000,

        /// <summary>
        /// Param has FieldMarshal.
        /// </summary>
        HasFieldMarshal = 0x2000,
    }
}
