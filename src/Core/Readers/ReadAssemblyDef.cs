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
        private readonly IFunction<Stream, IDictionary<TableId, ITuple<int, Stream>>> _readAllMetadataTables;
        private readonly IFunction<Stream, ITuple<int, int, int>> _readMetadataHeapIndexSizes;
        private readonly IFunction<ITuple<uint, Stream>, string> _readStringFromStringsHeap;
        private readonly IFunction<ITuple<uint, Stream>, byte[]> _readBlob;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public ReadAssemblyDef(IFunction<Stream, IDictionary<TableId, ITuple<int, Stream>>> readAllMetadataTables,
            IFunction<Stream, ITuple<int, int, int>> readMetadataHeapIndexSizes,
            IFunction<ITuple<uint, Stream>, string> readStringFromStringsHeap,
            IFunction<ITuple<uint, Stream>, byte[]> readBlob)
        {
            _readAllMetadataTables = readAllMetadataTables;
            _readBlob = readBlob;
            _readStringFromStringsHeap = readStringFromStringsHeap;
            _readMetadataHeapIndexSizes = readMetadataHeapIndexSizes;
        }

        /// <summary>
        /// Reads an <see cref="AssemblyDef"/> from a given stream.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <returns>An <see cref="AssemblyDef"/> object.</returns>
        public AssemblyDef Execute(Stream input)
        {
            var tables = _readAllMetadataTables.Execute(input);
            var tableEntry = tables[TableId.Assembly];
            var tableStream = tableEntry.Item2;

            var reader = new BinaryReader(tableStream);
            var result = new AssemblyDef();

            result.HashAlgorithm = (AssemblyHashAlgorithm)reader.ReadUInt32();
            result.MajorVersion = reader.ReadUInt16();
            result.MinorVersion = reader.ReadUInt16();
            result.BuildNumber = reader.ReadUInt16();
            result.RevisionNumber = reader.ReadUInt16();
            result.Flags = (AssemblyFlags)reader.ReadInt32();

            var heapSizes = _readMetadataHeapIndexSizes.Execute(input);

            var stringIndexSize = heapSizes.Item1;
            var blobIndexSize = heapSizes.Item2;

            // Read the public key
            var publicKeyIndex = Read(blobIndexSize, reader);
            if (publicKeyIndex != 0)
                result.PublicKey = _readBlob.Execute(publicKeyIndex, input);

            var nameIndex = Read(stringIndexSize, reader);
            var cultureIndex = Read(stringIndexSize, reader);

            if (nameIndex != 0)
                result.Name = _readStringFromStringsHeap.Execute(nameIndex, input);

            if (cultureIndex != 0)
                result.Culture = _readStringFromStringsHeap.Execute(cultureIndex, input);

            return result;
        }

        private uint Read(int indexSize, BinaryReader reader)
        {
            return indexSize == 2 ? reader.ReadUInt16() : reader.ReadUInt32();
        }
    }
}
