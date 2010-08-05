using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Model
{
    /// <summary>
    /// Represents a TYPEDBYREF param signature.
    /// </summary>
    public class TypedByRefParam : ParamSignature, ITypedByRefMethodSignatureElement
    {
        public override bool IsByRef
        {
            get { return false; }
        }
    }
}
