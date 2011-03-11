using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Tao.Containers;
using Tao.Interfaces;
using Tao.Model;

namespace Tao.UnitTests
{
    [TestFixture]
    public class ModelTests : BaseStreamTests
    {
        [Test]
        public void ShouldBeAbleToReadAssemblyFromStream()
        {
            var stream = GetStream();
            var reader = container.GetInstance<IFunction<Stream, AssemblyDefRow>>("ReadAssemblyDef");
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
            Assert.IsEmpty(result.Culture);
            Assert.IsNull(result.PublicKey);
        }

        [Test]
        public void ShouldBeAbleToReadModuleFromStream()
        {
            var stream = GetStream();
            var reader = container.GetInstance<IFunction<Stream, ModuleDefRow>>("ReadModuleDef");
            Assert.IsNotNull(reader);

            var result = reader.Execute(stream);
            Assert.IsNotNull(result);

            byte[] guidData = { 0xCD, 0xEE, 0x79, 0x0E, 0xD5, 0x88, 0x8F, 0x43, 0x86, 0x84, 0x11, 0x81, 0x81, 0x89, 0xFF, 0x1D };

            var expectedGuid = new Guid(guidData);
            Assert.AreEqual("skeleton.exe", result.Name);
            Assert.AreEqual(expectedGuid, expectedGuid);
        }

        [Test]
        public void ShouldBeAbleToReadSimpleTypeDefFromStream()
        {
            var stream = GetStream();
            var reader = container.GetInstance<IFunction<Stream, IEnumerable<TypeDefRow>>>("ReadTypeDefs");
            Assert.IsNotNull(reader);

            var results = reader.Execute(stream);
            var types = new List<TypeDefRow>(results);
            Assert.IsNotNull(results);
            Assert.IsTrue(types.Count > 0);

            var type = types[0];
            Assert.AreEqual(0, Convert.ToInt32(type.Flags));
            Assert.AreEqual("<Module>", type.Name);
            Assert.AreEqual(string.Empty, type.Namespace);

            var extends = type.Extends;
            Assert.AreEqual(TableId.TypeDef, extends.Item1);
            Assert.AreEqual(0, extends.Item2);

            Assert.AreEqual(1, type.FieldList);
            Assert.AreEqual(1, type.MethodList);
        }
    }
}
