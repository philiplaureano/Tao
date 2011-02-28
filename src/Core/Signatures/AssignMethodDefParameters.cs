using System.IO;
using Tao.Interfaces;
using Tao.Model;

namespace Tao.Signatures
{
    /// <summary>
    /// Represents a class that reads the method parameters for a given method signature from
    /// a blob stream.
    /// </summary>
    public class AssignMethodDefParameters : IAssignMethodSignatureParameters
    {
        private readonly IParamSignatureReader _paramSignatureReader;

        ///<summary>
        /// Initializes a new instance of the <see cref="AssignMethodDefParameters"/> class.
        ///</summary>
        ///<param name="paramSignatureReader">The paramater signature reader.</param>
        public AssignMethodDefParameters(IParamSignatureReader paramSignatureReader)
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
            var methodDefSig = input.Item2;

            for (var i = 0; i < methodDefSig.ParameterCount; i++)
            {
                var paramSignature = _paramSignatureReader.Execute(blobStream);
                methodDefSig.Parameters.Add(paramSignature);
            }
        }
    }
}
