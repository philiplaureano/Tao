using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Hiro.Containers;
using Tao.Interfaces;
using Tao.Schemas;

namespace Tao
{
    /// <summary>
    /// Represents a class that returns the column width schema for a given metadata table.
    /// </summary>
    public class GetMetadataTableColumnSizeCounts : IFunction<ITuple<TableId, Stream>, ITuple<int, int, int, int, int, int>>
    {
        private readonly IFunction<ITuple<TableId, ITuple<int, int, int, int, int, int, IEnumerable<ITuple<IEnumerable<TableId>, int>>>, Stream>, ITuple<int, int, int, int, int, int>>
            _getAdjustedSchemaCounts;

        private static readonly
            Dictionary<TableId, ITuple<int, int, int, int, int, int, IEnumerable<ITuple<IEnumerable<TableId>, int>>>>
            _schemas = new Dictionary<TableId, ITuple<int, int, int, int, int, int, IEnumerable<ITuple<IEnumerable<TableId>, int>>>>();
        static GetMetadataTableColumnSizeCounts()
        {
            _schemas[TableId.Assembly] = new AssemblyRowSchema();
            _schemas[TableId.AssemblyRef] = new AssemblyRefRowSchema();
            _schemas[TableId.ClassLayout] = new ClassLayoutRowSchema();
            _schemas[TableId.Constant] = new ConstantRowSchema();
            _schemas[TableId.CustomAttribute] = new CustomAttributeRowSchema();
            _schemas[TableId.DeclSecurity] = new DeclSecurityRowSchema();
            _schemas[TableId.Event] = new EventRowSchema();
            _schemas[TableId.EventMap] = new EventMapRowSchema();
            _schemas[TableId.FieldLayout] = new FieldLayoutRowSchema();
            _schemas[TableId.FieldMarshal] = new FieldMarshalRowSchema();
            _schemas[TableId.Field] = new FieldRowSchema();
            _schemas[TableId.FieldRVA] = new FieldRVARowSchema();
            _schemas[TableId.File] = new FileRowSchema();
            _schemas[TableId.GenericParamConstraint] = new GenericParamConstraintRowSchema();
            _schemas[TableId.GenericParam] = new GenericParamRowSchema();
            _schemas[TableId.ImplMap] = new ImplMapRowSchema();
            _schemas[TableId.InterfaceImpl] = new InterfaceImplRowSchema();
            _schemas[TableId.ManifestResource] = new ManifestResourceRowSchema();
            _schemas[TableId.MemberRef] = new MemberRefRowSchema();
            _schemas[TableId.MethodDef] = new MethodDefRowSchema();
            _schemas[TableId.MethodImpl] = new MethodImplRowSchema();
            _schemas[TableId.MethodSemantics] = new MethodSemanticsRowSchema();
            _schemas[TableId.MethodSpec] = new MethodSpecRowSchema();
            _schemas[TableId.ModuleRef] = new ModuleRefRowSchema();
            _schemas[TableId.Module] = new ModuleRowSchema();
            _schemas[TableId.NestedClass] = new NestedClassRowSchema();
            _schemas[TableId.Param] = new ParamRowSchema();
            _schemas[TableId.PropertyMap] = new PropertyMapRowSchema();
            _schemas[TableId.Property] = new PropertyRowSchema();
            _schemas[TableId.StandAloneSig] = new StandAloneSigRowSchema();
            _schemas[TableId.TypeDef] = new TypeDefRowSchema();
            _schemas[TableId.TypeRef] = new TypeRefRowSchema();
            _schemas[TableId.TypeSpec] = new TypeSpecRowSchema();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetMetadataTableColumnSizeCounts"/> class.
        /// </summary>
        public GetMetadataTableColumnSizeCounts(IFunction<ITuple<ITuple<int, int, int, int, int, int, IEnumerable<ITuple<IEnumerable<TableId>, int>>>, Stream>, ITuple<int, int>> getAdditionalColumnCounts, IFunction<ITuple<TableId, ITuple<int, int, int, int, int, int, IEnumerable<ITuple<IEnumerable<TableId>, int>>>, Stream>, ITuple<int, int, int, int, int, int>> getAdjustedSchemaCounts)
        {
            _getAdjustedSchemaCounts = getAdjustedSchemaCounts;
        }

        /// <summary>
        /// Returns the column width schema for a given metadata table.
        /// </summary>
        /// <param name="input">The taret table identifier.</param>
        /// <returns>The tuple that describes the table's schema.</returns>
        public ITuple<int, int, int, int, int, int> Execute(ITuple<TableId, Stream> input)
        {
            var tableId = input.Item1;
            var stream = input.Item2;

            // Obtain the raw schema counts
            var schema = _schemas[tableId];
            var result = _getAdjustedSchemaCounts.Execute(tableId, schema, stream);

            return result;
        }
    }
}
