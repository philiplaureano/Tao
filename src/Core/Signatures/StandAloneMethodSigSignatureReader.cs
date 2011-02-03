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
    /// Represents a type that can read <see cref="StandAloneMethodSignature"/> instances into memory.
    /// </summary>
    public class StandAloneMethodSigSignatureReader : MethodSignatureReader<IStandAloneMethodSignature>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MethodSignatureReader{TMethodSignature}"/> class.
        /// </summary>
        public StandAloneMethodSigSignatureReader(IFunction<ITuple<uint, Stream>, byte[]> readBlob, 
            IMethodSignatureStreamReader<IStandAloneMethodSignature> standAloneMethodSigSignatureStreamReader)
            : base(readBlob, standAloneMethodSigSignatureStreamReader)
        {
        }
    }
}
