using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tao.Interfaces;
using Tao.Containers;

namespace Tao.UnitTests
{
    [TestFixture]
    public class CodedTokenTests : BaseStreamTests
    {
        [Test]
        public void ShouldBeAbleToDecodeDwordTokenIntoTagAndRow()
        {
            int token = 0x321;
            var expectedRow = 0xC8;
            var expectedToken = 1;

            var decodeToken = container.GetInstance<IFunction<ITuple<CodedTokenType, int>, ITuple<byte, int>>>("DecodeToken");
            Assert.IsNotNull(decodeToken);

            var result = decodeToken.Execute(CodedTokenType.HasConstant, token);
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedToken, result.Item1);
            Assert.AreEqual(expectedRow, result.Item2);
        }

        [Test]
        public void ShouldBeAbleToDecodeWordTokenIntoTagAndRow()
        {
            short token = 0x321;
            var expectedRow = 0xC8;
            var expectedToken = 1;

            var decodeToken = container.GetInstance<IFunction<ITuple<CodedTokenType, short>, ITuple<byte, int>>>("DecodeToken");
            Assert.IsNotNull(decodeToken);

            var result = decodeToken.Execute(CodedTokenType.HasConstant, token);
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedToken, result.Item1);
            Assert.AreEqual(expectedRow, result.Item2);
        }

        [Test]
        public void ShouldBeAbleToReportTheCorrectNumberOfBitsToEncodeCustomAttributeType()
        {
            var expectedBitCount = 3;
            var codedTokenType = CodedTokenType.CustomAttributeType;

            Test(codedTokenType, expectedBitCount);
        }

        [Test]
        public void ShouldBeAbleToReportTheCorrectNumberOfBitsToEncodeHasConstant()
        {
            Test(CodedTokenType.HasConstant, 2);
        }

        [Test]
        public void ShouldBeAbleToReportTheCorrectNumberOfBitsToEncodeTypeDefOrTypeRef()
        {
            Test(CodedTokenType.TypeDefOrTypeRef, 2);
        }

        [Test]
        public void ShouldBeAbleToReportTheCorrectNumberOfBitsToEncodeHasCustomAttribute()
        {
            Test(CodedTokenType.HasCustomAttribute, 5);
        }

        [Test]
        public void ShouldBeAbleToReportTheCorrectNumberOfBitsToEncodeHasDeclSecurity()
        {
            Test(CodedTokenType.HasDeclSecurity, 2);
        }

        [Test]
        public void ShouldBeAbleToReportTheCorrectNumberOfBitsToEncodeMemberRefParent()
        {
            Test(CodedTokenType.MemberRefParent, 3);
        }

        [Test]
        public void ShouldBeAbleToReportTheCorrectNumberOfBitsToEncodeHasSemantics()
        {
            Test(CodedTokenType.HasSemantics, 1);
        }

        [Test]
        public void ShouldBeAbleToReportTheCorrectNumberOfBitsToEncodeMethodDefOrRef()
        {
            Test(CodedTokenType.MethodDefOrRef, 1);
        }

        [Test]
        public void ShouldBeAbleToReportTheCorrectNumberOfBitsToEncodeMemberForwarded()
        {
            Test(CodedTokenType.MemberForwarded, 1);
        }

        [Test]
        public void ShouldBeAbleToReportTheCorrectNumberOfBitsToEncodeImplementation()
        {
            Test(CodedTokenType.Implementation, 2);
        }

        [Test]
        public void ShouldBeAbleToReportTheCorrectNumberOfBitsToEncodeResolutionScope()
        {
            Test(CodedTokenType.ResolutionScope, 2);
        }

        private void Test(CodedTokenType codedTokenType, int expectedBitCount)
        {
            var getBitCount = container.GetInstance<IFunction<CodedTokenType, uint>>("GetNumberOfBitsToEncodeTag");
            Assert.IsNotNull(getBitCount);
            var bitCount = getBitCount.Execute(codedTokenType);
            Assert.AreEqual(expectedBitCount, bitCount, "Wrong bit count!");
        }
    }
}
