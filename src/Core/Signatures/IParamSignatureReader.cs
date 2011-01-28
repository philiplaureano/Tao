using System.IO;
using Tao.Model;

namespace Tao.Signatures
{
    /// <summary>
    /// Represents a type that can read parameter signatures into memory.
    /// </summary>
    public interface IParamSignatureReader
    {
        /// <summary>
        /// Reads a <see cref="MethodSignatureElement"/> from the given byte stream.
        /// </summary>
        /// <param name="input">The input bytes that contains the given <see cref="MethodSignatureElement"/>.</param>
        /// <returns>A <see cref="IMethodSignatureElement"/> instance.</returns>
        ITypedMethodSignatureElement Execute(Stream input);
    }
}