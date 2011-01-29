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
    /// Represents a type that can read <see cref="MethodRefSig"/> instances into memory.
    /// </summary>
    public class MethodRefSignatureReader : MethodSignatureReader<MethodRefSig>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MethodSignatureReader{TMethodSignature}"/> class.
        /// </summary>
        public MethodRefSignatureReader(IFunction<ITuple<uint, Stream>, byte[]> readBlob, 
            IMethodSignatureStreamReader<MethodRefSig> methodRefSignatureStreamReader) : base(readBlob, methodRefSignatureStreamReader)
        {
        }
    }
}
