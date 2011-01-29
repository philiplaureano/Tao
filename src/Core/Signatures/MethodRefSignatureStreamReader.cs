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
    /// Represents a type that can read a MethodRef signature from a given blob.
    /// </summary>
    public class MethodRefSignatureStreamReader : IMethodSignatureStreamReader<MethodRefSig>
    {
        private readonly IFunction<ITuple<Stream, IMethodSignature>> _readMethodSignatureFromBlob;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodRefSignatureStreamReader"/> class.
        /// </summary>
        public MethodRefSignatureStreamReader(IFunction<ITuple<Stream, IMethodSignature>> readMethodDefSignatureFromBlob)
        {
            _readMethodSignatureFromBlob = readMethodDefSignatureFromBlob;
        }

        public MethodRefSig ReadSignature(Stream blobStream)
        {
            var methodDefSig = new MethodRefSig();
            _readMethodSignatureFromBlob.Execute(blobStream, methodDefSig);

            return methodDefSig;
        }
    }
}
