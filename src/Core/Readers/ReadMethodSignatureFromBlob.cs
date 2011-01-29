using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Tao.Interfaces;
using Tao.Model;
using Tao.Signatures;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a type that reads the contents of a blob into a given <see cref="IMethodSignature"/> instance.
    /// </summary>
    public abstract class ReadMethodSignatureFromBlob : IFunction<ITuple<Stream, IMethodSignature>>
    {
        private readonly IRetTypeSignatureReader _retTypeSignatureReader;
        private readonly IAssignMethodSignatureParameters _assignMethodSignatureParameters;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadMethodSignatureFromBlob"/> class.
        /// </summary>
        protected ReadMethodSignatureFromBlob(IRetTypeSignatureReader retTypeSignatureReader, IAssignMethodSignatureParameters assignMethodSignatureParameters)
        {
            _retTypeSignatureReader = retTypeSignatureReader;
            _assignMethodSignatureParameters = assignMethodSignatureParameters;
        }

        /// <summary>
        /// Reads the contents of a blob into the given <see cref="IMethodSignature"/> instance.
        /// </summary>
        /// <param name="input">The input stream and the method signature.</param>
        public void Execute(ITuple<Stream, IMethodSignature> input)
        {
            var blobStream = input.Item1;
            var methodSignature = input.Item2;
            var flags = (MethodSignatureFlags)blobStream.ReadByte();

            methodSignature.HasThis = (flags & MethodSignatureFlags.HasThis) != 0;
            methodSignature.HasExplicitThis = (flags & MethodSignatureFlags.ExplicitThis) != 0;
            methodSignature.IsVarArg = (flags & MethodSignatureFlags.VarArg) != 0;
            methodSignature.IsGeneric = (flags & MethodSignatureFlags.Generic) != 0;

            // Read the number of generic parameters, if present
            methodSignature.GenericParameterCount = methodSignature.IsGeneric ? (uint)blobStream.ReadByte() : 0;

            // Read the parameter count                       
            methodSignature.ParameterCount = (uint)blobStream.ReadByte();

            // Read the return type
            methodSignature.ReturnType = _retTypeSignatureReader.Execute(blobStream);

            var bytes = blobStream.ToArray();
            var remainingBytes = blobStream.ReadToEnd(true);

            // Read the parameters
            _assignMethodSignatureParameters.Execute(blobStream, methodSignature);
        }
    }
}
