using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tao.Containers;
using Tao.Interfaces;
using Tao.Model;

namespace Tao.UnitTests
{
    [TestFixture]
    public class MethodSpecTests : BaseStreamTests
    {
        [Test]
        public void ShouldBeAbleToReadMethodSpec()
        {
            var elementType = ElementType.GenericInst;
            var objectType = ElementType.Class;
            byte encodedToken = 0x9;
            byte genArgCount = 1;
            var firstGenericParameterType = ElementType.I4;

            var bytes = new List<byte>
                            {
                                Convert.ToByte(elementType),
                                genArgCount,
                                Convert.ToByte(objectType),
                                encodedToken,
                                Convert.ToByte(firstGenericParameterType)
                            };

            var reader = container.GetInstance<IFunction<Stream, MethodSpec>>();
            Assert.IsNotNull(reader);

            var stream = new MemoryStream(bytes.ToArray());
            var signature = reader.Execute(stream) as MethodSpec;
            Assert.IsNotNull(signature);

            // The TypeDefOrRefEncoded index should be pointing to the TypeRef table, row 2
            var genericType = signature.GenericTypeDefinition as TypeDefOrRefEncodedSignature;
            Assert.IsNotNull(genericType);
            Assert.AreEqual(genericType.TableId, TableId.TypeRef);
            Assert.AreEqual(genericType.TableIndex, 2);

            Assert.AreEqual(1, signature.TypeParameters.Count);
            Assert.AreEqual(ElementType.I4, signature.TypeParameters[0].ElementType);
        }
    }
}
