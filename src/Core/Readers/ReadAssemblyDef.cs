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
        private readonly IFunction<Stream, string> _readNullTerminatedString;
        private readonly IFunction<Stream, ITuple<int, int, int>> _readMetadataHeapIndexSizes;
        private readonly IFunction<ITuple<string, Stream>, Stream> _readMetadataStreamByName;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public ReadAssemblyDef(IFunction<Stream, IDictionary<TableId, ITuple<int, Stream>>> readAllMetadataTables, IFunction<Stream, string> readNullTerminatedString, IFunction<Stream, ITuple<int, int, int>> readMetadataHeapIndexSizes, IFunction<ITuple<string, Stream>, Stream> readMetadataStreamByName)
        {
            _readAllMetadataTables = readAllMetadataTables;
            _readNullTerminatedString = readNullTerminatedString;
            _readMetadataHeapIndexSizes = readMetadataHeapIndexSizes;
            _readMetadataStreamByName = readMetadataStreamByName;
        }

        /// <summary>
        /// Reads an <see cref="AssemblyDef"/> from a given stream.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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

            var publicKeyIndex = Read(blobIndexSize, reader);

            var nameIndex = Read(stringIndexSize, reader);
            var cultureIndex = Read(stringIndexSize, reader);

            if (nameIndex != 0)
                result.Name = ReadString(nameIndex, input); 

            if (cultureIndex != 0)
                result.Culture = ReadString(cultureIndex, input);

            // TODO: Read the blob values
            var blobHeap = _readMetadataStreamByName.Execute("#Blob", input);

            return result;
        }

        private string ReadString(uint targetOffset, Stream input)
        {
            var stringHeap = _readMetadataStreamByName.Execute("#Strings", input);
            stringHeap.Seek(targetOffset, SeekOrigin.Begin);

            return _readNullTerminatedString.Execute(stringHeap);
        }

        private uint Read(int indexSize, BinaryReader reader)
        {
            return indexSize == 2 ? reader.ReadUInt16() : reader.ReadUInt32();
        }
    }
}
