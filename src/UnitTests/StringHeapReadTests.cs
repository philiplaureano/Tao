using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tao.Core;

namespace Tao.UnitTests
{
    [TestFixture]
    public class StringHeapReadTests : BaseHeaderReadTest
    {
        [Test]
        public void ShouldReadFirstNullString()
        {
            AssertEquals<StringHeap, string>(string.Empty, h => h.Strings[0]);
        }

        [Test]
        public void ShouldReadRemainingStrings()
        {
            AssertEquals<StringHeap, string>("<Module>", h => h.Strings[1]);
            AssertEquals<StringHeap, string>("skeleton.exe", h => h.Strings[2]);
            AssertEquals<StringHeap, string>("main", h => h.Strings[3]);
            AssertEquals<StringHeap, string>("donothing", h => h.Strings[4]);
        }

        [Test]
        public void ShouldReadStringUsingOffset()
        {
            AssertEquals<StringHeap, string>("skeleton.exe", h => h.GetStringFromOffset(0xA));
        }
        protected override void SetStreamPosition(Stream stream)
        {
            stream.Seek(0x334, SeekOrigin.Begin);
        }
    }
}
