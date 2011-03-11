using NUnit.Framework;
using Tao.Containers;
using Tao.Interfaces;

namespace Tao.UnitTests
{
    [TestFixture]
    public class CodedTokenTests : BaseStreamTests
    {
        #region Decoded Token Tests
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
        #endregion

        #region Token Tag Conversion Tests
        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_TypeDefOrTypeRef_And_RowAndTagFrom_TypeDef()
        {
            Test(CodedTokenType.TypeDefOrTypeRef, 0, TableId.TypeDef);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_TypeDefOrTypeRef_And_RowAndTagFrom_TypeRef()
        {
            Test(CodedTokenType.TypeDefOrTypeRef, 1, TableId.TypeRef);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_TypeDefOrTypeRef_And_RowAndTagFrom_TypeSpec()
        {
            Test(CodedTokenType.TypeDefOrTypeRef, 2, TableId.TypeSpec);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_HasConstant_And_RowAndTagFrom_Field()
        {
            Test(CodedTokenType.HasConstant, 0, TableId.Field);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_HasConstant_And_RowAndTagFrom_Param()
        {
            Test(CodedTokenType.HasConstant, 1, TableId.Param);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_HasConstant_And_RowAndTagFrom_Property()
        {
            Test(CodedTokenType.HasConstant, 2, TableId.Property);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_HasCustomAttribute_And_RowAndTagFrom_MethodDef()
        {
            Test(CodedTokenType.HasCustomAttribute, 0, TableId.MethodDef);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_HasCustomAttribute_And_RowAndTagFrom_Field()
        {
            Test(CodedTokenType.HasCustomAttribute, 1, TableId.Field);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_HasCustomAttribute_And_RowAndTagFrom_TypeRef()
        {
            Test(CodedTokenType.HasCustomAttribute, 2, TableId.TypeRef);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_HasCustomAttribute_And_RowAndTagFrom_TypeDef()
        {
            Test(CodedTokenType.HasCustomAttribute, 3, TableId.TypeDef);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_HasCustomAttribute_And_RowAndTagFrom_Param()
        {
            Test(CodedTokenType.HasCustomAttribute, 4, TableId.Param);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_HasCustomAttribute_And_RowAndTagFrom_InterfaceImpl()
        {
            Test(CodedTokenType.HasCustomAttribute, 5, TableId.InterfaceImpl);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_HasCustomAttribute_And_RowAndTagFrom_MemberRef()
        {
            Test(CodedTokenType.HasCustomAttribute, 6, TableId.MemberRef);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_HasCustomAttribute_And_RowAndTagFrom_Module()
        {
            Test(CodedTokenType.HasCustomAttribute, 7, TableId.Module);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_HasCustomAttribute_And_RowAndTagFrom_DeclSecurity()
        {
            Test(CodedTokenType.HasCustomAttribute, 8, TableId.DeclSecurity);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_HasCustomAttribute_And_RowAndTagFrom_Property()
        {
            Test(CodedTokenType.HasCustomAttribute, 9, TableId.Property);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_HasCustomAttribute_And_RowAndTagFrom_Event()
        {
            Test(CodedTokenType.HasCustomAttribute, 10, TableId.Event);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_HasCustomAttribute_And_RowAndTagFrom_StandAloneSig()
        {
            Test(CodedTokenType.HasCustomAttribute, 11, TableId.StandAloneSig);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_HasCustomAttribute_And_RowAndTagFrom_ModuleRef()
        {
            Test(CodedTokenType.HasCustomAttribute, 12, TableId.ModuleRef);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_HasCustomAttribute_And_RowAndTagFrom_TypeSpec()
        {
            Test(CodedTokenType.HasCustomAttribute, 13, TableId.TypeSpec);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_HasCustomAttribute_And_RowAndTagFrom_Assembly()
        {
            Test(CodedTokenType.HasCustomAttribute, 14, TableId.Assembly);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_HasCustomAttribute_And_RowAndTagFrom_AssemblyRef()
        {
            Test(CodedTokenType.HasCustomAttribute, 15, TableId.AssemblyRef);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_HasCustomAttribute_And_RowAndTagFrom_File()
        {
            Test(CodedTokenType.HasCustomAttribute, 16, TableId.File);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_HasCustomAttribute_And_RowAndTagFrom_ExportedType()
        {
            Test(CodedTokenType.HasCustomAttribute, 17, TableId.ExportedType);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_HasCustomAttribute_And_RowAndTagFrom_ManifestResource()
        {
            Test(CodedTokenType.HasCustomAttribute, 18, TableId.ManifestResource);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_HasFieldMarshall_And_RowAndTagFrom_Field()
        {
            Test(CodedTokenType.HasFieldMarshall, 0, TableId.Field);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_HasFieldMarshall_And_RowAndTagFrom_Param()
        {
            Test(CodedTokenType.HasFieldMarshall, 1, TableId.Param);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_HasDeclSecurity_And_RowAndTagFrom_TypeDef()
        {
            Test(CodedTokenType.HasDeclSecurity, 0, TableId.TypeDef);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_HasDeclSecurity_And_RowAndTagFrom_MethodDef()
        {
            Test(CodedTokenType.HasDeclSecurity, 1, TableId.MethodDef);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_HasDeclSecurity_And_RowAndTagFrom_Assembly()
        {
            Test(CodedTokenType.HasDeclSecurity, 2, TableId.Assembly);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_MemberRefParent_And_RowAndTagFrom_TypeRef()
        {
            Test(CodedTokenType.MemberRefParent, 1, TableId.TypeRef);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_MemberRefParent_And_RowAndTagFrom_ModuleRef()
        {
            Test(CodedTokenType.MemberRefParent, 2, TableId.ModuleRef);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_MemberRefParent_And_RowAndTagFrom_MethodDef()
        {
            Test(CodedTokenType.MemberRefParent, 3, TableId.MethodDef);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_MemberRefParent_And_RowAndTagFrom_TypeSpec()
        {
            Test(CodedTokenType.MemberRefParent, 4, TableId.TypeSpec);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_HasSemantics_And_RowAndTagFrom_Event()
        {
            Test(CodedTokenType.HasSemantics, 0, TableId.Event);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_HasSemantics_And_RowAndTagFrom_Property()
        {
            Test(CodedTokenType.HasSemantics, 1, TableId.Property);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_MethodDefOrRef_And_RowAndTagFrom_MethodDef()
        {
            Test(CodedTokenType.MethodDefOrRef, 0, TableId.MethodDef);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_MethodDefOrRef_And_RowAndTagFrom_MemberRef()
        {
            Test(CodedTokenType.MethodDefOrRef, 1, TableId.MemberRef);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_MemberForwarded_And_RowAndTagFrom_Field()
        {
            Test(CodedTokenType.MemberForwarded, 0, TableId.Field);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_MemberForwarded_And_RowAndTagFrom_MethodDef()
        {
            Test(CodedTokenType.MemberForwarded, 1, TableId.MethodDef);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_Implementation_And_RowAndTagFrom_File()
        {
            Test(CodedTokenType.Implementation, 0, TableId.File);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_Implementation_And_RowAndTagFrom_AssemblyRef()
        {
            Test(CodedTokenType.Implementation, 2, TableId.AssemblyRef);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_Implementation_And_RowAndTagFrom_ExportedType()
        {
            Test(CodedTokenType.Implementation, 3, TableId.ExportedType);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_CustomAttributeType_And_RowAndTagFrom_MethodDef()
        {
            Test(CodedTokenType.CustomAttributeType, 2, TableId.MethodDef);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_CustomAttributeType_And_RowAndTagFrom_MemberRef()
        {
            Test(CodedTokenType.CustomAttributeType, 3, TableId.MemberRef);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_ResolutionScope_And_RowAndTagFrom_Module()
        {
            Test(CodedTokenType.ResolutionScope, 0, TableId.Module);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_ResolutionScope_And_RowAndTagFrom_ModuleRef()
        {
            Test(CodedTokenType.ResolutionScope, 1, TableId.ModuleRef);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_ResolutionScope_And_RowAndTagFrom_AssemblyRef()
        {
            Test(CodedTokenType.ResolutionScope, 2, TableId.AssemblyRef);
        }

        [Test]
        public void ShouldBeAbleToGiveCorrectTableIdFromCodedTokenId_ResolutionScope_And_RowAndTagFrom_TypeRef()
        {
            Test(CodedTokenType.ResolutionScope, 3, TableId.TypeRef);
        }
        #endregion

        [Test]
        public void ShouldBeAbleToConvertCodedTokenIntoTableIdAndRow()
        {
            var getTableReference =
                container.GetInstance<IFunction<ITuple<CodedTokenType, int>, ITuple<TableId, int>>>(
                    "GetTableReferenceFromCodedToken");
            Assert.IsNotNull(getTableReference);

            var codedToken = 0x321;
            var expectedRow = 0xC8;
            var tableId = TableId.Param;

            var result = getTableReference.Execute(CodedTokenType.HasConstant, codedToken);
            Assert.IsNotNull(result);

            Assert.AreEqual(tableId, result.Item1);
            Assert.AreEqual(expectedRow, result.Item2);
        }
        private void Test(CodedTokenType tokenType, byte tag, TableId expectedTableId)
        {
            var getTableId = container.GetInstance<IFunction<ITuple<CodedTokenType, byte>, TableId?>>("GetTableId");
            Assert.IsNotNull(getTableId);

            var tableId = getTableId.Execute(tokenType, tag);
            Assert.IsNotNull(tableId);
            Assert.AreEqual(tableId, expectedTableId);
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
