using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tao.Interfaces;
using Tao.Model;
using Tao.Containers;

namespace Tao.UnitTests
{
    [TestFixture]
    public class MethodRefSigTests : BaseStreamTests
    {
        [Test]
        public void ShouldBeAbleToReadMethodRefSig()
        {
            var reader = container.GetInstance<IFunction<ITuple<uint, Stream>, IMethodRefSignature>>("MethodRefSignatureReader");
            Assert.IsNotNull(reader, "MethodRefSigReader not found");

            uint index = 0xA;
            var stream = GetStream();
            var input = Tuple.New(index, stream);

            var methodDefSig = reader.Execute(input);

            Assert.IsTrue(methodDefSig.HasThis);
            Assert.IsFalse(methodDefSig.HasExplicitThis);
            Assert.IsFalse(methodDefSig.IsVarArg);
            Assert.IsTrue(methodDefSig.IsGeneric);
            Assert.AreEqual(2, methodDefSig.GenericParameterCount);
            Assert.AreEqual(2, methodDefSig.ParameterCount);

            Assert.AreEqual(0, methodDefSig.AdditionalParameters.Count);

            ITypedMethodSignatureElement returnType = methodDefSig.ReturnType;
            Assert.AreEqual(ElementType.Void, returnType.Type.ElementType);

            IList<ITypedMethodSignatureElement> parameters = methodDefSig.Parameters;

            Assert.AreEqual(ElementType.I4, parameters[0].Type.ElementType);
            Assert.AreEqual(ElementType.Object, parameters[1].Type.ElementType);
        }

        [Test]
        public void ShouldBeAbleToReadMethodRefSigWithVariableArgumentsAtCallSite()
        {
            var flags = MethodSignatureFlags.HasThis | MethodSignatureFlags.VarArg;
            var totalParameters = 3;
            var returnType = ElementType.Void;

            // The simulated method signature will have
            // one required string parameter and two optional Int32 parameters
            var parameterTypes = 
                new byte[] 
                {
                    Convert.ToByte(ElementType.String), 
                    Convert.ToByte(ElementType.Sentinel),
                    Convert.ToByte(ElementType.I4),
                    Convert.ToByte(ElementType.I4)
                };

            var bytes = new List<byte>
                            {
                                Convert.ToByte(flags),
                                Convert.ToByte(totalParameters),
                                Convert.ToByte(returnType)
                            };

            bytes.AddRange(parameterTypes);

            var reader = container.GetInstance<IFunction<ITuple<Stream, IMethodSignature>>>("ReadMethodRefSignatureFromBlob");
            Assert.IsNotNull(reader);

            var methodRefSig = new MethodRefSig();
            var inputStream = new MemoryStream(bytes.ToArray());
            reader.Execute(inputStream, methodRefSig);

            Assert.AreEqual(3,methodRefSig.ParameterCount);
            Assert.AreEqual(2, methodRefSig.AdditionalParameters.Count);

            Assert.AreEqual(ElementType.String, methodRefSig.Parameters[0].Type.ElementType);
            Assert.AreEqual(ElementType.I4, methodRefSig.AdditionalParameters[0].Type.ElementType);
            Assert.AreEqual(ElementType.I4, methodRefSig.AdditionalParameters[1].Type.ElementType);
            Assert.AreEqual(ElementType.Void, methodRefSig.ReturnType.Type.ElementType);
            
            Assert.IsTrue(methodRefSig.IsVarArg);
            Assert.IsTrue(methodRefSig.HasThis);
            Assert.IsFalse(methodRefSig.HasExplicitThis);
            Assert.AreEqual(0,methodRefSig.GenericParameterCount);
        }

        protected override Stream GetStream()
        {
            var targetAssemblyLocation = "MethodRefSigAssembly.dll";
            var streamFactory = container.GetInstance<IFunction<string, Stream>>();
            Assert.IsNotNull(streamFactory);

            var result = streamFactory.Execute(targetAssemblyLocation);

            return result;
        }
    }
}
