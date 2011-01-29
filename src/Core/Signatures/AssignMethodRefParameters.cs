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
    /// Represents a class that reads the method parameters for a given method signature from
    /// a blob stream.
    /// </summary>
    public class AssignMethodRefParameters : IAssignMethodSignatureParameters
    {
        private readonly IParamSignatureReader _paramSignatureReader;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssignMethodRefParameters"/> class.
        /// </summary>
        ///<param name="paramSignatureReader">The paramater signature reader.</param>
        public AssignMethodRefParameters(IParamSignatureReader paramSignatureReader)
        {
            _paramSignatureReader = paramSignatureReader;
        }

        /// <summary>
        /// Reads the parameters from the input stream and assigns it to the given <see cref="IMethodSignature"/> instance.
        /// </summary>
        /// <param name="input">The input stream and the <see cref="IMethodSignature"/> instance.</param>
        public void Execute(ITuple<Stream, IMethodSignature> input)
        {
            var blobStream = input.Item1;
            var methodSignature = (MethodRefSig)input.Item2;

            var hasSentinel = methodSignature.IsVarArg;
            var parameters = methodSignature.Parameters;
            var index = 0;
            while(index < methodSignature.ParameterCount)
            {
                // Determine if the current parameter is an
                // additonal optional parameter from a VARARGS method call
                var nextElementType = (ElementType)blobStream.PeekByte();
                if (hasSentinel && nextElementType == ElementType.Sentinel)
                {
                    parameters = methodSignature.AdditionalParameters;

                    // Skip the Sentinel element
                    blobStream.Seek(1, SeekOrigin.Current);
                }
                    
                var paramSignature = _paramSignatureReader.Execute(blobStream);
                parameters.Add(paramSignature);

                index++;
            }
        }
    }
}
