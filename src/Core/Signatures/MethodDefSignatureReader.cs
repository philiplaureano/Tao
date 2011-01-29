using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Tao.Interfaces;
using Tao.Model;

namespace Tao.Signatures
{
    /// <summary>
    /// Represents a type that can read <see cref="MethodDefSig"/> instances into memory.
    /// </summary>
    public class MethodDefSignatureReader : MethodSignatureReader<MethodDefSig>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MethodSignatureReader{TMethodSignature}"/> class.
        /// </summary>
        public MethodDefSignatureReader(IFunction<ITuple<uint, Stream>, byte[]> readBlob, 
            IMethodSignatureStreamReader<MethodDefSig> methodDefSignatureStreamReader) 
            : base(readBlob, methodDefSignatureStreamReader)
        {
        }
    }
}
