using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using Tao.Containers;
using Tao.Interfaces;

namespace Tao.UnitTests
{
    [TestFixture]
    public class CustomAttributeElementReaderTests : BaseStreamTests
    {
        [Test]
        public void ShouldReadBoolElementType()
        {
            var elementType = ElementType.Boolean;
            object expectedValue = true;

            var bytes = new[] { Convert.ToByte(expectedValue) };

            TestElementRead(expectedValue, bytes, elementType);
        }

        [Test]
        public void ShouldReadCharElementType()
        {
            var elementType = ElementType.Char;
            object expectedValue = 'z';

            var bytes = new[] { Convert.ToByte(expectedValue) };
            TestElementRead(expectedValue, bytes, elementType);
        }

        [Test]
        public void ShouldReadSingleElementType()
        {
            var elementType = ElementType.R4;
            object expectedValue = Single.MaxValue;

            var stream = new MemoryStream();
            var writer = new BinaryWriter(stream);
            writer.Write(Single.MaxValue);

            TestElementRead(expectedValue, stream.ToArray(), elementType);
        }

        [Test]
        public void ShouldReadDoubleElementType()
        {
            var elementType = ElementType.R8;
            object expectedValue = Double.MaxValue;

            var stream = new MemoryStream();
            var writer = new BinaryWriter(stream);
            writer.Write(Double.MaxValue);

            TestElementRead(expectedValue, stream.ToArray(), elementType);
        }

        [Test]
        public void ShouldReadByteElementType()
        {
            var elementType = ElementType.U1;
            object expectedValue = Byte.MaxValue;

            var bytes = new[] { (byte)expectedValue };
            TestElementRead(expectedValue, bytes, elementType);
        }

        [Test]
        public void ShouldReadSignedByteElementType()
        {
            var elementType = ElementType.I1;
            object expectedValue = SByte.MaxValue;

            var stream = new MemoryStream();
            var writer = new BinaryWriter(stream);
            writer.Write(SByte.MaxValue);

            TestElementRead(expectedValue, stream.ToArray(), elementType);
        }

        [Test]
        public void ShouldReadInt16ElementType()
        {
            var elementType = ElementType.I2;
            var expectedValue = Int16.MaxValue;
            var stream = new MemoryStream();
            var writer = new BinaryWriter(stream);
            writer.Write(Int16.MaxValue);

            TestElementRead(expectedValue, stream.ToArray(), elementType);
        }

        [Test]
        public void ShouldReadInt32ElementType()
        {            
            var elementType = ElementType.I4;
            object expectedValue = Int32.MaxValue;

            var stream = new MemoryStream();
            var writer = new BinaryWriter(stream);
            writer.Write(Int32.MaxValue);

            TestElementRead(expectedValue, stream.ToArray(), elementType);            
        }

        [Test]
        public void ShouldReadInt64ElementType()
        {
            var elementType = ElementType.I8;
            object expectedValue = Int64.MaxValue;

            var stream = new MemoryStream();
            var writer = new BinaryWriter(stream);
            writer.Write(Int64.MaxValue);

            TestElementRead(expectedValue, stream.ToArray(), elementType);
        }

        [Test]
        public void ShouldReadUInt16ElementType()
        {            
            var elementType = ElementType.U2;
            object expectedValue = UInt16.MaxValue;

            var stream = new MemoryStream();
            var writer = new BinaryWriter(stream);
            writer.Write(UInt16.MaxValue);

            TestElementRead(expectedValue, stream.ToArray(), elementType);
        }

        [Test]
        public void ShouldReadUInt32ElementType()
        {
            var elementType = ElementType.U4;
            object expectedValue = UInt32.MaxValue;

            var stream = new MemoryStream();
            var writer = new BinaryWriter(stream);
            writer.Write(UInt32.MaxValue);

            TestElementRead(expectedValue, stream.ToArray(), elementType);
        }

        [Test]
        public void ShouldReadUInt64ElementType()
        {
            var elementType = ElementType.U8;
            object expectedValue = UInt64.MaxValue;

            var stream = new MemoryStream();
            var writer = new BinaryWriter(stream);
            writer.Write(UInt64.MaxValue);

            TestElementRead(expectedValue, stream.ToArray(), elementType);
        }

        [Test]
        public void ShouldReadStringElementType()
        {
            var elementType = ElementType.String;
            var text = "abcd";
            object expectedValue = text;

            var textBytes = Encoding.UTF8.GetBytes(text);
            
            var bytes = new List<byte>();
            bytes.Add((byte)text.Length);
            bytes.AddRange(textBytes);
            
            TestElementRead(expectedValue, bytes.ToArray(), elementType);
        }

        private void TestElementRead(object expectedValue, byte[] bytes, ElementType elementType)
        {
            var stream = new MemoryStream(bytes);
            var reader = container.GetInstance<IFunction<ITuple<ElementType, Stream>, object>>("ReadCustomAttributeElement");
            Assert.IsNotNull(reader);

            var result = reader.Execute(elementType, stream);
            Assert.AreEqual(expectedValue, result);
        }
    }
}
