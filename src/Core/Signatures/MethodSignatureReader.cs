using System.IO;
using Tao.Interfaces;
using Tao.Model;

namespace Tao.Signatures
{
    /// <summary>
    /// Represents a type that can read <see cref="MethodDefSig"/> instances into memory.
    /// </summary>
    public abstract class MethodSignatureReader<TMethodSignature> : IFunction<ITuple<uint, Stream>, TMethodSignature>
        where TMethodSignature : IMethodSignature
    {
        private readonly IFunction<ITuple<uint, Stream>, byte[]> _readBlob;
        private readonly IMethodSignatureStreamReader<TMethodSignature> _methodSignatureStreamReader;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodSignatureReader{TMethodSignature}"/> class.
        /// </summary>
        protected MethodSignatureReader(IFunction<ITuple<uint, Stream>, byte[]> readBlob, IMethodSignatureStreamReader<TMethodSignature> methodSignatureStreamReader)
        {
            _readBlob = readBlob;
            _methodSignatureStreamReader = methodSignatureStreamReader;
        }

        /// <summary>
        /// Reads a <see cref="MethodDefSig"/> instance into memory using a given blob stream offset
        /// and a <see cref="Stream"/> instance.
        /// </summary>
        /// <param name="input">The blob stream offset and the input stream.</param>
        /// <returns>A <see cref="MethodDefSig"/> object.</returns>
        public TMethodSignature Execute(ITuple<uint, Stream> input)
        {
            var blobStreamOffset = input.Item1;
            var inputStream = input.Item2;

            var blobBytes = _readBlob.Execute(blobStreamOffset, inputStream);
            var blobStream = new MemoryStream(blobBytes);

            var result = _methodSignatureStreamReader.ReadSignature(blobStream);

            return result;
        }        
    }
}
