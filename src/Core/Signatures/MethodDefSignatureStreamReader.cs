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
    /// Represents a type that can read a MethodDef signature from a given blob.N
    /// </summary>
    public class MethodDefSignatureStreamReader : IMethodSignatureStreamReader<MethodDefSig>
    {
        private readonly IFunction<ITuple<Stream, IMethodSignature>> _readMethodSignatureFromBlob;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodDefSignatureStreamReader"/> class.
        /// </summary>
        public MethodDefSignatureStreamReader(IFunction<ITuple<Stream, IMethodSignature>> readMethodDefSignatureFromBlob)
        {
            _readMethodSignatureFromBlob = readMethodDefSignatureFromBlob;
        }

        public MethodDefSig ReadSignature(Stream blobStream)
        {
            var methodDefSig = new MethodDefSig();
            _readMethodSignatureFromBlob.Execute(blobStream, methodDefSig);

            return methodDefSig;
        }              
    }
}
