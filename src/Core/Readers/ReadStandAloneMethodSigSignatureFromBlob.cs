using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.Model;
using Tao.Signatures;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a type that reads the contents of a blob into a given <see cref="IStandAloneMethodSignature"/> instance.
    /// </summary>
    public class ReadStandAloneMethodSigSignatureFromBlob : ReadMethodRefSignatureFromBlob
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadMethodSignatureFromBlob"/> class.
        /// </summary>
        public ReadStandAloneMethodSigSignatureFromBlob(IRetTypeSignatureReader retTypeSignatureReader,
            IAssignMethodSignatureParameters assignMethodRefParameters)
            : base(retTypeSignatureReader, assignMethodRefParameters)
        {
        }

        /// <summary>
        /// Reads the current <paramref name="flags"/> for the <paramref name="methodSignature"/>
        /// and assigns it to the given method signature.
        /// </summary>
        /// <param name="methodSignature">The target method signature.</param>
        /// <param name="flags">The <see cref="MethodSignatureFlags"/> enumeration that describes the method itself.</param>
        protected override void ReadMethodFlags(IMethodSignature methodSignature, MethodSignatureFlags flags)
        {
            var signature = methodSignature as IStandAloneMethodSignature;
            if (signature == null)
            {
                base.ReadMethodFlags(methodSignature, flags);
                return;
            }

            // Read the unmanaged signature flags
            signature.IsFastCall = flags.ToString().Contains("FastCall");

            signature.IsStdCall = (flags & MethodSignatureFlags.StdCall) != 0;
            signature.IsThisCall = (flags & MethodSignatureFlags.ThisCall) != 0;
            signature.IsUsingCCallingConvention = (flags & MethodSignatureFlags.C) != 0;

            base.ReadMethodFlags(methodSignature, flags);
        }
    }
}
