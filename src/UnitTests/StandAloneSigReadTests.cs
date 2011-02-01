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
    public class StandAloneSigReadTests : BaseStreamTests
    {
        [Test]
        public void ShouldBeAbleToReadManagedStandAloneMethodSig()
        {
            var reader = container.GetInstance<IFunction<ITuple<uint, Stream>, StandAloneMethodSig>>("StandAloneMethodSigSignatureReader");
            Assert.IsNotNull(reader, "StandAloneMethodSigReader not found");

            uint index = 0xA;
            var stream = GetStream();
            var input = Tuple.New(index, stream);

            var standAloneMethodSig = reader.Execute(input);

            Assert.IsTrue(standAloneMethodSig.HasThis);
            Assert.IsFalse(standAloneMethodSig.HasExplicitThis);
            Assert.IsFalse(standAloneMethodSig.IsVarArg);
            Assert.IsTrue(standAloneMethodSig.IsGeneric);
            Assert.AreEqual(2, standAloneMethodSig.GenericParameterCount);
            Assert.AreEqual(2, standAloneMethodSig.ParameterCount);

            Assert.AreEqual(0, standAloneMethodSig.AdditionalParameters.Count);

            ITypedMethodSignatureElement returnType = standAloneMethodSig.ReturnType;
            Assert.AreEqual(ElementType.Void, returnType.Type.ElementType);

            IList<ITypedMethodSignatureElement> parameters = standAloneMethodSig.Parameters;

            Assert.AreEqual(ElementType.I4, parameters[0].Type.ElementType);
            Assert.AreEqual(ElementType.Object, parameters[1].Type.ElementType);
        }

        [Test]
        public void ShouldBeAbleToReadStandAloneMethodSigWithVariableArgumentsAtCallSite()
        {
            var flags = MethodSignatureFlags.C | MethodSignatureFlags.VarArg;
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

            var reader = container.GetInstance<IFunction<ITuple<Stream, IMethodSignature>>>("ReadStandAloneMethodSigSignatureFromBlob");
            Assert.IsNotNull(reader, "ReadStandAloneMethodSignatureFromBlob not found");

            var standAloneMethodSig = new StandAloneMethodSig();
            var inputStream = new MemoryStream(bytes.ToArray());
            reader.Execute(inputStream, standAloneMethodSig);

            Assert.AreEqual(3, standAloneMethodSig.ParameterCount);
            Assert.AreEqual(2, standAloneMethodSig.AdditionalParameters.Count);

            Assert.AreEqual(ElementType.String, standAloneMethodSig.Parameters[0].Type.ElementType);
            Assert.AreEqual(ElementType.I4, standAloneMethodSig.AdditionalParameters[0].Type.ElementType);
            Assert.AreEqual(ElementType.I4, standAloneMethodSig.AdditionalParameters[1].Type.ElementType);
            Assert.AreEqual(ElementType.Void, standAloneMethodSig.ReturnType.Type.ElementType);
            
            Assert.IsTrue(standAloneMethodSig.IsVarArg);
            Assert.IsTrue(standAloneMethodSig.IsUsingCCallingConvention);

            Assert.IsFalse(standAloneMethodSig.IsStdCall);            
            
            Assert.IsFalse(standAloneMethodSig.HasThis);            
            Assert.IsFalse(standAloneMethodSig.IsFastCall);
            Assert.IsFalse(standAloneMethodSig.IsThisCall);
            Assert.IsFalse(standAloneMethodSig.HasExplicitThis);

            Assert.AreEqual(0, standAloneMethodSig.GenericParameterCount);
        }

        protected override Stream GetStream()
        {
            var targetAssemblyLocation = "StandAloneMethodSigAssembly.dll";
            var streamFactory = container.GetInstance<IFunction<string, Stream>>();
            Assert.IsNotNull(streamFactory);

            var result = streamFactory.Execute(targetAssemblyLocation);

            return result;
        }
    }
}
