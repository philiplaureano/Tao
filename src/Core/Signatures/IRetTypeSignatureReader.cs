using System.IO;
using Tao.Interfaces;
using Tao.Model;

namespace Tao.Signatures
{
    /// <summary>
    /// Represents a type that reads a return type.
    /// </summary>
    public interface IRetTypeSignatureReader : IFunction<Stream, ITypedMethodSignatureElement>
    {
    }
}