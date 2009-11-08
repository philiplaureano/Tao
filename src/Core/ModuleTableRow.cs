using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Core
{
    /// <summary>
    /// Represents a single row in the Module table.
    /// </summary>
    public class ModuleTableRow : IModuleTableRow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleTableRow"/> class.
        /// </summary>
        /// <param name="metadataStream">The metadata stream that contains information about the module table row.</param>
        /// <param name="stringHeap">The string heap.</param>
        public ModuleTableRow(IMetadataStream metadataStream, IStringHeap stringHeap)
        {
            if (metadataStream == null)
                throw new ArgumentNullException("metadataStream");

            NameIndex = new StringHeapIndex(metadataStream);
            Mvid = new GuidHeapIndex(metadataStream);
            EncId = new GuidHeapIndex(metadataStream);
            EncBaseId = new GuidHeapIndex(metadataStream);
        }

        /// <summary>
        /// Gets the value indicating the generation of the module.
        /// </summary>
        /// <value>The generation of the target module.</value>
        public ushort? Generation
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the name of the target module.
        /// </summary>
        /// <value>The name of the target module.</value>
        public IStringHeapIndex NameIndex
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the Mvid of the target module.
        /// </summary>
        /// <value>The module version ID.</value>
        public IHeapIndex Mvid
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the EncId of the target module.
        /// </summary>
        /// <value>The EncId of the target module.</value>
        public IHeapIndex EncId
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value indicating the EncBaseId of the target module.
        /// </summary>
        /// <value>The EncBaseId of the target module.</value>
        public IHeapIndex EncBaseId
        {
            get;
            private set;
        }

        /// <summary>
        /// Reads the module row from the binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public void ReadFrom(IBinaryReader reader)
        {
            Generation = reader.ReadUInt16();

            NameIndex.ReadFrom(reader);
            Mvid.ReadFrom(reader);
            EncId.ReadFrom(reader);
            EncBaseId.ReadFrom(reader);
        }
    }
}
