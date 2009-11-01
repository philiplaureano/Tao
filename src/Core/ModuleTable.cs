using System;
using System.Collections.Generic;
using System.Text;

namespace Tao.Core
{
    /// <summary>
    /// Represents the Module table in a .NET assembly.
    /// </summary>
    public class ModuleTable : IReader
    {
        private readonly IMetadataStream _metadataStream;
        private readonly List<IModuleTableRow> _rows = new List<IModuleTableRow>();
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleTable"/> class.
        /// </summary>
        public ModuleTable() : this(new MetadataStream())
        {            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleTable"/> class.
        /// </summary>
        /// <param name="metadataStream">The metadata stream that contains the information that describes the module table.</param>
        public ModuleTable(IMetadataStream metadataStream)
        {
            if (metadataStream == null)
                throw new ArgumentNullException("metadataStream");

            _metadataStream = metadataStream;           
        }

        /// <summary>
        /// Gets the value indicating the list of rows for the current table.
        /// </summary>
        /// <value>The list of rows for the current table.</value>
        public IList<IModuleTableRow> Rows
        {
            get
            {
                return _rows;
            }
        }
        
        /// <summary>
        /// Reads the table rows from the given binary reader.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        public void ReadFrom(IBinaryReader reader)
        {
            _metadataStream.ReadFrom(reader);

            var rowCount = _metadataStream.GetRowCount(TableId.Module);
            for(var i = 0;  i < rowCount; i++)
            {
                var row = new ModuleTableRow(_metadataStream);
                row.ReadFrom(reader);

                Rows.Add(row);
            }
        }
    }
}
