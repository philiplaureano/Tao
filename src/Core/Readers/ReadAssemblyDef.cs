using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tao.Interfaces;
using Tao.Model;

namespace Tao.Readers
{
    /// <summary>
    /// Represents a class that reads an <see cref="AssemblyDef"/> from a given stream.
    /// </summary>
    public class ReadAssemblyDef : IFunction<Stream, AssemblyDef>
    {
        private readonly IFunction<Stream, ITuple<int, int, int>> _readMetadataHeapIndexSizes;
        private readonly IFunction<ITuple<uint, Stream>, string> _readStringFromStringsHeap;
        private readonly IFunction<ITuple<uint, Stream>, byte[]> _readBlob;
        private readonly IFunction<ITuple<int, BinaryReader>, uint> _readHeapIndexValue;
        private readonly IFunction<ITuple<TableId, Stream>, ITuple<int, Stream>> _readMetadataTable;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadAssemblyDef"/> class.
        /// </summary>
        /// <param name="readBlob">The blob reader.</param>
        /// <param name="readHeapIndexValue">The heap index value reader.</param>
        /// <param name="readMetadataHeapIndexSizes">The reader that will determine the heap index sizes from the input stream.</param>
        /// <param name="readMetadataTable">The metadata table reader.</param>
        /// <param name="readStringFromStringsHeap">The string reader.</param>
        public ReadAssemblyDef(IFunction<Stream, ITuple<int, int, int>> readMetadataHeapIndexSizes,
            IFunction<ITuple<uint, Stream>, string> readStringFromStringsHeap,
            IFunction<ITuple<uint, Stream>, byte[]> readBlob,
            IFunction<ITuple<int, BinaryReader>, uint> readHeapIndexValue, 
            IFunction<ITuple<TableId, Stream>, ITuple<int, Stream>> readMetadataTable)
        {
            _readBlob = readBlob;
            _readMetadataTable = readMetadataTable;
            _readStringFromStringsHeap = readStringFromStringsHeap;
            _readMetadataHeapIndexSizes = readMetadataHeapIndexSizes;
            _readHeapIndexValue = readHeapIndexValue;
        }

        /// <summary>
        /// Reads an <see cref="AssemblyDef"/> from a given stream.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <returns>An <see cref="AssemblyDef"/> object.</returns>
        public AssemblyDef Execute(Stream input)
        {
            var tableEntry = _readMetadataTable.Execute(TableId.Assembly, input);
            var tableStream = tableEntry.Item2;

            var reader = new BinaryReader(tableStream);
            var result = new AssemblyDef
                             {
                                 HashAlgorithm = (AssemblyHashAlgorithm) reader.ReadUInt32(),
                                 MajorVersion = reader.ReadUInt16(),
                                 MinorVersion = reader.ReadUInt16(),
                                 BuildNumber = reader.ReadUInt16(),
                                 RevisionNumber = reader.ReadUInt16(),
                                 Flags = (AssemblyFlags) reader.ReadInt32()
                             };

            var heapSizes = _readMetadataHeapIndexSizes.Execute(input);

            var stringIndexSize = heapSizes.Item1;
            var blobIndexSize = heapSizes.Item2;

            // Read the public key
            var publicKeyIndex = _readHeapIndexValue.Execute(blobIndexSize, reader);
            if (publicKeyIndex != 0)
                result.PublicKey = _readBlob.Execute(publicKeyIndex, input);

            var nameIndex = _readHeapIndexValue.Execute(stringIndexSize, reader);
            var cultureIndex = _readHeapIndexValue.Execute(stringIndexSize, reader);

            result.Name = _readStringFromStringsHeap.Execute(nameIndex, input);
            result.Culture = _readStringFromStringsHeap.Execute(cultureIndex, input);

            return result;
        }
    }
}
