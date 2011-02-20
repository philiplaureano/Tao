using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tao.Interfaces;
using Tao.Containers;

namespace Tao.UnitTests
{
    [TestFixture]
    public class CustomAttributeTests : BaseStreamTests
    {
        [Test]
        [Ignore("TODO: Implement this")]
        public void ShouldBeAbleToReadCustomAttributeElement()
        {
            throw new NotImplementedException();
        }

        [Test]
        [Ignore("TODO: Implement this")]
        public void ShouldBeAbleToExtractAttributeSignatureFromMethodSignature()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void ShouldBeAbleToReadSerString()
        {
            var text = "Named1";
            byte length = (byte)text.Length;
            var textBytes = Encoding.UTF8.GetBytes(text);

            var bytes = new List<byte>();
            bytes.Add(length);
            bytes.AddRange(textBytes);            

            var reader = container.GetInstance<IFunction<Stream, string>>("ReadSerString");
            Assert.IsNotNull(reader, "ReadSerString reader not found");

            var stream = new MemoryStream(bytes.ToArray());
            var result = reader.Execute(stream);
            Assert.AreEqual(text, result);
        }
    }
}
