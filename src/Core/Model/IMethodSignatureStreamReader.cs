using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Tao.Model
{
    /// <summary>
    /// Represents a type that can read a method signature from a given blob.
    /// </summary>
    /// <typeparam name="TMethodSignature">The method signature type.</typeparam>
    public interface IMethodSignatureStreamReader<out TMethodSignature>
                    where TMethodSignature : IMethodSignature
    {
        /// <summary>
        /// Reads the method signature from the given <paramref name="blobStream"/>.
        /// </summary>
        /// <param name="blobStream">The stream that contains the method signature.</param>
        /// <returns>A method signature instance.</returns>
        TMethodSignature ReadSignature(Stream blobStream);
    }
}
