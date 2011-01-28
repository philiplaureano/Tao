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
    public class MethodDefSigReader : IFunction<ITuple<uint, Stream>, MethodDefSig>
    {
        private readonly IFunction<ITuple<uint, Stream>, byte[]> _readBlob;
        private readonly IRetTypeSignatureReader _retTypeSignatureReader;
        private readonly IParamSignatureReader _paramSignatureReader;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodDefSigReader"/> class.
        /// </summary>
        public MethodDefSigReader(IFunction<ITuple<uint, Stream>, byte[]> readBlob, IRetTypeSignatureReader retTypeSignatureReader, IParamSignatureReader paramSignatureReader)
        {
            _readBlob = readBlob;
            _paramSignatureReader = paramSignatureReader;
            _retTypeSignatureReader = retTypeSignatureReader;
        }

        /// <summary>
        /// Reads a <see cref="MethodDefSig"/> instance into memory using a given blob stream offset
        /// and a <see cref="Stream"/> instance.
        /// </summary>
        /// <param name="input">The blob stream offset and the input stream.</param>
        /// <returns>A <see cref="MethodDefSig"/> object.</returns>
        public MethodDefSig Execute(ITuple<uint, Stream> input)
        {
            var blobStreamOffset = input.Item1;
            var inputStream = input.Item2;

            var blobBytes = _readBlob.Execute(blobStreamOffset, inputStream);
            var blobStream = new MemoryStream(blobBytes);

            var flags = (MethodSignatureFlags)blobStream.ReadByte();
            var methodDefSig = new MethodDefSig
                                   {
                                       HasThis = (flags & MethodSignatureFlags.HasThis) != 0,
                                       HasExplicitThis = (flags & MethodSignatureFlags.ExplicitThis) != 0,
                                       IsVarArg = (flags & MethodSignatureFlags.VarArg) != 0,
                                       IsGeneric = (flags & MethodSignatureFlags.Generic) != 0
                                   };

            // Read the number of generic parameters, if present
            methodDefSig.GenericParameterCount = methodDefSig.IsGeneric ? (uint)blobStream.ReadByte() : 0;

            // Read the parameter count                       
            methodDefSig.ParameterCount = (uint)blobStream.ReadByte();

            // Read the return type
            methodDefSig.ReturnType = _retTypeSignatureReader.Execute(blobStream);

            // Read the parameters
            for (var i = 0; i < methodDefSig.ParameterCount; i++)
            {
                var paramSignature = _paramSignatureReader.Execute(blobStream);
                methodDefSig.Parameters.Add(paramSignature);
            }

            return methodDefSig;
        }
    }
}
