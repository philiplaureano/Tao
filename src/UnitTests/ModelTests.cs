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
    public class ModelTests : BaseStreamTests
    {
        [Test]
        public void ShouldBeAbleToReadAssemblyFromStream()
        {
            var stream = GetStream();
            var reader = container.GetInstance<IFunction<Stream, AssemblyDef>>("ReadAssemblyDef");
            Assert.IsNotNull(reader);

            var result = reader.Execute(stream);
            Assert.IsNotNull(result);
            Assert.AreEqual(AssemblyHashAlgorithm.None, result.HashAlgorithm);
            Assert.AreEqual(0, result.MajorVersion);
            Assert.AreEqual(0, result.MinorVersion);
            Assert.AreEqual(0, result.BuildNumber);
            Assert.AreEqual(0, result.RevisionNumber);
            Assert.AreEqual(0, Convert.ToInt32(result.Flags));
            Assert.AreEqual("donothing", result.Name);
            Assert.IsNull(result.Culture);
            Assert.IsNull(result.PublicKey);
        }
    }
}
