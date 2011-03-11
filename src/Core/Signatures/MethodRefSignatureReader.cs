using System.IO;
using Tao.Interfaces;
using Tao.Model;

namespace Tao.Signatures
{
    /// <summary>
    /// Represents a type that can read <see cref="MethodRefSig"/> instances into memory.
    /// </summary>
    public class MethodRefSignatureReader : MethodSignatureReader<IMethodRefSignature>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MethodSignatureReader{TMethodSignature}"/> class.
        /// </summary>
        public MethodRefSignatureReader(IFunction<ITuple<uint, Stream>, byte[]> readBlob,
            IMethodSignatureStreamReader<IMethodRefSignature> methodRefSignatureStreamReader)
            : base(readBlob, methodRefSignatureStreamReader)
        {
        }
    }
}
