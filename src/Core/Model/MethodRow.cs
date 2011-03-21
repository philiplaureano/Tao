using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tao.Model
{
    /// <summary>
    /// Represents a single row in the Method metadata table.
    /// </summary>
    public class MethodRow
    {
        /// <summary>
        /// Gets or sets the value indicating the RVA of the current method.
        /// </summary>
        /// <value>The method relative virtual address that points to the location of the method header.</value>
        public uint Rva { get; set; }

        /// <summary>
        /// Gets or sets the value indicating the method implementation flags for the current method.
        /// </summary>
        /// <value>The method implementation flags.</value>
        public MethodImplAttributes ImplFlags { get; set; }

        /// <summary>
        /// Gets or sets the value indicating the string heap index that points to the name of the current method.
        /// </summary>
        /// <value>The streap heap index pointing to the name of the current method.</value>
        public uint Name { get; set; }

        /// <summary>
        /// Gets or sets the value indicating the <see cref="MethodAttributes"/> of the current method.
        /// </summary>
        /// <value>The method attributes that describe the current method.</value>
        public MethodAttributes Flags { get; set; }

        /// <summary>
        /// Gets or sets the value indicating the blob heap index that points to the signature of the current method.
        /// </summary>
        /// <value>The blob heap index that points to the method signature.</value>
        public uint Signature { get; set; }


        /// <summary>
        /// Gets or sets a value indicating an index into the Param table. It marks the first of a contiguous run of Parameters owned by this method. 
        /// The run continues to the smaller of the last row of the Param table, or the next run of Parameters, found by inspecting the ParamList of the next row of the MethodDef table.
        /// </summary>
        public uint ParamList { get; set; }
    }
}
