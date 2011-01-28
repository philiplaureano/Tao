using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tao.Interfaces;
using Tao.Containers;
using Tao.Model;

namespace Tao.UnitTests
{
    [TestFixture]
    public class MethodDefSigTests : BaseStreamTests
    {
        [Test]
        public void ShouldReadCorrectBlobDataForGivenMethod()
        {
            var blobReader = container.GetInstance<IFunction<ITuple<uint, Stream>, byte[]>>();

            var stream = GetStream();

            // Read the first method signature blob 
            uint index = 0xA;
            var blob = blobReader.Execute(index, stream);
            Assert.AreEqual(6, blob.Length);
            Assert.AreEqual(0x30, blob[0]);
            Assert.AreEqual(0x02, blob[1]);
            Assert.AreEqual(0x02, blob[2]);
            Assert.AreEqual(0x01, blob[3]);
            Assert.AreEqual(0x08, blob[4]);
            Assert.AreEqual(0x1C, blob[5]);
        }

        [Test]
        public void ShouldBeAbleToReadMethodDefSig()
        {
            var reader = container.GetInstance<IFunction<ITuple<uint, Stream>, MethodDefSig>>("MethodDefSigReader");
            Assert.IsNotNull(reader, "MethodDefSigReader not found");

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

            ITypedMethodSignatureElement returnType = methodDefSig.ReturnType;            
            Assert.AreEqual(ElementType.Void, returnType.Type.ElementType);

            IList<ITypedMethodSignatureElement> parameters = methodDefSig.Parameters;

            Assert.AreEqual(ElementType.I4, parameters[0].Type.ElementType);
            Assert.AreEqual(ElementType.Object, parameters[1].Type.ElementType);
        }

        protected override Stream GetStream()
        {
            var targetAssemblyLocation = "MethodDefSigAssembly.dll";
            var streamFactory = container.GetInstance<IFunction<string, Stream>>();
            Assert.IsNotNull(streamFactory);

            var result = streamFactory.Execute(targetAssemblyLocation);

            return result;
        }
    }
}
