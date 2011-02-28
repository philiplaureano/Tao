using System.IO;
using Tao.Interfaces;
using Tao.Model;

namespace Tao.Signatures
{
    /// <summary>
    /// Represents a class that creates a function pointer <see cref="TypeSignature"/> instance from a given stream.
    /// </summary>
    public class CreateFunctionPointerSignature : IFunction<ITuple<Stream, IMethodSignatureStreamReader<IMethodRefSignature>, IMethodSignatureStreamReader<IMethodSignature>>, TypeSignature>
    {
        /// <summary>
        /// Creates a function pointer <see cref="TypeSignature"/> instance from the <paramref name="input">given stream</paramref>.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <returns>A <see cref="TypeSignature"/> instance that represents the function pointer signature type.</returns>
        public TypeSignature Execute(ITuple<Stream, IMethodSignatureStreamReader<IMethodRefSignature>, IMethodSignatureStreamReader<IMethodSignature>> input)
        {
            var inputStream = input.Item1;
            var methodRefSignatureStreamReader = input.Item2;
            var methodDefSignatureStreamReader = input.Item3;

            var flags = (MethodSignatureFlags)inputStream.PeekByte();
            var isVarArg = (flags & MethodSignatureFlags.VarArg) != 0;

            // Use the MethodRefSignatureStreamReader for VARARG methods
            var streamReader = isVarArg ? methodRefSignatureStreamReader : methodDefSignatureStreamReader;
            var targetMethod = streamReader.ReadSignature(inputStream);

            var functionPointer = new FunctionPointerSignature
            {
                ElementType = ElementType.FnPtr,
                TargetMethod = targetMethod
            };

            return functionPointer;
        }        
    }
}
