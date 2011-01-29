using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.Model;
using Tao.Signatures;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a type that reads the contents of a blob into a given <see cref="IMethodSignature"/> instance.
    /// </summary>
    public class ReadMethodDefSignatureFromBlob : ReadMethodSignatureFromBlob
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadMethodSignatureFromBlob"/> class.
        /// </summary>
        public ReadMethodDefSignatureFromBlob(IRetTypeSignatureReader retTypeSignatureReader, 
            IAssignMethodSignatureParameters assignMethodDefParameters) : 
            base(retTypeSignatureReader, assignMethodDefParameters)
        {
        }
    }
}
