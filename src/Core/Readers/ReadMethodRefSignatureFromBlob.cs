using Tao.Model;
using Tao.Signatures;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a type that reads the contents of a blob into a given <see cref="IMethodSignature"/> instance.
    /// </summary>
    public class ReadMethodRefSignatureFromBlob : ReadMethodSignatureFromBlob
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadMethodSignatureFromBlob"/> class.
        /// </summary>
        public ReadMethodRefSignatureFromBlob(IRetTypeSignatureReader retTypeSignatureReader,
            IAssignMethodSignatureParameters assignMethodRefParameters) :
            base(retTypeSignatureReader, assignMethodRefParameters)
        {
        }
    }
}
