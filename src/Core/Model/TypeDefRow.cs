using Tao.Interfaces;

namespace Tao.Model
{
    /// <summary>
    /// Represents a type definition.
    /// </summary>
    public class TypeDefRow
    {
        /// <summary>
        /// Gets a value indicating the string index that points to the name of the current type.
        /// </summary>
        /// <value>The Name of the current type.</value>
        public uint Name { get; set; }

        /// <summary>
        /// Gets a value indicating the string index that points to the namespace of the current type.
        /// </summary>
        /// <value>The Name of the current type.</value>
        public uint Namespace { get; set; }

        /// <summary>
        /// Gets a value indicating the Flags of the current type.
        /// </summary>
        /// <value>The Flags of the current type.</value>
        public TypeAttributes? Flags { get; set; }

        /// <summary>
        /// Gets or sets the value indicating the <see cref="TableId"/> and the row of the parent type.
        /// </summary>
        /// <value>The value indicating the <see cref="TableId"/> and the row of the parent type.</value>
        public ITuple<TableId, int> Extends { get; set; }

        /// <summary>
        /// Gets or sets the value indicating an index into the FieldDef table.
        /// </summary>
        /// <value>THe index into the FieldDef table.</value>
        public uint FieldList { get; set; }

        /// <summary>
        /// Gets or sets the value indicating an index into the MethodDef table.
        /// </summary>
        /// <value>THe index into the MethodDef table.</value>
        public uint MethodList { get; set; }
    }
}
