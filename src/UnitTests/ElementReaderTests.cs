using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tao.Containers;
using Tao.Interfaces;

namespace Tao.UnitTests
{
    [TestFixture]
    public class ElementReaderTests : BaseStreamTests
    {
        [Test]
        public void ShouldBeAbleToReadBooleanElement()
        {
            var className = "ReadBoolean";
            bool expectedValue = true;

            var bytes = BitConverter.GetBytes(expectedValue);
            TestElementRead(className, expectedValue, bytes);
        }
        [Test]
        public void ShouldBeAbleToReadCharElement()
        {
            var className = "ReadChar";
            char expectedValue = 'x';

            var bytes = BitConverter.GetBytes(expectedValue);
            TestElementRead(className, expectedValue, bytes);
        }

        [Test]
        public void ShouldBeAbleToReadSingleElement()
        {
            var className = "ReadSingle";
            const float expectedValue = (Single)3.14;

            var bytes = BitConverter.GetBytes(expectedValue);
            TestElementRead(className, expectedValue, bytes);
        }

        [Test]
        public void ShouldBeAbleToReadDoubleElement()
        {
            var className = "ReadDouble";
            double expectedValue = double.MaxValue;

            var bytes = BitConverter.GetBytes(expectedValue);
            TestElementRead(className, expectedValue, bytes);
        }

        [Test]
        public void ShouldBeAbleToReadByteElement()
        {
            var className = "ReadByte";
            byte expectedValue = 65;
            
            var bytes = BitConverter.GetBytes(expectedValue);
            TestElementRead(className, expectedValue, bytes);
        }

        [Test]
        public void ShouldBeAbleToReadUInt16Element()
        {
            var className = "ReadUInt16";
            UInt16 expectedValue = UInt16.MaxValue;

            var bytes = BitConverter.GetBytes(expectedValue);
            TestElementRead(className, expectedValue, bytes);
        }

        [Test]
        public void ShouldBeAbleToReadInt16Element()
        {
            var className = "ReadInt16";
            short expectedValue = short.MaxValue;

            var bytes = BitConverter.GetBytes(expectedValue);
            TestElementRead(className, expectedValue, bytes);
        }

        [Test]
        public void ShouldBeAbleToReadUInt64Element()
        {
            var className = "ReadUInt64";
            UInt64 expectedValue = UInt64.MaxValue;

            var bytes = BitConverter.GetBytes(expectedValue);
            TestElementRead(className, expectedValue, bytes);
        }

        [Test]
        public void ShouldBeAbleToReadInt64Element()
        {
            var className = "ReadInt64";
            long expectedValue = long.MaxValue;

            var bytes = BitConverter.GetBytes(expectedValue);
            TestElementRead(className, expectedValue, bytes);
        }

        [Test]
        public void ShouldBeAbleToReadI4Element()
        {
            var className = "ReadInt32";            
            int expectedValue = -42;
            var bytes = BitConverter.GetBytes(expectedValue);
            TestElementRead(className, expectedValue, bytes);
        }

        [Test]
        public void ShouldBeAbleToReadUI4Element()
        {
            var className = "ReadUInt32";
            uint expectedValue = 42;
            var bytes = BitConverter.GetBytes(expectedValue);
            TestElementRead(className, expectedValue, bytes);
        }
       
        [Test]
        [Ignore("TODO: Implement this")]
        public void ShouldMapElementToCorrespondingReaderType()
        {
            // TODO: Write a test that maps each element type to an IFunction<Stream, TElement> instance
            throw new NotImplementedException();
        }

        private void TestElementRead<TElement>(string className, TElement expectedValue, byte[] bytes)
           where TElement : struct
        {
            var inputStream = new MemoryStream(bytes);            
            var reader = container.GetInstance<IFunction<Stream, TElement>>(className);
            Assert.IsNotNull(reader, string.Format("{0} reader not found", className));

            var result = reader.Execute(inputStream);
            Assert.AreEqual(expectedValue, result);
        }
    }
}
