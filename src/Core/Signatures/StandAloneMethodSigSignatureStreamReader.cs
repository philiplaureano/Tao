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
    /// Represents a type that can read a StandAloneMethodSignature signature from a given blob.
    /// </summary>
    public class StandAloneMethodSigSignatureStreamReader : IMethodSignatureStreamReader<IStandAloneMethodSignature>
    {
        private readonly IFunction<ITuple<Stream, IMethodSignature>> _readMethodSignatureFromBlob;

        /// <summary>
        /// Initializes a new instance of the <see cref="StandAloneMethodSigSignatureStreamReader"/> class.
        /// </summary>
        public StandAloneMethodSigSignatureStreamReader(IFunction<ITuple<Stream, IMethodSignature>> readStandAloneMethodSigSignatureFromBlob)
        {
            _readMethodSignatureFromBlob = readStandAloneMethodSigSignatureFromBlob;
        }

        /// <summary>
        /// Reads the method signature from the given <paramref name="blobStream"/>.
        /// </summary>
        /// <param name="blobStream">The stream that contains the method signature.</param>
        /// <returns>A method signature instance.</returns>
        public IStandAloneMethodSignature ReadSignature(Stream blobStream)
        {
            var standAloneMethodSig = new StandAloneMethodSignature();
            _readMethodSignatureFromBlob.Execute(blobStream, standAloneMethodSig);

            return standAloneMethodSig;
        }
    }
}
