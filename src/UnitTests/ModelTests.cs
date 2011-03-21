using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            Assert.AreEqual(0x8004, Convert.ToUInt32(result.HashAlgorithm));
            Assert.AreEqual(2, result.MajorVersion);
            Assert.AreEqual(3, result.MinorVersion);
            Assert.AreEqual(0, result.BuildNumber);
            Assert.AreEqual(0x2FD6, result.RevisionNumber);
            Assert.AreEqual(0, Convert.ToInt32(result.Flags));
            Assert.AreEqual(0x12, result.Name);
            Assert.AreEqual(0, result.Culture);
            Assert.AreEqual(0x1F97, result.PublicKey);
        }

        [Test]
        public void ShouldBeAbleToReadFieldFromStream()
        {
            var stream = GetStream();
            var reader = container.GetInstance<IFunction<Stream, IEnumerable<FieldRow>>>("ReadFields");
            Assert.IsNotNull(reader);

            var results = reader.Execute(stream);
            Assert.IsNotNull(results);

            var fields = results.ToArray();
            Assert.AreEqual(3964, fields.Length, "Invalid field row count");

            // Check the first and the last fields to ensure that
            // the reader is reading the correct portions of the table stream
            var firstField = fields[0];
            Assert.AreEqual(0x21, Convert.ToUInt32(firstField.Flags));
            Assert.AreEqual(0x402, firstField.Name);
            Assert.AreEqual(0x62EF, firstField.Signature);

            var lastField = fields[3963];
            Assert.AreEqual(lastField.Flags, FieldAttributes.Public);
            Assert.AreEqual(0x735C, lastField.Name);
            Assert.AreEqual(0x631D, lastField.Signature);
        }

        [Test]
        public void ShouldBeAbleToReadMethodFromStream()
        {
            var stream = GetStream();
            var reader = container.GetInstance<IFunction<Stream, IEnumerable<MethodRow>>>("ReadMethodRows");
            Assert.IsNotNull(reader);

            var results = reader.Execute(stream);
            Assert.AreEqual(5568, results.Count());

            var firstRow = results.First();
            Assert.AreEqual(0, firstRow.Rva);
            Assert.AreEqual(MethodImplAttributes.Managed, firstRow.ImplFlags);
            Assert.AreEqual((ushort)0x5C6, (ushort)firstRow.Flags);
            Assert.AreEqual(0x7730, firstRow.Name);
            Assert.AreEqual(0x72D6, firstRow.Signature);
            Assert.AreEqual(1, firstRow.ParamList);

            var lastRow = results.Last();
            Assert.AreEqual(0x4CEE8, lastRow.Rva);
            Assert.AreEqual(MethodImplAttributes.Managed, lastRow.ImplFlags);
            Assert.AreEqual((ushort)0x86, (ushort)lastRow.Flags);
            Assert.AreEqual(0xFE82, lastRow.Name);
            Assert.AreEqual(0x151A, lastRow.ParamList);
        }

        [Test]
        public void ShouldBeAbleToReadModuleFromStream()
        {
            var stream = GetStream();
            var reader = container.GetInstance<IFunction<Stream, ModuleDefRow>>("ReadModuleDef");
            Assert.IsNotNull(reader);

            var result = reader.Execute(stream);
            Assert.IsNotNull(result);
            Assert.AreEqual(0x1076D, result.Name);
            Assert.AreEqual(1, result.Mvid);
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
            Assert.AreEqual(0x133DA, type.Name);
            Assert.AreEqual(0, type.Namespace);

            var extends = type.Extends;
            Assert.AreEqual(TableId.TypeDef, extends.Item1);
            Assert.AreEqual(0, extends.Item2);

            Assert.AreEqual(1, type.FieldList);
            Assert.AreEqual(1, type.MethodList);
        }

        [Test]
        public void ShouldBeAbleToReadTypeRefsFromStream()
        {
            var stream = GetStream();
            var reader = container.GetInstance<IFunction<Stream, IEnumerable<TypeRefRow>>>("ReadTypeRefs");
            Assert.IsNotNull(reader);

            var results = reader.Execute(stream);
            Assert.IsNotNull(results);
            Assert.AreEqual(222, results.Count());

            // Check the first and the last rows to ensure that
            // we're reading the right data 
            var items = results.ToArray();

            var firstItem = items[0];

            // Match the table reference
            var resolutionScope = firstItem.ResolutionScope;
            Assert.AreEqual(TableId.AssemblyRef, resolutionScope.TableId);
            Assert.AreEqual(1, resolutionScope.Row);

            Assert.AreEqual(0x17528, firstItem.Name);
            Assert.AreEqual(0x17536, firstItem.Namespace);

            var lastItem = items[221];
            resolutionScope = lastItem.ResolutionScope;
            Assert.AreEqual(1, resolutionScope.Row);

            Assert.AreEqual(0x181B4, lastItem.Name);
            Assert.AreEqual(0x17D5E, lastItem.Namespace);
        }

        [Test]
        public void ShouldBeAbleToReadClassLayoutFromStream()
        {
            var stream = GetStream();
            var reader = container.GetInstance<IFunction<Stream, IEnumerable<ClassLayoutRow>>>("ReadClassLayouts");

            Assert.IsNotNull(reader);

            var results = reader.Execute(stream);

            var layouts = new List<ClassLayoutRow>(results);
            Assert.AreEqual(8, layouts.Count);

            var layout = layouts[0];

            Assert.IsNotNull(layout);

            ushort packingSize = layout.PackingSize;
            uint classSize = layout.ClassSize;
            uint parent = layout.Parent;

            Assert.AreEqual(0, packingSize);
            Assert.AreEqual(1, classSize);
            Assert.AreEqual(0x02D9, parent);
        }

        protected override Stream GetStream()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var targetFile = Path.Combine(baseDirectory, "SampleBinaries\\LinFu.Core.dll");
            var stream = new FileStream(targetFile, FileMode.Open, FileAccess.Read);
            return stream;
        }
    }
}
